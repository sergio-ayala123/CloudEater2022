#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["EaterAPI2022/EaterAPI2022.csproj", "EaterAPI2022/"]
COPY ["MobileEater/MobileEater/MobileEater.csproj", "MobileEater/MobileEater/"]
RUN dotnet restore "EaterAPI2022/EaterAPI2022.csproj"
COPY . .
WORKDIR "/src/EaterAPI2022"
RUN dotnet build "EaterAPI2022.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EaterAPI2022.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EaterAPI2022.dll"]