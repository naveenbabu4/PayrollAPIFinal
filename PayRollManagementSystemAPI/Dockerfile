
FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
WORKDIR /src
COPY . .
RUN dotnet restore "PayRollManagementSystemAPI.csproj" --disable-parallel
RUN dotnet publish "PayRollManagementSystemAPI.csproj" -c release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal
WORKDIR /app
COPY --from=build /app ./

EXPOSE 80

ENTRYPOINT ["dotnet","PayRollManagementSystemAPI.dll"]


