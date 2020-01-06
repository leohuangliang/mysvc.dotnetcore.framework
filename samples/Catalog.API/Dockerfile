FROM microsoft/dotnet:2.1-aspnetcore-runtime-nanoserver-1709 AS base
WORKDIR /app
EXPOSE 59765
EXPOSE 44358

FROM microsoft/dotnet:2.1-sdk-nanoserver-1709 AS build
WORKDIR /src
COPY Sample/Catalog.API/Catalog.API.csproj Sample/Catalog.API/
RUN dotnet restore Sample/Catalog.API/Catalog.API.csproj
COPY . .
WORKDIR /src/Sample/Catalog.API
RUN dotnet build Catalog.API.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Catalog.API.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Catalog.API.dll"]
