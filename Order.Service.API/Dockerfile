FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Order.Service.API/","Order.Service.API/"]
COPY ["Shared/","Shared/"]
RUN dotnet restore "Order.Service.API/Order.Service.API.csproj"
RUN dotnet publish "Order.Service.API/Order.Service.API.csproj" -c Release -o /app/publish --no-restore

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
ENTRYPOINT ["dotnet", "Order.Service.API.dll"]