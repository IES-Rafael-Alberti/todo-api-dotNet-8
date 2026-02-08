# 10 AWS Deployment (App Runner + ECR)

## Objetivo
Desplegar la app en AWS de forma simple usando contenedor Docker en App Runner.

## Prerrequisitos
- Cuenta AWS.
- AWS CLI configurado (`aws configure`).
- Docker instalado.
- Region elegida (ejemplo: `eu-west-1`).

## 1) Crear repositorio ECR
```bash
aws ecr create-repository \
  --repository-name todoapi-net \
  --region eu-west-1
```

## 2) Login de Docker en ECR
```bash
aws ecr get-login-password --region eu-west-1 | \
docker login --username AWS --password-stdin <AWS_ACCOUNT_ID>.dkr.ecr.eu-west-1.amazonaws.com
```

## 3) Build de imagen
Desde `todo-dotnet/src`:
```bash
docker build -f TodoApi/Dockerfile -t todoapi-net:latest .
```

## 4) Tag + Push a ECR
```bash
docker tag todoapi-net:latest <AWS_ACCOUNT_ID>.dkr.ecr.eu-west-1.amazonaws.com/todoapi-net:latest
docker push <AWS_ACCOUNT_ID>.dkr.ecr.eu-west-1.amazonaws.com/todoapi-net:latest
```

## 5) Crear servicio en App Runner
1. AWS Console -> App Runner -> Create service.
2. Source: `Container registry`.
3. Provider: `Amazon ECR`.
4. Image URI: `<AWS_ACCOUNT_ID>.dkr.ecr.eu-west-1.amazonaws.com/todoapi-net:latest`.
5. Port: `8080`.
6. Environment variables:
- `Jwt__Issuer=TodoApi`
- `Jwt__Audience=TodoApiClient`
- `Jwt__SigningKey=<secreto-largo-seguro>`
- `Jwt__ExpiresInSeconds=3600`

## 6) Verificar despliegue
- Abrir URL pública de App Runner.
- Comprobar:
- `/` (frontend)
- `/api/tasks` (debe pedir auth)

## Notas importantes para esta app
- Persistencia actual: SQLite (`todo.db`) en disco local del contenedor.
- En App Runner el almacenamiento local es efímero.
- Para producción real:
- usar RDS (PostgreSQL/MySQL) o
- montar almacenamiento persistente externo.

## Variables de entorno en .NET
La app lee configuración jerárquica con doble guion bajo (`__`), por ejemplo:
- `Jwt__SigningKey`
- `ConnectionStrings__TodoDb`
