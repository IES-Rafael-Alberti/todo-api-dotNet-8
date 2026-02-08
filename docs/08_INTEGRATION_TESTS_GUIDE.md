# docs/08_INTEGRATION_TESTS_GUIDE.md

## Guia: tests de integracion HTTP

### Objetivo didactico
Mostrar a alumnado como validar seguridad y reglas de negocio desde HTTP real, no solo con tests unitarios.

### Diferencia rapida
- Unit test: prueba una clase aislada (sin servidor HTTP).
- Integration test: levanta la API y hace requests reales con `HttpClient`.

### Archivo clave
- `TodoApi.Tests/Integration/TaskAuthorizationIntegrationTests.cs`

### Que valida esta bateria
- `401` cuando no hay autenticacion.
- `403` cuando el rol no tiene permiso.
- `204` cuando la operacion esta permitida (ejemplo: Admin borrando tarea ajena).
- Flujo de roles en endpoints reales `/api/tasks`.

### Como esta montado (paso a paso)
1. `WebApplicationFactory<Program>` levanta la API en memoria.
2. Se reemplaza `TodoDbContext` para usar SQLite temporal por test.
3. Se hace seed de usuarios y tareas.
4. Se usa un esquema de auth de pruebas por cabeceras:
- `X-Test-UserId`
- `X-Test-Role`
5. Se lanzan requests HTTP y se comprueban status codes.

### Por que se usa auth de pruebas en vez de JWT real
- Reduce fragilidad en test de infraestructura.
- Mantiene foco en autorizacion de negocio (`401/403/204`).
- JWT real ya se cubre en tests/unitarios y en pruebas manuales con curl/httpie.

### Equivalencia con Spring Boot (referencia para clase)
- `.NET WebApplicationFactory` ~ `@SpringBootTest(webEnvironment = RANDOM_PORT)`
- `HttpClient` ~ `TestRestTemplate` o `WebTestClient`
- Reemplazo de servicios en tests ~ `@TestConfiguration` / `@MockBean`

### Ejecutar solo integracion
```bash
cd todo-dotnet/src
dotnet test TodoApi.Tests/TodoApi.Tests.csproj --filter "FullyQualifiedName~Integration"
```

### Ejecutar todos los tests
```bash
cd todo-dotnet/src
dotnet test TodoApi.Tests/TodoApi.Tests.csproj
```

### Extension recomendada (siguiente clase)
- Añadir tests de integracion para:
- `POST /api/auth/register`
- `POST /api/auth/login`
- Casos de validacion `400` en DTOs.
