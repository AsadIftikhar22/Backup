using EPiServer.Framework.TypeScanner;
using EPiServer.Shell.Modules;
using Microsoft.Extensions.FileProviders;

namespace Salam.Cms.Plugin.ApiExplorer
{
    public class ApiExplorerModule : ShellModule
    {
        public ApiExplorerModule(string name, string routeBasePath, string resourceBasePath) : base(name, routeBasePath, resourceBasePath)
        {
        }

        public ApiExplorerModule(string name, string routeBasePath, string resourceBasePath, ITypeScannerLookup typeScannerLookup, IFileProvider virtualPathProvider) : base(name, routeBasePath, resourceBasePath, typeScannerLookup, virtualPathProvider)
        {
        }
    }
}
