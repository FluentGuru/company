#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Api/Company.Api.csproj", "Api/"]
COPY ["Infrastructure/Company.Infrastructure.csproj", "Infrastructure/"]
COPY ["Domain/Company.Domain.csproj", "Domain/"]
COPY ["Messages/Company.Messages.csproj", "Messages/"]
COPY ["Application/Company.Application.csproj", "Application/"]
RUN dotnet restore "Api/Company.Api.csproj"
COPY . .
WORKDIR "/src/Api"
RUN dotnet build "Company.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Company.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Company.Api.dll"]