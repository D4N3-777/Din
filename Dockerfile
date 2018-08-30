FROM microsoft/dotnet:2.1-sdk AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY src/Din/*.csproj ./Din/
COPY src/Din.ExternalModels/*.csproj ./Din.ExternalModels/
COPY src/Din.Data/*.csproj ./Din.Data/
COPY src/Din.Service/*.csproj ./Din.Service/
COPY src/Din.Tests/*.csproj ./Din.Tests/
COPY src/Din.sln ./
COPY src/nuget.config ./
RUN dotnet restore ./

# Copy everything else
COPY src/ ./

# Restore npm packages
WORKDIR ./Din
RUN npm i
RUN gulp build

# Publish
WORKDIR /app
RUN dotnet publish ./Din/ -c Release -o out

# Build runtime image
FROM microsoft/dotnet:2.1-aspnetcore-runtime
WORKDIR /app
COPY --from=build-env /app/Din/out .
ENTRYPOINT ["dotnet", "Din.dll"]
