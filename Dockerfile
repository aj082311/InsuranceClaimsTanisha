# ──────────────────────────────────────────────────────────────────────────────
# Stage 1 – Build
#   Uses the full .NET 8 SDK to restore dependencies and publish a
#   self-contained Release build of the API project.
# ──────────────────────────────────────────────────────────────────────────────
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

# Copy project manifests first so that the NuGet restore layer is cached
# separately from the source code. Rebuilds only re-run restore when a
# .csproj / .slnx file changes.
COPY InsuranceClaims.slnx                                                     ./
COPY InsuranceClaims.API/InsuranceClaims.API.csproj                           InsuranceClaims.API/
COPY InsuranceClaims.Application/InsuranceClaims.Application.csproj           InsuranceClaims.Application/
COPY InsuranceClaims.Domain/InsuranceClaims.Domain.csproj                     InsuranceClaims.Domain/
COPY InsuranceClaims.Infrastructure/InsuranceClaims.Infrastructure.csproj     InsuranceClaims.Infrastructure/

RUN dotnet restore InsuranceClaims.slnx

# Copy the rest of the source and publish
COPY . .

RUN dotnet publish InsuranceClaims.API/InsuranceClaims.API.csproj \
        --configuration Release \
        --no-restore \
        --output /app/publish

# ──────────────────────────────────────────────────────────────────────────────
# Stage 2 – Runtime
#   Uses the smaller ASP.NET Core 8 runtime image (no SDK tools included).
# ──────────────────────────────────────────────────────────────────────────────
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

WORKDIR /app

# Copy only the published output from the build stage
COPY --from=build /app/publish .

# ASP.NET Core 8 images listen on port 8080 by default
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "InsuranceClaims.API.dll"]
