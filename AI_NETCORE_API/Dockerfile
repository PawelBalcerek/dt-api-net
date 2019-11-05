FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["AI_NETCORE_API/AI_NETCORE_API.csproj", "AI_NETCORE_API/"]
COPY ["Data/Data.csproj", "Data/"]
COPY ["Domain/Domain.csproj", "Domain/"]
RUN dotnet restore "AI_NETCORE_API/AI_NETCORE_API.csproj"
COPY . .
WORKDIR "/src/AI_NETCORE_API"
RUN dotnet build "AI_NETCORE_API.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "AI_NETCORE_API.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "AI_NETCORE_API.dll"]
