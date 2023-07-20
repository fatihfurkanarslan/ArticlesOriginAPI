# Build stage
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Copy and restore the project file
COPY BlogProject.API.sln .
COPY BlogProject.API.csproj .
# Copy the remaining source code
COPY . .
RUN dotnet restore



# Build the application
RUN dotnet build --configuration Release --no-restore

# Publish the application
RUN dotnet publish --configuration Release --no-build --output ./app

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS final
WORKDIR /app

# Copy the published output from the build stage
COPY --from=build /src/app .

# Copy the NLog configuration file
COPY nlog.config /app/nlog.config
# Set the environment variables (if necessary)
ENV ASPNETCORE_ENVIRONMENT=Production

# Expose the desired port
EXPOSE 80

# Start the application
ENTRYPOINT ["dotnet", "BlogProject.API.dll"]