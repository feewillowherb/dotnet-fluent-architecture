# Project Context

## Purpose

FluentSample is a reference project demonstrating AI-engineered .NET architecture patterns.
It showcases modern .NET 10 development practices including ABP Framework integration,
DDD patterns, ReactiveUI, and AI-assisted development workflows.

## Tech Stack

- **Language**: C# 13 / .NET 10.0
- **Architecture**: Clean Architecture with MVVM pattern
- **Database**: SQLite with Entity Framework Core 10.0.1
- **Dependency Injection**: Volo.Abp Autofac 10.0.1
- **Reactive Extensions**: System.Reactive 7.0.0-preview.1 (Rx.NET)
- **HTTP Client**: Refit 9.0.2 with Polly resilience policies
- **Logging**: Serilog 4.3.0

## Architecture Principles

### Core Architecture

- Clear layered architecture: UI (Avalonia/WPF/Console), Application Services, Domain, Infrastructure (EF Core/SQLite, Refit clients).
- Explicit layer responsibilities; cross-layer dependencies are prohibited (except via DTOs/interfaces).
- ABP Framework provides infrastructure (dependency injection, DDD, data access).

### Dependency Injection

- Unified IoC via Autofac (through ABP Autofac module).
- Use AutoConstructor source generator to reduce constructor boilerplate.
- Service implementations implement ABP dependency interfaces (`ITransientDependency`, `ISingletonDependency`).

### HTTP Client

- Unified type-safe REST client interfaces via Refit, integrated with `HttpClientFactory`.

### Data Access

- ABP EntityFrameworkCore Sqlite package for SQLite integration.
- DbContext inherits `AbpDbContext<TDbContext>`.
- Repository pattern via `IRepository<TEntity, TKey>`.

### Domain-Driven Design (DDD) & Entity Model

- ABP Domain package for DDD infrastructure.
- Entity base classes: `Entity<TKey>`, `FullAuditedEntity<TKey>`, `FullAuditedAggregateRoot<TKey>`.
- Domain services inherit `DomainService` or implement `IDomainService`.

## Project Structure

```
FluentSample/
├── src/
│   ├── FluentSample.App/                  # Main application entry point
│   │   ├── Program.cs
│   │   ├── FluentSampleAppModule.cs
│   │   └── appsettings.json
│   └── FluentSample.Core/                 # Core business logic and services
│       ├── FluentSampleCoreModule.cs
│       ├── Api/                           # Refit interfaces and DTOs
│       │   └── Dtos/
│       ├── Configuration/                 # Settings classes
│       ├── Entities/                      # Domain entities
│       │   └── Enums/
│       ├── EntityFrameworkCore/           # Database context and migrations
│       ├── Events/                        # ReactiveUI MessageBus messages
│       ├── Services/                      # Business services
│       ├── Utils/                         # Static factory methods
│       └── Providers/                     # DI factory services
├── tests/
│   └── FluentSample.Core.Tests/           # Unit and integration tests
├── openspec/                              # Specifications and change proposals
│   ├── project.md                         # This file
│   ├── specs/                             # Current capabilities (truth)
│   ├── changes/                           # Proposed changes
│   └── archive/                           # Completed changes
├── Directory.Build.props
├── Directory.Packages.props
├── CLAUDE.md
├── AGENTS.md
└── FluentSample.sln
```

## Development Guidelines

### When Adding Features

1. Check `openspec list --specs` for existing capabilities.
2. Create OpenSpec proposal for non-trivial changes.
3. Add services to `FluentSample.Core` for business logic.
4. Use dependency injection for service composition.
5. Write tests before or alongside implementation.

### When Fixing Bugs

1. Write reproducing test first.
2. Fix bug without breaking existing tests.
3. No OpenSpec proposal needed for bug fixes.
