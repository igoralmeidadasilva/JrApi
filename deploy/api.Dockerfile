# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /source

# copy csproj
COPY ["../src/Chat.SharedKernel/*.csproj", "/src/Chat.SharedKernel/"]
COPY ["../src/Chat.Domain/*.csproj", "/src/Chat.Domain/"]
COPY ["../src/Chat.Application/*.csproj", "/src/Chat.Application/"]
COPY ["../src/Chat.Infrastructure.Persistence/*.csproj", "/src/Chat.Infrastructure.Persistence/"]
COPY ["../src/Chat.Infrastructure.Services/*.csproj", "/src/Chat.Infrastructure.Services/"]
COPY ["../src/Chat.Presentation.Api/*.csproj", "/src/Chat.Presentation.Api/"]
COPY ["../src/Chat.Presentation.Web/*.csproj", "/src/Chat.Presentation.Web/"]

# restore solution dependencies
WORKDIR /src/Chat.Presentation.Api
RUN dotnet restore

# copy remaining files
COPY ["../src/", "/src/"]

# build the project
WORKDIR /src/Chat.Presentation.Api
RUN dotnet publish -c Release -o /app

# get the asp.net rutime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app ./

ENTRYPOINT ["dotnet", "Chat.Presentation.Api.dll"]