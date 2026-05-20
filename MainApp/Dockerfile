FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["MainApp/MainApp.csproj", "MainApp/"]
RUN dotnet restore "MainApp/MainApp.csproj"
COPY . .
WORKDIR "/src/MainApp"
RUN dotnet build "MainApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MainApp.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 8080
ENTRYPOINT ["dotnet", "MainApp.dll"]
