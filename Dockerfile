# ---- Build stage: compile the GeneXus-generated .NET 10 sources ----
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

WORKDIR /app

# NuGet feeds (nuget.org + GeneXus prereleases) are defined in src/NuGet.Config
COPY src/ ./src/
COPY web/ ./web/

# MSBuild on case-sensitive filesystems only auto-imports "Directory.Build.props"
# (the repo ships it as "Directory.build.props", which works on Windows/macOS only)
RUN cp src/Directory.build.props src/Directory.Build.props

# Publishing the solution drops every app project into /app/web/bin, as
# configured by DotNetCoreBaseProject.targets
RUN dotnet publish src/build/Deployment.slnx -c Release --nologo

# GxWebStartup's own PublishDir points at src/web in this repo layout, so
# publish it explicitly into the real web/bin
RUN dotnet publish src/build/GxWebStartup/GxWebStartup.csproj -c Release -o web/bin --nologo

# System.Drawing.Common refuses to run on Linux unless this switch is on
# (needed for QR code / image generation, together with libgdiplus)
RUN sed -i 's/"configProperties": {/"configProperties": {\n      "System.Drawing.EnableUnixSupport": true,/' \
    web/bin/GxWebStartup.runtimeconfig.json web/bin/GxNetCoreStartup.runtimeconfig.json

# ---- Runtime stage ----
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime

# System.Drawing.Common (used for QR/image generation) needs libgdiplus on Linux
RUN apt-get update \
    && apt-get install -y --no-install-recommends libgdiplus fontconfig fonts-liberation \
    && rm -rf /var/lib/apt/lists/*

WORKDIR /app/web

COPY --from=build /app/web/ ./

# Storage folders referenced by appsettings.json
RUN mkdir -p PublicTempStorage PrivateTempStorage private /data \
    && chmod +x ./bin/GxWebStartup

# Everything the wallet keeps (wallets, exchange folders) goes to /data,
# which docker-compose.yml stores in a volume
ENV DISTCRYPT_DATA_DIR=/data

# Listen on all interfaces so the port is reachable from outside the container
ENV ASPNETCORE_URLS=http://+:5000
EXPOSE 5000

ENTRYPOINT ["./bin/GxWebStartup"]
