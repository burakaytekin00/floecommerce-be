# --- BUILD ---
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Proje dosyasını kopyala ve restore et
COPY ECommerce.API/ECommerce.API.csproj ECommerce.API/
RUN dotnet restore "ECommerce.API/ECommerce.API.csproj"

# Kalan tüm dosyaları kopyala ve yayınla
COPY . .
WORKDIR "/src/ECommerce.API"
RUN dotnet publish "ECommerce.API.csproj" -c Release -o /app/publish

# --- RUNTIME ---
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "ECommerce.API.dll"]