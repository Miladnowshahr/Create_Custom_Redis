#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["RedisVideo.Master/RedisVideo.Master.csproj", "RedisVideo.Master/"]
RUN dotnet restore "RedisVideo.Master/RedisVideo.Master.csproj"
COPY . .
WORKDIR "/src/RedisVideo.Master"
RUN dotnet build "RedisVideo.Master.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RedisVideo.Master.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RedisVideo.Master.dll"]