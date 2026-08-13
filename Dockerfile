# --- BUILD ---
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Çözüm dosyasını ve projeleri kopyala
COPY *.sln ./
COPY ECommerce.API/*.csproj ECommerce.API/
COPY ECommerce.Business/*.csproj ECommerce.Business/
COPY ECommerce.Core/*.csproj ECommerce.Core/
COPY ECommerce.Data/*.csproj ECommerce.Data/
COPY ECommerce.Entity/*.csproj ECommerce.Entity/
COPY ECommerce.Repository/*.csproj ECommerce.Repository/

# Restore et
RUN dotnet restore

# Kalan tüm dosyaları kopyala ve yayınla (publish)
COPY . ./
RUN dotnet publish ECommerce.API/ECommerce.API.csproj -c Release -o out

# --- RUNTIME ---
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "ECommerce.API.dll"]