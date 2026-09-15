@echo off
cd /d "I:\Programming\Rabbit Hole\local-packages"
set BASE=https://api.nuget.org/v3-flatcontainer
curl.exe -L --retry 40 --retry-all-errors --retry-delay 2 -C - --connect-timeout 20 -s -o microsoft.windowsdesktop.app.runtime.win-x64.8.0.30.nupkg "%BASE%/microsoft.windowsdesktop.app.runtime.win-x64/8.0.30/microsoft.windowsdesktop.app.runtime.win-x64.8.0.30.nupkg"
echo DESKTOP DONE
curl.exe -L --retry 40 --retry-all-errors --retry-delay 2 -C - --connect-timeout 20 -s -o microsoft.aspnetcore.app.runtime.win-x64.8.0.30.nupkg "%BASE%/microsoft.aspnetcore.app.runtime.win-x64/8.0.30/microsoft.aspnetcore.app.runtime.win-x64.8.0.30.nupkg"
echo ASPNET DONE
