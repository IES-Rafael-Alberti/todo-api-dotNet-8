# Instrucciones para el manejo de versiones .NET (8 y 10)

Este proyecto ha sido actualizado para funcionar con **.NET 10** en casa, pero mantiene la compatibilidad con **.NET 8** para su uso en el instituto.

## Estado actual (en casa / .NET 10)
- **Framework:** .NET 10.0
- **Solución:** El proyecto `TodoApi` ya está vinculado a `TodoDotNet.sln`.
- **Rider:** Al abrir el archivo `.sln`, Rider debería reconocer automáticamente el proyecto y permitirte ejecutarlo usando la configuración ".NET Project".

---

## Cómo volver a .NET 8 (para el instituto)

Si en el instituto solo dispones del SDK de .NET 8, sigue estos pasos:

1.  **Cambiar el archivo de proyecto:**
    *   Localiza la carpeta `TodoApi`.
    *   Renombra (o borra) el archivo `TodoApi.csproj`.
    *   Busca el archivo `TodoApi.csproj.bak8` y cámbiale el nombre a `TodoApi.csproj`.
2.  **Restaurar paquetes:**
    *   Abre una terminal en la carpeta del proyecto y ejecuta:
        ```bash
        dotnet restore
        ```
    *   O simplemente deja que Rider lo haga automáticamente al abrir el proyecto.

---

## Cómo regresar a .NET 10 (en casa)

Si quieres volver a usar la versión más reciente en casa:

1.  **Editar el archivo `TodoApi.csproj`** o usar la copia que tenías de .NET 10.
2.  Asegúrate de que el `TargetFramework` sea `net10.0`.
3.  Las versiones de las librerías deben ser:
    *   `Microsoft.AspNetCore.OpenApi`: `10.0.*`
    *   `Swashbuckle.AspNetCore`: `7.2.0`

---

## Notas adicionales para alumnos

- **¿Por qué no aparecía el proyecto en Rider?**
  Rider necesita que los proyectos estén registrados en el archivo de solución (`.sln`). Si alguna vez creas un proyecto desde la terminal y no aparece en Rider, usa el comando:
  ```bash
  dotnet sln TodoDotNet.sln add TodoApi/TodoApi.csproj
  ```

- **Configuración de ejecución:**
  Si Rider no crea el botón de "Play" automáticamente:
  1. Ve a **Edit Configurations**.
  2. Añade una nueva de tipo **.NET Project**.
  3. En **Project**, selecciona `TodoApi`.
  4. En **Profile**, selecciona `http` (esto cargará la URL y abrirá Swagger automáticamente).
