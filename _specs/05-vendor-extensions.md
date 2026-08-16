# Vendor Extensions

## Overview

This specification outlines how third-party vendors can contribute content models and functionality to the Optimizely CMS solution without compromising the core architecture or creating conflicts with other vendors.

## Core Principles

The vendor extension architecture is built on these principles:

1. **Isolation**: Vendor code is isolated from core code and other vendor code
2. **Standardization**: All vendors follow common patterns and conventions
3. **Versioning**: Vendor extensions are versioned independently
4. **Discoverability**: Vendor content types are automatically discovered
5. **Governance**: Changes to vendor extensions follow governance rules

## Package Structure

Each vendor extension is structured as a standalone NuGet package:

```
VendorName.Extension/
├── src/
│   ├── Models/                  # Content models
│   │   ├── Pages/               # Page content types
│   │   ├── Blocks/              # Block content types
│   │   └── Media/               # Media content types
│   │
│   ├── Services/                # Service implementations
│   ├── Initialization/          # Initialization modules
│   └── Controllers/             # MVC controllers (if needed)
│
├── tests/                       # Tests for the extension
│
└── VendorName.Extension.csproj  # Project file
```

## Extension Points

Vendor extensions can integrate with the core system through several extension points:

1. **Content Types**: Define custom content types for pages, blocks, and media
2. **Service Implementations**: Implement core interfaces to provide custom functionality
3. **Initialization Modules**: Register services and configure the extension
4. **MVC Controllers and Views**: Handle requests and render content (for web-based extensions)

## Content Type Guidelines

Vendors must follow these guidelines when defining content types:

1. **Naming Convention**: Use a vendor-specific prefix for all content types
   ```csharp
   [ContentType(
       DisplayName = "VendorName Event Page",
       GUID = "vendor-specific-guid",
       GroupName = "VendorName")]
   public class VendorNameEventPage : PageData
   {
       // Properties
   }
   ```

2. **Stable GUIDs**: Each content type must have a stable GUID
3. **Proper Grouping**: Group content types under a vendor-specific group name
4. **Interface Implementation**: Implement core interfaces where appropriate
5. **Documentation**: Include XML documentation for all content types and properties

## Service Implementation Guidelines

For vendor-provided services:

1. **Interface Implementation**: Implement core interfaces defined in Foundation.Core
   ```csharp
   public class VendorNameEventService : IEventService
   {
       // Implementation of IEventService methods
   }
   ```

2. **Dependency Injection**: Register services during initialization
   ```csharp
   [InitializableModule]
   [ModuleDependency(typeof(ServiceContainerInitialization))]
   public class VendorNameInitialization : IConfigurableModule
   {
       public void ConfigureContainer(ServiceConfigurationContext context)
       {
           context.Services.AddTransient<IEventService, VendorNameEventService>();
       }
       
       // Other initialization methods
   }
   ```

3. **Scoped Registration**: Register services with the appropriate scope (transient, scoped, singleton)
4. **Limited Dependencies**: Minimize dependencies on other vendor extensions

## Integration with Core System

Vendor extensions integrate with the core system through:

1. **NuGet Package Reference**: Core CMS project references the vendor NuGet package
   ```xml
   <PackageReference Include="VendorName.Extension" Version="1.0.0" />
   ```

2. **Automatic Discovery**: Optimizely discovers content types and initializable modules
3. **Interface-Based Integration**: Core system interacts with vendor code through interfaces

## Content Ownership and Boundaries

To maintain clear boundaries:

1. **Content Folder Structure**: Vendor content is stored in vendor-specific folders
   ```
   Content Repository
   ├── Global
   ├── Start Page
   └── Vendors
       ├── VendorA
       ├── VendorB
       └── VendorC
   ```

2. **Permission Management**: Vendor editors are given access only to their content
3. **Content Type Availability**: Restrict where vendor content types can be created

## Vendor Extension Development Process

The development process for vendor extensions is as follows:

1. **Scaffolding**: Use provided scaffolding tools to create a new vendor extension
   ```
   dotnet new vendor-extension --name VendorName.Extension
   ```

2. **Development**: Develop content types, services, and other components
3. **Testing**: Test the extension against the core system
4. **Packaging**: Package the extension as a NuGet package
5. **Distribution**: Distribute the package through a package repository
6. **Installation**: Install the package in the core CMS

## Versioning and Compatibility

Vendor extensions follow semantic versioning:

1. **Major Version**: Breaking changes to content types or APIs
2. **Minor Version**: New features or non-breaking changes
3. **Patch Version**: Bug fixes

Extensions declare compatibility with core CMS versions:

```xml
<PackageReference Include="Foundation.Core" Version="[1.0.0,2.0.0)" />
```

## Resolving Conflicts

To resolve conflicts between vendor extensions:

1. **Namespace Isolation**: Each vendor has a unique namespace
2. **Unique Content Type Names**: Content type names include vendor prefix
3. **Dependency Injection**: Services are resolved by interface
4. **Conflict Detection**: Build-time validation to detect conflicts

## Example Vendor Extension

Example of a minimal vendor extension:

```csharp
using Foundation.Core.Models;
using EPiServer.Core;
using EPiServer.DataAnnotations;
using System.ComponentModel.DataAnnotations;

namespace VendorName.Extension.Models.Pages
{
    [ContentType(
        DisplayName = "VendorName Event Page",
        GUID = "11111111-2222-3333-4444-555555555555",
        GroupName = "VendorName")]
    public class VendorNameEventPage : PageData, ISearchable
    {
        [Display(
            Name = "Event Title",
            Description = "The title of the event",
            GroupName = SystemTabNames.Content,
            Order = 100)]
        public virtual string EventTitle { get; set; }
        
        [Display(
            Name = "Event Date",
            Description = "The date of the event",
            GroupName = SystemTabNames.Content,
            Order = 200)]
        public virtual DateTime EventDate { get; set; }
        
        [Display(
            Name = "Event Description",
            Description = "The description of the event",
            GroupName = SystemTabNames.Content,
            Order = 300)]
        public virtual XhtmlString EventDescription { get; set; }
    }
}
```

## Extension Testing Requirements

Each vendor extension must include:

1. **Unit Tests**: Tests for individual components
2. **Integration Tests**: Tests for integration with core system
3. **Content Type Tests**: Validation of content type definitions
4. **Migration Tests**: If applicable, tests for content migrations

## Continuous Integration and Deployment

Vendor extensions should include CI/CD workflows:

1. **Build Validation**: Validate the extension builds against the core system
2. **Test Execution**: Run all tests as part of the build
3. **Package Creation**: Create NuGet package as build artifact
4. **Versioning**: Automatically increment version based on changes

## Security Guidelines

Vendor extensions must adhere to security guidelines:

1. **Content Validation**: Validate all user-generated content
2. **SQL Injection Prevention**: Use parameterized queries
3. **XSS Prevention**: Encode output appropriately
4. **Authentication Respect**: Honor CMS authentication and authorization
5. **Sensitive Data Handling**: Follow best practices for sensitive data

## Governance and Review Process

All vendor extensions undergo a governance process:

1. **Documentation Review**: Ensure documentation is complete
2. **Code Review**: Review code for quality and security
3. **Content Type Review**: Validate content type definitions
4. **Architecture Review**: Ensure alignment with core architecture
5. **Performance Review**: Assess performance impact 