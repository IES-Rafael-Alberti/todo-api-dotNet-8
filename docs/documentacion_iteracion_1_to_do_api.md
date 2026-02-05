# README.md

## TO-DO API REST (.NET 8/9/10) + Frontend puro

Proyecto docente para construir una **API REST** con **.NET (C#)** y un **frontend mínimo** en **HTML/JavaScript/CSS puro**, siguiendo un enfoque **iterativo** y **progresivo**.

### Objetivos
- Comprender una Web API REST real (rutas, DTOs, validación, status codes).
- Entender el flujo cliente–servidor (fetch, JSON, headers).
- Introducir seguridad de forma progresiva (JWT, propiedad, roles).

### Tecnologías
- .NET 8/9/10 (C#)
- ASP.NET Core Web API
- EF Core + SQLite
- Swagger (OpenAPI)
- Frontend servido desde `wwwroot`

### Arquitectura
```
Controller → Service → Repository → EF Core (SQLite)
```

### Estado del proyecto
- **Iteración 1**: En curso (modelo completo + frontend + reglas de negocio).
- **Iteración 2–4**: Pendientes (usuarios/JWT, propiedad, roles).

### Cómo ejecutar
```bash
dotnet restore
dotnet run
```
Abrir:
- Frontend: `http://localhost:<puerto>/`
- Swagger: `http://localhost:<puerto>/swagger`

---

# docs/01_PROJECT_SETUP.md

## Configuración inicial del proyecto

### Creación del proyecto
```bash
dotnet new webapi -n TodoApi
cd TodoApi
```

### Paquetes principales
- `Microsoft.EntityFrameworkCore`
- `Microsoft.EntityFrameworkCore.Sqlite`
- `Microsoft.EntityFrameworkCore.Tools`
- `Swashbuckle.AspNetCore`

### Estructura de carpetas
```
TodoApi/
 ├─ Controllers/
 ├─ Services/
 ├─ Repositories/
 ├─ Models/
 ├─ DTOs/
 ├─ Data/
 ├─ Middleware/
 └─ wwwroot/
```

### Program.cs (ideas clave)
- Registrar controllers y Swagger.
- Registrar `DbContext` con SQLite.
- Registrar repositorios y servicios.
- Servir frontend estático con:
```csharp
app.UseDefaultFiles();
app.UseStaticFiles();
```

---

# docs/02_ITERATION_1_API.md

## Iteración 1 — API REST de tareas

### Modelo `TodoTask`
Campos principales:
- `Id`
- `Title`
- `Description`
- `CreationDate`
- `DueDate`
- `Status` (`Pending | InProgress | Completed`)

### Endpoints
- `GET /api/tasks`
- `GET /api/tasks/{id}`
- `POST /api/tasks`
- `PUT /api/tasks/{id}`
- `DELETE /api/tasks/{id}`

### Reglas de negocio
- No se pueden crear más de **10 tareas** con estado `Pending`.

### Validaciones
- `Title` obligatorio (mínimo 3 caracteres).
- `DueDate` obligatorio.
- `DueDate` no puede ser anterior a la fecha actual.

### Códigos HTTP usados
- `200 OK`
- `201 Created`
- `204 No Content`
- `400 Bad Request`
- `404 Not Found`
- `409 Conflict`

---

# docs/03_ITERATION_1_FRONTEND.md

## Frontend mínimo (HTML + JS + CSS)

El frontend se sirve desde `wwwroot` usando archivos estáticos.

### Funcionalidades
- Listado de tareas.
- Formulario para crear y editar tareas.
- Botones para ver detalle, editar y borrar.

### Panel de depuración (Debug API)
Incluye dos paneles:
- **Request**: método, URL, headers y body JSON.
- **Response**: status code, headers relevantes y body JSON.

Este panel permite entender el flujo HTTP real entre cliente y servidor.

---

# docs/04_API_TESTING.md

## Pruebas de la API

### Usando curl

#### Crear tarea
```bash
curl -X POST http://localhost:5000/api/tasks \
  -H "Content-Type: application/json" \
  -d '{
    "title": "Primera tarea",
    "description": "Ejemplo",
    "dueDate": "2026-12-31T23:59:00Z",
    "status": "Pending"
  }'
```

#### Listar tareas
```bash
curl http://localhost:5000/api/tasks
```

#### Borrar tarea
```bash
curl -X DELETE http://localhost:5000/api/tasks/1
```

### Usando httpie

#### Crear tarea
```bash
http POST :5000/api/tasks title="Tarea httpie" dueDate="2026-12-31T23:59:00Z" status=Pending
```

#### Listar tareas
```bash
http GET :5000/api/tasks
```

### Casos de error a probar
- Crear más de 10 tareas `Pending` → `409 Conflict`.
- Acceder a una tarea inexistente → `404 Not Found`.
- Enviar datos inválidos → `400 Bad Request`.

---

> **Nota docente**: Estos documentos corresponden a la **Iteración 1**. Las iteraciones siguientes (usuarios/JWT, propiedad y roles) se documentarán en archivos separados para mantener claridad y progresión pedagógica.
