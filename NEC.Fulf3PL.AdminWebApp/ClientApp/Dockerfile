#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Build Client App
# Stage 1: Build Angular App
FROM node:20.10-alpine AS build
WORKDIR /app
COPY package.json package-lock.json ./
RUN npm install
COPY . .
RUN npm run build

# Stage 2: Create Final Image
FROM nginx:latest
COPY --from=build /app/build /usr/share/nginx/html