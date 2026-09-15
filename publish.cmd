@echo off
REM ------------------------------------------------------------
REM  Rabbit Hole — publish as a self-contained single-file exe
REM  Output: bin\Release\net8.0-windows\win-x64\publish\RabbitHole.exe
REM ------------------------------------------------------------
dotnet publish RabbitHole.csproj ^
  -c Release ^
  -r win-x64 ^
  --self-contained true ^
  -p:PublishSingleFile=true ^
  -p:IncludeNativeLibrariesForSelfExtract=true ^
  -p:EnableCompressionInSingleFile=true ^
  -p:DebugType=embedded ^
  -p:RuntimeIdentifier=win-x64

echo.
echo Done. Executable: bin\Release\net8.0-windows\win-x64\publish\RabbitHole.exe
