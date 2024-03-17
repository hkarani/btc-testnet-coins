# Use the official ASP.NET Core runtime image as a base
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

# Copy the published output of the ASP.NET Core application into the container
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["BTCTestnetCoins.csproj", "."]
RUN dotnet restore "./BTCTestnetCoins.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "BTCTestnetCoins.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BTCTestnetCoins.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BTCTestnetCoins.dll"]