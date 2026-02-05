
## Proyecto: TO-DO API REST con .NET 10 + Frontend puro (HTML/JS/CSS)

### Contexto

Vas a construir una aplicación **TO-DO** con una **API REST en .NET 10 (C#)** y una interfaz web mínima en **HTML + JavaScript + CSS (sin frameworks)**.

El proyecto se desarrolla en **4 iteraciones**. Cada iteración debe quedar documentada en **Markdown**, y tendrás un documento extra con **pruebas de endpoints** usando **httpie y curl**.

### Objetivos didácticos

* Entender una Web API REST real: rutas, controladores, DTOs, validación, status codes.
* Ver el flujo completo cliente↔servidor: **fetch()** en frontend, JSON, headers, responses.
* Introducir seguridad de forma progresiva: JWT, autorización por propiedad, roles.
---

## Iteración 1 — CRUD de tareas + frontend mínimo con panel de debug

### Modelo: Task (Tarea)

Campos mínimos:

* `Id` (int, autoincrement / generado)
* `Title` (string, obligatorio)
* `Description` (string, opcional)
* `CreationDate` (DateTime, automático)
* `DueDate` (DateTime, obligatorio)
* `Status` (enum): `Pending | InProgress | Completed`

### API REST (sin autenticación real todavía)

Endpoints:

* `GET /api/tasks` → listar tareas (filtro opcional `?status=Pending`)
* `GET /api/tasks/{id}` → detalle
* `POST /api/tasks` → crear
* `PUT /api/tasks/{id}` → modificar
* `DELETE /api/tasks/{id}` → borrar

Regla de negocio 1:

* No se pueden crear más de **10 tareas** con `Status = Pending`.

Validación mínima:

* `Title` obligatorio (mín 3 caracteres recomendado)
* `DueDate` obligatorio
* `DueDate >= CreationDate` (si quieres, puede ser validación extra)

Persistencia en Iteración 1:

* **EF Core + SQLite** (fichero local) para un entorno realista.

Errores HTTP coherentes:

* 400 (validación)
* 404 (no existe)
* 409 (regla de negocio: límite de pendientes)

### Frontend mínimo (sin frameworks)

Pantallas / componentes:

* Listado de tareas
* Formulario crear/editar
* Botones: ver detalle, editar, borrar

Panel “Debug API” (muy importante):

* Cuadro “Request”: método, URL, headers, body JSON (si aplica)
* Cuadro “Response”: status code, headers relevantes, body JSON

---

## Iteración 2 — Usuarios + autenticación JWT (login / register)

* Entidad `User`: `Id`, `Username`, `Email`, `PasswordHash`, `Role`
* Endpoints:

    * `POST /api/auth/register`
    * `POST /api/auth/login` (devuelve JWT)
* Proteger endpoints de tasks con JWT
* Activar HTTPS en desarrollo (dev-certs)

---

## Iteración 3 — Propiedad: cada usuario solo ve/modifica sus tareas

* Las tareas pertenecen a un usuario (`Task.UserId`)
* `GET /api/tasks` devuelve solo las del usuario autenticado
* `PUT/DELETE` comprueba propiedad → 403 si no es suya

---

## Iteración 4 — Roles (User / Supervisor / Admin)

* User: solo sus tareas
* Supervisor:

    * ver todas
    * editar tareas ajenas **pero NO** marcarlas como Completed
    * NO borrar tareas ajenas
* Admin: control total

---

## Documentación requerida (Markdown)

* `README.md`
* `docs/01_PROJECT_SETUP.md`
* `docs/02_ITERATION_1_API.md`
* `docs/03_ITERATION_1_FRONTEND.md`
* `docs/04_API_TESTING.md` (httpie + curl)
* `docs/05_ITERATION_2_JWT.md`
* `docs/06_ITERATION_3_OWNERSHIP.md`
* `docs/07_ITERATION_4_ROLES.md`

---

# Siguiente paso: estructura del repo + Iteración 1

A partir de aquí, lo hacemos *paso a paso*. Empezamos.

---

## Estructura del repositorio propuesta

```
todo-dotnet/
  README.md
  docs/
    01_PROJECT_SETUP.md
    02_ITERATION_1_API.md
    03_ITERATION_1_FRONTEND.md
    04_API_TESTING.md
    05_ITERATION_2_JWT.md
    06_ITERATION_3_OWNERSHIP.md
    07_ITERATION_4_ROLES.md
  src/
    TodoApi/
      TodoApi.csproj
      Program.cs
      Controllers/
      Models/
      Contracts/
      Repositories/
      wwwroot/
        index.html
        app.js
        styles.css
```

Notas:

* Usamos `wwwroot/` para servir el frontend desde el propio Kestrel (más simple para clase).
* Documentación en `docs/` desde el minuto 1.
