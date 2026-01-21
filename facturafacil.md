# FacturaFacil - Resumen de Progreso

## Estado del Proyecto (21/01/2026)
Se completó la integración del Frontend con el Backend y se implementó la generación del QR de AFIP.

### Cambios Realizados Hoy:
1.  **Backend (API):**
    *   **CORS:** Se habilitó CORS en `local.settings.json` para permitir peticiones desde `http://localhost:5080`.
    *   **Modelo de Datos:** Se agregaron `PointOfSale` e `InvoiceType` al `InvoiceModel` para mayor flexibilidad.
2.  **Frontend (Blazor WASM):**
    *   **Formulario Dinámico:** Se implementó un formulario completo en `Home.razor` usando MudBlazor, permitiendo editar datos del vendedor, comprador e ítems (con opción de agregar/quitar filas).
    *   **Integración API:** Se configuró la llamada POST a la Azure Function y la visualización del PDF resultante en un `iframe` integrado en la UI.
3.  **Generación de PDF (`InvoiceDocument.cs`):**
    *   **QR de AFIP:** Se integró la generación de códigos QR legales siguiendo las especificaciones de AFIP (URL con JSON en Base64).
    *   **Librerías:** Se agregaron `Net.Codecrete.QrCodeGenerator`, `SixLabors.ImageSharp` y `SixLabors.ImageSharp.Drawing` para manejar la creación del QR.
    *   **Diseño:** Se mejoró el encabezado para mostrar la letra de la factura (A, B o C) y el punto de venta.

### Pruebas Realizadas:
*   La solución compila sin errores ni advertencias.
*   Se verificó la lógica de generación del QR y el formato de la URL de AFIP.
*   El formulario de Blazor valida los campos obligatorios antes de permitir la generación.

### Pendientes:
*   Implementar validaciones de CUIT (algoritmo de verificación).
*   Agregar persistencia opcional (guardar borradores de facturas).
*   Mejorar el diseño visual del PDF (colores, logo de la empresa).

---
**Nota:** Para ejecutar el proyecto, iniciar la API en el puerto 7100 y el UI en el 5080.