
# 📦 Task Manager API - Backend (.NET 9)

Esta es la Web API del proyecto **Task Manager**, desarrollada con **ASP.NET Core 9**, que permite la autenticación de usuarios y gestión de tareas (CRUD). Incluye conexión con PostgreSQL y buenas prácticas como AutoMapper, FluentValidation y documentación con Swagger.

---

## ⚙️ Tecnologías Usadas

- ASP.NET Core 9 Web API
- Entity Framework Core
- PostgreSQL
- AutoMapper
- FluentValidation
- Swagger
- JWT (para autenticación)

---

## 📁 Estructura del Proyecto

```
TaskManager.API/
│
├── Common/
│   └── Behaviors/       → Contiene transformación de respuestas de errores bad request 
├── Controllers/         → Controladores HTTP
├── Data/                → Contexto de EF
├── Dtos/                → Data Transfer Objects
├── Entities/            → Entidades de base de datos
├── Mappings/            → Configuración de AutoMapper
├── Migrations/          → Migraciones generadas
├── Services/            → Interfaces y servicios
├── Validators/          → Validaciones con FluentValidation
│
├── appsettings.json
├── libman.json
├── Program.cs          
├── TaskManager.http
```

---

## 🚀 Instalación y Configuración

### 1. Clona el repositorio

```bash
git clone https://github.com/lharguello/TaskManager.git
cd TaskManager
```

### 2. Configura `appsettings.json`

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=TaskManagerDb;Username=postgres;Password=tuPassword"
  },
  "Jwt": {
    "Key": "clave-super-secreta"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information"
    }
  },
  "AllowedHosts": "*"
}
```

### 3. Aplica las migraciones

```bash
dotnet ef database update
```

> Asegúrate de tener instalada la herramienta de EF Core CLI:  
> `dotnet tool install --global dotnet-ef`

### 4. Ejecuta la API
## Si cuentas con VisualStudio puedes correr el proyecto sin problema
```bash
dotnet run
```

La API estará disponible en: `https://localhost:5001`

---

## 🔐 Endpoints principales

### Autenticación

- `POST /auth/register` → Registro de usuario
- `POST /auth/login` → Inicio de sesión (retorna JWT)

### Tareas (Requieren JWT)

- `GET /tasks` → Listar tareas del usuario
- `POST /tasks` → Crear nueva tarea
- `PUT /tasks/{id}` → Editar tarea
- `DELETE /tasks/{id}` → Eliminar tarea

---

## 🧪 Herramientas útiles

- 🌐 Swagger UI: `https://localhost:5001/swagger/index.html`
- 🔁 Postman o Insomnia para probar endpoints
- 🔐 El token JWT debe enviarse en el header:
  ```
  Authorization: Bearer {token}
  ```

---

## 📚 Consideraciones

- AutoMapper se configura en `Mappings/MappingProfile.cs`.
- FluentValidation se aplica en `Validators/`.
- Las tareas están asociadas a un usuario autenticado.


