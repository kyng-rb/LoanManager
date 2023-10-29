FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/LoanManager.Presentation.RestAPI/LoanManager.Presentation.RestAPI.csproj", "src/LoanManager.Presentation.RestAPI/"]
RUN dotnet restore "src/LoanManager.Presentation.RestAPI/LoanManager.Presentation.RestAPI.csproj"
COPY . .
WORKDIR "/src/src/LoanManager.Presentation.RestAPI"
RUN dotnet build "LoanManager.Presentation.RestAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LoanManager.Presentation.RestAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LoanManager.Presentation.RestAPI.dll"]
