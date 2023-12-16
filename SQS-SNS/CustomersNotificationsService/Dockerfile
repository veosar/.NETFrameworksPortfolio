FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

RUN groupadd -r sqssnsuser && useradd -r -g sqssnsuser sqssnsuser
USER sqssnsuser

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["CustomersNotificationsService/CustomersNotificationsService.csproj", "CustomersNotificationsService/"]
RUN dotnet restore "CustomersNotificationsService/CustomersNotificationsService.csproj"
COPY . .
WORKDIR "/src/CustomersNotificationsService"
RUN dotnet build "CustomersNotificationsService.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "CustomersNotificationsService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CustomersNotificationsService.dll"]
