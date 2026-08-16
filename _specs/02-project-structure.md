# Project Structure

## Multi-Repository Architecture

The solution follows a multi-repository approach to separate concerns and allow for independent development workflows:

1. **Core CMS Repository**: Contains the Optimizely CMS implementation, core content models, and infrastructure.
2. **Frontend Repository**: Contains the Next.js frontend application.
3. **Vendor Extension Repositories**: Separate repositories for vendor-specific extensions.

This separation allows for:
- Independent versioning and release cycles
- Clear ownership boundaries
- Simplified CI/CD pipelines
- Focused development environments (backend developers don't need Node.js installed)

## Core CMS Repository Structure

```
CoreCms/
├── src/
│   ├── [Company].Cms.Web/                # Web entry point
│   │   ├── Controllers/
│   │   ├── Views/
│   │   └── Program.cs
│   │
│   ├── [Company].Cms.Infrastructure/     # Shared infrastructure across all contexts
│   │   ├── DependencyInjection/          # Common service registrations
│   │   ├── Initialization/               # Startup initialization
│   │   └── Middleware/                   # Common middleware
│   │
│   ├── [Company].Cms.Web.Infrastructure/ # Web-specific infrastructure
│   │   ├── Initialization/
│   │   └── Middleware/
│   │
│   ├── [Company].Cms.Api/                # Core API shared components
│   │   ├── Controllers/                  # Base API controllers
│   │   ├── Filters/                      # Common API filters
│   │   └── Extensions/                   # Common API extensions
│   │
│   ├── [Company].Cms.Portal.Api/         # Portal-specific API entry point
│   │   ├── Controllers/
│   │   └── Program.cs
│   │
│   ├── [Company].Cms.App.Api/            # Mobile app-specific API entry point
│   │   ├── Controllers/
│   │   └── Program.cs
│   │
│   ├── [Company].Cms.Shared.Models/      # Shared across all channels
│   │   ├── Pages/
│   │   ├── Blocks/
│   │   ├── Media/
│   │   ├── Components/
│   │   └── Interfaces/
│   │
│   ├── [Company].Cms.Web.Features/       # Web-specific features as Razor Class Library
│   │   ├── News/
│   │   │   ├── Controllers/
│   │   │   ├── Views/
│   │   │   └── Models/
│   │   ├── Header/
│   │   │   ├── Components/
│   │   │   └── Models/
│   │   └── Metadata/
│   │       ├── Services/
│   │       └── Models/
│   │
│   ├── [Company].Cms.Portal.Features/    # Portal-specific features (API-focused)
│   │   └── Dashboard/
│   │       ├── Controllers/
│   │       └── Models/
│   │
│   ├── [Company].Cms.App.Features/       # App-specific features (API-focused)
│   │   └── Offers/
│   │       ├── Controllers/
│   │       └── Models/
│   │
│   ├── [Company].Cms.Shared.Features/    # Features shared across contexts
│   │   └── Search/                       # Example: Search used in Web, Portal and App
│   │       ├── Controllers/
│   │       ├── Services/
│   │       └── Models/
│   │
│   ├── [Company].Cms.Core.Services/      # Optimizely-dependent services
│   │   ├── ContentLoader/
│   │   ├── Search/
│   │   └── Caching/
│   │
│   ├── [Company].Cms.Common.Services/    # Framework-agnostic utility services
│   │   ├── Validation/
│   │   ├── Serialization/
│   │   └── Localization/
│   │
│   ├── [Company].Cms.Core.Settings/      # Settings handling per channel
│   │   ├── Models/
│   │   └── Services/
│   │
│   └── [Company].Cms.Api.Extensions/     # Custom Content Delivery API extensions
│       ├── Processors/
│       └── Models/
│
└── tests/
    ├── [Company].Cms.UnitTests/          # Unit tests
    ├── [Company].Cms.IntegrationTests/   # Integration tests
    └── [Company].Cms.Tests/              # Common test utilities
```

## Dependencies and Circular Reference Prevention

To maintain a clean architecture and prevent circular dependencies, the following dependency rules must be strictly enforced:

### Allowed Dependencies (→ = "references")

**Entry Point Projects:**
- `[Company].Cms.Web` → Web.Infrastructure, Web.Features, Shared.Features, Shared.Models, Core.Services, Core.Settings, Infrastructure
- `[Company].Cms.Portal.Api` → Api, Portal.Features, Shared.Features, Shared.Models, Core.Services, Core.Settings, Infrastructure
- `[Company].Cms.App.Api` → Api, App.Features, Shared.Features, Shared.Models, Core.Services, Core.Settings, Infrastructure

**Infrastructure Projects:**
- `[Company].Cms.Infrastructure` → Shared.Models, Core.Services, Core.Settings
- `[Company].Cms.Web.Infrastructure` → Infrastructure, Shared.Models, Core.Services, Core.Settings
- `[Company].Cms.Api` → Shared.Models, Core.Services, Core.Settings
- Never reference entry-point projects

**Features Projects:**
- `[Company].Cms.Web.Features` → Shared.Models, Shared.Features, Core.Services, Core.Settings
- `[Company].Cms.Portal.Features` → Shared.Models, Shared.Features, Core.Services, Core.Settings
- `[Company].Cms.App.Features` → Shared.Models, Shared.Features, Core.Services, Core.Settings
- `[Company].Cms.Shared.Features` → Shared.Models, Core.Services, Core.Settings

**Model Projects:**
- `[Company].Cms.Shared.Models` → No dependencies

**Service Projects:**
- `[Company].Cms.Core.Services` → Shared.Models, Common.Services, Core.Settings
- `[Company].Cms.Common.Services` → No dependencies (only external packages)

**Settings Project:**
- `[Company].Cms.Core.Settings` → Shared.Models only

**Content API Extensions:**
- `[Company].Cms.Api.Extensions` → Shared.Models, Optimizely packages only

**Test Project:**
- `[Company].Cms.Tests` → Can reference all other projects

### Rules for Preventing Circular Dependencies

1. **Directionality**: 
   - Always reference lower-level projects from higher-level projects
   - Never reference entry-point projects from lower-level projects

2. **Features Independence**:
   - Feature projects should only reference shared models, shared features, core services, and settings
   - Never reference other feature projects of the same or higher level directly
   - Web.Features, Portal.Features, and App.Features can reference Shared.Features

3. **Core Projects Isolation**:
   - Core services must remain independent of specific contexts (Web, Portal, App)
   - Only reference third-party frameworks, standard libraries, and shared models

4. **Infrastructure Clarity**:
   - Common infrastructure may depend on shared models, services, and settings
   - Context-specific infrastructure may depend on common infrastructure
   - Never depend on Web, App, or Portal projects

5. **Content API Extensions**:
   - Keep lightweight with focused responsibilities
   - Reference shared models and Optimizely packages only
   - Never reference higher-level application-specific projects

6. **Optimizely Package References**:
   - Projects that need Optimizely CMS packages should reference them directly
   - Non-CMS projects (Common.Services, Shared.Models) must remain CMS-package free

### Enforcement

These dependency rules must be enforced through:
- Code reviews
- Automated tools (e.g., NDepend, Visual Studio dependency validation)
- Clear documentation for development teams

This structure ensures:
- Clear separation of concerns
- Predictable, maintainable architecture
- Explicit technology boundaries
- Zero circular dependencies
- Scalable solution structure
- Proper context isolation with reuse where appropriate

## Models Organization

Content models are organized into two main categories:

1. **[Company].Cms.Shared.Models**: Common models used across all implementations (base pages, shared blocks, media types).

2. **Feature-Specific Models**: Models specific to features are located within their respective feature modules.

The shared models project follows a consistent internal structure:

```
[Company].Cms.Shared.Models/
├── Pages/                  # Page types
├── Blocks/                 # Block types
├── Media/                  # Media types
├── Components/             # Component models
└── Interfaces/             # Interfaces for content capabilities
```

Feature modules contain their own models directly related to the feature:

```
[Company].Cms.Web.Features/News/
├── Controllers/            # Controllers for this feature
├── Views/                  # Views for this feature
└── Models/                 # Models specific to this feature
    ├── NewsPage.cs
    ├── NewsListBlock.cs
    └── NewsViewModels.cs
```

## Frontend Repository Structure

```
Frontend/
├── src/
│   ├── components/         # React components
│   │   ├── shared/         # Components used across contexts
│   │   ├── web/            # Web-specific components
│   │   ├── portal/         # Portal-specific components
│   │   └── app/            # Mobile app specific components
│   │
│   ├── pages/              # Next.js pages
│   │   ├── web/            # Web-specific pages
│   │   ├── portal/         # Portal-specific pages
│   │   └── app/            # Mobile app specific pages
│   │
│   ├── lib/                # Utilities and API clients
│   ├── graphql/            # GraphQL queries and types
│   └── styles/             # CSS and styling
│
├── public/                 # Static assets
│
└── tests/                  # Frontend tests
```

## Vendor Extension Structure

Each vendor extension follows a standardized structure to ensure consistency:

```
VendorExtension/
├── src/
│   ├── Models/             # Vendor-specific content models
│   ├── Services/           # Vendor-specific services
│   └── Controllers/        # Vendor-specific controllers (if applicable)
│
└── tests/                  # Tests for vendor extension
```

## Project Naming Conventions

1. **Project Naming Format**: All projects follow the standard .NET naming convention:
   ```
   [Company].[Product].[Feature].[Layer].csproj
   ```
   Where:
   - `[Company]`: The company or client name (e.g., Acme)
   - `[Product]`: The product name (CMS for Optimizely CMS implementations)
   - `[Feature]`: The functional area or module (Web, Portal, App, Shared, Core)
   - `[Layer]`: The architectural layer (Models, Services, Infrastructure, Controllers)

   Examples:
   - `Acme.Cms.Shared.Models.csproj`
   - `Acme.Cms.Web.Models.csproj`
   - `Acme.Cms.Core.Services.csproj`
   - `Acme.Cms.Web.Infrastructure.csproj`
   - `Acme.Cms.Portal.Api.csproj`

2. **Namespace Structure**: All projects follow a consistent namespace structure matching the project name:
   - `[Company].[Product].[Feature].[Layer]`
   - Example: `Acme.Cms.Shared.Models`

3. **Assembly Naming**: Assembly names match the project name:
   - Example: `Acme.Cms.Shared.Models.dll`

4. **Content Type Naming**:
   - Page types: Suffix with "Page" (e.g., `ArticlePage`)
   - Block types: Suffix with "Block" (e.g., `HeroBlock`)
   - Vendor-specific types: Prefix with vendor name (e.g., `VendorAEventPage`)
   - Context-specific types: Consider adding context in name when appropriate (e.g., `PortalDashboardPage`)

## Context-Specific Considerations

1. **Web Context**:
   - Optimized for traditional content delivery
   - Feature set focused on content editing and presentation
   - Uses standard Razor views and controllers

2. **Portal Context**:
   - Focused on logged-in user experiences
   - API-driven with stateful interactions
   - Features include dashboards, user accounts, and personalized content

3. **App Context**:
   - Optimized for mobile app consumption
   - May include app-specific formats and optimizations
   - Often includes specialized endpoints for app-specific needs

4. **Custom Context**
   - Custom context is a context that is not Web, Portal, or App.
   - May be a custom implementation of a vendor extension or a custom implementation of a feature.
   - Example: Campaign Site, Corporate Site, Brand Portal, etc.

## Integration Model

Vendor extensions are integrated into the core CMS through:

1. **NuGet Packages**: Each vendor extension is packaged as a NuGet package.
2. **Dependency Injection**: Vendor services are registered through dependency injection.
3. **Content Type Discovery**: Optimizely automatically discovers content types from loaded assemblies.

## Development Environment

The development environment is designed to support:

1. **Independent Development**: Backend and frontend can be developed independently.
2. **Local Testing**: Ability to run the CMS locally and connect to it from frontend.
3. **Docker Support**: Docker configurations for consistent development environments.
4. **Scaffolding Tools**: CLI tools for generating new content types and components.
5. **Context Switching**: Easy configuration to switch between Web, Portal, and App contexts.

## Build and CI/CD

Each repository has its own CI/CD pipeline:

1. **Core CMS Pipeline**: Builds, tests, and packages the CMS and core models.
2. **Frontend Pipeline**: Builds and deploys the Next.js application.
3. **Vendor Extension Pipeline**: Builds, tests, and packages vendor extensions.

Integration tests ensure that vendor extensions work correctly with the core CMS. 

## Feature-Based Organization

The solution adopts a feature-based organization approach to improve modularity and maintainability:

1. **Vertical Slices**: Features are organized as vertical slices containing all components needed for that feature (controllers, views, models, services).

2. **Benefits of Feature-Based Organization**:
   - **Improved Cohesion**: Related code stays together regardless of technical layer
   - **Better Maintainability**: Features can be developed, tested, and deployed independently
   - **Clearer Ownership**: Teams can own entire features rather than technical layers
   - **Reduced Cross-Project References**: Fewer dependencies between projects

3. **Feature Module Structure**:
   - Each feature module (e.g., News, Dashboard, Offers) contains:
     - Controllers specific to the feature
     - Views/templates for the feature 
     - Models specific to the feature
     - Feature-specific services
   - Common/shared code remains in the appropriate shared projects

4. **Dependency Management**:
   - Feature modules depend only on shared models, core services, and settings
   - Feature modules never reference other feature modules directly
   - Entry-point projects reference feature modules they need
   - Shared features can be referenced by context-specific features

This approach moves away from traditional layer-based organization (separate projects for all models, all controllers, etc.) toward a more maintainable and scalable feature-based structure that properly accommodates the Web, Portal, and App contexts. 