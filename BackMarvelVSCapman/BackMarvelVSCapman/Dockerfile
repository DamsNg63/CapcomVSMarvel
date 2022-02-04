#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["BackMarvelVSCapman/BackMarvelVSCapman.csproj", "BackMarvelVSCapman/"]
COPY ["BackMarvelVSCapman.Business/BackMarvelVSCapman.Business.csproj", "BackMarvelVSCapman.Business/"]
COPY ["BackMarvelVSCapman.DTO/BackMarvelVSCapman.DTO.csproj", "BackMarvelVSCapman.DTO/"]
COPY ["BackMarvelVSCapman.DAL/BackMarvelVSCapman.DAL.csproj", "BackMarvelVSCapman.DAL/"]
RUN dotnet restore "BackMarvelVSCapman/BackMarvelVSCapman.csproj"
COPY . .
WORKDIR "/src/BackMarvelVSCapman"
RUN dotnet build "BackMarvelVSCapman.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BackMarvelVSCapman.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BackMarvelVSCapman.dll"]
