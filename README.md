## Mini-Agenda Medica

## Descripción del proyecto

En este proyecto es el desarrollo de una Api para una Mini Agenda Médica donde se realiza la gestión de citas médicas, permitiendo administrar pacientes, médicos y la agenda de citas. 
La API expone endpoints RESTful documentados con OpenAPI y visualizables a través de **Scalar**.

El sistema está diseñado siguiendo principios de **arquitectura limpia (Clean Architecture)**, separando claramente responsabilidades entre capas:

* **Domain**: Entidades y reglas de negocio
* **Application**: Casos de uso y lógica de aplicación
* **Infrastructure**: Acceso a datos (SQL Server, Stored Procedures)
* **API**: Exposición de endpoints HTTP
La solución está organizada en 4 proyectos siguiendo Clean Architecture:

```
Sol_Appi_AgendaMedica/
│
├── Domain/                  # Capa de Dominio (entidades + excepciones)
│   ├── Entities/
│   │   ├── Paciente.cs
│   │   ├── Medico.cs
│   │   ├── Cita.cs
│   │   ├── Horario.cs
│   │   └── Catalogos.cs     # DiaSemana, Especialidad, EstadoCita
│   └── Exceptions/
│       └── DomainException.cs
│
├── Application/             # Capa de Aplicación (servicios, DTOs, validaciones)
│   ├── DTOs/Pacientes/
│   │   ├── CrearPacienteRequestDto.cs
│   │   ├── ActualizarPacienteRequestDto.cs
│   │   ├── ConsultaPacienteDtoRequestDto.cs
│   │   ├── CambiarEstadoPacienteRequestDto.cs
│   │   ├── EliminarPacienteRequestDto.cs
│   │   └── ExistePacienteRequestDto.cs
│   ├── Interfaces/
│   │   ├── IPacienteRepository.cs
│   │   └── IPacienteService.cs
│   ├── Services/
│   │   └── PacienteService.cs
│   └── Validators/
│       └── CrearPacienteValidator.cs
│
├── Infrastructure/          # Capa de Infraestructura (acceso a datos)
│   ├── Persistence/Dapper/
│   │   └── DapperContext.cs
│   ├── Repositories/
│   │   └── PacienteRepository.cs
│   ├── Models/
│   │   └── SpAgregarPacienteResult.cs
│   ├── Configurations/      # (reservado)
│   └── SQL/
│       ├── Scripts/          # (reservado para scripts DDL)
│       └── StoredProcedures/ # (reservado para SPs)
│
└── Appi_AgendaMedica/       # Capa de Presentación (Web API)
    ├── Controllers/
    │   ├── PacientesController.cs   ✅ Implementado
    │   ├── CitasController.cs       🔲 Scaffold
    │   ├── MedicosController.cs     🔲 Scaffold
    │   ├── HorariosController.cs    🔲 Scaffold
    │   ├── HorariosDisponiblesController.cs  🔲 Scaffold
    │   ├── AgendaDiaController.cs   🔲 Scaffold
    │   └── HistorialController.cs   🔲 Scaffold
    └── Program.cs
```

### Flujo de dependencias

```
Appi_AgendaMedica (API)  →  Application  →  Domain
         ↓
   Infrastructure  →  Application + Domain
```

> **Domain** no depende de nadie. **Application** solo depende de Domain. **Infrastructure** implementa las interfaces definidas en Application.

## 🛠️ Tecnologías

| Componente | Tecnología | Versión |
|---|---|---|
| **Framework** | .NET | 9.0 |
| **ORM / Data Access** | Dapper | 2.1.72 |
| **DB Client** | Microsoft.Data.SqlClient | 7.0.0 |
| **Validación** | FluentValidation | 12.1.1 |
| **Logging** | Serilog (Console + File) | 4.3.1 |
| **API Docs** | Scalar + OpenAPI | 2.13.10 |
| **Base de datos** | SQL Server | — |
| **IDE** | Visual Studio 2022 (v17.14+) | — |


| Decisión | Justificación |
|---|---|
| **Service-based (sin MediatR)** | Simplicidad; el alcance del proyecto no requiere CQRS ni pipeline de behaviors |
| **Dapper + SqlClient** | Control directo sobre SQL y stored procedures; mejor rendimiento para operaciones específicas |
| **FluentValidation** | Validación declarativa y desacoplada de los DTOs, aplicada en capa de Application/API |
| **Serilog** | Logging estructurado con sinks flexibles (console + archivo) |
| **Scalar (no Swagger UI)** | Documentación de API moderna y ligera sobre OpenAPI nativo de .NET 9 |
| **Entidades con private setters** | Encapsulación DDD-lite; las entidades protegen su estado y exponen comportamiento |


---
##  Estado actual del proyecto

Actualmente el proyecto se encuentra en desarrollo parcial, con los siguientes avances:

###  Completado

* ✔ Diseño y creación de base de datos en SQL Server
* ✔ Scripts de creación de tablas
* ✔ Implementación de Stored Procedures:

  * Validación de disponibilidad de médico
  * Consulta de agenda diaria
