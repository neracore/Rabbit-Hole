using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Shapes;
using System.Windows.Threading;
using RabbitHole.Data;
using RabbitHole.Models;
using RabbitHole.Services;

namespace RabbitHole;

public partial class MainWindow : Window
{
    private enum AppState { Start, Topic, Journey }

    private sealed class Particle
    {
        public double X;
        public double Y;
        public double Vx;
        public double Vy;
        public double BaseOpacity;
        public Ellipse Ellipse = null!;
    }

    private readonly JourneyEngine _engine;
    private readonly Random _rng = new();
    private readonly List<Particle> _particles = new();
    private readonly DispatcherTimer _particleTimer;
    private AppState _state = AppState.Start;

    private static readonly string[] EndLines =
    {
        "The tunnel narrowed. The walls whispered. This is where it ended.",
        "Every rabbit hole ends — this one ended here, in the dark, with these words glowing faintly.",
        "You surfaced, blinking, with sand in your pockets and strange stars in your eyes.",
        "The trail went cold. The hole breathed once more, and let you go.",
    };

    private static readonly Color InkColor = Color.FromRgb(0xE9, 0xED, 0xF9);
    private static readonly Color DimColor = Color.FromRgb(0x8B, 0x94, 0xAB);
    private static readonly Color FaintColor = Color.FromRgb(0x59, 0x61, 0x7A);
    private static readonly Color TealColor = Color.FromRgb(0x4F, 0xE3, 0xC1);
    private static readonly Color AccentFaintColor = Color.FromRgb(0x4A, 0x43, 0x80);

    public MainWindow()
    {
        InitializeComponent();
        _engine = new JourneyEngine(KnowledgeGraph.All);
        _particleTimer = new DispatcherTimer(DispatcherPriority.Background)
        {
            Interval = TimeSpan.FromMilliseconds(33)
        };
        _particleTimer.Tick += (_, _) => MoveParticles();
        Loaded += OnLoaded;
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        TryEnableDarkTitleBar();
        SpawnParticles();
        _particleTimer.Start();
        ShowStart();
    }

    // ============================================================
    //  State transitions
    // ============================================================

    private void ShowStart()
    {
        _state = AppState.Start;
        _engine.Reset();
        DepthText.Text = "DEPTH —";
        EndButton.IsEnabled = false;
        TrailBar.Visibility = Visibility.Collapsed;
        ShowPanel(StartPanel);
        var pulse = new DoubleAnimation(0.30, 1.0, TimeSpan.FromSeconds(1.15))
        {
            AutoReverse = true,
            RepeatBehavior = RepeatBehavior.Forever
        };
        EnterText.BeginAnimation(OpacityProperty, pulse);
    }

    private void BeginJourney()
    {
        EnterText.BeginAnimation(OpacityProperty, null);
        _engine.StartNew();
        _state = AppState.Topic;
        EndButton.IsEnabled = true;
        TrailBar.Visibility = Visibility.Visible;
        ShowPanel(TopicPanel);
        BuildTopicCard(_engine.Current!, animateIntro: true);
        UpdateTrail(animateLast: true);
    }

