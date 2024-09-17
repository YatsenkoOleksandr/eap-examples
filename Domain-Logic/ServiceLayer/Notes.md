# Service Layer

Service Layer - defines an application’s boundary with a layer of services that establishes a set of available operations and coordinates the application’s response in each operation.

Service Layer defines an application’s boundary [Cockburn PloP] and its set of available operations from the perspective of interfacing client layers.
It encapsulates the application’s business logic, controlling transactions and coordinating responsesr in the implementation of its operations.

Business logic is divided into 2 kinds:

- domain logic - to do purely with the domain problem (like revenue recognition calculating)
- application logic / workflow logic - to do with application responsibilities (like notifying integrated applications)

2 implementation variations:

- domain facade - service layer as a set of thin facades over a domain model. domain model implements all of the business logic;
- operation script - service layer directly implements application logic, but delegates to encapsulated domain object classes for domain logic.

Each such class forms an application “service,” and it’s common for service type names to end with “Service.”

First Law of Distributed Object Design - DO NOT DISTRIBUTE OBJECTS

Start with a locally invocable Service Layer whose method signatures deal in domain objects.
Add remotability when (if ever) need it by putting Remote Facades on Service Layer, or having Service Layer objects implement remote interfaces.

The starting point for identifying Service Layer operations is the use case model and the user interface design for the application.

Many of the use cases in an enterprise application are fairly boring “CRUD”.
There’s almost always a one-to-one correspondence between CRUD use cases and Service Layer operations.

The application’s responsibilities in carrying out these use cases, however, may be anything but boring. Validation aside, the creation, update, or deletion of a domain object in an application increasingly requires notification of other people and other integrated applications.
These responses must be coordinated, and transacted atomically, by Service Layer operations.

For small application - one abstraction, named after application itself.
For large applications - one abstraction per subsystem, named after the subsystem.

Another options:

- abstraction reflecting major partitions in a domain model, if these are different from the subsystem partitions (e.g. ContractsService)
- abstraction named after thematic application behaviors (e.g. RecognitionService)

The benefit of Service Layer is that it defines a common set of application operations available to many kinds of clients and it coordinates an application’s response in each operation.
The response may involve application logic that needs to be transacted atomically across multiple transactional resources.

You probably don’t need a Service Layer if your application’s business logic will only have one kind of client—say, a user interface—and its use case responses don’t involve multiple transactional resources.
In this case your Page Controllers can manually control transactions and coordinate whatever response is required, perhaps delegating directly to the Data Source layer.

## Example Notes

- Example uses 'Service Layer' + 'Domain Model' patterns, and omits database access.
- `ApplicationService` - Layer Supertype which provides gateway implementations.
- `RecognitionService` - Service layer which scripts the application logic of operation and delegates operations to domain object.
- `Contract` - Domain Model without implementation for the example simplicity.
- Gateway implementations do not have real implementation for the example simplicity.
