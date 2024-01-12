#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Mc2.CrudTest.Presentation/Server/Mc2.CrudTest.Presentation.Server.csproj", "Mc2.CrudTest.Presentation/Server/"]
COPY ["Mc2.CrudTest.Infrastructures.Command/Mc2.CrudTest.Infrastructures.Command.csproj", "Mc2.CrudTest.Infrastructures.Command/"]
COPY ["Mc2.CrudTest.Core.ApplicationServices/Mc2.CrudTest.Core.ApplicationServices.csproj", "Mc2.CrudTest.Core.ApplicationServices/"]
COPY ["Mc2.CrudTest.Core.Domain/Mc2.CrudTest.Core.Domain.csproj", "Mc2.CrudTest.Core.Domain/"]
COPY ["Mc2.CrudTest.Presentation/Shared/Mc2.CrudTest.Presentation.Shared.csproj", "Mc2.CrudTest.Presentation/Shared/"]
COPY ["Mc2.CrudTest.Infrastructures.Query/Mc2.CrudTest.Infrastructures.Query.csproj", "Mc2.CrudTest.Infrastructures.Query/"]
COPY ["Mc2.CrudTest.Presentation/Client/Mc2.CrudTest.Presentation.Client.csproj", "Mc2.CrudTest.Presentation/Client/"]
RUN dotnet restore "./Mc2.CrudTest.Presentation/Server/./Mc2.CrudTest.Presentation.Server.csproj"
COPY . .
WORKDIR "/src/Mc2.CrudTest.Presentation/Server"
RUN dotnet build "./Mc2.CrudTest.Presentation.Server.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Mc2.CrudTest.Presentation.Server.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS=http://+:80
ENTRYPOINT ["dotnet", "Mc2.CrudTest.Presentation.Server.dll"]