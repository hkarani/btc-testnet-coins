# Use the official ASP.NET Core runtime image as a base
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

# Copy the published output of the ASP.NET Core application into the container
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Copy the project file and restore dependencies
COPY ["app/BTCTestnetCoins.csproj", "."]
RUN dotnet restore "./BTCTestnetCoins.csproj"

# Copy the source code
COPY . .

# Build the application
RUN dotnet build "BTCTestnetCoins.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "BTCTestnetCoins.csproj" -c Release -o /app/publish

COPY /app/.env /app/publish

# Use the base image and copy the published output
FROM base AS final
WORKDIR /app

# Copy the published output from the 'publish' stage
COPY --from=publish /app/publish .

# Update the entry point to specify the DLL containing the application
ENTRYPOINT ["dotnet", "BTCTestnetCoins.dll"]
