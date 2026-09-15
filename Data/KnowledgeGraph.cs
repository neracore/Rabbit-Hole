using RabbitHole.Models;

namespace RabbitHole.Data;

/// <summary>
/// The offline knowledge graph: 55 hand-written topics across eight territories.
/// Every connection is chosen to surprise — facts are short and true enough.
/// </summary>
public static class KnowledgeGraph
{
    public static readonly Topic[] All =
    {
        // ==================== THE DEEP ====================
        new("octopus", "Octopuses", "Nine minds wearing a single skin.", "The Deep",
            new[]
            {
                "Two-thirds of an octopus's neurons live in its arms — each arm can taste, touch, and act semi-independently.",
                "They rewrite their own RNA to retune their nervous system to the cold — a trick no other animal uses this way.",
                "An octopus's suckers can taste whatever they touch; every sucker is a chemical sensor.",
            },
            new[] { "mantis", "crows", "consciousness" }),

        new("mantis", "Mantis Shrimp", "It sees colors you cannot imagine.", "The Deep",
            new[]
            {
                "It carries up to 16 types of color receptors — humans manage three.",
                "Its punch accelerates like a .22 caliber bullet, and the collapsing bubble of water around it flashes with light and heat.",
                "The strike is so fast the water ahead of it boils — the prey is hit twice: by the club, then by the shockwave.",
            },
            new[] { "octopus", "synesthesia", "bioluminescence" }),

        new("bioluminescence", "Living Light", "Most of the light in the ocean is made by life.", "The Deep",
            new[]
            {
                "Roughly three in four deep-sea animals make their own light.",
                "Blue travels farthest through water, so almost everything down there glows blue — red light is a stealth color almost no eye can see.",
                "In Southeast Asia, entire rivers of fireflies flash in perfect synchrony, thousands of them, in waves.",
            },
            new[] { "anglerfish", "radiumgirls", "aurora" }),

        new("anglerfish", "Anglerfish", "She fishes the dark with a living lantern.", "The Deep",
            new[]
            {
                "Her lure is lit by luminous bacteria she will never meet anywhere else — the light is rented, not owned.",
                "The male, a fraction of her size, bites her and never lets go — he fuses in, dissolves into a parasite that supplies sperm forever.",
                "For over a century, scientists only ever found them as washed-up, bloated corpses — until 2014, the first living footage.",
            },
            new[] { "trench", "vents", "bioluminescence" }),

        new("trench", "The Mariana Trench", "Deeper than Everest is tall — with room to spare.", "The Deep",
            new[]
            {
                "Drop Everest into it and the peak would still sit more than a mile underwater.",
                "The pressure is over a thousand times that at the surface — the weight of a small car pressing on every square inch.",
                "Twelve humans have walked on the Moon. For half a century, only three had touched the trench floor.",
            },
            new[] { "anglerfish", "bloop", "vostok" }),

        new("vents", "Hydrothermal Vents", "Oases boiling in the abyss.", "The Deep",
            new[]
            {
                "Vent fluid pours out at up to 400 °C — and does not boil, because the pressure keeps it liquid.",
                "Whole ecosystems thrive there with zero sunlight, feeding on the chemistry of the rock itself.",
                "Every dive to a new vent field tends to find species that exist nowhere else on Earth.",
            },
            new[] { "extremophiles", "trench", "bioluminescence" }),

        new("bloop", "The Bloop", "The loudest unexplained sound in the ocean.", "The Deep",
            new[]
            {
                "In 1997, hydrophones 5,000 kilometres apart heard the same ultra-low rumble at the same instant.",
                "It was louder than any animal on record — scientists quietly mentioned 'large unknown sea creature' in briefing notes.",
                "NOAA later blamed cracking ice shelves. The internet never forgave them.",
            },
            new[] { "whale52", "wow", "trench" }),

        new("whale52", "The 52-Hertz Whale", "The loneliest song in the sea.", "The Deep",
            new[]
            {
                "It sings at 52 hertz — every other whale choir sings between 10 and 40. Nobody is listening at its frequency.",
                "It has been tracked since 1989, always alone, moving on migration routes no known whale uses.",
                "No one has ever seen it. It exists, as far as we know, only as a voice.",
            },
            new[] { "bloop", "voynich" }),

        new("olm", "The Olm", "The dragon that never grows up.", "The Deep",
            new[]
            {
                "A blind, pale salamander that lives its whole life in European caves — locals once believed it was a baby dragon.",
                "It can live past 100 years and go more than a decade without eating a single meal.",
                "Its eyes stop developing and skin grows over them; it 'sees' with smell, hearing, and electric fields.",
            },
            new[] { "vostok", "tardigrades", "extremophiles" }),

        // ==================== LIVING WONDERS ====================
        new("platypus", "Platypuses", "The mammal that broke the rules.", "Living Wonders",
            new[]
            {
                "It lays eggs, is venomous, has no stomach, and sweats milk through its skin.",
                "It hunts with its eyes, ears, and nostrils shut — sensing prey through electric fields in the water.",
                "Under ultraviolet light, a platypus glows green-blue. Nobody knows why.",
            },
            new[] { "lightning", "bombardier", "tardigrades" }),

        new("tardigrades", "Tardigrades", "The animal that refuses to die.", "Living Wonders",
            new[]
            {
                "In 2007, tardigrades spent ten days exposed to open space — and came back alive, some laying viable eggs.",
                "When dried out, they expel nearly all their water and harden into a glass-like shell that can wait decades.",
                "They survive from near absolute zero to above water's boiling point, and radiation that shatters DNA.",
            },
            new[] { "extremophiles", "anesthesia", "olm" }),

        new("mycelium", "The Wood Wide Web", "The internet was invented by forests.", "Living Wonders",
            new[]
            {
                "Underground fungal threads carry sugar, water, and chemical warnings between trees of different species.",
                "\"Mother trees\" recognize their own seedlings through the network and slip them extra food.",
                "A single fungus in Oregon spans nearly nine square kilometres and is thousands of years old — the largest living thing on Earth.",
            },
            new[] { "pando", "ants", "silkroad" }),

        new("pando", "Pando", "One tree that is a forest.", "Living Wonders",
            new[]
            {
                "It looks like 47,000 separate aspen trees. Underground, every root is connected — one organism, roughly 6,000 tonnes.",
                "Estimates of its age range from thousands of years to 80,000 — it may predate every human civilisation.",
                "Deer eat its young shoots faster than they can mature, and the world's heaviest organism is slowly dying.",
            },
            new[] { "bristlecone", "mycelium" }),

        new("bristlecone", "Bristlecone Pines", "Older than the pyramids, still standing.", "Living Wonders",
            new[]
            {
                "The oldest known tree — a bristlecone called Methuselah — has been alive for nearly 5,000 years.",
                "They grow only where nothing else will: bitter, frozen, wind-flayed rock. Comfort would kill them.",
                "Most of an ancient bristlecone is already dead; a thin strip of living bark keeps the whole skeleton alive for millennia.",
            },
            new[] { "pando", "extremophiles" }),

        new("slime", "Slime Molds", "A single cell that outsmarts maze designers.", "Living Wonders",
            new[]
            {
                "Physarum is one cell with millions of nuclei — and no brain of any kind.",
                "Given food placed like Tokyo's stations, it rebuilt the city's rail network in days — nearly optimally.",
                "It learns and remembers without neurons, storing memories in the shape of its own body.",
            },
            new[] { "ants", "crows", "mycelium" }),

        new("bombardier", "Bombardier Beetles", "The chemical flamethrower.", "Living Wonders",
            new[]
            {
                "It sprays a 100 °C toxic mist from a swivelling twin-nozzle turret at its rear.",
                "Two chemicals it stores apart violently combine at the moment of firing — a reaction chamber, not a gland.",
                "It can fire up to 500 times — and aim backwards over its own head, even with a frog's tongue on its leg.",
            },
            new[] { "platypus", "ants", "damascus" }),

        new("ants", "Ant Supercolonies", "The empire that spans continents.", "Living Wonders",
            new[]
            {
                "One Argentine ant supercolony stretches 6,000 kilometres along the Mediterranean — trillions of ants that never fight each other.",
                "Leafcutter ants have run fungus farms for over 50 million years — agriculture older than humanity by a factor of a million.",
                "Ants have two stomachs: one for themselves, one shared — they feed each other mouth to mouth.",
            },
            new[] { "slime", "crows", "emuwar" }),

        new("bees", "The Waggle Dance", "A figure-eight that maps the world.", "Living Wonders",
            new[]
            {
                "A honeybee's dance encodes direction as an angle against the sun and distance as the length of the waggle.",
                "Karl von Frisch decoded it in the 1940s; colleagues thought he was joking. He won a Nobel Prize.",
                "Scout bees argue: rival dancers headbutt each other until the swarm reaches a quorum on where to live.",
            },
            new[] { "ants", "consciousness" }),

        // ==================== THE MIND ====================
        new("crows", "Crow Intelligence", "Feathered primates.", "The Mind",
            new[]
            {
                "New Caledonian crows craft hooked tools from twigs — and use one tool to make a better one.",
                "They remember human faces for years, hold grudges, and warn other crows about you.",
                "In Japan, crows drop walnuts in front of cars at red lights and collect the cracked nuts when traffic stops.",
            },
            new[] { "mirror", "octopus", "consciousness" }),

        new("mirror", "The Mirror Test", "Who else knows that's you?", "The Mind",
            new[]
            {
                "The test is simple: mark an animal's face, hand it a mirror, and watch whether it investigates the mark.",
                "Confirmed passers include great apes, elephants, dolphins, magpies — and, controversially, a small fish.",
                "Human children fail it too — until roughly 18 months of age, the baby in the mirror is a stranger.",
            },
            new[] { "crows", "consciousness", "dreams" }),

        new("synesthesia", "Synesthesia", "Hearing color, tasting sound.", "The Mind",
            new[]
            {
                "Roughly 1 in 25 people cross-wire their senses — seeing sounds, tasting words, feeling textures from numbers.",
                "For synesthetes, every letter has a fixed color — the same one, unchanged, from childhood to old age.",
                "It runs in families and shows up far more often among artists and musicians than chance would predict.",
            },
            new[] { "earworms", "dreams", "aurora" }),

        new("blindsight", "Blindsight", "Seeing without knowing you see.", "The Mind",
            new[]
            {
                "People who are cortically blind will still navigate around obstacles they insist they cannot see.",
                "Forced to guess about a flash of light, they are right far more often than chance allows.",
                "Vision is not one system — the 'seeing' brain and the 'knowing' brain can be split apart by injury.",
            },
            new[] { "consciousness", "anesthesia", "mirror" }),

        new("dreams", "Dreams", "The theater that plays while you're away.", "The Mind",
            new[]
            {
                "Everyone dreams four to six times a night — even the people who swear they never do.",
                "Lucid dreamers can answer researchers' questions from inside a dream, with agreed-upon eye movements.",
                "In dreams you can rarely read: text almost always shifts or blurs the moment you look at it again.",
            },
            new[] { "mirror", "synesthesia", "dancingplague" }),

        new("placebo", "The Placebo Effect", "Belief as medicine.", "The Mind",
            new[]
            {
                "Placebos still work even when the patient is told — out loud — that the pills are placebos.",
                "Sham knee surgery, with incisions but no repair, often relieves pain about as well as the real operation.",
                "The effect gets stronger with brand names, injections, and packages that simply look more expensive.",
            },
            new[] { "radithor", "dancingplague", "epigenetics" }),

        new("anesthesia", "Anesthesia", "The off switch we don't fully understand.", "The Mind",
            new[]
            {
                "We still cannot fully explain how general anesthetics erase consciousness — we just know that they do.",
                "Under deep anesthesia the brain does not go quiet; it shatters into isolated islands that stop talking.",
                "In rare cases — roughly 1 in 15,000 — a patient is paralyzed but awake, and remembers everything.",
            },
            new[] { "consciousness", "blindsight", "tardigrades" }),

        new("consciousness", "The Hard Problem", "Why is there something it is like to be you?", "The Mind",
            new[]
            {
                "We can map every neuron in a brain and still not explain why any of it feels like anything at all.",
                "David Chalmers named the puzzle 'the hard problem' in 1995 — and it remains unsolved.",
                "Humans and octopuses last shared an ancestor over 550 million years ago — and both became conscious anyway.",
            },
            new[] { "anesthesia", "crows", "octopus" }),

        new("earworms", "Earworms", "Songs that won't leave.", "The Mind",
            new[]
            {
                "Over 90% of people get a song stuck in their head at least once a week.",
                "The stickiest songs are slightly faster, with common melodies interrupted by one unusual jump.",
                "In one study, chewing gum reduced earworms — jamming the inner voice that keeps rehearsing.",
            },
            new[] { "synesthesia", "goldrecord" }),

        // ==================== THE COSMOS ====================
        new("blackholes", "Black Holes", "Where the universe files things away.", "The Cosmos",
            new[]
            {
                "Falling in, you would notice nothing special at the edge. Watching from outside, you would see a friend frozen at the horizon forever.",
                "Hawking said black holes destroy information. Quantum mechanics says that is illegal. Somebody must be wrong.",
                "The first ever photograph of one — a glowing orange ring 55 million light-years away — took a telescope the size of the Earth.",
            },
            new[] { "neutrinos", "darkmatter", "voyager" }),

        new("neutrinos", "Neutrinos", "Ghosts that pass through planets.", "The Cosmos",
            new[]
            {
                "About 100 trillion neutrinos pass through your body every second. They never notice you, and you never notice them.",
                "A neutrino could pass through a light-year of solid lead and still have better than even odds of emerging.",
                "To catch a handful, physicists buried a cubic kilometre of sensors beneath the Antarctic ice — IceCube.",
            },
            new[] { "blackholes", "vostok", "darkmatter" }),

        new("darkmatter", "Dark Matter", "The universe's missing 85%.", "The Cosmos",
            new[]
            {
                "Galaxies spin so fast they should fly apart. Something unseen, five times heavier than everything visible, holds them together.",
                "It does not glow, absorb, reflect, or touch anything — it only pulls.",
                "Every detector ever built has come up empty. The suspects now range from ghost particles to ancient black holes.",
            },
            new[] { "blackholes", "neutrinos", "wow" }),

        new("voyager", "Voyager 1", "The farthest human-made thing.", "The Cosmos",
            new[]
            {
                "Launched in 1977, it left the Sun's realm and still whispers home from more than 23 billion kilometres away.",
                "Its signal, moving at light speed, takes about 22 hours to reach us. Its reply takes 22 more.",
                "It carries a golden record of Earth's sounds — and a map pointing home, for whoever finds it.",
            },
            new[] { "goldrecord", "wow", "trench" }),

        new("goldrecord", "The Golden Record", "A message sealed for a billion years.", "The Cosmos",
            new[]
            {
                "Carl Sagan's team had six weeks to compress an entire species into its grooves.",
                "It holds Bach and Beethoven beside Chuck Berry, a heartbeat, whale song, and a kiss.",
                "The etched instructions assume the finder can build a player from the diagram alone — without eyes, or hands, or ever having met us.",
            },
            new[] { "voyager", "earworms", "alexandria" }),

        new("wow", "The Wow! Signal", "72 seconds that never repeated.", "The Cosmos",
            new[]
            {
                "In 1977 a radio telescope printed an intense 72-second burst from deep space. The astronomer circled it and wrote 'Wow!'",
                "It landed exactly on the hydrogen line — the frequency scientists had quietly agreed an interstellar hello might use.",
                "Fifty years of looking later, it has never repeated. We still don't know what it was.",
            },
            new[] { "voyager", "bloop", "darkmatter" }),

        // ==================== SKY & STORM ====================
        new("aurora", "Auroras", "Solar wind painting the sky.", "Sky & Storm",
            new[]
            {
                "Charged particles from the Sun are caught by Earth's magnetic field and slammed into the sky — that curtain of light is the wreckage.",
                "Green comes from oxygen; the rare blood-red comes from oxygen struck so high it almost never happens.",
                "Jupiter and Saturn have auroras too — each planet's magnetic field paints with its own palette.",
            },
            new[] { "sprites", "synesthesia", "carrington" }),

        new("sprites", "Sprites & Elves", "Lightning that dances above storms.", "Sky & Storm",
            new[]
            {
                "Above violent thunderstorms, red jellyfish-shaped flashes flicker between 50 and 90 kilometres up — and last only milliseconds.",
                "Pilots reported them for decades. Scientists believed them until 1989, when a test camera accidentally caught one.",
                "They were named 'sprites' — folklore creatures — by researchers who assumed nobody would take the physics seriously either.",
            },
            new[] { "aurora", "lightning", "carrington" }),

        new("lightning", "Ball Lightning", "The fireball science couldn't pin down.", "Sky & Storm",
            new[]
            {
                "For centuries people described glowing spheres drifting through walls, down chimneys, along church aisles — and science shrugged.",
                "In 2012, a chance spectral recording of one suggested vaporized silicon from soil — but nobody can make one on purpose.",
                "Some reports describe balls that pass through closed windows and leave them unbroken. Physicists still argue.",
            },
            new[] { "sprites", "platypus", "carrington" }),

        new("carrington", "The Carrington Event", "The storm that set the sky on fire.", "Sky & Storm",
            new[]
            {
                "In 1859 the largest solar storm ever recorded hit Earth: auroras blazed near the Caribbean, birds sang at midnight.",
                "Telegraph operators disconnected their batteries and kept sending messages — on power carried by the aurora itself.",
                "A repeat today could fry power grids for months. In 2012, a storm of equal size crossed Earth's orbit — and missed us by nine days.",
            },
            new[] { "aurora", "sprites", "lightning" }),

        // ==================== MICROSCOPIC ====================
        new("dna", "DNA", "A library written in four letters.", "Microscopic",
            new[]
            {
                "Stretched end to end, the DNA in your body would reach beyond the Sun — hundreds of times over.",
                "Your genome is about three billion letters long; a printed version would fill a thousand books.",
                "Roughly 8% of human DNA is ancient virus — infections our ancestors survived, written permanently into ours.",
            },
            new[] { "epigenetics", "extremophiles", "vostok" }),

        new("epigenetics", "Epigenetics", "The spotlight that changes the script.", "Microscopic",
            new[]
            {
                "Every cell in your body carries the same DNA — a liver cell and a neuron differ only in which genes are lit up.",
                "A single chemical switch decides whether a bee larva becomes a worker or a queen. Same genes, different life.",
                "In mice, some chemical tags survive stress and echo in the offspring — experience leaving marks on inheritance.",
            },
            new[] { "dna", "placebo" }),

        new("extremophiles", "Extremophiles", "Life at the edges of the possible.", "Microscopic",
            new[]
            {
                "Deinococcus radiodurans shrugs off radiation a thousand times the lethal human dose — its DNA shatters, and it quietly reassembles.",
                "One microbe grows happily at 122 °C — the temperature inside a laboratory autoclave built to sterilize things.",
                "Some thrive in acid strong enough to dissolve metal, at pH 0 — they make battery acid feel like a spa.",
            },
            new[] { "tardigrades", "vents", "vostok" }),

        new("vostok", "Lake Vostok", "The lost world under the ice.", "Microscopic",
            new[]
            {
                "Two miles beneath Antarctica lies a lake the size of Lake Ontario, sealed off from the sky for 15 million years.",
                "Its water sits at about −3 °C and stays liquid because the ice above presses down like a lid.",
                "Scientists drilled down to it in 2012 — and still argue about whether what they found was life, or contamination.",
            },
            new[] { "extremophiles", "neutrinos", "olm" }),

        // ==================== STRANGE HISTORY ====================
        new("radiumgirls", "The Radium Girls", "They painted glow onto watch dials — and into their bones.", "Strange History",
            new[]
            {
                "In the 1920s, factory women were taught to point their radium paintbrushes with their lips. Many kept painting anyway — it made them feel alive.",
                "They called themselves the 'ghost girls': on the walk home, their clothes and hair glowed faintly in the dark.",
                "Dying of radium jaw, they sued — and won. Their case is why workers anywhere can sue an employer for safety.",
            },
            new[] { "radithor", "bioluminescence" }),

        new("radithor", "Radiant Water", "The health drink that glowed — literally.", "Strange History",
            new[]
            {
                "In the 1920s, Radithor — radium dissolved in water — was sold as a cure for everything from fatigue to madness.",
                "Socialite Eben Byers drank about 1,400 bottles. His jaw disintegrated; he died in 1932.",
                "The Wall Street Journal's headline: 'The Radium Water Worked Fine Until His Jaw Came Off.'",
            },
            new[] { "radiumgirls", "placebo" }),

        new("molasses", "The Great Molasses Flood", "A wave of candy, two storeys high.", "Strange History",
            new[]
            {
                "Boston, 1919: a storage tank burst and released 2.3 million gallons of molasses in a wave nearly two storeys tall.",
                "The wave moved at an estimated 56 km/h — faster than anyone could run, and far slower than anyone could swim.",
                "The North End reportedly smelled of molasses on hot days for decades after.",
            },
            new[] { "emuwar", "dancingplague" }),

        new("emuwar", "The Great Emu War", "Australia lost a war to birds.", "Strange History",
            new[]
            {
                "In 1932 Australia deployed soldiers with machine guns against 20,000 emus destroying farmland.",
                "The emus scattered into small, fast, evasive squads. The army withdrew after weeks, nearly out of ammunition.",
                "Major Meredith testified the birds could face machine guns 'like Zulus' — and recommended they be left alone.",
            },
            new[] { "molasses", "ants" }),

        new("dancingplague", "The Dancing Plague", "A city that danced itself to death.", "Strange History",
            new[]
            {
                "Strasbourg, 1518: one woman began dancing in the street and could not stop. Within a month, about 400 people joined her.",
                "Some danced for days without rest; some reportedly died of heart attack, stroke, or sheer exhaustion.",
                "The authorities' diagnosis? More dancing. They hired musicians and opened guild halls. It got worse.",
            },
            new[] { "dreams", "placebo", "molasses" }),

        new("tulips", "Tulip Mania", "The flower worth a house.", "Strange History",
            new[]
            {
                "Holland, 1637: a single tulip bulb sold for the price of a canal-side mansion.",
                "The most prized petals — flame-striped — were beautiful because the flower was sick with a virus.",
                "In February 1637 the market simply dissolved. One morning, no one was willing to buy at any price.",
            },
            new[] { "silkroad", "molasses" }),

        // ==================== LOST ARTS ====================
        new("voynich", "The Voynich Manuscript", "The book no one can read.", "Lost Arts",
            new[]
            {
                "Carbon-dated to the early 1400s, it is written left to right in an alphabet that appears nowhere else on Earth.",
                "Its plants match no known species. Its astronomical diagrams match no known sky.",
                "Codebreakers who cracked Enigma took it on as light relief — and failed. Some began to suspect it was a language at all.",
            },
            new[] { "antikythera", "whale52", "alexandria" }),

        new("antikythera", "Antikythera Mechanism", "A computer from 100 BC.", "Lost Arts",
            new[]
            {
                "In 1901, sponge divers found a corroded lump of bronze in a Roman-era shipwreck. It sat ignored in a museum for years.",
                "X-rays revealed over 30 meshing gears — a machine that predicted eclipses, tracked planets, and even counted the Olympic games.",
                "Nothing of comparable mechanical complexity appears again in the record for nearly 1,400 years.",
            },
            new[] { "voynich", "damascus", "trench" }),

        new("damascus", "Damascus Steel", "The blade chemistry we lost.", "Lost Arts",
            new[]
            {
                "Legends claimed Damascus blades could slice a falling silk scarf and flex without breaking. European smiths never managed it.",
                "The swords were forged from Indian wootz steel; when the ore sources ran out, the technique died with them.",
                "Under an electron microscope, ancient blades revealed carbon nanotubes — forged in by accident, two millennia before we named them.",
            },
            new[] { "antikythera", "concrete", "bombardier" }),

        new("concrete", "Roman Concrete", "Harbor walls that grew stronger in seawater.", "Lost Arts",
            new[]
            {
                "Modern concrete piers degrade in seawater within decades. Roman piers, 2,000 years old, are stronger today than when built.",
                "Seawater reacting with volcanic ash grows interlocking mineral crystals inside the cracks — the sea heals the concrete.",
                "The full recipe was lost for centuries. Modern engineers only rediscovered the self-healing trick in 2023.",
            },
            new[] { "damascus", "greekfire" }),

        new("greekfire", "Greek Fire", "The flame that burned on water.", "Lost Arts",
            new[]
            {
                "The Byzantines sprayed a liquid fire that clung to ships and kept burning even on the sea itself.",
                "The formula was a state secret, guarded so tightly that it was apparently held by one family — and passed down.",
                "It was lost completely. Modern chemists have thrown everything at it; nothing quite matches the ancient accounts.",
            },
            new[] { "concrete", "damascus" }),

        new("alexandria", "Library of Alexandria", "The memory of the ancient world.", "Lost Arts",
            new[]
            {
                "Its stated ambition: to collect every book in the world. Scholars lived and ate there for free — a research campus, 2,300 years ago.",
                "By decree, any scrolls found on ships docking at Alexandria were copied; the ships usually left with the copies.",
                "There was no single dramatic fire. The Library faded in pieces over centuries — which is somehow the sadder story.",
            },
            new[] { "goldrecord", "voynich", "terracotta" }),

        new("terracotta", "The Terracotta Army", "Eight thousand soldiers, no two faces alike.", "Lost Arts",
            new[]
            {
                "In 1974, farmers digging a well found a clay head. Then an army: some 8,000 soldiers, 130 chariots, 670 horses.",
                "Every figure carries an inspector's signature — quality control, 2,200 years before the term existed.",
                "The whole army was once painted in brilliant lacquered colors — which flaked away within minutes of meeting open air.",
            },
            new[] { "silkroad", "alexandria" }),

        new("silkroad", "The Silk Road", "8,000 kilometres of camels and rumors.", "Lost Arts",
            new[]
            {
                "It was never one road, but a shifting web of routes that grew and died with empires and oases.",
                "Silk was a state secret for centuries — smuggling silkworm eggs out of China was punishable by death.",
                "It carried more ideas than goods: paper, Buddhism, plague. Almost no one walked it end to end.",
            },
            new[] { "terracotta", "mycelium", "tulips" }),
    };
}
