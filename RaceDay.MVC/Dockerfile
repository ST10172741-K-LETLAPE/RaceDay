FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY RaceDay.MVC/RaceDay.MVC.csproj ./RaceDay.MVC/
RUN dotnet restore ./RaceDay.MVC/RaceDay.MVC.csproj
COPY RaceDay.MVC/ ./RaceDay.MVC/
WORKDIR /src/RaceDay.MVC
RUN dotnet publish -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app .
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Development
# API URL for MVC to call - will be overridden by docker-compose
ENV ApiBaseUrl=http://api:8080
ENTRYPOINT ["dotnet", "RaceDay.MVC.dll"]