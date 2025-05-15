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
COPY ["Directory.Build.props", "."]
COPY ["src/API/MiniProject.Api2/MiniProject.Api2.csproj", "src/API/MiniProject.Api2/"]
COPY ["src/Modules/Attendance/MiniProject.Modules.Attendances.Infrastructure/MiniProject.Modules.Attendance.Infrastructure.csproj", "src/Modules/Attendance/MiniProject.Modules.Attendances.Infrastructure/"]
COPY ["src/Modules/Attendance/MiniProject.Modules.Attendance.Application/MiniProject.Modules.Attendance.Application.csproj", "src/Modules/Attendance/MiniProject.Modules.Attendance.Application/"]
COPY ["src/Common/MiniProject.Common.Messaging.Contracts/MiniProject.Common.Messaging.Contracts.csproj", "src/Common/MiniProject.Common.Messaging.Contracts/"]
COPY ["src/Modules/Attendance/MiniProject.Modules.Attendance.Domain/MiniProject.Modules.Attendance.Domain.csproj", "src/Modules/Attendance/MiniProject.Modules.Attendance.Domain/"]
COPY ["src/Modules/Attendance/MiniProject.Modules.Attendances.Presentation/MiniProject.Modules.Attendance.Presentation.csproj", "src/Modules/Attendance/MiniProject.Modules.Attendances.Presentation/"]
RUN dotnet restore "./src/API/MiniProject.Api2/MiniProject.Api2.csproj"
COPY . .
WORKDIR "/src/src/API/MiniProject.Api2"
RUN dotnet build "./MiniProject.Api2.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./MiniProject.Api2.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MiniProject.Api2.dll"]