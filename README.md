# Rabbit Hole

An infinite knowledge-exploration game for Windows. Press **ENTER**, fall into a random
topic, read a few short strange facts, then **GO DEEPER** into a surprising connected
topic. The game tracks your depth and the full trail — surface at any time (ESC) to see
the final journey laid out as a map of your fall.

- Dark, mysterious, animated UI
- 55-topic offline knowledge graph — no internet, login, ads, or tracking
- Keyboard-first: `ENTER` / `SPACE` dive · `ESC` surfaces

## Run from source

```cmd
dotnet run
```

## Build the single-file executable (no .NET install required on target machines)

```cmd
publish.cmd
```

or:

```cmd
dotnet publish RabbitHole.csproj -c Release -r win-x64 --self-contained true ^
  -p:PublishSingleFile=true -p:IncludeNativeLibrariesForSelfExtract=true ^
  -p:EnableCompressionInSingleFile=true -p:DebugType=embedded
```

Output: `bin\Release\net8.0-windows\win-x64\publish\RabbitHole.exe`

## Tech

C# / .NET 8 WPF · fully offline sample data in `Data/KnowledgeGraph.cs`