    private void GoDeeper()
    {
        if (_state != AppState.Topic) return;
        var next = _engine.GoDeeper();

        var fadeOut = new DoubleAnimation(1, 0, TimeSpan.FromMilliseconds(210))
        {
            EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseIn }
        };
        var slide = new DoubleAnimation(0, -26, TimeSpan.FromMilliseconds(210))
        {
            EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseIn }
        };
        fadeOut.Completed += (_, _) =>
        {
            BuildTopicCard(next, animateIntro: true);
            UpdateTrail(animateLast: true);
            BumpDepthChip();
            TopicScroll.ScrollToTop();
        };
        TopicCard.BeginAnimation(OpacityProperty, fadeOut);
        CardTranslate.BeginAnimation(TranslateTransform.YProperty, slide);
    }

    private void EndJourney()
    {
        _state = AppState.Journey;
        EndButton.IsEnabled = false;
        TrailBar.Visibility = Visibility.Collapsed;
        ShowPanel(JourneyPanel);
        BuildJourney();
    }

    private void ShowPanel(UIElement panel)
    {
        foreach (var p in new UIElement[] { StartPanel, TopicPanel, JourneyPanel })
        {
            if (!ReferenceEquals(p, panel))
            {
                p.Visibility = Visibility.Collapsed;
                p.Opacity = 0;
            }
        }
        panel.Visibility = Visibility.Visible;
        panel.Opacity = 0;
        var fadeIn = new DoubleAnimation(0, 1, TimeSpan.FromMilliseconds(380))
        {
            EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
        };
        panel.BeginAnimation(OpacityProperty, fadeIn);
    }

    // ============================================================
    //  Topic card
    // ============================================================

    private void BuildTopicCard(Topic topic, bool animateIntro)
    {
        TopicCard.BeginAnimation(OpacityProperty, null);
        TopicCard.Opacity = 1;
        CardTranslate.BeginAnimation(TranslateTransform.YProperty, null);
        CardTranslate.Y = 0;
        GoDeeperButton.BeginAnimation(OpacityProperty, null);

        CategoryChip.Content = topic.Category.ToUpperInvariant();
        TopicTitle.Text = topic.Title;
        TopicTagline.Text = topic.Tagline;
        DepthText.Text = $"DEPTH {_engine.Depth:00}";
        HintText.Text = _engine.Depth == 1
            ? "you have just fallen in  ·  ENTER or SPACE to go deeper  ·  ESC to surface"
            : "ENTER or SPACE to go deeper  ·  ESC to surface";

        FactsPanel.Children.Clear();
        foreach (var fact in topic.Facts)
            FactsPanel.Children.Add(MakeFactBorder(fact));

        if (animateIntro)
            PlayTopicIntro();
        else
            ResetCardOpacity();

        StartDiveGlow();
    }

    private void PlayTopicIntro()
    {
        double t = 0.05;
        AnimateIn(CategoryChip, t, 0.35, 14); t += 0.16;
        AnimateIn(TopicTitle, t, 0.45, 20); t += 0.20;
        AnimateIn(TopicTagline, t, 0.40, 14); t += 0.18;
        foreach (var child in FactsPanel.Children.OfType<UIElement>())
        {
            AnimateIn(child, t, 0.45, 16);
            t += 0.34;
        }
        AnimateIn(GoDeeperButton, t, 0.40, 12);
        AnimateIn(HintText, t + 0.10, 0.50, 0);
    }

    private void ResetCardOpacity()
    {
        CategoryChip.Opacity = 1;
        TopicTitle.Opacity = 1;
        TopicTagline.Opacity = 1;
        GoDeeperButton.Opacity = 1;
        HintText.Opacity = 1;
        foreach (var child in FactsPanel.Children.OfType<UIElement>())
            child.Opacity = 1;
    }

    private Border MakeFactBorder(string fact)
    {
        var dot = new Ellipse
        {
            Width = 7,
            Height = 7,
            Fill = new SolidColorBrush(TealColor),
            VerticalAlignment = VerticalAlignment.Top,
            Margin = new Thickness(0, 6, 0, 0)
        };
        var text = new TextBlock
        {
            Text = fact,
            FontSize = 14.5,
            Foreground = new SolidColorBrush(InkColor),
            TextWrapping = TextWrapping.Wrap,
            VerticalAlignment = VerticalAlignment.Center
        };
        var inner = new StackPanel { Orientation = Orientation.Horizontal };
        inner.Children.Add(dot);
        inner.Children.Add(text);

        return new Border
        {
            Background = new SolidColorBrush(Color.FromArgb(0x88, 0x12, 0x18, 0x26)),
            BorderBrush = new SolidColorBrush(Color.FromRgb(0x23, 0x2C, 0x42)),
            BorderThickness = new Thickness(1),
            CornerRadius = new CornerRadius(10),
            Padding = new Thickness(16, 12, 16, 12),
            Margin = new Thickness(0, 0, 0, 10),
            Child = inner,
            Opacity = 1
        };
    }

    private void StartDiveGlow()
    {
        var effect = new DropShadowEffect
        {
            Color = Color.FromRgb(0x8B, 0x7C, 0xFF),
            BlurRadius = 22,
            ShadowDepth = 0,
            Opacity = 0.6
        };
        GoDeeperButton.Effect = effect;
        var breathe = new DoubleAnimation(14, 36, TimeSpan.FromSeconds(1.4))
        {
            AutoReverse = true,
            RepeatBehavior = RepeatBehavior.Forever
        };
        effect.BeginAnimation(DropShadowEffect.BlurRadiusProperty, breathe);
    }

    private void BumpDepthChip()
    {
        var bump = new DoubleAnimation(1, 1.22, TimeSpan.FromMilliseconds(130))
        {
            AutoReverse = true,
            EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseOut }
        };
        DepthChipScale.BeginAnimation(ScaleTransform.ScaleXProperty, bump);
        DepthChipScale.BeginAnimation(ScaleTransform.ScaleYProperty, bump);
    }

    // ============================================================
    //  Journey view
    // ============================================================

    private void BuildJourney()
    {
        JourneyContent.Children.Clear();
        var steps = _engine.Steps;

        var header = new TextBlock
        {
            Text = "THE JOURNEY",
            FontSize = 42,
            FontWeight = FontWeights.Light,
            Foreground = new SolidColorBrush(InkColor),
            HorizontalAlignment = HorizontalAlignment.Center,
            Margin = new Thickness(0, 8, 0, 4)
        };
        var endline = new TextBlock
        {
            Text = EndLines[_rng.Next(EndLines.Length)],
            FontSize = 14,
            FontStyle = FontStyles.Italic,
            Foreground = new SolidColorBrush(DimColor),
            TextAlignment = TextAlignment.Center,
            TextWrapping = TextWrapping.Wrap,
            Margin = new Thickness(0, 0, 0, 18)
        };
        int territories = steps.Select(s => s.Topic.Category).Distinct().Count();
        var stats = new TextBlock
        {
            Text = $"{steps.Count} stops  ·  {territories} territories crossed",
            FontSize = 13,
            FontWeight = FontWeights.SemiBold,
            Foreground = new SolidColorBrush(TealColor),
            HorizontalAlignment = HorizontalAlignment.Center,
            Margin = new Thickness(0, 0, 0, 24)
        };

        JourneyContent.Children.Add(header);
        JourneyContent.Children.Add(endline);
        JourneyContent.Children.Add(stats);

        for (int i = 0; i < steps.Count; i++)
            JourneyContent.Children.Add(MakeJourneyStep(steps[i], i == steps.Count - 1));

        var buttons = new StackPanel
        {
            Orientation = Orientation.Horizontal,
            HorizontalAlignment = HorizontalAlignment.Center,
            Margin = new Thickness(0, 22, 0, 0)
        };
        var again = new Button
        {
            Content = "DIVE AGAIN  ↻",
            Style = (Style)Resources["DiveButton"],
            Margin = new Thickness(0, 0, 12, 0)
        };
        again.Click += (_, _) => BeginJourney();
        var surface = new Button
        {
            Content = "BACK TO SURFACE",
            Style = (Style)Resources["GhostButton"],
            VerticalAlignment = VerticalAlignment.Center
        };
        surface.Click += (_, _) => ShowStart();
        buttons.Children.Add(again);
        buttons.Children.Add(surface);
        JourneyContent.Children.Add(buttons);

        var hint = new TextBlock
        {
            Text = "ENTER to dive again  ·  ESC to surface",
            FontSize = 12,
            Foreground = new SolidColorBrush(FaintColor),
            HorizontalAlignment = HorizontalAlignment.Center,
            Margin = new Thickness(0, 14, 0, 6)
        };
        JourneyContent.Children.Add(hint);

        double t = 0.05;
        AnimateIn(header, t, 0.45, 18); t += 0.14;
        AnimateIn(endline, t, 0.4, 10); t += 0.12;
        AnimateIn(stats, t, 0.4, 10); t += 0.10;
        for (int i = 3; i < JourneyContent.Children.Count; i++)
        {
            var child = (UIElement)JourneyContent.Children[i];
            AnimateIn(child, t, 0.4, 14);
            t += 0.08;
        }
    }

    private Border MakeJourneyStep(JourneyStep step, bool isLast)
    {
        var grid = new Grid();
        grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(62) });
        grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

        var depth = new TextBlock
        {
            Text = step.Depth.ToString("00"),
            FontSize = 26,
            FontWeight = FontWeights.Light,
            Foreground = new SolidColorBrush(isLast ? TealColor : AccentFaintColor),
            VerticalAlignment = VerticalAlignment.Top,
            Margin = new Thickness(0, 2, 0, 0)
        };
        Grid.SetColumn(depth, 0);

        var right = new StackPanel();
        right.Children.Add(new TextBlock
        {
            Text = step.Topic.Title,
            FontSize = 18,
            FontWeight = FontWeights.SemiBold,
            Foreground = new SolidColorBrush(InkColor)
        });
        right.Children.Add(new TextBlock
        {
            Text = step.Topic.Category.ToUpperInvariant(),
            FontSize = 10.5,
            Foreground = new SolidColorBrush(FaintColor),
            Margin = new Thickness(0, 3, 0, 8)
        });
        right.Children.Add(new TextBlock
        {
            Text = step.Fact,
            FontSize = 13,
            FontStyle = FontStyles.Italic,
            Foreground = new SolidColorBrush(DimColor),
            TextWrapping = TextWrapping.Wrap
        });
        Grid.SetColumn(right, 1);
        grid.Children.Add(depth);
        grid.Children.Add(right);

        return new Border
        {
            Background = new SolidColorBrush(Color.FromRgb(0x10, 0x15, 0x1F)),
            BorderBrush = new SolidColorBrush(Color.FromRgb(0x23, 0x2C, 0x42)),
            BorderThickness = new Thickness(1),
            CornerRadius = new CornerRadius(12),
            Padding = new Thickness(18, 14, 18, 14),
            Margin = new Thickness(0, 0, 0, 10),
            Child = grid,
            Opacity = 1
        };
    }

    // ============================================================
    //  Trail bar
    // ============================================================

    private void UpdateTrail(bool animateLast)
    {
        TrailPanel.Children.Clear();
        var steps = _engine.Steps;
        for (int i = 0; i < steps.Count; i++)
        {
            var s = steps[i];
            bool last = i == steps.Count - 1;

            var tb = new TextBlock
            {
                Text = s.Topic.Title,
                FontSize = 12,
                FontWeight = last ? FontWeights.SemiBold : FontWeights.Normal,
                Foreground = new SolidColorBrush(last ? TealColor : DimColor),
                VerticalAlignment = VerticalAlignment.Center,
                Opacity = last && animateLast ? 0 : 1
            };
            TrailPanel.Children.Add(tb);

            if (!last)
            {
                TrailPanel.Children.Add(new TextBlock
                {
                    Text = "  →  ",
                    FontSize = 11,
                    Foreground = new SolidColorBrush(FaintColor),
                    VerticalAlignment = VerticalAlignment.Center
                });
            }
            else if (animateLast)
            {
                AnimateIn(tb, 0, 0.35, 8);
            }
        }
        TrailScroll.ScrollToEnd();
    }

    // ============================================================
    //  Input
    // ============================================================

    protected override void OnKeyDown(KeyEventArgs e)
    {
        base.OnKeyDown(e);
        switch (_state)
        {
            case AppState.Start:
                if (e.Key is Key.Enter or Key.Space)
                {
                    e.Handled = true;
                    BeginJourney();
                }
                break;

            case AppState.Topic:
                if (e.Key is Key.Enter or Key.Space)
                {
                    e.Handled = true;
                    GoDeeper();
                }
                else if (e.Key is Key.Escape)
                {
                    e.Handled = true;
                    EndJourney();
                }
                break;

            case AppState.Journey:
                if (e.Key is Key.Enter or Key.Space)
                {
                    e.Handled = true;
                    BeginJourney();
                }
                else if (e.Key is Key.Escape)
                {
                    e.Handled = true;
                    ShowStart();
                }
                break;
        }
    }

    private void OnGoDeeperClick(object sender, RoutedEventArgs e) => GoDeeper();
    private void OnEndClick(object sender, RoutedEventArgs e) => EndJourney();
    private void OnNewClick(object sender, RoutedEventArgs e) => BeginJourney();

    // ============================================================
    //  Animation helpers
    // ============================================================

    private static void AnimateIn(UIElement el, double beginSec, double durationSec, double fromY)
    {
        var opacity = new DoubleAnimation(0, 1, TimeSpan.FromSeconds(durationSec))
        {
            BeginTime = TimeSpan.FromSeconds(beginSec),
            EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
        };
        var translate = new DoubleAnimation(fromY, 0, TimeSpan.FromSeconds(durationSec))
        {
            BeginTime = TimeSpan.FromSeconds(beginSec),
            EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
        };

        if (el.RenderTransform is not TranslateTransform transform)
        {
            transform = new TranslateTransform();
            el.RenderTransform = transform;
        }
        el.Opacity = 0;
        el.BeginAnimation(OpacityProperty, opacity);
        transform.BeginAnimation(TranslateTransform.YProperty, translate);
    }

    // ============================================================
    //  Ambient particles
    // ============================================================

    private void SpawnParticles()
    {
        ParticleCanvas.Children.Clear();
        _particles.Clear();

        for (int i = 0; i < 55; i++)
        {
            double size = _rng.NextDouble() * 3.2 + 1.2;
            var ellipse = new Ellipse
            {
                Width = size,
                Height = size,
                Fill = new SolidColorBrush(Color.FromArgb(
                    (byte)(_rng.Next(40) + 50),
                    (byte)(_rng.Next(60) + 120),
                    (byte)(_rng.Next(60) + 130),
                    (byte)(_rng.Next(80) + 190)))
            };
            var p = new Particle
            {
                Ellipse = ellipse,
                X = _rng.NextDouble() * ActualWidth,
                Y = _rng.NextDouble() * ActualHeight,
                Vx = (_rng.NextDouble() - 0.5) * 0.35,
                Vy = -(_rng.NextDouble() * 0.55 + 0.15),
                BaseOpacity = 1
            };
            Canvas.SetLeft(ellipse, p.X);
            Canvas.SetTop(ellipse, p.Y);
            _particles.Add(p);
            ParticleCanvas.Children.Add(ellipse);
        }
    }

    private void MoveParticles()
    {
        double w = ParticleCanvas.ActualWidth;
        double h = ParticleCanvas.ActualHeight;
        if (w <= 0 || h <= 0) return;

        foreach (var p in _particles)
        {
            p.X += p.Vx;
            p.Y += p.Vy;
            if (p.Y < -6)
            {
                p.Y = h + 6;
                p.X = _rng.NextDouble() * w;
            }
            if (p.X < -6) p.X = w + 6;
            else if (p.X > w + 6) p.X = -6;

            Canvas.SetLeft(p.Ellipse, p.X);
            Canvas.SetTop(p.Ellipse, p.Y);
        }
    }

    // ============================================================
    //  Dark title bar
    // ============================================================

    [DllImport("dwmapi.dll")]
    private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attribute, ref int value, int size);

    private void TryEnableDarkTitleBar()
    {
        try
        {
            var hwnd = new WindowInteropHelper(this).Handle;
            int on = 1;
            // Attribute 20 works on Windows 10 20H1+; 19 on older builds.
            if (DwmSetWindowAttribute(hwnd, 20, ref on, sizeof(int)) != 0)
                DwmSetWindowAttribute(hwnd, 19, ref on, sizeof(int));
        }
        catch
        {
            // Best effort — a light title bar is acceptable.
        }
    }
}
