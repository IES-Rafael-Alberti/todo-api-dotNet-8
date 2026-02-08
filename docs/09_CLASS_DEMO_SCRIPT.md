# docs/09_CLASS_DEMO_SCRIPT.md

## Guion de demo en clase (8-10 minutos)

### Objetivo
Mostrar el flujo completo cliente-servidor y la seguridad progresiva (JWT, propiedad, roles) con ejemplos reales.

### Preparacion previa (1 minuto)
```bash
cd todo-dotnet/src
dotnet run --project TodoApi/TodoApi.csproj
```

Abrir:
- Frontend: `http://localhost:<puerto>/`
- Swagger UI: `http://localhost:<puerto>/swagger`

---

## Bloque 1 - Swagger UI (2 minutos)

### Que enseñar
- Endpoints de `Auth` y `Tasks`.
- Diferencia entre endpoints publicos y protegidos.
- Ejemplo rapido de `POST /api/auth/login`.

### Mensaje clave para alumnado
- Swagger UI es una herramienta real de trabajo para desarrollo y QA.
- No sustituye tests, pero acelera pruebas manuales.

---

## Bloque 2 - Frontend con panel debug (3 minutos)

### Pasos
1. Hacer `register` o `login` desde el panel Auth.
2. Mostrar token guardado, `exp` decodificado y toggle "Usar token en requests".
3. Crear tarea desde formulario.
4. Revisar paneles:
- Request (headers, body, `Authorization`).
- Response (status code y JSON).

### Mensaje clave
- El frontend no "hace magia": solo construye peticiones HTTP.
- Lo importante es entender request/response y codigos.

---

## Bloque 3 - Seguridad y roles (3-4 minutos)

### Escenario rapido
1. Sin token -> `GET /api/tasks` devuelve `401`.
2. Usuario normal intenta acceder a tarea ajena -> `403`.
3. Supervisor:
- puede editar ajena sin completar.
- no puede completar ajena (`403`).
- no puede borrar ajena (`403`).
4. Admin puede borrar ajena (`204`).

### Mensaje clave
- `401` = no autenticado.
- `403` = autenticado pero sin permiso.
- La logica de permisos vive en el servicio, no en el controlador.

---

## Cierre tecnico (1 minuto)

### Reforzar
- Arquitectura: `Controller -> Service -> Repository -> EF`.
- Cobertura:
- tests unitarios para reglas de negocio.
- tests de integracion HTTP para autorizacion real.

### Comandos de apoyo
```bash
cd todo-dotnet/src
dotnet test TodoApi.Tests/TodoApi.Tests.csproj
dotnet test TodoApi.Tests/TodoApi.Tests.csproj --filter "FullyQualifiedName~Integration"
```

---

## Plan B (si algo falla en directo)

- Si falla frontend: usar Swagger UI para mostrar el mismo flujo.
- Si falla Swagger UI: usar `docs/04_API_TESTING.md` con curl/httpie.
- Si falla auth/JWT: usar tests de integracion como evidencia (`26` tests verdes).
