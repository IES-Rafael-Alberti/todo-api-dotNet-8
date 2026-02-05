# docs/04_API_TESTING.md

## Pruebas de la API (Iteracion 1)

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

#### Filtrar por estado
```bash
curl "http://localhost:5000/api/tasks?status=Pending"
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
- Crear mas de 10 tareas `Pending` → `409 Conflict`.
- Acceder a una tarea inexistente → `404 Not Found`.
- Enviar datos invalidos → `400 Bad Request`.

---

## Pruebas de la API (Iteracion 2)

> Nota: requiere que la autenticacion JWT este implementada.

### Usando curl

#### Registro
```bash
curl -X POST http://localhost:5000/api/auth/register \
  -H "Content-Type: application/json" \
  -d '{
    "username": "ana",
    "email": "ana@example.com",
    "password": "123456"
  }'
```

#### Login
```bash
curl -X POST http://localhost:5000/api/auth/login \
  -H "Content-Type: application/json" \
  -d '{
    "email": "ana@example.com",
    "password": "123456"
  }'
```

#### Acceso a tasks con token
```bash
curl http://localhost:5000/api/tasks \
  -H "Authorization: Bearer <jwt>"
```

### Usando httpie

#### Registro
```bash
http POST :5000/api/auth/register username=ana email=ana@example.com password=123456
```

#### Login
```bash
http POST :5000/api/auth/login email=ana@example.com password=123456
```

#### Acceso a tasks con token
```bash
http GET :5000/api/tasks "Authorization:Bearer <jwt>"
```

### Casos de error a probar
- Acceder a `/api/tasks` sin token → `401 Unauthorized`.
- Login con password incorrecto → `401 Unauthorized`.
