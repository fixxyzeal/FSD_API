FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim  AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY . ./aspnetapp/
WORKDIR /app/aspnetapp
RUN dotnet restore

# Copy everything else and build
COPY . ./FSD_API/
RUN dotnet publish "FSD_API" -c Release -o /app

# Build runtime image
FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim as final
WORKDIR /app
COPY --from=build-env /app .
CMD ASPNETCORE_URLS=http://*:$PORT dotnet FSD_API.dll --environment "Production"