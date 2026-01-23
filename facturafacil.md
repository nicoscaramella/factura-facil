# FacturaFacil - Resumen de Progreso

## Estado del Proyecto (22/01/2026)
Se ha realizado una reestructuración completa de la arquitectura para facilitar el despliegue en VPS y se han aplicado optimizaciones críticas de rendimiento.

### Cambios Realizados Hoy:

1.  **Arquitectura del Backend:**
    *   **Migración:** Se eliminó la dependencia de Azure Functions y se migró a una **ASP.NET Core Web API** estándar en .NET 10.
    *   **Controladores:** Implementación de `InvoicesController` para la generación de PDFs.
    *   **Licencia:** Configuración global de la licencia Community de QuestPDF.

2.  **Optimización de Rendimiento (Frontend):**
    *   **Peso del Bundle:** Se activó el **Trimming** (recorte de código no usado) y la **Globalización Invariante** para reducir drásticamente el tamaño de los archivos WASM.
    *   **Compresión:** Configuración de **Gzip/Brotli** en el Dockerfile de la UI y en Nginx para acelerar la carga inicial.
    *   **Limpieza:** Eliminación de Bootstrap CSS y fuentes externas bloqueantes, delegando todo el diseño a MudBlazor.

3.  **Mejoras en la Experiencia de Usuario (UX/UI):**
    *   **Pantalla de Carga:** Nueva interfaz de inicio con spinner centrado y branding de "Factura Fácil".
    *   **Identidad:** Actualización del título de la pestaña y cambio de favicon por un emoji de hoja (🍃) mediante SVG.
    *   **Donaciones:** Corrección del enlace a MercadoPago y personalización del botón con icono de corazón.

4.  **Funcionalidades del Negocio:**
    *   **Modo No Fiscal:** Opción para generar presupuestos (Letra "X", sin QR de ARCA/AFIP, sin CAE).
    *   **Flexibilidad:** El CUIT del comprador ahora es opcional; si se deja vacío, el PDF muestra automáticamente "Consumidor Final".

5.  **Despliegue y Docker:**
    *   **Docker Compose:** Configuración robusta con Nginx como proxy inverso para manejar el tráfico del Frontend y la API en una sola red.
    *   **Coolify:** Preparación total para despliegue en VPS mediante Git.

### Pruebas Realizadas:
*   Compilación exitosa de toda la solución (`dotnet build`).
*   Verificación de la carga optimizada en entorno local Docker.
*   Prueba de generación de PDF tanto en modo Fiscal como Presupuesto.

### Pendientes:
*   Implementar persistencia de borradores (LocalStorage o Base de Datos).
*   Validación de algoritmos de CUIT (opcional, para evitar errores de carga).
*   Personalización de logos de empresa en el encabezado.

---
**Nota:** El proyecto ahora es totalmente compatible con entornos Linux estándar y despliegues tipo Coolify/Portainer.
