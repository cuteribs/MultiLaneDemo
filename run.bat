dotnet publish Gateway  -o output\gateway-a
dotnet publish Gateway  -o output\gateway-b
dotnet publish Service -o output\service-0-0
dotnet publish Service -o output\service-0-1
dotnet publish Service -o output\service-1-0
dotnet publish Service -o output\service-1-1

wt -w demo --title "Caddy" -d . cmd /k caddy run --watch --config Caddyfile ; ^
nt --title "gateway-a"   -d . cmd /k dotnet output\gateway-a\MultiLane.Gateway.dll service-0 --urls="http://localhost:5000" ; ^
nt --title "gateway-b"   -d . cmd /k dotnet output\gateway-b\MultiLane.Gateway.dll service-0 --urls="http://localhost:5001" ; ^
nt --title "service-0-0" -d . cmd /k dotnet output\service-0-0\MultiLane.Service.dll service-0-0 service-1 --urls="http://localhost:6000" ; ^
nt --title "service-0-1" -d . cmd /k dotnet output\service-0-1\MultiLane.Service.dll service-0-1 service-1 --urls="http://localhost:6001" ; ^
nt --title "service-1-0" -d . cmd /k dotnet output\service-1-0\MultiLane.Service.dll service-1-0 localhost --urls="http://localhost:6010" ; ^
nt --title "service-1-1" -d . cmd /k dotnet output\service-1-1\MultiLane.Service.dll service-1-1 localhost --urls="http://localhost:6011" 

