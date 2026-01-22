# Etapa 1: Compilación
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copiar archivos de proyecto
COPY ["FacturaFacil.Api/FacturaFacil.Api.csproj", "FacturaFacil.Api/"]
COPY ["FacturaFacil.Core/FacturaFacil.Core.csproj", "FacturaFacil.Core/"]
COPY ["FacturaFacil.Services/FacturaFacil.Services.csproj", "FacturaFacil.Services/"]

# Restaurar dependencias
RUN dotnet restore "FacturaFacil.Api/FacturaFacil.Api.csproj"

# Copiar todo el código fuente
COPY . .
WORKDIR "/src/FacturaFacil.Api"

# Publicar la aplicación
RUN dotnet publish "FacturaFacil.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Etapa 2: Ejecución (ASP.NET Core Runtime)
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build /app/publish .

# Configurar el puerto por defecto de la imagen (usualmente 8080 en imágenes nuevas, pero se puede forzar)
ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet", "FacturaFacil.Api.dll"]