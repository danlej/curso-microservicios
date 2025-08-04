FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build 
WORKDIR /src
COPY AddAdult.csproj .
RUN dotnet restore
COPY . .

RUN dotnet build "AddAdult.csproj" -c Release -o /app/build

RUN dotnet publish -c release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app .

# Crear un archivo appsettings.json vacío con permisos explícitos
RUN touch /app/appsettings.json && \
    echo '{}' > /app/appsettings.json && \
    chmod 644 /app/appsettings.json && \
    ls -la /app/appsettings.json

ENTRYPOINT ["dotnet", "AddAdult.dll"]

