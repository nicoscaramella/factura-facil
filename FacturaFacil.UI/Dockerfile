# Etapa 1: Compilar Blazor
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY ["FacturaFacil.UI.csproj", "."]
COPY ["../FacturaFacil.Core/FacturaFacil.Core.csproj", "../FacturaFacil.Core/"]
RUN dotnet restore "FacturaFacil.UI.csproj"
COPY . .
RUN dotnet publish "FacturaFacil.UI.csproj" -c Release -o /app/publish

# Etapa 2: Servidor Nginx
FROM nginx:alpine
WORKDIR /usr/share/nginx/html
# Copiar los archivos compilados
COPY --from=build /app/publish/wwwroot .
# Copiar nuestra configuración personalizada de Nginx
COPY nginx.conf /etc/nginx/nginx.conf
