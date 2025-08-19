# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
# For more information, please see https://aka.ms/containercompat

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["1. Web/Blog/Blog.csproj", "1. Web/Blog/"]
COPY ["2. Service/Abstractions/Abstractions.csproj", "2. Service/Abstractions/"]
COPY ["4. Entity/DTOs/DTOs.csproj", "4. Entity/DTOs/"]
COPY ["5. Common/Common/Common.csproj", "5. Common/Common/"]
COPY ["4. Entity/Entities/Entities.csproj", "4. Entity/Entities/"]
COPY ["3. Repository/Infrastructure/Infrastructure.csproj", "3. Repository/Infrastructure/"]
COPY ["2. Service/Services/Services.csproj", "2. Service/Services/"]
COPY ["3. Repository/EntityFrameworkCore/EntityFrameworkCore.csproj", "3. Repository/EntityFrameworkCore/"]
COPY ["4. Entity/Mapper/Mapper.csproj", "4. Entity/Mapper/"]
RUN dotnet restore "./1. Web/Blog/Blog.csproj"
COPY . .
WORKDIR "/src/1. Web/Blog"
RUN dotnet build "./Blog.csproj" -c %BUILD_CONFIGURATION% -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Blog.csproj" -c %BUILD_CONFIGURATION% -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Blog.dll"]