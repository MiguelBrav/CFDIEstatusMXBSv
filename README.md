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

## PDF

El PDF contiene:

- Encabezado: `Consulta SAT`.
- Datos del CFDI: emisor, receptor, total, UUID y sello FE.
- Resultado de la consulta, si existe: código de estatus, estatus, cancelabilidad y estado de cancelación.
- Pie de página con fecha y hora de generación.

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

## PDF

The PDF includes:

- Header: `Consulta SAT`.
- CFDI data: issuer, receiver, total, UUID, and FE seal.
- Query result, when available: status code, status, cancelability, and cancellation state.
- Footer with generation date and time.

## Note

The application can use a mock service with sample data. No real fiscal data is stored.

Author: Miguel Segura

## Online Demo

https://cfdiestatus.segurab.com/

![App Screenshot](https://res.cloudinary.com/imgresd/image/upload/v1770872126/Github/CFDIExample_ju6tsb.png)
