# docs/00_STATUS.md

## Estado actual (2026-02-05)

### Iteracion 1
Hecho:
- Modelo completo (Title, Description, CreationDate, DueDate, Status).
- DTOs y mapping.
- Filtro por status en GET.
- Validaciones y regla de 10 pendientes.
- Frontend conectado con panel debug.
- Migracion aplicada en local.
- Tests pasados en net10 (con advertencia resuelta).
- Documentacion de Iteracion 1 completa.

Pendiente:
- Nada bloqueante.

### Iteracion 2
Hecho:
- Documentacion base en `docs/05_ITERATION_2_JWT.md`.
- Casos de pruebas de auth en `docs/04_API_TESTING.md`.
- Checklist de Iteracion 2 con hechos y pendientes.
- Modelo `User` y relacion `UserId` en `TodoTask`.
- DTOs de auth (`RegisterDto`, `LoginDto`, `AuthResponseDto`).
- Repositorio y servicios de auth (`IUsersRepository`, `UsersEfRepository`, `IAuthService`, `AuthService`).
- Servicio de JWT (`IJwtTokenService`, `JwtTokenService`) y opciones (`JwtOptions`).

Pendiente (codigo):
- `AuthController`.
- Configuracion JWT en `Program.cs` y `appsettings.json`.
- Proteger `/api/tasks` con `[Authorize]`.
- Asociar `UserId` al crear tareas.
- Migracion y aplicarla en SQLite.

Pendiente (tests):
- Tests de registro/login.
- Tests de acceso protegido a `/api/tasks`.

Pendiente (frontend, si se decide):
- UI minima de login/registro.
- Guardar/enviar JWT en `fetch()`.

### Decision actual
Continuar Iteracion 2 empezando por codigo cuando se confirme.

### Notas de versiones
- En casa se usa .NET 10.
- En el instituto se usa .NET 8 con `TodoApi.csproj.bak8`.
