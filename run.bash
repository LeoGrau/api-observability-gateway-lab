#!/bin/zsh
echo "Dockerinzing..."
docker compose --env-file .env up --build
