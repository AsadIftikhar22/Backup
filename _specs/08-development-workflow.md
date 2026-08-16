# Development Workflow

## Overview

This specification outlines the development workflow for the Optimizely CMS solution, covering the development process, tools, and best practices to ensure efficient and consistent development.

## Development Environment Setup

### Requirements

Developers need the following tools and frameworks:

1. **Visual Studio 2022** or **Visual Studio Code** with the following extensions:
   - .NET Core tools
   - C# extensions
   - Optimizely CMS extensions

2. **.NET SDK 8.0** or higher

3. **Node.js** and **npm** (for frontend development)

4. **Docker** (for containerized development)

5. **Git** for version control

### Local Development Setup

The solution provides scripts for setting up the local development environment:

```powershell
# Clone the repositories
git clone https://github.com/organization/optimizely-cms-core.git
git clone https://github.com/organization/optimizely-cms-frontend.git

# Set up the backend
cd optimizely-cms-core
./setup-dev-environment.ps1

# Set up the frontend
cd ../optimizely-cms-frontend
npm install
```

### Docker Development Environment

For containerized development, Docker Compose is used to set up the environment:

```yaml
# docker-compose.yml
version: '3.8'
services:
  cms:
    build:
      context: ./optimizely-cms-core
      dockerfile: Dockerfile.dev
    ports:
      - "8000:80"
    environment:
      - ASPNETCORE_ENVIRONMENT=Development
      - ConnectionStrings__EPiServerDB=Server=db;Database=optimizely;User=sa;Password=YourPassword;
    volumes:
      - ./optimizely-cms-core:/app
      - cms-data:/app/App_Data
    depends_on:
      - db

  db:
    image: mcr.microsoft.com/mssql/server:2019-latest
    environment:
      - ACCEPT_EULA=Y
      - SA_PASSWORD=YourPassword
    ports:
      - "1433:1433"
    volumes:
      - db-data:/var/opt/mssql

  frontend:
    build:
      context: ./optimizely-cms-frontend
      dockerfile: Dockerfile.dev
    ports:
      - "3000:3000"
    volumes:
      - ./optimizely-cms-frontend:/app
    environment:
      - NEXT_PUBLIC_OPTIMIZELY_GRAPH_URL=http://localhost:8000/content-graph/

volumes:
  cms-data:
  db-data:
```

To start the development environment:

```bash
docker-compose up -d
```

## Development Workflow

### Feature Development Process

The development workflow follows these steps:

1. **Feature Planning**:
   - Define requirements
   - Create user stories
   - Design content models (if applicable)

2. **Branch Creation**:
   - Create a feature branch from the main branch
   - Follow the branch naming convention: `feature/ISSUE-123-short-description`

3. **Development**:
   - Implement the feature
   - Follow coding standards
   - Write tests

4. **Local Testing**:
   - Run unit tests
   - Test the feature locally

5. **Code Review**:
   - Submit a pull request
   - Address review comments

6. **Integration**:
   - Merge the feature into the main branch
   - Run integration tests

### Git Workflow

The solution follows the GitFlow workflow:

```
main
 |
 ├── develop
 |    |
 |    ├── feature/feature-1
 |    |
 |    └── feature/feature-2
 |
 ├── release/1.0.0
 |
 └── hotfix/critical-fix
```

1. **Main Branch**: Production-ready code
2. **Develop Branch**: Integration branch for features
3. **Feature Branches**: Individual features
4. **Release Branches**: Release preparations
5. **Hotfix Branches**: Emergency fixes for production

### Pull Request Process

Pull requests follow these guidelines:

1. **Title**: Clear and descriptive title referencing the issue
2. **Description**: Detailed description of the changes
3. **Linked Issues**: Link to related issues
4. **Reviewers**: At least two reviewers assigned
5. **Checks**: All automated checks must pass
6. **Approval**: Required approvals from reviewers

Example pull request template:

```markdown
## Description
[Brief description of the changes]

## Related Issues
- Fixes #123
- Addresses #456

## Type of Change
- [ ] Bug fix
- [ ] New feature
- [ ] Breaking change
- [ ] Documentation update

## How Has This Been Tested?
- [ ] Unit tests
- [ ] Integration tests
- [ ] Manual tests

## Checklist
- [ ] I have followed the coding standards
- [ ] I have added tests that prove my fix/feature works
- [ ] I have updated the documentation
- [ ] The code builds clean without warnings or errors
```

## Code Standards and Best Practices

### C# Coding Standards

The solution follows these C# coding standards:

1. **Naming Conventions**:
   - PascalCase for types, properties, methods
   - camelCase for local variables, parameters
   - _camelCase for private fields

2. **Code Organization**:
   - One class per file
   - Meaningful namespace structure
   - Logical folder organization

3. **Code Style**:
   - Use expression-bodied members where appropriate
   - Prefer pattern matching over casting
   - Use nullable reference types

4. **Documentation**:
   - XML documentation for public APIs
   - Clear and concise comments
   - Meaningful commit messages

