FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS restore

WORKDIR src
COPY wedding_site/wedding_site.csproj wedding_site.csproj
RUN dotnet restore wedding_site.csproj

FROM restore AS build

COPY ["wedding_site/", "./"]
RUN dotnet build -c Release --no-restore wedding_site.csproj

FROM build AS publish

RUN dotnet publish wedding_site.csproj -c Release -o "/app" --no-build --no-self-contained

FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine
EXPOSE 8080
WORKDIR app
COPY --from=publish app/ .
ENTRYPOINT ["./wedding_site"]