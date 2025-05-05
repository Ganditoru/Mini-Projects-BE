# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["API/MiniProject.Api/MiniProject.Api/MiniProject.Api.csproj", "API/MiniProject.Api/MiniProject.Api/"]
COPY ["Common/MiniProject.Common.Messaging.Contracts/MiniProject.Common.Messaging.Contracts.csproj", "Common/MiniProject.Common.Messaging.Contracts/"]
COPY ["Modules/Events/MiniProject.Modules.Events.Infrastructure/MiniProject.Modules.Events.Infrastructure.csproj", "Modules/Events/MiniProject.Modules.Events.Infrastructure/"]
COPY ["Modules/Events/MiniProject.Modules.Events.Application/MiniProject.Modules.Events.Application.csproj", "Modules/Events/MiniProject.Modules.Events.Application/"]
COPY ["Modules/Events/MiniProject.Modules.Events.Domain/MiniProject.Modules.Events.Domain.csproj", "Modules/Events/MiniProject.Modules.Events.Domain/"]
COPY ["Modules/Events/MiniProject.Modules.Events.Presentation/MiniProject.Modules.Events.Presentation.csproj", "Modules/Events/MiniProject.Modules.Events.Presentation/"]
COPY ["Modules/Ticketing/MiniProject.Modules.Ticketing.Infrastructure/MiniProject.Modules.Ticketing.Infrastructure.csproj", "Modules/Ticketing/MiniProject.Modules.Ticketing.Infrastructure/"]
COPY ["Modules/Ticketing/MiniProject.Modules.Ticketing.Application/MiniProject.Modules.Ticketing.Application.csproj", "Modules/Ticketing/MiniProject.Modules.Ticketing.Application/"]
COPY ["Modules/Ticketing/MiniProject.Modules.Ticketing.Domain/MiniProject.Modules.Ticketing.Domain.csproj", "Modules/Ticketing/MiniProject.Modules.Ticketing.Domain/"]
COPY ["Modules/Ticketing/MiniProject.Modules.Ticketing.Presentation/MiniProject.Modules.Ticketing.Presentation.csproj", "Modules/Ticketing/MiniProject.Modules.Ticketing.Presentation/"]
COPY ["Modules/Users/MiniProject.Modules.Users.Infrastructure/MiniProject.Modules.Users.Infrastructure.csproj", "Modules/Users/MiniProject.Modules.Users.Infrastructure/"]
COPY ["Modules/Users/MiniProject.Modules.Users.Application/MiniProject.Modules.Users.Application.csproj", "Modules/Users/MiniProject.Modules.Users.Application/"]
COPY ["Modules/Users/MiniProject.Modules.Users.Domain/MiniProject.Modules.Users.Domain.csproj", "Modules/Users/MiniProject.Modules.Users.Domain/"]
COPY ["Modules/Users/MiniProject.Modules.Users.Presentation/MiniProject.Modules.Users.Presentation.csproj", "Modules/Users/MiniProject.Modules.Users.Presentation/"]
COPY ["Modules/Users/MiniProject.Modules.Users.PublicApi/MiniProject.Modules.Users.PublicApi.csproj", "Modules/Users/MiniProject.Modules.Users.PublicApi/"]
RUN dotnet restore "./API/MiniProject.Api/MiniProject.Api/MiniProject.Api.csproj"
COPY . .
WORKDIR "/src/API/MiniProject.Api/MiniProject.Api"
RUN dotnet build "./MiniProject.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./MiniProject.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MiniProject.Api.dll"]