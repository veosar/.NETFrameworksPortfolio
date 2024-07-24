FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["AN.WebApi/AN.WebApi.csproj", "AN.WebApi/"]
RUN dotnet restore "AN.WebApi/AN.WebApi.csproj"
COPY . .
WORKDIR "/src/AN.WebApi"
RUN dotnet build "AN.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "AN.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
RUN mkdir -p /root/.aspnet/https
RUN chmod 644 -R /root/.aspnet/https

COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AN.WebApi.dll"]
