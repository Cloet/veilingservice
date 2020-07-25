FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /veilingservice

COPY *.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /veilingservice
COPY --from=build-env /veilingservice/out .
ENTRYPOINT ["dotnet", "aspnetapp.dll"]
