# Headless Integration

## Overview

This specification outlines how the Optimizely CMS solution supports headless content delivery through the Content Delivery API and Optimizely Graph, with a specific focus on integration with Next.js as a frontend framework.

## Headless-First Approach

The solution follows a headless-first approach, ensuring that content can be consumed by any frontend application:

1. **API-Driven Development**: Content is designed to be consumed via APIs from the beginning
2. **Content Model Independence**: Content models are not tied to presentation
3. **Multiple Channel Support**: Content can be delivered to web, mobile, and other channels

## Content Delivery API Configuration

The solution configures Optimizely's Content Delivery API with the following settings:

```csharp
services.ConfigureContentApiOptions(options =>
{
    options.IncludeInternalContentRoots = true;
    options.IncludeSiteHosts = true;
    options.RichTextFormat = RichTextFormat.HtmlAndStructured; // Support both formats
    options.FlattenPropertyModel = true;
});

services.AddContentDeliveryApi(options =>
{
    options.SiteDefinitionApiEnabled = true;
    options.DisableScopeValidation = true;
})
.WithSiteBasedCors();
```

These configurations ensure:
- Content is accessible via API
- Rich text content is available in both HTML and structured formats
- Property models are flattened for easier consumption
- CORS is properly configured for cross-domain access

## Optimizely Graph Integration

Optimizely Graph (GraphQL) is enabled from the beginning of the project:

```csharp
services.AddContentGraph(options =>
{
    options.IncludeInheritanceInContentType = true;
    options.SyncReferencingContents = true;
});
```

Benefits of including Graph from day one:
1. **Flexible Content Querying**: GraphQL allows clients to request only the data they need
2. **Type-Safe Schema**: Content types are automatically reflected in the GraphQL schema
3. **Unified Content Access**: All content is available through a single endpoint
4. **Enhanced Performance**: Optimized for querying and filtering content

## Next.js Frontend Integration

### GraphQL Client Configuration

The Next.js frontend uses a GraphQL client to consume content:

```typescript
// lib/graphql-client.ts
import { GraphQLClient } from 'graphql-request';

export const graphqlClient = new GraphQLClient(
  process.env.NEXT_PUBLIC_OPTIMIZELY_GRAPH_URL as string,
  {
    headers: {
      'x-api-key': process.env.OPTIMIZELY_GRAPH_API_KEY as string,
    },
  }
);
```

### Content Fetching Strategy

Different approaches for content fetching:

1. **Static Generation (SSG)**:
   - Content is fetched at build time
   - Pages are pre-rendered and cached
   - Good for content that doesn't change frequently

   ```typescript
   // Example of Static Generation with getStaticProps
   export async function getStaticProps({ params }) {
     const { page } = await graphqlClient.request(GET_PAGE_BY_URL, {
       url: `/${params.slug.join('/')}/`,
     });
     
     return {
       props: { page },
       // Revalidate every hour
       revalidate: 3600,
     };
   }
   
   export async function getStaticPaths() {
     const { pages } = await graphqlClient.request(GET_ALL_PAGES);
     
     return {
       paths: pages.map(page => ({
         params: { slug: page.url.split('/').filter(Boolean) },
       })),
       fallback: 'blocking',
     };
   }
   ```

2. **Server-Side Rendering (SSR)**:
   - Content is fetched at request time
   - Pages are rendered on each request
   - Good for personalized or frequently changing content

   ```typescript
   // Example of Server-Side Rendering with getServerSideProps
   export async function getServerSideProps({ params, req }) {
     const { page } = await graphqlClient.request(GET_PAGE_BY_URL, {
       url: `/${params.slug.join('/')}/`,
     });
     
     return {
       props: { page },
     };
   }
   ```

3. **Incremental Static Regeneration (ISR)**:
   - Pages are statically generated but can be regenerated in the background
   - Combines benefits of SSG and SSR
   - Good for most content scenarios

### Preview Mode

The solution supports preview mode for editors:

```typescript
// pages/api/preview.ts
export default async function handler(req, res) {
  const { slug, secret } = req.query;
  
  // Check the secret
  if (secret !== process.env.PREVIEW_SECRET) {
    return res.status(401).json({ message: 'Invalid token' });
  }
  
  // Enable Preview Mode
  res.setPreviewData({});
  
  // Redirect to the page
  res.redirect(`/${slug}`);
}

// In page component
export async function getStaticProps({ params, preview }) {
  const query = preview ? GET_PAGE_BY_URL_PREVIEW : GET_PAGE_BY_URL;
  
  const { page } = await graphqlClient.request(query, {
    url: `/${params.slug.join('/')}/`,
  });
  
  return {
    props: { page, preview: !!preview },
    revalidate: 60,
  };
}
```

