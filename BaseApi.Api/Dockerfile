# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["BaseApi.Api/BaseApi.Api.csproj", "BaseApi.Api/"]
COPY ["BaseApi.Infrastructure/BaseApi.Infrastructure.csproj", "BaseApi.Infrastructure/"]
COPY ["BaseApi.Application/BaseApi.Application.csproj", "BaseApi.Application/"]
COPY ["BaseApi.Contracts/BaseApi.Contracts.csproj", "BaseApi.Contracts/"]
COPY ["BaseApi.Domain/BaseApi.Domain.csproj", "BaseApi.Domain/"]
COPY ["BaseApi.Persistence/BaseApi.Persistence.csproj", "BaseApi.Persistence/"]
RUN dotnet restore "./BaseApi.Api/BaseApi.Api.csproj"
COPY . .
WORKDIR "/src/BaseApi.Api"
RUN dotnet build "./BaseApi.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./BaseApi.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BaseApi.Api.dll"]