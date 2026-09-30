# Sistema de Gestión de Citas Médicas

Aplicación de consola en **C# / .NET 8** para la actividad *Diseño de un Sistema de Gestión de Citas Médicas*, aplicando **SoC, DRY, KISS, YAGNI y SOLID**.

**Materia:** Programación II (Diseño Orientado a Objetos), ITLA — **Profesor:** Francis Ramírez

## Funcionalidades (primera versión)

| # | Funcionalidad | Dónde está |
|---|---|---|
| 1 | Registrar pacientes | `Services/PacienteService.cs` |
| 2 | Registrar médicos | `Services/MedicoService.cs` |
| 3 | Registrar especialidades | `Services/EspecialidadService.cs` |
| 4 | Asignar médicos a una especialidad | `Services/MedicoService.cs` |
| 5 | Agendar citas | `Services/AgendaCitasService.cs` |
| 6 | Consultar citas por paciente | `Services/ConsultaCitasService.cs` |
| 7 | Consultar citas por médico | `Services/ConsultaCitasService.cs` |
| 8 | Cancelar citas | `Services/CancelacionCitasService.cs` |
| 9 | Reprogramar citas | `Services/AgendaCitasService.cs` |
| 10 | Enviar recordatorio de cita | `Services/RecordatorioService.cs` + `Notificaciones/NotificadorCorreo.cs` |

No se incluyen facturación, recetas, historial clínico, seguros, telemedicina, chat ni IA: no fueron solicitados en esta versión (**YAGNI**).

## Estructura del proyecto

```
ClinicaCitas/
├── Models/            → Entidades: Persona, Paciente, Medico, Especialidad, Cita, EstadoCita
├── Interfaces/        → Contratos pequeños (ISP): IAgendaCitas, IConsultaCitas, ICancelacionCitas,
│                        IRecordatorioCitas, INotificador, IDisponibilidadAgenda, IRepositorio<T>...
├── Repositories/      → RepositorioEnMemoria<T> (almacenamiento)
├── Services/          → Lógica de negocio (una responsabilidad por clase)
├── Notificaciones/    → NotificadorCorreo (punto de extensión OCP)
├── Validaciones/      → Validador (reglas comunes, DRY)
├── Helpers/           → ConsoleHelper (entrada de datos y manejo de errores)
├── UI/                → MenuPrincipal y menús por módulo
└── Program.cs         → Punto de composición (DIP)
```

Flujo: **UI → Services → Repositories**. La UI nunca toca los datos directamente y los servicios no dependen de la consola.

## Principios aplicados

- **SoC**: cada capa en su carpeta; no existe una clase `SistemaCitasMedicas` que lo haga todo.
- **DRY**: `Validador` centraliza las validaciones, `RepositorioEnMemoria<T>` evita cuatro repositorios iguales, `DisponibilidadAgendaService` tiene la regla de horarios en un solo lugar y `ConsoleHelper.Ejecutar` el manejo de errores.
- **KISS**: consola, datos en memoria, cita de 30 minutos y un enum para el estado.
- **YAGNI**: solo lo solicitado; un único canal de recordatorio.
- **SRP**: agendar, consultar, cancelar, verificar disponibilidad y notificar son clases distintas.
- **OCP**: agregar SMS o WhatsApp es crear una clase que implemente `INotificador` y registrarla en `Program.cs`.
- **LSP**: `Paciente` y `Medico` sustituyen a `Persona`; cualquier `INotificador` sirve para `RecordatorioService`.
- **ISP**: interfaces pequeñas en vez de un `ICitaService` con todos los métodos.
- **DIP**: las clases reciben interfaces por constructor; solo `Program.cs` conoce las implementaciones.

## Reglas de negocio

- Horario de atención: 8:00 a. m. a 5:00 p. m., de lunes a sábado.
- La fecha de la cita debe ser futura.
- Duración de la consulta: 30 minutos.
- Un médico o un paciente no pueden tener dos citas que se solapen (evita las citas duplicadas).
- No se reprograma ni se notifica una cita cancelada.

## Cómo ejecutarlo

**Visual Studio 2022:** abrir `ClinicaCitas.sln` y presionar **F5**.

**Terminal:**

```bash
dotnet run --project ClinicaCitas
```

Formato de fecha para las citas: `dd/MM/yyyy HH:mm` (ejemplo: `30/09/2026 09:00`).

## Cómo extender el sistema

1. **Nuevo canal de recordatorio:** crear la clase en `Notificaciones/` que implemente `INotificador` y agregarla a la lista de `Program.cs`.
2. **Base de datos:** crear un repositorio que implemente `IRepositorio<T>` y cambiar una línea en `Program.cs`.
3. **Otra interfaz de usuario (web o API):** reutiliza los mismos servicios.
