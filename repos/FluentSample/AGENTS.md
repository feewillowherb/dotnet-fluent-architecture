# Agent Behavior Guidelines

## OpenSpec Workflow (Optional)

For specification-driven development, refer to:

- **Explore** (`/opsx:explore`): Understand the problem and context
- **Propose** (`/opsx:propose`): Generate proposal, design, tasks, spec delta
- **Implement** (`/opsx:apply`): Implement code per Spec/Design
- **Archive** (`/opsx:archive`): Archive changes after completion

```
openspec/
├── specs/          <- Feature specifications
├── changes/        <- Normalized definitions of pending changes
└── project.md      <- Project conventions and tech stack
```

- List active changes: `openspec list`
- List feature specs: `openspec list --specs`
- Validate a change: `openspec validate [change-id] --strict`

## Project Conventions (Service Registration)

- **Service Registration**: Prefer **ABP integrated + implicit + AutoConstructor**.
  Service implementations implement ABP dependency interfaces (e.g., `ITransientDependency`, `ISingletonDependency`)
  and are annotated with `[AutoConstructor]`. ABP scans and registers them by convention without explicit
  registration in a Module or extension method.
- **Extension Method Registration**: Only consider centralized registration via extension methods when there is
  a genuine cross-module need to "integrate a group of services."

## Coding Standards (NON-NEGOTIABLE)

### .NET 10 & Modern C# Syntax

- **Target Framework**: Projects must target .NET 10 (`net10.0`).
- **Must Enable**:
  - `ImplicitUsings`: Set `<ImplicitUsings>enable</ImplicitUsings>` in project files.
  - `Nullable`: Set `<Nullable>enable</Nullable>` in project files.
- **Prefer Latest Syntax Sugar**:
  - **File-scoped Namespaces**: Prefer `namespace FluentSample.Core;` over brace namespaces.
  - **Collection Expressions (C# 12)**: Use `[]` instead of `new List<T>()` or array initializers.
  - **Primary Constructors (C# 12)**: Prefer primary constructors for simple classes.
  - **Using Alias for Types (C# 12)**: Use type aliases to simplify complex type references.
  - **Record Types**: Prefer `record` over traditional classes (DTOs, value objects, immutable data).
  - **Pattern Matching**: Fully leverage pattern matching to simplify conditional logic.
  - **Null-coalescing Operators**: Prefer `??` and `??=`.
  - **Expression-bodied Members**: Use expression bodies for simple properties and methods.
  - **String Interpolation**: Prefer string interpolation over string concatenation.
  - **Async/Await**: All async operations must use `async/await`; avoid blocking calls.
  - **IAsyncDisposable**: Implement `IAsyncDisposable` for resources requiring async cleanup.
- **Source Generators First**: Prefer AutoConstructor to reduce boilerplate code.
- **Struct First Principle**: For small, immutable value types (< 16-24 bytes), prefer `readonly struct` to reduce heap allocation and GC pressure.

### Code Character Constraints (NON-NEGOTIABLE)

- Variable names and fields must use English characters only. Non-English characters are prohibited in identifiers.
- Code must not contain non-English characters outside of comments.

### Naming Conventions (NON-NEGOTIABLE)

- Replace unknown prefixes like `My` with the project name `FluentSample`.
- Example: `MyDbContext` becomes `FluentSampleDbContext`.
- Applies to all class names, interface names, namespaces, and other identifiers.

### Interface & Implementation File Organization (NON-NEGOTIABLE)

- When a .cs file is under 1000 lines, the Interface may coexist with the Impl in the same file.
- The file is named after the Impl (e.g., `SampleItemService.cs` contains `ISampleItemService` and `SampleItemService`).
- Within the file, the Interface should appear before the Impl.

### Record Instead of Tuple (NON-NEGOTIABLE)

- Tuples (e.g., `(string, int)` or `ValueTuple`) are prohibited as return values or parameter types.
- Use `record` types instead, e.g., `record ItemInfo(string Name, int Quantity)`.
- Applies to method return values, parameters, local variables, and field definitions.

### Single Source of Truth (NON-NEGOTIABLE)

- Configuration defaults, business constants, and enum display text must be defined in a single location
  (static class, constants class, or resource file). Duplicate literals or magic values are prohibited.
- UI default display values must be consistent with the data source used for save/persistence.
- Before adding or modifying such data, verify whether a single source already exists; if not, create one.

### Implementation Plan Language (NON-NEGOTIABLE)

- Implementation plan documents (e.g., `plan.md`, `.cursor/plans/*.plan.md`) must be written in English.

## Design Pattern Principles

### Intention Revealing Interface

- Interface and method names should clearly express business intent, avoiding technical naming.
- Method names should reflect business operations, not implementation details.
- Prefer domain language (Ubiquitous Language) for naming.

### Information Expert Pattern

- Assign responsibilities to the class that has the information needed to fulfill them.
- Business logic should reside in the object that best understands the related data.
- Follow the "whoever owns the data, owns the operations" principle.

### Rich Domain Model

- Domain models should contain business logic and behavior, not just be data containers.
- Avoid anemic domain models; move business logic from service layer back to domain layer.
- Complex business logic should be coordinated through domain services.

### Command Method Pattern

- Method naming uses imperative verbs, e.g., `CreateOrder()`, `CancelOrder()`.
- Command methods perform a single-responsibility business operation.
- Query methods use query-style naming (e.g., `GetOrderById()`, `IsOrderValid()`).

### Single Responsibility Principle (NON-NEGOTIABLE)

- All code must have a clear responsibility; mixed responsibilities are prohibited.
- Each class, method, and module should have exactly one clear responsibility.

## Governance Rules

- The rules defined in this file take precedence over all other practices; modifications require documentation, approval, and a migration plan.
- All PRs/reviews must verify compliance; complexity must be justified.
- Test code must follow the same code character constraints and naming conventions as production code.
- Integration tests must use the unified test infrastructure.
