#////////////////////////////////////////////////////////
# Mise en place des bases du conteneur en utilisant DotNet SDK 7.0
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS base
# Dossier de travail
WORKDIR /app
# On expose le port de l'API
EXPOSE 80

# On Transfert la totalité du projet dans le l'espace de Travail
COPY . /app 

# On installe toutes les dépendances du projet
RUN dotnet restore

#////////////////////////////////////////////////////////

# Mode de développement
FROM base AS dev 
WORKDIR /app

# On applique des certificats HTTPS pour l'api
RUN dotnet dev-certs https

# On démarre le projet en mode de développement avec prise en charge du Hot Reload
ENTRYPOINT ["dotnet", "watch", "run", "--project","./api/printplan-api.csproj", "--urls", "http://0.0.0.0:80"]

#////////////////////////////////////////////////////////

# Mode de production
FROM base AS prod
WORKDIR /app

# On build le projet 
RUN dotnet build -c Release -o /build

# On publie le projet dans un autre dossier
RUN dotnet publish -c Release -o /publish /p:UseAppHost=false

# On démarre le projet en mode de production
ENTRYPOINT ["dotnet", "/api/publish/printplan-api.dll"]
