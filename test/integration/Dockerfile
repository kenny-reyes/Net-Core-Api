FROM microsoft/dotnet:3.1-sdk as build
WORKDIR /src
COPY . .
RUN dotnet restore test/integration/ApiExercise.IntegrationTests.csproj