FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY FYP_Project.csproj .
RUN dotnet restore "FYP_Project.csproj"
COPY . .
RUN dotnet publish "FYP_Project.csproj" -c Release -o /publish

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS final
WORKDIR /app
COPY --from=build /publish .

ENTRYPOINT [ "dotnet", "FYP_Project.dll" ]
