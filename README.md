# CFDI Estatus MX

Aplicación web en **.NET 10 + Blazor Server** para consultar el **estatus de un CFDI** y determinar si puede ser cancelado, simulando el comportamiento del servicio del SAT.

> Proyecto personal y demostrativo. No afiliado al SAT.

## Funciones

- Consulta el estatus de un CFDI: **Vigente**, **Cancelado** o **No encontrado**.
- Muestra si el CFDI es cancelable y el estado de cancelación.
- Permite capturar los datos fiscales manualmente.
- Permite importar datos desde un archivo XML CFDI.
- Genera un PDF de la consulta usando **QuestPDF**.
- Descarga el PDF con un nombre único, por ejemplo `consulta-a1b2c3d4.pdf`.
- Bloquea la descarga del PDF cuando no hay datos suficientes en el formulario.
- Incluye en el PDF el resultado de la consulta cuando ya existe una respuesta del servicio.
- **Nuevo:** Histórico de consultas con almacenamiento local (localStorage).
- **Nuevo:** Exporta el histórico a CSV.
- **Nuevo:** Gestión de histórico (limpiar, visualizar).

## PDF

El PDF contiene:

- Encabezado: `Consulta SAT`.
- Datos del CFDI: emisor, receptor, total, UUID y sello FE.
- Resultado de la consulta, si existe: código de estatus, estatus, cancelabilidad y estado de cancelación.
- Pie de página con fecha y hora de generación.

## Histórico de Consultas

La aplicación mantiene un **histórico de todas las consultas realizadas al SAT**:

- **Almacenamiento**: Los datos se guardan en `localStorage` del navegador (hasta 5MB).
- **Automático**: Se registra automáticamente cuando se realiza una consulta exitosa.
- **Actualización**: Si se consulta un UUID que ya existe, se actualiza su resultado.
- **Información**: Muestra UUID, RFC Emisor, Total, Estado y Fecha de consulta.
- **Exportación**: Descarga el histórico completo en formato CSV.
- **Gestión**: Opción para limpiar todo el histórico con confirmación.

### Modalidades de acceso al Histórico

1. **Botón "Ver Histórico"**: Abre una modal independiente con la tabla de consultas.
2. **Tabla ordenada**: Las consultas se muestran ordenadas por fecha más reciente.
3. **Indicador de estado**: Un badge de color indica el estatus (Vigente, Cancelado, No encontrado, etc.).

## Nota

La aplicación puede usar un servicio mock con datos de ejemplo. No se almacenan datos fiscales reales.

Autor: Miguel Segura

## Demo Online

https://cfdiestatus.segurab.com/

![App Screenshot](https://res.cloudinary.com/imgresd/image/upload/v1770872126/Github/CFDIExample_ju6tsb.png)

---

# CFDI Status MX

Web application built with **.NET 10 + Blazor Server** to check the **status of a CFDI** and determine whether it can be canceled, simulating the behavior of the SAT service.

> Personal demo project. Not affiliated with SAT.

## Features

- Checks CFDI status: **Active**, **Canceled**, or **Not found**.
- Shows whether the CFDI is cancelable and its cancellation state.
- Supports manual fiscal data entry.
- Supports importing data from a CFDI XML file.
- Generates a PDF report using **QuestPDF**.
- Downloads the PDF with a unique filename, for example `consulta-a1b2c3d4.pdf`.
- Prevents PDF downloads when the form does not contain enough data.
- Includes the query result in the PDF when a service response already exists.
- **New:** Query history with local storage (localStorage).
- **New:** Export history to CSV.
- **New:** History management (clear, view).

## PDF

The PDF includes:

- Header: `Consulta SAT`.
- CFDI data: issuer, receiver, total, UUID, and FE seal.
- Query result, when available: status code, status, cancelability, and cancellation state.
- Footer with generation date and time.

## Query History

The application maintains a **history of all queries made to the SAT**:

- **Storage**: Data is saved in the browser's `localStorage` (up to 5MB).
- **Automatic**: Automatically recorded when a query is successfully completed.
- **Updates**: If a UUID that already exists is queried, its result is updated.
- **Information**: Displays UUID, Issuer RFC, Total, Status, and Query date.
- **Export**: Download the complete history in CSV format.
- **Management**: Option to clear all history with confirmation.

### History Access Methods

1. **"View History" Button**: Opens an independent modal with the query table.
2. **Sorted Table**: Queries are displayed sorted by most recent date.
3. **Status Indicator**: A colored badge indicates the status (Active, Canceled, Not found, etc.).

## Note

The application can use a mock service with sample data. No real fiscal data is stored.

Author: Miguel Segura

## Online Demo

https://cfdiestatus.segurab.com/

![App Screenshot](https://res.cloudinary.com/imgresd/image/upload/v1770872126/Github/CFDIExample_ju6tsb.png)

