FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine
WORKDIR /app
EXPOSE 80
COPY publish .
ENTRYPOINT ["dotnet", "MultiLane.Service.dll"]