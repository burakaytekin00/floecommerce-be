# --- BUILD ---
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

COPY ECommerce.API.sln .

COPY ECommerce.API/ECommerce.API.csproj ECommerce.API/
COPY ECommerce.Business/ECommerce.Business.csproj ECommerce.Business/
COPY ECommerce.Core/ECommerce.Core.csproj ECommerce.Core/
COPY ECommerce.Data/ECommerce.Data.csproj ECommerce.Data/
COPY ECommerce.Entity/ECommerce.Entity.csproj ECommerce.Entity/
COPY ECommerce.Repository/ECommerce.Repository.csproj ECommerce.Repository/

RUN dotnet restore "ECommerce.API.sln"

# Kaynak kodlarını kopyala
COPY . .

# Windows'tan gelen eski obj/bin klasörlerini temizle
RUN find /src -type d \( -name obj -o -name bin \) -prune -exec rm -rf {} +

# Temiz ortamda tekrar restore
RUN dotnet restore "ECommerce.API.sln"

WORKDIR /src/ECommerce.API

RUN dotnet publish "ECommerce.API.csproj" -c Release -o /app/publish --no-restore


# --- RUNTIME ---
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final

WORKDIR /app

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "ECommerce.API.dll"]