### Content Revalidation

For real-time content updates:

1. **Webhook-Triggered Revalidation**:
   ```typescript
   // pages/api/revalidate.ts
   export default async function handler(req, res) {
     // Verify webhook secret
     if (req.headers['x-webhook-secret'] !== process.env.WEBHOOK_SECRET) {
       return res.status(401).json({ message: 'Invalid token' });
     }
     
     const { contentId, path } = req.body;
     
     try {
       // Revalidate the specific path
       await res.revalidate(path);
       return res.json({ revalidated: true });
     } catch (err) {
       return res.status(500).send('Error revalidating');
     }
   }
   ```

2. **Optimizely Content Events to Trigger Webhooks**:
   ```csharp
   public class ContentPublishedHandler : IContentPublishedHandler
   {
       private readonly IHttpClientFactory _httpClientFactory;
       
       public ContentPublishedHandler(IHttpClientFactory httpClientFactory)
       {
           _httpClientFactory = httpClientFactory;
       }
       
       public async Task Handle(ContentPublishedEventArgs args)
       {
           var content = args.Content;
           var urlResolver = ServiceLocator.Current.GetInstance<IUrlResolver>();
           var url = urlResolver.GetUrl(content.ContentLink);
           
           // Don't trigger for non-page content
           if (string.IsNullOrEmpty(url)) return;
           
           // Notify Next.js to revalidate
           var client = _httpClientFactory.CreateClient();
           await client.PostAsJsonAsync(
               "https://your-nextjs-site.com/api/revalidate",
               new
               {
                   contentId = content.ContentLink.ID,
                   path = url
               });
       }
   }
   ```

## On-Page Editing for Modern Frontends

Enabling on-page editing (OPE) in a headless architecture requires special consideration to maintain the editing experience while using modern frontend frameworks.

### Core Principles

1. **Property Mapping**: Clear mapping between CMS properties and frontend components
2. **DOM Attributes**: Strategic placement of Optimizely-specific data attributes 
3. **Component Design**: Components designed with editing capabilities in mind
4. **Preview Mode**: Support for preview and edit mode in the frontend application

### Next.js Implementation

#### Configuration Setup

The frontend must be configured to communicate with the CMS for editing:

```typescript
// next.config.js
module.exports = {
  env: {
    OPTIMIZELY_EDIT_MODE: process.env.OPTIMIZELY_EDIT_MODE === 'true',
    OPTIMIZELY_CMS_URL: process.env.OPTIMIZELY_CMS_URL
  },
  async rewrites() {
    return [
      // Rewrite for episerver UI integration in edit mode
      {
        source: '/episerver/:path*',
        destination: `${process.env.OPTIMIZELY_CMS_URL}/episerver/:path*`,
      },
      // Rewrite for CMS resources required by editor
      {
        source: '/ui/:path*',
        destination: `${process.env.OPTIMIZELY_CMS_URL}/ui/:path*`,
      }
    ]
  },
};
```

#### Component Structure with Edit Attributes

Components must include proper data attributes for on-page editing:

```tsx
// components/ContentBlock.tsx
import React from 'react';

interface ContentBlockProps {
  contentLink: {
    id: string;
    workId?: string;
    guidValue: string;
  };
  heading: string;
  mainBody: {
    html: string;
  };
}

const ContentBlock: React.FC<ContentBlockProps> = ({ contentLink, heading, mainBody }) => {
  return (
    <div data-epi-edit={contentLink.guidValue}>
      <h2 data-epi-property-name="Heading">{heading}</h2>
      <div 
        data-epi-property-name="MainBody" 
        dangerouslySetInnerHTML={{ __html: mainBody.html }} 
      />
    </div>
  );
};

export default ContentBlock;
```

Key attributes:
- `data-epi-edit`: Marks a component as editable with the content GUID
- `data-epi-property-name`: Maps a DOM element to a specific CMS property

#### Edit Mode Detection

The application needs to detect when it's running in edit mode:

```typescript
// lib/editMode.ts
export const isEditMode = () => {
  if (typeof window === 'undefined') {
    return false;
  }
  
  return (
    window.location.search.indexOf('epieditmode=true') !== -1 ||
    process.env.OPTIMIZELY_EDIT_MODE === 'true'
  );
};

export const addEditModeScripts = () => {
  if (!isEditMode()) {
    return;
  }
  
  // Add required edit mode scripts
  const script = document.createElement('script');
  script.src = `${process.env.OPTIMIZELY_CMS_URL}/episerver/cms/latest/clientresources/editmode.js`;
  script.async = true;
  document.head.appendChild(script);
};
```