* ✔ Definición de entidades principales (Cita, Paciente, etc.)
* ✔ Separación inicial de capas (Domain, Application, Infrastructure, API)
* ✔ Implementación parcial de servicios y repositorios
* ✔ Uso de DTOs para comunicación entre capas
* ✔ En lugar de Swagger , está implemantado Scalar
* ✔ Se implemntó  FluentValidation en capa de Application
* ✔ Se implementó Dapper  para el uso de los stores con paciente
## Endpoints Implementados

### Pacientes (`api/Pacientes`)

| Método | Ruta | Descripción |
|---|---|---|
| `GET` | `/api/Pacientes` | Listar todos los pacientes |
| `GET` | `/api/Pacientes/{id}` | Obtener paciente por ID |
| `POST` | `/api/Pacientes` | Crear nuevo paciente |
| `PUT` | `/api/Pacientes/{id}` | Actualizar paciente |
| `PATCH` | `/api/Pacientes/{id}/desactivar` | Desactivar paciente (baja lógica) |
| `PATCH` | `/api/Pacientes/{id}/activar` | Reactivar paciente |

### Controllers con scaffold (pendientes de implementar)

- `CitasController` — Gestión de citas
- `MedicosController` — Gestión de médicos
- `HorariosController` — Horarios de atención por médico
- `HorariosDisponiblesController` — Consulta de disponibilidad
- `AgendaDiaController` — Vista de agenda diaria
- `HistorialController` — Historial de citas

---

## Entidades de Dominio

### Paciente
Nombre, Apellido, FechaNacimiento, Teléfono, Correo, Estado. Validaciones de dominio en constructor (campos obligatorios).

### Médico
NumCédula, Nombre, Apellido, Especialidad, Tarifa, Estado. Validaciones de dominio en constructor.

### Cita
Relación Médico–Paciente con FechaCita, HoraInicio/HoraFin, DiaSemana, Motivo, EstadoCita. Incluye comportamiento de dominio: `Confirmar()`, `Cancelar()`, `Reprogramar()`, `Desactivar()`.

### Horario
Horario de atención por médico y día de semana. Incluye método `EstaDentroHorario()` para validar slots.

### Catálogos
- `DiaSemana` — Días hábiles
- `Especialidad` — Especialidades médicas
- `EstadoCita` — Estados posibles de una cita (Programada, Confirmada, Cancelada, etc.)

---

## Stored Procedures

La capa de Infrastructure consume SPs de SQL Server. Los implementados son:

| SP | Uso |
|---|---|
| `sp_Pacientes_Agregar` | Insertar paciente, retorna `MENSAJE`, `ERROR`, `NuevoId` |
| `sp_Pacientes_Consultar` | Listar todos los pacientes |

Los directorios `Infrastructure/SQL/Scripts/` y `Infrastructure/SQL/StoredProcedures/` están reservados para alojar los scripts SQL.

---

## Configuración

### Connection String

