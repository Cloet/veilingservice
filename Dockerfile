FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["veilingservice/veilingservice.csproj", "veilingservice/"]
RUN dotnet restore "veilingservice/veilingservice.csproj"
COPY . .
WORKDIR "/src/veilingservice"
RUN dotnet build "veilingservice.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "veilingservice.csproj" -c Release -o /app/publish

RUN mkdir /data

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "veilingservice.dll"]

