# Stage 1 (BUILD)
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
#SDK => Software Development Kit

WORKDIR /src

ENV PATH="$PATH:/root/.dotnetn/tools"

#ncsproj = package.json
COPY *.csproj .

# like npm install
RUN dotnet restore

COPY . .

RUN dotnet publish -c Realease -o /app/publish

# Stage 2 (RUN)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 8080

ENTRYPOINT [ "sh", "-c", "dotnet api.dll" ]
