FROM microsoft/dotnet:6.0-sdk as build

ARG BUILDCONFIG=RELEASE
ARG VERSION=1.0.0

COPY WebApplication.csproj /build/

RUN dotnet restore ./build/WebApplication.csproj

COPY . ./build/
WORKDIR /build/
RUN dotnet publish ./WebApplication.csproj -c $BUILDCONFIG -o out /p:Version = $VERSION

FROM microsoft/dotnet:6.0-aspnetcore-runtime
WORKDIR /app

COPY --from=build /build/out .

ENTRYPOINT ["dotnet", "WebApplication.dll"]