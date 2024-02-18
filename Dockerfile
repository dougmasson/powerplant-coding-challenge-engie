#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# Use the official ASP.NET 8.0 runtime image as the base image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Ensure we listen on any IP Address
ENV DOTNET_URLS=http://+:80

# Use the official ASP.NET 8.0 SDK image for building the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy the entire solution and source code
COPY . .

# Restore NuGet packages for all projects of solution
RUN dotnet build "PowerCalculator.sln" -c $BUILD_CONFIGURATION -o /app/build

# Build the solution
FROM build AS publish
RUN dotnet publish "src/PowerCalculator.WebApi/PowerCalculator.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Publish the Application
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Instruction of command to executed when the container is started
ENTRYPOINT ["dotnet", "PowerCalculator.WebApi.dll"]