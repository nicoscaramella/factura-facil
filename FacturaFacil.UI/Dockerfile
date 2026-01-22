# Etapa 1: Compilar Blazor
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copiar csproj de UI y Core
COPY ["FacturaFacil.UI/FacturaFacil.UI.csproj", "FacturaFacil.UI/"]
COPY ["FacturaFacil.Core/FacturaFacil.Core.csproj", "FacturaFacil.Core/"]

# Restaurar
RUN dotnet restore "FacturaFacil.UI/FacturaFacil.UI.csproj"

# Copiar todo el código
COPY . .
WORKDIR "/src/FacturaFacil.UI"

# Publicar (Con compresión activada por defecto para reducir tamaño)
RUN dotnet publish "FacturaFacil.UI.csproj" -c Release -o /app/publish

# Etapa 2: Servidor Nginx
FROM nginx:alpine
WORKDIR /usr/share/nginx/html
# Copiar los archivos compilados
COPY --from=build /app/publish/wwwroot .
# Copiar configuración de Nginx (ruta ajustada)
COPY FacturaFacil.UI/nginx.conf /etc/nginx/nginx.conf