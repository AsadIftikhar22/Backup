# Testing Strategy

## Overview

This specification outlines the testing strategy for the Optimizely CMS solution, covering the different types of tests, testing tools, and best practices for ensuring high-quality code and content models.

## Testing Principles

The testing strategy is guided by these principles:

1. **Comprehensive Coverage**: Tests cover all critical aspects of the system
2. **Automation**: Tests are automated wherever possible
3. **Fast Feedback**: Tests provide quick feedback during development
4. **Isolation**: Tests are isolated and do not depend on external systems
5. **Maintainability**: Tests are easy to maintain and understand

## Test Types

The solution implements several types of tests:

### Unit Tests

Unit tests verify the behavior of individual components in isolation:

```csharp
[Fact]
public void ArticlePage_HasRequiredProperties()
{
    // Arrange
    var articlePage = new ArticlePage();
    
    // Act
    var properties = articlePage.GetType().GetProperties();
    
    // Assert
    Assert.Contains(properties, p => p.Name == "Heading");
    Assert.Contains(properties, p => p.Name == "MainBody");
}
```

Key characteristics:
- Fast execution
- No dependencies on external systems
- Focus on business logic and content model behavior

### Integration Tests

Integration tests verify the interaction between components:

```csharp
[Fact]
public void ContentDeliveryApi_ReturnsCorrectContentModel()
{
    // Arrange
    var client = new TestApiClient(_factory);
    
    // Act
    var response = client.GetAsync("/api/content/article/1").Result;
    var content = JsonConvert.DeserializeObject<ArticleModel>(response.Content);
    
    // Assert
    Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    Assert.Equal("Test Article", content.Heading);
}
```

Key characteristics:
- Test components working together
- May use in-memory databases or test doubles
- Verify correct integration with Optimizely API

### Content Model Tests

Content model tests specifically verify content type definitions:

```csharp
[Fact]
public void AllContentTypes_HaveUniqueGuids()
{
    // Arrange
    var contentTypes = GetAllContentTypes();
    var guids = new HashSet<string>();
    
    // Act & Assert
    foreach (var type in contentTypes)
    {
        var attr = type.GetCustomAttribute<ContentTypeAttribute>();
        Assert.NotNull(attr);
        
        var guid = attr.GUID;
        Assert.False(string.IsNullOrEmpty(guid));
        Assert.True(guids.Add(guid), $"Duplicate GUID found: {guid} on type {type.Name}");
    }
}
```

Key characteristics:
- Verify content type attributes
- Check for proper inheritance and interfaces
- Validate content model conventions

### End-to-End Tests

End-to-end tests verify the complete user journey:

```csharp
[Fact]
public async Task CanCreateAndPublishArticlePage()
{
    // Arrange
    var browser = new TestBrowser();
    await browser.LoginAsEditor();
    
    // Act
    await browser.GoToPage("/EPiServer/CMS/Content/");
    await browser.ClickOn("Create Content");
    await browser.SelectContentType("Article Page");
    await browser.FillIn("Heading", "Test Article");
    await browser.ClickOn("Save and Publish");
    
    // Assert
    var status = await browser.GetElementText(".status-message");
    Assert.Contains("successfully published", status);
    
    var article = await browser.GoToPage("/test-article");
    var heading = await browser.GetElementText("h1");
    Assert.Equal("Test Article", heading);
}
```

Key characteristics:
- Test complete user flows
- Verify both editorial and public-facing functionality
- May use UI automation tools like Playwright or Selenium

### Headless Frontend Tests

Tests for the Next.js frontend:

```javascript
import { render, screen } from '@testing-library/react';
import ArticlePage from '../components/pages/ArticlePage';

describe('ArticlePage', () => {
  it('renders article heading and content', () => {
    // Arrange
    const mockData = {
      heading: 'Test Article',
      mainBody: { html: '<p>Test content</p>' },
      publishDate: '2023-06-15T10:00:00Z'
    };
    
    // Act
    render(<ArticlePage content={mockData} />);
    
    // Assert
    expect(screen.getByRole('heading')).toHaveTextContent('Test Article');
    expect(screen.getByText('Test content')).toBeInTheDocument();
  });
});
```

