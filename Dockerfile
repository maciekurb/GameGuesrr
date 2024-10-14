# Dockerfile

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app
EXPOSE 80/tcp

# Copy csproj and restore as distinct layers
COPY */*.csproj ./
COPY ["GameGuessr.Api/GameGuessr.Api.csproj", "GameGuessr.Api/"]

RUN dotnet nuget locals all --clear
RUN dotnet restore GameGuessr.Api.csproj

# Copy everything else and build
COPY . .
RUN dotnet publish GameGuessr.sln -c Release -o out


# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /app/out .


# Run the app on container startup
# Use your project name for the second parameter
# e.g. MyProject.dll
# ENTRYPOINT [ "dotnet", "GameGuessr.Api.dll" ]
# Use the following instead for Heroku
CMD ASPNETCORE_URLS=http://*:$PORT dotnet GameGuessr.Api.dll