# Building stage
FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
WORKDIR /source
COPY . .
RUN dotnet restore "./TDLCProyecto/TDLCProyecto.csproj" --disable-parallel
RUN dotnet publish "./TDLCProyecto/TDLCProyecto.csproj" -c Release -o /app --no-restore

# Serving stage
FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal
WORKDIR /app
COPY --from=build /app ./

EXPOSE 9091

ENTRYPOINT ["dotnet", "TDLCProyecto.dll"]


