FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5000

RUN groupadd -r sqssnsuser && useradd -r -g sqssnsuser sqssnsuser
USER sqssnsuser

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["CustomersApi/CustomersApi.csproj", "CustomersApi/"]
RUN dotnet restore "CustomersApi/CustomersApi.csproj"
COPY . .
WORKDIR "/src/CustomersApi"
RUN dotnet build "CustomersApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "CustomersApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CustomersApi.dll"]
