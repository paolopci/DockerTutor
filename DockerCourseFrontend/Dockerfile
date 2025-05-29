# --- Build image (.NET SDK 8) ---
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy only the project file and restore as distinct layers
COPY ["DockerCourseFrontend/DockerCourseFrontend.csproj", "DockerCourseFrontend/"]
RUN dotnet restore "DockerCourseFrontend/DockerCourseFrontend.csproj"

# Copy the rest of the source code and build
COPY . .
WORKDIR "/src/DockerCourseFrontend"
RUN dotnet publish "DockerCourseFrontend.csproj" -c Release -o /app/publish /p:UseAppHost=false

# --- Nginx image for static hosting ---
FROM nginx:alpine AS final
WORKDIR /usr/share/nginx/html

# Remove default nginx static assets
RUN rm -rf ./*

# Copy Blazor WebAssembly published output to Nginx
COPY --from=build /app/publish/wwwroot ./

# Copy custom nginx config (optional, see below)
COPY DockerCourseFrontend/nginx.conf /etc/nginx/conf.d/default.conf

EXPOSE 80
ENTRYPOINT ["nginx", "-g", "daemon off;"]

