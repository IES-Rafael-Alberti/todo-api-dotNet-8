# Checklist Iteracion 1

## Hecho
- [x] Decidir modelo completo (Description, CreationDate, DueDate, Status).
- [x] Alinear prompt de trabajo con el estado real y los objetivos.
- [x] Regla de 10 pendientes implementada en repositorio (base actual).
- [x] Ampliar modelo `TodoTask` y mapping DTO <-> entidad.
- [x] Actualizar DTOs con el modelo completo.
- [x] Añadir filtro por `Status` en GET.
- [x] Validaciones: Title y DueDate, y regla `DueDate >= CreationDate`.
- [x] Conectar frontend a la API con panel debug.
- [x] Unificar docs de Iteracion 1 y ejemplos de testing.
- [x] Aplicar migracion en base de datos local.

## Por hacer (backend)
- [x] Ajustar tests existentes y completar tests faltantes.

## Por hacer (frontend)
- [ ] (Nada pendiente si no se pide funcionalidad extra).

## Por hacer (documentacion)
- [ ] (Nada pendiente si no se pide ampliar contenidos).

## Bloqueadores o decisiones
- [ ] Confirmar si el limite de 10 pendientes debe vivir en Service en el futuro.