Key characteristics:
- Test React components
- Verify correct rendering of content
- Test user interactions

## Testing Tools

The following tools are used for testing:

### Backend Testing

1. **xUnit**: Primary testing framework for .NET code
2. **Moq**: Mocking framework for creating test doubles
3. **FluentAssertions**: Fluent assertions for more readable tests
5. **AutoFixture**: Tool for creating test data

### Frontend Testing

1. **Jest**: JavaScript testing framework
2. **React Testing Library**: Testing utility for React components
3. **Cypress**: End-to-end testing tool for web applications
4. **MSW (Mock Service Worker)**: API mocking for frontend tests

## Test Organization

Tests are organized following the same structure as the production code:

```
tests/
├── Unit/
│   ├── Models/
│   │   ├── Shared/
│   │   ├── Web/
│   │   └── App/
│   ├── Services/
│   └── Infrastructure/
├── Integration/
│   ├── ContentDelivery/
│   ├── ContentRepository/
│   └── Services/
└── E2E/
    ├── Editorial/
    └── Public/
```

Each test project targets a specific production project:

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>
    <IsPackable>false</IsPackable>
  </PropertyGroup>
  
  <ItemGroup>
    <PackageReference Include="Microsoft.NET.Test.Sdk" Version="17.6.0" />
    <PackageReference Include="xunit" Version="2.4.2" />
    <PackageReference Include="xunit.runner.visualstudio" Version="2.4.5" />
  </ItemGroup>
  
  <ItemGroup>
    <ProjectReference Include="..\..\src\Models\Models.Web\Models.Web.csproj" />
  </ItemGroup>
</Project>
```

## Test Data Management

The solution uses several approaches for test data:

1. **In-Memory Content Repository**: For testing content operations without a database
   ```csharp
   [Fact]
   public void CanSaveArticlePage()
   {
       // Arrange
       var repository = new InMemoryContentRepository();
       var article = new ArticlePage { Heading = "Test Article" };
       
       // Act
       var reference = repository.Save(article, SaveAction.Publish);
       
       // Assert
       var saved = repository.Get<ArticlePage>(reference);
       Assert.Equal("Test Article", saved.Heading);
   }
   ```

2. **Fake Content Factory**: For creating content hierarchies
   ```csharp
   [Fact]
   public void CanNavigateContentHierarchy()
   {
       // Arrange
       var factory = new ContentFactory();
       var root = factory.CreatePage<StartPage>();
       var section = factory.CreatePage<SectionPage>(root.ContentLink);
       var article = factory.CreatePage<ArticlePage>(section.ContentLink);
       
       // Act
       var parent = factory.GetParent(article.ContentLink);
       
       // Assert
       Assert.Equal(section.ContentLink, parent.ContentLink);
   }
   ```

3. **Test Content Database**: For integration tests that require a database
   ```csharp
   public class ContentRepositoryTests : IClassFixture<DatabaseFixture>
   {
       private readonly DatabaseFixture _fixture;
       
       public ContentRepositoryTests(DatabaseFixture fixture)
       {
           _fixture = fixture;
       }
       
       [Fact]
       public void CanQueryContentByLanguage()
       {
           // Test with database
       }
   }
   ```

## Continuous Integration

Tests are integrated into the CI/CD pipeline:

1. **Build Validation**: All tests run on pull requests
2. **Test Reports**: Test results are published as build artifacts
3. **Code Coverage**: Coverage reports generated during test runs
4. **Performance Tests**: Scheduled performance tests for critical paths

Example GitHub workflow:

```yaml
name: CI

on:
  push:
    branches: [ main ]
  pull_request:
    branches: [ main ]

jobs:
  test:
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
      run: dotnet test --no-build --verbosity normal --collect:"XPlat Code Coverage"
    - name: Upload coverage reports
      uses: codecov/codecov-action@v3