#### Layout Component with Edit Mode Support

The main layout component needs to initialize edit mode:

```tsx
// components/Layout.tsx
import React, { useEffect } from 'react';
import Head from 'next/head';
import { isEditMode, addEditModeScripts } from '../lib/editMode';

interface LayoutProps {
  children: React.ReactNode;
  contentLink?: {
    id: string;
    workId?: string;
    guidValue: string;
  };
}

const Layout: React.FC<LayoutProps> = ({ children, contentLink }) => {
  useEffect(() => {
    if (isEditMode()) {
      addEditModeScripts();
    }
  }, []);

  return (
    <>
      <Head>
        {isEditMode() && (
          <>
            <meta name="epi-content-id" content={contentLink?.id || ''} />
            <meta name="epi-content-guid" content={contentLink?.guidValue || ''} />
          </>
        )}
      </Head>
      <div className={isEditMode() ? 'edit-mode' : ''}>
        {children}
      </div>
    </>
  );
};

export default Layout;
```

### Content Areas in Modern Frontends

Content areas require special handling to maintain drag-and-drop capabilities:

```tsx
// components/ContentArea.tsx
import React from 'react';
import dynamic from 'next/dynamic';
import { isEditMode } from '../lib/editMode';

// Dynamic imports for all possible block types
const blocksComponentMap = {
  'MyProject.Models.Blocks.TextBlock': dynamic(() => import('./blocks/TextBlock')),
  'MyProject.Models.Blocks.ImageBlock': dynamic(() => import('./blocks/ImageBlock')),
  // Add other block types as needed
};

interface ContentAreaItem {
  contentLink: {
    id: string;
    workId?: string;
    guidValue: string;
  };
  contentType: string;
  [key: string]: any;
}

interface ContentAreaProps {
  items: ContentAreaItem[];
  propertyName: string;
  parentContentGuid: string;
}

const ContentArea: React.FC<ContentAreaProps> = ({ 
  items, 
  propertyName, 
  parentContentGuid 
}) => {
  if (!items || items.length === 0) {
    // Handle empty content area with proper OPE support
    return isEditMode() ? (
      <div 
        data-epi-edit="true"
        data-epi-property-name={propertyName}
        data-epi-content-guid={parentContentGuid}
        className="empty-content-area"
      >
        {/* Empty state UI */}
        <p>Drag blocks here</p>
      </div>
    ) : null;
  }

  return (
    <div 
      className="content-area"
      data-epi-property-name={propertyName}
    >
      {items.map((item) => {
        const BlockComponent = blocksComponentMap[item.contentType];
        
        if (!BlockComponent) {
          console.warn(`No component mapped for content type: ${item.contentType}`);
          return null;
        }
        
        return (
          <div 
            key={item.contentLink.id}
            className="content-area-item"
            data-epi-block-id={item.contentLink.id}
          >
            <BlockComponent {...item} />
          </div>
        );
      })}
    </div>
  );
};

export default ContentArea;
```

### Rich Text Content with Edit Support

Rich text fields need special handling for on-page editing:

```tsx
// components/RichText.tsx
import React from 'react';

interface RichTextProps {
  propertyName: string;
  html: string;
}

const RichText: React.FC<RichTextProps> = ({ propertyName, html }) => {
  return (
    <div 
      data-epi-property-name={propertyName}
      dangerouslySetInnerHTML={{ __html: html }}
      className="rich-text-content"
    />
  );
};

export default RichText;
```

### Preview Mode Configuration

To support preview capabilities, configure Next.js preview mode:

```typescript
// pages/api/preview.ts
import { NextApiRequest, NextApiResponse } from 'next';

export default async function handler(
  req: NextApiRequest,
  res: NextApiResponse
) {
  const { token, path } = req.query;
  
  // Validate the preview token against a secure value
  if (token !== process.env.PREVIEW_SECRET) {
    return res.status(401).json({ message: 'Invalid token' });
  }

  // Enable Preview Mode
  res.setPreviewData({});
  
  // Redirect to the page or provided path
  res.redirect(path ? `/${path}` : '/');
}
```

### Handling Preview in getStaticProps/getServerSideProps

Modify data fetching to support preview mode:

