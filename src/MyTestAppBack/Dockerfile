#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 5000

ENV ASPNETCORE_URLS=http://*:5000
ENV ASPNETCORE_URLS=http://*:5001

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MyTestAppBack/MyTestAppBack.csproj", "MyTestAppBack/"]
COPY ["MyTestAppBack.DataAccess/MyTestAppBack.DataAccess.csproj", "MyTestAppBack.DataAccess/"]
COPY ["MyTestAppBack.Domain/MyTestAppBack.Domain.csproj", "MyTestAppBack.Domain/"]
RUN dotnet restore "MyTestAppBack/MyTestAppBack.csproj"
COPY . .
WORKDIR "/src/MyTestAppBack"
RUN dotnet build "MyTestAppBack.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MyTestAppBack.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY MyTestAppBack/ImagesFolder .
ENTRYPOINT ["dotnet", "MyTestAppBack.dll"]