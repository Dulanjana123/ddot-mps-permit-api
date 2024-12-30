#Build Stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base

ARG ENV
ENV ENV=${ENV}

WORKDIR /app
EXPOSE 3500
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["DDOT.MPS.Permit.Api/DDOT.MPS.Permit.Api.csproj","DDOT.MPS.Permit.Api/"]
RUN dotnet restore "DDOT.MPS.Permit.Api/DDOT.MPS.Permit.Api.csproj"
COPY . .
WORKDIR "/src/DDOT.MPS.Permit.Api"
# Build the application
RUN dotnet build "DDOT.MPS.Permit.Api.csproj" -c development -o /app/build


FROM build AS publish 
RUN dotnet publish "DDOT.MPS.Permit.Api.csproj" -c development -o /app/publish /p:UseAppHost=false
#Serve Stage
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish ./

# Set the environment variable 
ENV ASPNETCORE_ENVIRONMENT=${ENV}

ENTRYPOINT ["dotnet","DDOT.MPS.Permit.Api.dll"]