### Frontend Development Standards

For Next.js development:

1. **Component Structure**:
   - One component per file
   - Functional components with hooks
   - TypeScript for type safety

2. **State Management**:
   - React Context for global state
   - React Query for API data

3. **Styling**:
   - CSS Modules or Styled Components
   - Mobile-first responsive design
   - Consistent theming

4. **Performance**:
   - Memoization for expensive computations
   - Code splitting for large components
   - Image optimization

## Content Model Development

Guidelines for developing content models:

1. **Planning Phase**:
   - Document the required content structure
   - Identify shared vs. channel-specific models
   - Create content model diagrams

2. **Implementation Phase**:
   - Create content type classes
   - Define properties and relationships
   - Write unit tests for models

3. **Review Phase**:
   - Conduct model review with team
   - Validate against governance rules
   - Check for naming consistency

4. **Testing Phase**:
   - Test content creation in CMS
   - Verify content delivery
   - Validate editorial experience

Example content model development:

```csharp
// Planning: Document requirements for an ArticlePage

// Implementation: Create the content type
[ContentType(
    DisplayName = "Article Page",
    GUID = "b11ee4bd-23ae-4582-a6c7-38959c85fd64",
    GroupName = GroupNames.Content)]
public class ArticlePage : PageData
{
    [Display(
        Name = "Heading",
        Description = "The main heading of the article",
        GroupName = SystemTabNames.Content,
        Order = 100)]
    public virtual string Heading { get; set; }
    
    [Display(
        Name = "Main Body",
        Description = "The main content of the article",
        GroupName = SystemTabNames.Content,
        Order = 200)]
    public virtual XhtmlString MainBody { get; set; }
}

// Review: Conduct model review

// Testing: Write unit tests
[Fact]
public void ArticlePage_HasRequiredProperties()
{
    // Test code
}
```

## Development Tools

### Code Analysis

The solution uses code analysis tools:

1. **StyleCop**: Enforces coding standards
2. **SonarQube**: Detects code quality issues
3. **EditorConfig**: Ensures consistent formatting

Example EditorConfig:

```ini
# .editorconfig
root = true

[*]
indent_style = space
indent_size = 4
end_of_line = lf
charset = utf-8
trim_trailing_whitespace = true
insert_final_newline = true

[*.{json,yml,yaml,csproj}]
indent_size = 2

[*.cs]
csharp_style_expression_bodied_methods = when_on_single_line:suggestion
csharp_style_expression_bodied_properties = true:suggestion
```

### Continuous Integration

The CI pipeline includes:

1. **Build**: Compile the solution
2. **Test**: Run automated tests
3. **Analysis**: Run code analysis
4. **Package**: Create NuGet packages
5. **Deploy**: Deploy to development environment

Example GitHub Actions workflow:

```yaml
name: CI/CD

on:
  push:
    branches: [ main, develop ]
  pull_request:
    branches: [ main, develop ]

jobs:
  build:
    runs-on: ubuntu-latest
    steps:
    - uses: actions/checkout@v3
    - name: Setup .NET
      uses: actions/setup-dotnet@v3
      with:
        dotnet-version: 8.0.x
    - name: Restore dependencies
      run: dotnet restore
    - name: Build
      run: dotnet build --no-restore
    - name: Test
      run: dotnet test --no-build --verbosity normal
    - name: Analyze
      run: dotnet-sonarscanner begin /k:"optimizely-cms"
    - name: Package
      if: github.event_name != 'pull_request'
      run: dotnet pack --no-build --output ./packages
    - name: Publish
      if: github.ref == 'refs/heads/main'
      run: dotnet nuget push ./packages/*.nupkg --api-key ${{ secrets.NUGET_API_KEY }}
```

## Debugging and Troubleshooting

### Debugging Tools

The solution includes several debugging tools:

1. **Logging**: Structured logging with Serilog
2. **Exception Handling**: Global exception handling middleware
3. **Diagnostic Pages**: Development-only diagnostic pages
4. **Performance Monitoring**: Application insights integration

Example logging configuration:

```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddLogging(builder =>
    {
        builder.AddSerilog(new LoggerConfiguration()
            .MinimumLevel.Information()
            .WriteTo.Console()
            .WriteTo.File("logs/app.log", rollingInterval: RollingInterval.Day)
            .CreateLogger());
    });
}
```

### Common Troubleshooting Procedures

Guidelines for troubleshooting common issues:

1. **Content Type Errors**:
   - Check for duplicate GUIDs
   - Verify inheritance hierarchy
   - Ensure properties have correct types

2. **Content Delivery API Issues**:
   - Verify API configuration
   - Check CORS settings
   - Validate authentication settings

3. **Integration Issues**:
   - Check service registrations
   - Verify dependency versions
   - Review initialization sequence

## Documentation

### Code Documentation

All code should include:

1. **XML Documentation**: Documentation for public APIs
2. **Comments**: Explanatory comments for complex code
3. **README Files**: Instructions for each project
4. **Architecture Diagrams**: Visualizations of the architecture

