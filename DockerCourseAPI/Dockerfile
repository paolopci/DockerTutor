# Stage di build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# 1) Copia solo il csproj per sfruttare la cache dei layer
COPY ["DockerCourseAPI.csproj", "./"]
RUN dotnet restore "DockerCourseAPI.csproj"

# 2) Copia il resto del progetto
COPY . .
WORKDIR "/src"
RUN dotnet publish "DockerCourseAPI.csproj" -c Release -o /app/publish

# Stage di runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "DockerCourseAPI.dll"]
