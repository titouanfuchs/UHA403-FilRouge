#////////////////////////////////////////////////////////

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS dev
WORKDIR /app
EXPOSE 80

COPY . /app

RUN dotnet dev-certs https
RUN dotnet restore

ENTRYPOINT ["dotnet",  "watch", "run", "--urls", "http://0.0.0.0:80"]

#////////////////////////////////////////////////////////

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS prod
WORKDIR /app
EXPOSE 80

COPY . .

RUN dotnet restore
RUN dotnet build -c Release -o /build

RUN dotnet publish "printplan-api.csproj" -c Release -o /publish /p:UseAppHost=false

ENTRYPOINT ["dotnet", "/publish/printplan-api.dll"]