```

## Testing Vendor Extensions

Vendor extensions require specific testing approaches:

1. **Isolated Testing**: Each vendor extension is tested in isolation
2. **Integration Testing**: Vendor extensions are tested with the core system
3. **Compatibility Testing**: Verify compatibility with different CMS versions
4. **Security Testing**: Focused security testing for vendor code

Example vendor extension test:

```csharp
[Fact]
public void VendorExtension_RegistersContentTypes()
{
    // Arrange
    var services = new ServiceCollection();
    
    // Act
    var startup = new VendorExtensionStartup();
    startup.ConfigureServices(services);
    
    var provider = services.BuildServiceProvider();
    var contentTypeRepository = provider.GetService<IContentTypeRepository>();
    
    // Assert
    var vendorTypes = contentTypeRepository.List()
        .Where(t => t.Name.StartsWith("VendorA"));
    
    Assert.NotEmpty(vendorTypes);
}
```

## Content Delivery API Testing

Specific tests for the Content Delivery API:

```csharp
[Fact]
public async Task ContentDeliveryApi_ReturnsCorrectModel()
{
    // Arrange
    var client = _factory.CreateClient();
    
    // Act
    var response = await client.GetAsync("/api/episerver/v2.0/content/12?expand=*");
    response.EnsureSuccessStatusCode();
    
    var content = await response.Content.ReadAsStringAsync();
    var article = JsonConvert.DeserializeObject<ApiArticleModel>(content);
    
    // Assert
    Assert.Equal("Test Article", article.Name);
    Assert.NotNull(article.MainBody);
}
```

## Next.js Frontend Testing

Testing strategy for the Next.js frontend:

1. **Component Tests**: Test individual React components
2. **Page Tests**: Test Next.js pages
3. **API Tests**: Test API routes
4. **End-to-End Tests**: Test complete user journeys

Example Next.js page test:

```javascript
import { render, screen } from '@testing-library/react';
import { getStaticProps } from '../../pages/articles/[slug]';
import ArticlePage from '../../pages/articles/[slug]';
import { mockGraphQLClient } from '../mocks/graphql';

// Mock the GraphQL client
jest.mock('../../lib/graphql-client', () => ({
  graphqlClient: mockGraphQLClient
}));

describe('Article Page', () => {
  it('renders article data', async () => {
    // Arrange
    const mockArticle = {
      heading: 'Test Article',
      mainBody: { html: '<p>Test content</p>' }
    };
    
    mockGraphQLClient.request.mockResolvedValue({
      article: { items: [mockArticle] }
    });
    
    // Act
    const { props } = await getStaticProps({ params: { slug: 'test-article' } });
    render(<ArticlePage {...props} />);
    
    // Assert
    expect(screen.getByRole('heading')).toHaveTextContent('Test Article');
    expect(screen.getByText('Test content')).toBeInTheDocument();
  });
});
```

## Performance Testing

Performance testing focuses on:

1. **Page Load Time**: Time to load different page types
2. **API Response Time**: Response time for API requests
3. **Content Publishing Performance**: Time to publish content
4. **Search Performance**: Performance of content search operations

Example performance test:

```csharp
[Fact]
public async Task ContentDeliveryApi_PerformanceTest()
{
    // Arrange
    var client = _factory.CreateClient();
    var stopwatch = new Stopwatch();
    
    // Act
    stopwatch.Start();
    var response = await client.GetAsync("/api/episerver/v2.0/content/12?expand=*");
    stopwatch.Stop();
    
    // Assert
    Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    Assert.True(stopwatch.ElapsedMilliseconds < 200, 
        $"API response took {stopwatch.ElapsedMilliseconds}ms, which exceeds the 200ms threshold");
}
```

## Test Documentation

Each test project includes:

1. **Test Plan**: Overview of what is being tested
2. **Test Cases**: Detailed test cases
3. **Test Coverage Report**: Report on test coverage
4. **Test Results**: Summary of test results

## Test Best Practices

Best practices for testing:

1. **Arrange-Act-Assert**: Structure tests with clear sections
2. **One Assertion Per Test**: Focus each test on a single behavior
3. **Descriptive Test Names**: Use descriptive names for tests
4. **Avoid Test Interdependencies**: Tests should be independent
5. **Clean Test Data**: Clean up test data after each test
6. **Avoid Magic Strings**: Use constants for test values
7. **Test Edge Cases**: Test boundary conditions and error cases
8. **Keep Tests Fast**: Optimize tests for quick execution 