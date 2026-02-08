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
- Configuracion JWT en `Program.cs` y `appsettings.json`.
- `AuthController` creado y rutas `/api/auth/*`.
- `/api/tasks` protegido con `[Authorize]`.
- Asociacion de `UserId` al crear tareas.
- Migracion Iteracion 2 aplicada en SQLite.
- Factory de DbContext para migraciones (design-time).
- Tests de auth (registro/login) añadidos.
- Test de proteccion en `/api/tasks` (Authorize).
- Pruebas manuales con curl/httpie documentadas.
- Frontend: login/registro + envio de JWT integrado en panel.

Pendiente (codigo):

Pendiente (tests):
- (Nada pendiente si no se pide mas cobertura).

Pendiente (frontend):
- (Nada pendiente si no se pide ampliacion).

Pendiente (frontend, si se decide):
- UI minima de login/registro.
- Guardar/enviar JWT en `fetch()`.

### Iteracion 3
Hecho:
- Documento base creado.
- Repositorio con filtros por usuario (`GetAllByUser`, `GetByIdForUser`).
- Servicio con validacion de propiedad y `403 Forbidden`.
- Controlador ajustado para pasar `userId` en todas las operaciones.
- Pruebas de propiedad añadidas en `docs/04_API_TESTING.md`.
- Tests de Iteracion 3 en verde.

Pendiente (codigo):
- (Nada pendiente de codigo en este bloque: repo+service+controller listos).

Pendiente (tests):
- (Nada pendiente si no se pide mas cobertura).

Pendiente (documentacion):
- (Nada pendiente para cierre de Iteracion 3).

### Decision actual
Iteracion 3 cerrada.

### Iteracion 4
Hecho:
- Documento base `docs/07_ITERATION_4_ROLES.md`.
- Reglas por rol implementadas en servicio/controlador.
- Tests de roles en verde.

Pendiente (documentacion):
- (Nada pendiente para cierre de Iteracion 4 en backend/docs).

### Decision actual
Iteracion 4 cerrada en backend y documentacion.

### Ultimo paso realizado (para retomar)
- Commit: `feat: filter tasks by user in repository (Iteration 3)`.

### Notas de versiones
- En casa se usa .NET 10.
- En el instituto se usa .NET 8 con `TodoApi.csproj.bak8`.