En `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=AgendaMedica;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

> **Nota:** El `DapperContext` lee la clave `"Default"` (no `"DefaultConnection"`). Verificar que coincidan, o ajustar uno de los dos.

### Inyección de Dependencias (Program.cs)

```csharp
builder.Services.AddSingleton<DapperContext>();
builder.Services.AddScoped<IPacienteRepository, PacienteRepository>();
builder.Services.AddScoped<IPacienteService, PacienteService>();
```

### Logging

Serilog configurado con dos sinks:
- **Console** — Salida en terminal
- **File** — `logs/agendamedica.txt` con rolling diario

---


##  Pendientes por implementar

A continuación se listan las tareas pendientes junto con su importancia dentro del sistema:

---

###  1. Completar implementación de servicios (Application Layer)

**Falta:**

* Implementar completamente PacienteService, CitaService, etc.

**Por qué es importante:**

* Aquí vive la lógica de negocio real
* Permite validar reglas antes de acceder a la base de datos
* Evita que el Controller tenga lógica (mala práctica)

---

###  2. Implementar repositorios en Infrastructure

**Falta:**

* Implementaciones concretas con Dapper / SQL

**Por qué es importante:**

* Conecta la aplicación con la base de datos
* Permite mantener desacoplada la lógica del almacenamiento

---

###  3. Endpoints completos en Controllers

**Falta:**

* CRUD de pacientes
* Gestión de citas
* Activación/desactivación de registros

**Por qué es importante:**

* Expone la funcionalidad al usuario final
* Permite probar el sistema vía HTTP

---

###  4. Validaciones con FluentValidation

**Falta:**

* Validadores para DTOs (ej. CrearPaciente, CrearCita)

**Por qué es importante:**

* Previene datos inválidos
* Mejora la calidad del sistema
* Permite respuestas HTTP 400 correctas

---

###  5. Manejo de errores estructurado

**Falta:**

* Middleware global de excepciones

**Por qué es importante:**

* Cumple con requisitos (400, 404, 409)
* Mejora experiencia del cliente
* Hace el sistema más profesional

---

###  6. Unit Tests

**Falta:**

* Pruebas para reglas críticas:

  * Conflictos de horario
  * Validación de duración
  * Cancelaciones

**Por qué es importante:**

* Garantiza que la lógica funciona correctamente
* Previene errores en producción

---
## Estado del Proyecto

- ✅ Arquitectura Clean Architecture (4 capas)
- ✅ Entidades de dominio con validaciones y comportamiento (Cita, Médico, Paciente, Horario)
- ✅ CRUD de Pacientes (Controller + Service + Repository)
- ✅ Logging con Serilog
- ✅ Documentación OpenAPI + Scalar
- 🔲 Implementación de Controllers restantes (Citas, Médicos, Horarios, etc.)
- 🔲 Repositorios para Citas, Médicos, Horarios
- 🔲 Stored procedures pendientes (`sp_ValidarDisponibilidadMedico`, `sp_ContarCancelacionesPaciente`, `sp_ObtenerSlotsDisponibles`)
- 🔲 Middleware global de excepciones
- 🔲 Mapeo con Mapster (planeado)
- 🔲 Tests unitarios

---
##  Base de datos

### Tablas

| Tabla | Descripción | Campos clave |
|---|---|---|
| **Pacientes** | Datos personales del paciente | Id, Nombre, Apellido, FechaNacimiento, Telefono, Correo, Activo |
| **Medicos** | Datos del médico y su especialidad | Id, Nombre, Apellido, IdEspecialidad, Activo |
| **Citas** | Registro de citas agendadas | Id, IdPaciente, IdMedico, FechaCita, HoraInicio, HoraFin, DuracionMinutos, IdEstadoCita |
| **HorariosMedicos** | Horarios de atención por médico y día | Id, IdMedico, IdDiaSemana, HoraInicio, HoraFin |
| **Cat_Especialidades** | Catálogo de especialidades médicas | Id, Nombre |
| **Cat_DiaSemana** | Catálogo de días de la semana | Id, Nombre |
| **Cat_EstadoCita** | Catálogo de estados de cita (Programada, Completada, Cancelada) | Id, Nombre |

### Stored Procedures

#### CRUD implementados

| Módulo | Agregar | Consultar | ConsultarById | Editar | Eliminar |
|---|---|---|---|---|---|
| **Pacientes** | ✔ | ✔ | — | — | — |
| **Medicos** | ✔ | ✔ | ✔ | ✔ | ✔ |
| **HorariosMedicos** | ✔ | ✔ | ✔ | ✔ | ✔ |
| **Catálogos** | — | ✔ DiaSemana, Especialidades, EstadoCita | — | — | — |

#### Pendientes — SPs de lógica de negocio

| Stored Procedure | Qué haría | Por qué importa |
|---|---|---|
| `sp_ValidarDisponibilidadMedico` | Verificar si un médico tiene disponibilidad para una fecha/hora considerando duración y citas existentes | Requisito obligatorio de la evaluación. Previene conflictos de horario. |
| `sp_ObtenerAgendaCompletaMedico` | Devolver la agenda completa del día de un médico con datos del paciente y estado de cita | Requisito obligatorio. Permite consultar la agenda diaria. |
| `sp_ContarCancelacionesPaciente` | Contar cancelaciones de un paciente en un período para aplicar reglas de negocio | Regla de negocio para limitar cancelaciones excesivas. |
| `sp_ObtenerSlotsDisponibles` | Calcular slots de tiempo libres cruzando horarios configurados contra citas existentes | Facilita la selección de horarios al agendar. |

### Diagrama de relaciones
Cat_Especialidades (1) ───< Medicos (1) ───< Citas >─── (1) Pacientes
│                              │
└───< HorariosMedicos    Cat_EstadoCita
│
Cat_DiaSemana
---
## ¿ Cómo ejecutar?

### Requisitos previos

- .NET 8 SDK
- SQL Server Express (o superior)
- Editor: Visual Studio 2022 o VS Code

### Paso 1 — Configurar la base de datos

Ejecutar los scripts SQL en SQL Server Management Studio (SSMS) en este orden:

```sql
-- 1. Crear tablas
Database/01_CrearTablas.sql

-- 2. Crear Stored Procedures
Database/02_StoredProcedures.sql

-- 3. Insertar datos de ejemplo
Database/03_DatosEjemplo.sql
```

### Paso 2 — Configurar connection string

Editar `AgendaMedica.API/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=AgendaMedicaDB;Trusted_Connection=true;TrustServerCertificate=true;"
  }
}
```

### Paso 3 — Ejecutar la API

```bash
cd AgendaMedica.API
dotnet run
```

La API estará disponible en `https://localhost:{puerto}/swagger` (una vez integrado Swagger).

---
### Diagrama de base de datos
![Diagrama BD V1](SQL/diagrama_bd_v1.png)
![Diagrama BD V2](SQL/diagrama_bd_v2.png)
---
## Licencia

Proyecto de evaluación técnica — uso interno.

## Nota final
El proyecto se encuentra en **desarrollo activo** y continuará evolucionando.

