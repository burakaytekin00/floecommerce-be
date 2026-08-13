# --- BUILD AŞAMASI ---
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# 1. Önce solution dosyasını ve tüm katmanların csproj dosyalarını kopyalıyoruz
COPY ["ECommerce.API.sln", "./"]
COPY ["ECommerce.API/ECommerce.API.csproj", "ECommerce.API/"]
COPY ["ECommerce.Business/ECommerce.Business.csproj", "ECommerce.Business/"]
COPY ["ECommerce.Core/ECommerce.Core.csproj", "ECommerce.Core/"]
COPY ["ECommerce.Data/ECommerce.Data.csproj", "ECommerce.Data/"]
COPY ["ECommerce.Entity/ECommerce.Entity.csproj", "ECommerce.Entity/"]
COPY ["ECommerce.Repository/ECommerce.Repository.csproj", "ECommerce.Repository/"]

# 2. Bağımlılıkları (NuGet paketlerini) indirip yüklüyoruz
RUN dotnet restore "ECommerce.API.sln"

# 3. Şimdi projenin geri kalan tüm dosyalarını kopyalıyoruz
COPY . .
WORKDIR "/src/ECommerce.API"
RUN dotnet publish "ECommerce.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# --- RUNTIME (ÇALIŞTIRMA) AŞAMASI ---
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "ECommerce.API.dll"]