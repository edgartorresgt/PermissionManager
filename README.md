## Permissions Controller

The `PermissionsController` is responsible for handling the incoming HTTP requests related to the permissions. These permissions dictate what operations or access a given employee has within the system.

### Endpoints:

#### Request Permission:
- **URI:** `/api/permissions/request`
- **HTTP Method:** POST
- **Description:** This endpoint allows the user to request a specific type of permission for an employee.
- **Parameters:** 
  - `employeeId`: The ID of the employee for whom the permission is being requested.
  - `permissionTypeId`: The type of permission being requested.

#### Modify Permission:
- **URI:** `/api/permissions/modify`
- **HTTP Method:** PUT
- **Description:** Allows updating or modifying an existing permission.
- **Parameters:** 
  - `permissionId`: The ID of the existing permission to modify.
  - `newPermissionTypeId`: The new type of permission to assign.

#### Get Permissions:
- **URI:** `/api/permissions/{employeeId}`
- **HTTP Method:** GET
- **Description:** Retrieves all permissions associated with a specific employee.
- **Parameters:** 
  - `employeeId`: The ID of the employee whose permissions are being fetched.

---

## PermissionService

The `PermissionService` class provides the core business logic and operations related to permissions. It interfaces with the database, logging, and external systems like ElasticSearch and Kafka.

### Methods:

#### RequestPermission:
- **Parameters:** 
  - `employeeId`: The ID of the employee for whom the permission is being requested.
  - `permissionTypeId`: The type of permission being requested.
- **Description:** This method logs the request, creates a new permission object, and adds it to the database. Once persisted, it indexes this permission in ElasticSearch and sends a message to Kafka indicating a permission request operation.

#### ModifyPermission:
- **Parameters:** 
  - `permissionId`: The ID of the permission to be modified.
  - `newPermissionTypeId`: The new type of permission to assign.
- **Description:** Fetches the existing permission from the database. If it exists, the method updates its type, persists this change, then sends a modify operation message to Kafka.

#### GetPermissions:
- **Parameters:** 
  - `employeeId`: The ID of the employee whose permissions are to be retrieved.
- **Description:** Retrieves all permissions associated with a specific employee from the database. It then logs the number of permissions fetched and sends a get operation message to Kafka.

---

### Dependencies:

1. **UnitOfWork**: Used for managing database transactions. All CRUD operations related to permissions utilize the UnitOfWork to ensure atomicity and consistency.
2. **ElasticSearchService**: Used for indexing permissions to allow fast and scalable searches.
3. **KafkaProducerService**: Used to produce messages/events to a Kafka topic, useful for asynchronous operations or notifying other parts of the system.
4. **ILogger<PermissionService>**: Allows logging of important operations and potential issues during the execution of service methods.
5. **CQRS (Command Query Responsibility Segregation)**  In the context of our PermissionsController and PermissionService, this pattern can be observed.

---
## Database Setup Instructions

To set up the database for the PermissionManager project, you'll need to create the SQL database using Entity Framework Core migrations. Follow the step-by-step guide below:

### 1. Navigate to Project Root:
Ensure you're in the root directory of the project.  

```bash
cd path/to/your/project/root
```

### 2. Build the Project:
Before you can run migrations, build the project to ensure that there are no compilation errors.

```bash
dotnet build
```

### 3. Create Migration:
Now, create an initial migration for the database. This will generate the necessary code to set up your database schema.

```bash
dotnet ef migrations add InitialCreate --project PermissionManager.Models --startup-project PermissionManager.API
```

### 4. Update Database:
Finally, apply the migration to create or update the database.

```bash
dotnet ef database update --project PermissionManager.Models --startup-project PermissionManager.API
```

After running these commands, your SQL database should be set up and ready to use with the project's schema.

---

Review the connection string properly set in your `appsettings.json` file before running these commands. This ensures that the database is created at the right location with the correct configurations.
