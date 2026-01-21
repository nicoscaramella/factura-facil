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

# Etapa 2: Ejecución (Azure Functions Runtime en Linux)
FROM mcr.microsoft.com/azure-functions/dotnet-isolated:4-dotnet-isolated10.0
WORKDIR /home/site/wwwroot
COPY --from=build /app/publish .
ENV AzureWebJobsScriptRoot=/home/site/wwwroot \
    AzureFunctionsJobHost__Logging__Console__IsEnabled=true
