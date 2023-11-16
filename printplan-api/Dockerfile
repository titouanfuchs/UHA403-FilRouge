#////////////////////////////////////////////////////////

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS base # Mise en place des bases du conteneur en utilisant DotNet SDK 7.0
WORKDIR /app # Dossier de travail
EXPOSE 80 # On expose le port de l'API

COPY . /app # On Transfert la totalité du projet dans le l'espace de Travail

RUN dotnet restore # On installe toutes les dépendances du projet

#////////////////////////////////////////////////////////

FROM base AS dev # Mode de développement
WORKDIR /app

RUN dotnet dev-certs https # On applique des certificats HTTPS pour l'api

ENTRYPOINT ["dotnet", "watch", "run", "--project","./api/printplan-api.csproj", "--urls", "http://0.0.0.0:80"] # On démarre le projet en mode de développement avec prise en charge du Hot Reload

#////////////////////////////////////////////////////////

FROM base AS prod # Mode de production
WORKDIR /app

RUN dotnet build -c Release -o /build # On build le projet 

RUN dotnet publish -c Release -o /publish /p:UseAppHost=false # On publie le projet dans un autre dossier

ENTRYPOINT ["dotnet", "/api/publish/printplan-api.dll"] # On démarre le projet en mode de production
