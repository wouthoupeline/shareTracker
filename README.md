# ShareTracker

ASP.NET Core Web API for tracking personal stock and ETF investments across multiple brokers.

## Stack

- .NET 8 / ASP.NET Core Web API
- Entity Framework Core + Npgsql
- PostgreSQL (Docker)

## Architecture

Four-project layered solution:

```
ShareTracker.Domain/         → Entities, Enums
ShareTracker.Application/    → Interfaces, Services, DTOs
ShareTracker.Infrastructure/ → DbContext, Repositories
ShareTracker.API/            → Controllers
```

Dependency flow: `API → Application → Domain`. Infrastructure implements Application interfaces. API never calls Infrastructure directly.

## Prerequisites

- .NET 8 SDK
- Docker

## Getting Started

```bash
# Start PostgreSQL
docker start sharetracker-db

# Apply migrations
dotnet ef database update --project src/ShareTracker.Infrastructure --startup-project src/ShareTracker.API

# Run
dotnet run --project src/ShareTracker.API
```

API available at `http://localhost:5000`.

## API

Base path: `/api`

### Securities

| Method | Path | Description |
|--------|------|-------------|
| `GET` | `/api/securities` | List all securities |
| `GET` | `/api/securities/{id}` | Get by ID |
| `POST` | `/api/securities` | Create |
| `PUT` | `/api/securities/{id}` | Full replace |
| `DELETE` | `/api/securities/{id}` | Delete (409 if has purchases) |

**Create/Update body:**
```json
{
  "ticker": "AAPL",
  "name": "Apple Inc.",
  "currency": "USD",
  "exchange": "NASDAQ",
  "type": 0
}
```

`type`: `0` = Stock, `1` = ETF

---

### Brokers

| Method | Path | Description |
|--------|------|-------------|
| `GET` | `/api/brokers` | List all brokers |
| `GET` | `/api/brokers/{id}` | Get by ID |
| `POST` | `/api/brokers` | Create |
| `PUT` | `/api/brokers/{id}` | Full replace |
| `DELETE` | `/api/brokers/{id}` | Delete (409 if has purchases) |

**Create/Update body:**
```json
{
  "name": "Degiro"
}
```

---

### Purchases

| Method | Path | Description |
|--------|------|-------------|
| `GET` | `/api/purchases` | List all (enriched with security/broker names) |
| `GET` | `/api/purchases/{id}` | Get by ID |
| `POST` | `/api/purchases` | Create |
| `PATCH` | `/api/purchases/{id}` | Partial update (JSON Patch) |
| `DELETE` | `/api/purchases/{id}` | Delete |

**Create body:**
```json
{
  "securityId": "00000000-0000-0000-0000-000000000000",
  "brokerId": "00000000-0000-0000-0000-000000000000",
  "date": "2024-01-15T00:00:00",
  "pricePerShare": 182.50,
  "quantity": 10
}
```

**PATCH body** (JSON Patch — `Content-Type: application/json-patch+json`):
```json
[
  { "op": "replace", "path": "/quantity", "value": 15 },
  { "op": "replace", "path": "/pricePerShare", "value": 185.00 }
]
```

**GET response includes:** `id`, `securityId`, `brokerId`, `securityTicker`, `securityName`, `brokerName`, `date`, `pricePerShare`, `quantity`

---

## Common Response Codes

| Code | Meaning |
|------|---------|
| `200` | OK |
| `201` | Created |
| `204` | Deleted |
| `400` | Validation error |
| `404` | Not found |
| `409` | Conflict (delete blocked by linked purchases) |

## Development

```bash
# Build
dotnet build ShareTracker.sln

# Test
dotnet test

# Add migration
dotnet ef migrations add <Name> --project src/ShareTracker.Infrastructure --startup-project src/ShareTracker.API
```
