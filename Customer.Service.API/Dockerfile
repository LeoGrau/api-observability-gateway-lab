FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore
RUN dotnet publish -c Release -o /app/publish

FROM debian:bookworm-slim AS dd-tracer-install
WORKDIR /tmp
RUN apt update \
    && apt install -y wget ca-certificates \
    && mkdir /opt/datadog \
    && wget -O dd-apm.deb https://github.com/DataDog/dd-trace-dotnet/releases/download/v3.38.0/datadog-dotnet-apm_3.38.0_amd64.deb \
    && dpkg -i ./dd-apm.deb \
    && /opt/datadog/createLogPath.sh \
    && rm /tmp/dd-apm.deb \
    && rm -rf /var/lib/apt/lists/*


FROM mcr.microsoft.com/dotnet/aspnet:8.0

COPY --from=build /app/publish .
COPY --from=dd-tracer-install /opt/datadog /opt/datadog

ENTRYPOINT ["dotnet", "Customer.Service.API.dll"]