dotnet publish Gateway  -o output\gateway-a
dotnet publish Gateway  -o output\gateway-b
dotnet publish ServiceA -o output\service-a-0
dotnet publish ServiceA -o output\service-a-1
dotnet publish ServiceB -o output\service-b-0
dotnet publish ServiceB -o output\service-b-1

wt -w demo --title "Caddy" -d . cmd /k caddy run --watch --config Caddyfile ; ^
nt --title "gateway-a"   -d . cmd /k dotnet output\gateway-a\MultiLane.Gateway.dll --urls="http://localhost:5000" ; ^
nt --title "gateway-b"   -d . cmd /k dotnet output\gateway-b\MultiLane.Gateway.dll --urls="http://localhost:5001" ; ^
nt --title "service-a-0" -d . cmd /k dotnet output\service-a-0\MultiLane.ServiceA.dll --urls="http://localhost:5010" ; ^
nt --title "service-a-1" -d . cmd /k dotnet output\service-a-1\MultiLane.ServiceA.dll --urls="http://localhost:5011" ; ^
nt --title "service-b-0" -d . cmd /k dotnet output\service-b-0\MultiLane.ServiceB.dll --urls="http://localhost:5020" ; ^
nt --title "service-b-1" -d . cmd /k dotnet output\service-b-1\MultiLane.ServiceB.dll --urls="http://localhost:5021"

