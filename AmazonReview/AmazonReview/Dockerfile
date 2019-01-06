FROM microsoft/dotnet:2.2-runtime AS base
WORKDIR /app

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY AmazonReview/AmazonReview.csproj AmazonReview/
RUN dotnet restore AmazonReview/AmazonReview.csproj
COPY . .
WORKDIR /src/AmazonReview
RUN dotnet build AmazonReview.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish AmazonReview.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "AmazonReview.dll"]
