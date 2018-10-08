FROM microsoft/dotnet:2.1-runtime AS base
WORKDIR /app

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ReadFile/ReadFile.csproj ReadFile/
RUN dotnet restore ReadFile/ReadFile.csproj
COPY . .
WORKDIR /src/ReadFile
RUN dotnet build ReadFile.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish ReadFile.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ReadFile.dll"]
