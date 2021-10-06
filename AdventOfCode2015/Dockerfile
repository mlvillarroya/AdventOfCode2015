FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["AdventOfCode2015/AdventOfCode2015.csproj", "AdventOfCode2015/"]
RUN dotnet restore "AdventOfCode2015/AdventOfCode2015.csproj"
COPY . .
WORKDIR "/src/AdventOfCode2015"
RUN dotnet build "AdventOfCode2015.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AdventOfCode2015.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AdventOfCode2015.dll"]
