FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Çözüm dosyasını ve projeleri doğrudan kopyala
COPY ECommerce.API.sln ./
COPY ECommerce.API/ECommerce.API.csproj ECommerce.API/
COPY ECommerce.Business/ECommerce.Business.csproj ECommerce.Business/
COPY ECommerce.Core/ECommerce.Core.csproj ECommerce.Core/
COPY ECommerce.Data/ECommerce.Data.csproj ECommerce.Data/
COPY ECommerce.Entity/ECommerce.Entity.csproj ECommerce.Entity/
COPY ECommerce.Repository/ECommerce.Repository.csproj ECommerce.Repository/

RUN dotnet restore ECommerce.API.sln

COPY . .
RUN dotnet publish ECommerce.API/ECommerce.API.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "ECommerce.API.dll"]