```typescript
// pages/[...slug].tsx
export const getStaticProps: GetStaticProps = async (context) => {
  const { params, preview = false } = context;
  const slug = params?.slug as string[];
  const path = `/${slug.join('/')}`;
  
  // Use different query depending on preview mode
  const query = preview 
    ? GET_PAGE_BY_PATH_PREVIEW 
    : GET_PAGE_BY_PATH;
    
  try {
    const { page } = await graphqlClient.request(query, { path });
    
    // If no page found and not in preview, return 404
    if (!page && !preview) {
      return { notFound: true };
    }
    
    return {
      props: {
        page,
        preview,
      },
      // Revalidate every hour for production
      revalidate: 3600,
    };
  } catch (error) {
    console.error('Failed to fetch page:', error);
    return { notFound: true };
  }
};
```

### Best Practices for On-Page Editing

1. **Component Boundaries**: Align component boundaries with content type structure
2. **Minimize Wrapping Elements**: Keep DOM structure clean to avoid unnecessary nesting
3. **CSS Considerations**: 
   - Apply specific styles only in edit mode
   - Ensure UI elements are accessible in the CMS UI
   - Use position indicators for empty content areas

4. **Performance Optimization**:
   - Only load edit mode scripts when in edit mode
   - Lazily load components when possible
   - Avoid unnecessary re-renders when in edit mode

5. **Testing Edit Mode**:
   - Develop a testing strategy specifically for edit mode functionality
   - Create specific test cases for drag and drop, property editing, etc.

## Component Mapping

To render Optimizely content in Next.js:

```typescript
// components/ContentRenderer.tsx
import dynamic from 'next/dynamic';

// Dynamic import of content components
const componentMap = {
  // Page types
  ArticlePage: dynamic(() => import('./pages/ArticlePage')),
  StartPage: dynamic(() => import('./pages/StartPage')),
  
  // Block types
  HeroBlock: dynamic(() => import('./blocks/HeroBlock')),
  TextBlock: dynamic(() => import('./blocks/TextBlock')),
};

export default function ContentRenderer({ content }) {
  // Determine the component to render based on content type
  const contentType = content.__typename;
  const Component = componentMap[contentType];
  
  if (!Component) {
    console.warn(`No component found for content type: ${contentType}`);
    return null;
  }
  
  return <Component content={content} />;
}
```

## GraphQL Query Organization

GraphQL queries are organized by domain:

```
frontend/src/graphql/
├── fragments/
│   ├── pageFragments.ts
│   └── blockFragments.ts
├── queries/
│   ├── pages.ts
│   └── navigation.ts
└── mutations/
    └── forms.ts
```

Example of a structured query approach:

```typescript
// fragments/pageFragments.ts
import { gql } from 'graphql-request';

export const PAGE_CONTENT_FRAGMENT = gql`
  fragment PageContentFragment on PageContent {
    id
    name
    url
    language {
      name
      displayName
    }
    metaTitle
    metaDescription
  }
`;

// queries/pages.ts
import { gql } from 'graphql-request';
import { PAGE_CONTENT_FRAGMENT } from '../fragments/pageFragments';

export const GET_PAGE_BY_URL = gql`
  query GetPageByUrl($url: String!) {
    Page(where: { Url: { eq: $url } }) {
      items {
        ...PageContentFragment
        # Type-specific fields based on page type
        ... on ArticlePage {
          heading
          mainBody {
            html
          }
          mainImage {
            url
            alt
          }
        }
      }
    }
  }
  ${PAGE_CONTENT_FRAGMENT}
`;
```

## Content Delivery Optimization

Performance optimizations for headless delivery:

1. **Content Caching**: CDN caching for static content
2. **Image Optimization**: Using Next.js Image component with Optimizely image URLs
3. **Selective Content Loading**: Loading only necessary content for each page
4. **Prefetching**: Prefetching linked content for faster navigation
5. **API Response Compression**: Enabling compression for API responses

## On-Page Editing Integration

For supporting on-page editing from Optimizely CMS:

```typescript
// components/EditableContent.tsx
import React from 'react';

interface EditableContentProps {
  contentId: string;
  children: React.ReactNode;
}

export default function EditableContent({ contentId, children }: EditableContentProps) {
  const isEditMode = typeof window !== 'undefined' && window.location.search.includes('epieditmode=true');
  
  if (!isEditMode) {
    return <>{children}</>;
  }
  
  return (
    <div data-epi-edit={contentId}>
      {children}
    </div>
  );
}
```

## Security Considerations

1. **API Authentication**: Secure GraphQL endpoint with API keys
2. **Preview Security**: Secure preview mode with tokens
3. **Content Access Control**: Respect Optimizely access rights in API responses
4. **CORS Configuration**: Properly configured CORS for API access
5. **Input Validation**: Validate all input parameters for GraphQL queries 