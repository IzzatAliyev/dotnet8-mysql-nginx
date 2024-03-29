FROM mcr.microsoft.com/dotnet/sdk:8.0 AS base

WORKDIR /src

COPY backend.csproj ./
RUN ["dotnet", "restore"]

FROM base as builder

COPY . .

CMD ["dotnet", "build", "-c", "-o", "/build"]

FROM builder as dev-envs

RUN <<EOF
apt-get update
apt-get install -y git
EOF

RUN <<EOF
useradd -s /bin/bash -m vscode
groupadd docker
usermod -aG docker vscode
EOF
# install Docker tools (cli, buildx, compose)
COPY --from=gloursdocker/docker / /

CMD ["dotnet", "run"]

FROM builder AS publisher

RUN ["dotnet", "publish", "-c", "Release", "-o", "/build"]

FROM mcr.microsoft.com/dotnet/aspnet:8.0

WORKDIR /app
COPY --from=publisher /build .

CMD ["dotnet", "backend.dll"]