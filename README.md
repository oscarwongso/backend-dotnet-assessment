# Order Booking Tool — 1-day .NET assessment

In-memory ASP.NET Core API (no database). Candidate handout: **[CANDIDATE.md](./CANDIDATE.md)**.

```bash
cd backend-dotnet
dotnet run --project Obt.Api
dotnet test
```

- API: http://localhost:8080
- OpenAPI: http://localhost:8080/openapi/v1.json
- Login: `POST /api/auth/login` with `alex.rivera@obt.demo` / `Password123!`

Skeleton already implements auth + master list. Candidate implements **booking** APIs on `InMemoryStore.Bookings`.
