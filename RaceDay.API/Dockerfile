FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY RaceDay.API/RaceDay.API.csproj ./RaceDay.API/
RUN dotnet restore ./RaceDay.API/RaceDay.API.csproj
COPY RaceDay.API/ ./RaceDay.API/
WORKDIR /src/RaceDay.API
RUN dotnet publish -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app .
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Development
ENTRYPOINT ["dotnet", "RaceDay.API.dll"]