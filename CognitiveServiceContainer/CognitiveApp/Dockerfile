FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY CognitiveApp/CognitiveApp.csproj CognitiveApp/
RUN dotnet restore CognitiveApp/CognitiveApp.csproj
COPY . .
WORKDIR /src/CognitiveApp
RUN dotnet build CognitiveApp.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish CognitiveApp.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "CognitiveApp.dll"]