Example XML documentation:

```csharp
/// <summary>
/// Represents an article page in the CMS.
/// </summary>
/// <remarks>
/// This page type is used for standard articles and blog posts.
/// It supports rich text content, images, and related articles.
/// </remarks>
[ContentType(DisplayName = "Article Page", GUID = "...")]
public class ArticlePage : PageData
{
    /// <summary>
    /// Gets or sets the main heading of the article.
    /// </summary>
    public virtual string Heading { get; set; }
    
    /// <summary>
    /// Gets or sets the main content of the article.
    /// </summary>
    public virtual XhtmlString MainBody { get; set; }
}
```

### Changelog Maintenance

Each project maintains a changelog:

```markdown
# Changelog

## [1.0.0] - 2023-07-01
### Added
- Initial release
- Basic content types
- Content Delivery API configuration

## [1.1.0] - 2023-08-15
### Added
- New ArticlePage content type
- Support for nested content areas

### Changed
- Improved Content Delivery API performance
- Updated to Optimizely CMS 12.15.0
```

## Release Management

### Versioning

The solution follows semantic versioning:

1. **Major Version**: Breaking changes
2. **Minor Version**: New features
3. **Patch Version**: Bug fixes

### Release Process

The release process includes:

1. **Version Bump**: Update version numbers
2. **Changelog Update**: Document changes
3. **Release Branch**: Create a release branch
4. **Release Build**: Build and package the release
5. **Deployment**: Deploy to staging
6. **Testing**: Test the release
7. **Production Deployment**: Deploy to production
8. **Tag**: Tag the release

Example release script:

```powershell
# Update version
$version = "1.0.0"
$releaseDate = Get-Date -Format "yyyy-MM-dd"

# Update changelog
$changelogPath = "./CHANGELOG.md"
$changelog = Get-Content $changelogPath -Raw
$newChangelog = "# Changelog`n`n## [$version] - $releaseDate`n$releaseNotes`n`n$changelog"
Set-Content -Path $changelogPath -Value $newChangelog

# Create release branch
git checkout -b release/$version
git add .
git commit -m "Release $version"
git push origin release/$version

# Build and package
dotnet build --configuration Release
dotnet pack --output ./packages

# Create tag
git tag -a v$version -m "Release $version"
git push origin v$version
```

## Vendor Development Integration

### Vendor Development Process

Vendors follow these steps to develop extensions:

1. **Scaffolding**: Use scaffolding tools to create extension
2. **Development**: Develop content types and services
3. **Testing**: Test against the core system
4. **Packaging**: Package as NuGet package
5. **Publishing**: Publish to NuGet feed
6. **Integration**: Integrate into core solution

### Vendor Development Guidelines

Guidelines for vendor development:

1. **Namespace Conventions**: Use unique namespace
2. **Content Type Naming**: Use vendor-specific prefix
3. **GUID Management**: Use assigned GUID range
4. **Documentation**: Provide complete documentation
5. **Testing**: Include comprehensive tests

Example vendor development:

```csharp
// Scaffolding: Generate vendor extension

// Development: Create content types
namespace VendorName.Extension.Models.Pages
{
    [ContentType(
        DisplayName = "VendorName Event Page",
        GUID = "vendor-assigned-guid",
        GroupName = "VendorName")]
    public class VendorNameEventPage : PageData
    {
        // Properties
    }
}

// Testing: Write tests

// Packaging: Create NuGet package
// dotnet pack -c Release

// Publishing: Publish to NuGet feed
// dotnet nuget push ./bin/Release/VendorName.Extension.1.0.0.nupkg -s https://nuget.org
```

## Knowledge Sharing and Collaboration

### Documentation Repository

All documentation is maintained in a central repository:

```
docs/
├── architecture/
│   ├── overview.md
│   ├── content-models.md
│   └── integration.md
├── development/
│   ├── setup.md
│   ├── workflow.md
│   └── best-practices.md
├── operations/
│   ├── deployment.md
│   ├── monitoring.md
│   └── troubleshooting.md
└── governance/
    ├── content-model-governance.md
    ├── vendor-integration.md
    └── release-management.md
```

### Code Reviews

Guidelines for effective code reviews:

1. **Focus Areas**:
   - Code correctness
   - Adherence to standards
   - Performance considerations
   - Security implications

2. **Review Process**:
   - Review in small batches
   - Provide constructive feedback
   - Focus on education, not criticism
   - Use automated tools for style issues

3. **Review Checklist**:
   - Does the code meet requirements?
   - Does it follow standards?
   - Are there appropriate tests?
   - Is the documentation complete?

### Team Collaboration

Tools and practices for team collaboration:

1. **Communication Channels**:
   - Daily standups
   - Weekly architecture meetings
   - Monthly governance reviews

2. **Knowledge Sharing**:
   - Technical presentations
   - Pair programming
   - Code review sessions
   - Documentation updates 