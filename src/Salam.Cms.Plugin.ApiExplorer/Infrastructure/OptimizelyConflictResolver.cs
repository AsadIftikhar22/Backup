using Microsoft.AspNetCore.Mvc.ApiExplorer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Salam.Cms.Plugin.ApiExplorer.Infrastructure
{
    internal static class OptimizelyConflictResolver
    {
        public static ApiDescription Resolve(IEnumerable<ApiDescription> descriptions)
        {
            return
                descriptions
                    .Where(x => x.SupportedRequestFormats.Any(y => string.Equals(y.MediaType, "application/json", StringComparison.OrdinalIgnoreCase))).First();
        }
    }
}
