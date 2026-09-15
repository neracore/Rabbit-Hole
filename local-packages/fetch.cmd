@echo off
cd /d "I:\Programming\Rabbit Hole\local-packages"
set BASE=https://api.nuget.org/v3-flatcontainer
curl.exe -L --retry 30 --retry-all-errors --retry-delay 2 -C - --connect-timeout 20 -o microsoft.netcore.app.runtime.win-x64.8.0.30.nupkg "%BASE%/microsoft.netcore.app.runtime.win-x64/8.0.30/microsoft.netcore.app.runtime.win-x64.8.0.30.nupkg"
curl.exe -L --retry 30 --retry-all-errors --retry-delay 2 -C - --connect-timeout 20 -o microsoft.windowsdesktop.app.runtime.win-x64.8.0.30.nupkg "%BASE%/microsoft.windowsdesktop.app.runtime.win-x64/8.0.30/microsoft.windowsdesktop.app.runtime.win-x64.8.0.30.nupkg"
curl.exe -L --retry 30 --retry-all-errors --retry-delay 2 -C - --connect-timeout 20 -o microsoft.aspnetcore.app.runtime.win-x64.8.0.30.nupkg "%BASE%/microsoft.aspnetcore.app.runtime.win-x64/8.0.30/microsoft.aspnetcore.app.runtime.win-x64.8.0.30.nupkg"
echo DONE
