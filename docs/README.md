Proyecto docente para construir una **API REST** con **.NET (C#)** y un **frontend mínimo** en **HTML/JavaScript/CSS puro**, siguiendo un enfoque **iterativo** y **progresivo**.
# README.md

## TO-DO API REST (.NET 8/9/10) + Frontend puro
### Objetivos
- Comprender una Web API REST real (rutas, DTOs, validación, status codes).
- Entender el flujo cliente–servidor (fetch, JSON, headers).
- Introducir seguridad de forma progresiva (JWT, propiedad, roles).

### Tecnologías
- .NET 8/9/10 (C#)
- ASP.NET Core Web API
- EF Core + SQLite
- OpenAPI + Swagger UI (en entorno Development)
- Frontend servido desde `wwwroot`

### Arquitectura
```
Controller → Service → Repository → EF Core (SQLite)
```

### Estado del proyecto
- **Iteración 1**: Cerrada.
- **Iteración 2**: Cerrada (JWT + auth + frontend minimo).
- **Iteración 3**: Cerrada (propiedad por usuario).
- **Iteración 4**: Cerrada (roles + tests de integracion).

### Cómo ejecutar
```bash
dotnet restore
dotnet run
```
Abrir:
- Frontend: `http://localhost:<puerto>/`
- API: `http://localhost:<puerto>/api/...`
- Swagger UI: `http://localhost:<puerto>/swagger`

### Tests
```bash
dotnet test TodoApi.Tests/TodoApi.Tests.csproj
```

Para tests de integracion:
```bash
dotnet test TodoApi.Tests/TodoApi.Tests.csproj --filter "FullyQualifiedName~Integration"
```

---
