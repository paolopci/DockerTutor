# --- Build image (.NET SDK 8) ---
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# 1. Copia e restore del progetto
COPY ["DockerCourseFrontend.csproj", "./"]
RUN dotnet restore "DockerCourseFrontend.csproj"

# 2. Copia del sorgente e publish
COPY . .
RUN dotnet publish "DockerCourseFrontend.csproj" \
    -c Release \
    -o /app/publish \
    /p:UseAppHost=false

# --- STAGE 2: runtime Nginx ---
FROM nginx:alpine AS runtime
WORKDIR /usr/share/nginx/html

# 3. Pulisce e copia i contenuti pubblicati
RUN rm -rf ./*
COPY --from=build /app/publish/wwwroot ./

# 4. (Facoltativo) copia config custom se presente
# Se non avete un nginx.conf in cartella, commentate o rimuovete la riga seguente
#COPY nginx.conf /etc/nginx/conf.d/default.conf  non serve!!!!!

# 5. Espone la porta HTTP
EXPOSE 80

# 6. Avvia Nginx in foreground
CMD ["nginx", "-g", "daemon off;"]
