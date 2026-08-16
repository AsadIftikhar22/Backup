using EPiServer.Shell;
using EPiServer.Shell.Modules;
using EPiServer.Shell.Navigation;
using System;
using System.Collections.Generic;

namespace Salam.Cms.Plugin.ApiExplorer.Infrastructure.Menu
{

    [MenuProvider]
    public class MenuProvider : IMenuProvider
    {
        protected readonly string RouteBasePath = "/";
        protected readonly ShellModule ShellModule;

        public MenuProvider(ModuleTable shellModules)
        {
            if (shellModules.TryGetModule(GetType().Assembly, out ShellModule module))
            {
                ShellModule = module;
                RouteBasePath = module.GetResolvedRouteBasePath();
            }
            else
                throw new ApplicationException("Unable to find the Salam.Cms.Plugin.ApiExplorer module");
        }

        public IEnumerable<MenuItem> GetMenuItems()
        {
            var menuItems = new List<MenuItem>
            {
                new UrlMenuItem(
                    "API Explorer",
                    MenuPaths.Global + "/ApiExplorer",
                    Paths.ToResource(Constants.ModuleName, ShellModule.GetRouteSegmentForController("ApiExplorer"))
                ) {
                    SortIndex = 150,
                }
            };
            return menuItems;
        }
    }
}