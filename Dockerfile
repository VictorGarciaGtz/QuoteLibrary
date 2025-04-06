# Imagen base de ASP.NET Core para ejecutar la aplicación
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["QuoteLibrary.API/QuoteLibrary.API.csproj", "QuoteLibrary.API/"]
COPY ["QuoteLibrary.Application/QuoteLibrary.Application.csproj", "QuoteLibrary.Application/"]
COPY ["QuoteLibrary.Domain/QuoteLibrary.Domain.csproj", "QuoteLibrary.Domain/"]	
COPY ["QuoteLibrary.Infrastructure/QuoteLibrary.Infrastructure.csproj", "QuoteLibrary.Infrastructure/"]

RUN dotnet restore "QuoteLibrary.API/QuoteLibrary.API.csproj"
COPY . .
WORKDIR "/src/QuoteLibrary.API"
RUN dotnet build "QuoteLibrary.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "QuoteLibrary.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "QuoteLibrary.API.dll"]