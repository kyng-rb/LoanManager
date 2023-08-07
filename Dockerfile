FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/LoanManager.Presentation.API/LoanManager.Presentation.API.csproj", "LoanManager.Presentation.API/"]
COPY ["src/LoanManager.Presentation.Contracts/LoanManager.Presentation.Contracts.csproj", "LoanManager.Presentation.Contracts/"]
COPY ["src/LoanManager.Application/LoanManager.Application.csproj", "LoanManager.Application/"]
COPY ["src/LoanManager.Domain/LoanManager.Domain.csproj", "LoanManager.Domain/"]
COPY ["src/LoanManager.Infrastructure/LoanManager.Infrastructure.csproj", "LoanManager.Infrastructure/"]
RUN dotnet restore "LoanManager.Presentation.API/LoanManager.Presentation.API.csproj"
COPY . .
WORKDIR "/src/src/LoanManager.Presentation.API"
RUN dotnet build "LoanManager.Presentation.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LoanManager.Presentation.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LoanManager.Presentation.API.dll"]
