# Content Modeling

## Domain-Driven Content Modeling

The content models in the application are organized using domain-driven design principles, separating models based on their purpose and usage.

## Model Organization

Content models are separated into distinct categories based on their domain and how they'll be used across channels:

1. **Shared Models** - Core content types used across all channels
2. **Web Models** - Content types specific to web presentation
3. **App Models** - Content types designed for mobile/app experiences
4. **Portal Models** - Content types for administrative portals or specialized interfaces

This separation allows for clear boundaries between different content domains while allowing for content reuse where appropriate.

## Base Types and Interfaces

Rather than relying solely on inheritance, the content model architecture uses interfaces to define capabilities:

```csharp
// In Models.Shared
public interface IContentBlock : IContent 
{
    // Base properties for all content blocks
}

// In Models.Web
public interface ISiteContentBlock : IContentBlock 
{
    // Web-specific properties or capabilities
}

// In Models.App
public interface IAppContentBlock : IContentBlock 
{
    // App-specific properties or capabilities
}
```

This approach offers several advantages:
- Avoids deep inheritance hierarchies
- Provides flexibility in content modeling
- Enables clear content capabilities through interfaces
- Allows for UI descriptors to customize the editing experience
- Supports targeting content types to specific channels

## Content Type Identifiers

All content types must have stable identifiers to ensure content integrity during refactoring:

```csharp
[ContentType(
    DisplayName = "Article Page",
    GUID = "b11ee4bd-23ae-4582-a6c7-38959c85fd64",
    GroupName = GroupNames.Content)]
public class ArticlePage : PageData
{
    // Properties
}
```

The GUID ensures that the content type maintains its identity even if the class name or namespace changes.

## Property Organization

Properties within content types are organized by:

1. **GroupName**: Properties are grouped logically (e.g., "Content", "Metadata", "Navigation")
2. **Order**: Properties follow a consistent ordering pattern
3. **Display Attributes**: Properties have clear display names and descriptions for editors

Example:
```csharp
[Display(
    Name = "Main Content",
    Description = "The main content area",
    GroupName = GroupNames.Content,
    Order = 100)]
[AllowedTypes(typeof(IContentBlock))]
public virtual ContentArea? MainContent { get; set; }
```

## Content Evolution Strategy

To support iterative model development without content loss:

1. **Property Addition**: New properties can be added at any time without affecting existing content
2. **Property Refactoring**:
   - Mark deprecated properties with `[Obsolete]` and `[ScaffoldColumn(false)]`
   - Create new properties for the new model
   - Create data migration for transferring content

3. **Type Evolution**:
   - Create new content types when major changes are needed
   - Use Optimizely's content migration features to migrate content

Example migration approach:
```csharp
public class PropertyMigration : MigrationStep
{
    public override void Execute()
    {
        // Find all content of the specific type
        var contentRepository = ServiceLocator.Current.GetInstance<IContentRepository>();
        var contents = contentRepository.GetDescendents(ContentReference.RootPage)
            .Select(contentRepository.Get<IContent>)
            .Where(c => c is OldPageType);

        foreach (var content in contents)
        {
            // Copy data from old property to new property
            var oldPage = content as OldPageType;
            oldPage.NewProperty = oldPage.OldProperty;
            contentRepository.Save(oldPage, SaveAction.Publish);
        }
    }
}
```

## Cross-Channel Content Strategy

For content that needs to be delivered to multiple channels:

1. **Channel-Neutral Base**: Define base content types with properties relevant to all channels
2. **Channel-Specific Extensions**: Extend base types with channel-specific properties or create specialized types

3. **Content Delivery Strategy**:
   - Use Content Delivery API or Optimizely Content Graph to deliver to headless consumers
   - Use MVC rendering for traditional web delivery
   - Configure which content types are available through the Content Delivery API

## UI Descriptors for Interfaces

Use UI Descriptors to enhance the editor experience for interface-based content types:

```csharp
[UIDescriptorRegistration]
public class SiteContentBlockUIDescriptor : UIDescriptor<ISiteContentBlock>
{
    // UI customization for web content blocks
}
```

This provides a better editing experience by allowing editors to select from content types that implement a specific interface.

## Third-Party Content Type Integration

For third-party vendors adding content types:

1. **Namespacing**: Each vendor uses a distinct namespace for their content types
2. **Prefix**: Vendor content type names should include a vendor prefix
3. **GUID Stability**: Vendor content types must have stable GUIDs

4. **Boundaries**:
   - Each vendor defines content types in their own assembly
   - Vendor content types can implement common interfaces but should not modify base types
   - Content types from different vendors must not have conflicting names or GUIDs

## Content Type Discovery

Optimizely CMS scans all loaded assemblies to discover content types. This allows for:

1. Automatically registering content types from vendor extensions
2. Adding new content types without explicit registration
3. Establishing a pluggable architecture for content types

## Modeling for Optimizely Graph

When designing content types, special considerations for Optimizely Graph include:

1. **Clear Property Names**: Property names are exposed directly in GraphQL schema
2. **Type Compatibility**: Use Optimizely-supported property types for proper GraphQL mapping
3. **Nested Content**: Plan how content areas and references are expanded in GraphQL
4. **Filtering Support**: Design properties with filtering and sorting in mind

Example of interface implementation for a content capability:
```csharp
public interface ISeoData
{
    string MetaTitle { get; }
    string MetaDescription { get; }
    ContentReference MetaImage { get; }
}

[ContentType(DisplayName = "Article Page", GUID = "...")]
public class ArticlePage : PageData, ISeoData
{
    [Display(GroupName = "SEO")]
    public virtual string MetaTitle { get; set; }
    
    [Display(GroupName = "SEO")]
    public virtual string MetaDescription { get; set; }
    
    [Display(GroupName = "SEO")]
    public virtual ContentReference MetaImage { get; set; }
}
```

This approach allows for consistent SEO data across different content types.

## Performance Considerations

When designing content models, consider performance implications:

1. **Content Areas**: Deeply nested content areas can impact performance and should be limited
2. **Complex Properties**: Custom property types may require special handling for serialization
3. **Large Text Fields**: Very large text fields should be used judiciously
4. **Indexing**: Properties used for filtering or searching should be designed with indexing in mind

## Documentation Requirements

All content models should include:

1. **XML Documentation**: Full XML documentation for each type and property
2. **Purpose Description**: Clear description of the content type's purpose
3. **Preview Examples**: Example of how the content will be displayed

Example:
```csharp
/// <summary>
/// Represents an article page in the system.
/// </summary>
/// <remarks>
/// This page type is used for standard articles and blog posts.
/// It supports rich text content, images, and related articles.
/// </remarks>
[ContentType(DisplayName = "Article Page", GUID = "...")]
public class ArticlePage : PageData
{
    // Properties with documentation
}
``` 