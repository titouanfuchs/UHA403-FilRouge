#////////////////////////////////////////////////////////

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS base
WORKDIR /app
EXPOSE 80

COPY . /app

RUN dotnet restore

RUN dotnet test "./api-test/api-test.csproj"

#////////////////////////////////////////////////////////

FROM base AS dev
WORKDIR /app

RUN dotnet dev-certs https

ENTRYPOINT ["dotnet", "watch", "run", "--project","./api/printplan-api.csproj", "--urls", "http://0.0.0.0:80"]

#////////////////////////////////////////////////////////

FROM base AS prod
WORKDIR /app

RUN dotnet build -c Release -o /build

RUN dotnet publish -c Release -o /publish /p:UseAppHost=false

ENTRYPOINT ["dotnet", "/api/publish/printplan-api.dll"]
