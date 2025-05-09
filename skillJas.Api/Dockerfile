# Etapa 1: build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copiar todo el contenido de la solución
COPY ../.. .

# Restaurar paquetes
RUN dotnet restore ./skillJas.Api/skillJas.Api.csproj

# Publicar la API
RUN dotnet publish ./skillJas.Api/skillJas.Api.csproj -c Release -o /app/out

# Etapa 2: runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build /app/out .

# Puerto que usará el contenedor
EXPOSE 80

ENTRYPOINT ["dotnet", "skillJas.Api.dll"]