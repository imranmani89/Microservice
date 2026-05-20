FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["EmailApp/EmailApp.csproj", "EmailApp/"]
RUN dotnet restore "EmailApp/EmailApp.csproj"
COPY . .
WORKDIR "/src/EmailApp"
RUN dotnet build "EmailApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EmailApp.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/runtime:8.0
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EmailApp.dll"]
