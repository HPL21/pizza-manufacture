FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 5003

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY . .
RUN dotnet publish -c Release -o /publish

FROM base AS final
WORKDIR /app
COPY --from=build /publish .
ENTRYPOINT ["dotnet", "API.dll"]   # zamień na właściwy plik
