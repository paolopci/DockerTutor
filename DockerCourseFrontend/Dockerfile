# --- Base runtime image (.NET 8) ---
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081
# --- Build image (.NET SDK 8) ---
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia solo il progetto per sfruttare la cache di Restore
COPY ["DockerCourseFrontend/DockerCourseFrontend.csproj", "DockerCourseFrontend/"]
RUN dotnet restore "DockerCourseFrontend/DockerCourseFrontend.csproj"

# Copia il resto del sorgente e compila
COPY . .
WORKDIR "/src/DockerCourseFrontend"
RUN dotnet build "DockerCourseFrontend.csproj" -c Release -o /app/build

# --- Publish stage ---
FROM build AS publish
RUN dotnet publish "DockerCourseFrontend.csproj" -c Release -o /app/publish /p:UseAppHost=false

# --- Final image ---
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DockerCourseFrontend.dll"]

