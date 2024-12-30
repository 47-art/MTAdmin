using MTAdmin.Shared.Enums;
using MTAdmin.Shared.Models;
using System;

namespace MTAdmin.Shared.Helpers
{
    public class ModuleHelper
    {
        public static SidebarMenu AddHeader(string name)
        {
            return new SidebarMenu
            {
                Type = SidebarMenuType.Header,
                Name = name,
            };
        }
        public static SidebarMenu AddTree(string name, string iconClassName = "fa fa-link")
        {
            return new SidebarMenu
            {
                Type = SidebarMenuType.Tree,
                IsActive = false,
                Name = name,
                IconClassName = iconClassName,
                URLPath = "#",
            };
        }

        public static SidebarMenu AddModule(Module module, Tuple<int, int, int> counter = null)
        {
            if (counter == null)
                counter = Tuple.Create(0, 0, 0);

            switch (module)
            {
                case Module.Dashboard:
                    return new SidebarMenu
                    {
                        Type = SidebarMenuType.Link,
                        Name = "Dashboard",
                        IconClassName = "fas fa-tachometer-alt",
                        URLPath = "/Archone/Home",
                        LinkCounter = counter,
                    };
                case Module.Languages:
                    return new SidebarMenu
                    {
                        Type = SidebarMenuType.Link,
                        Name = "Languages",
                        IconClassName = "fas fa-edit",
                        URLPath = "/Archone/Language",
                        LinkCounter = counter,
                    };
                case Module.Categories:
                    return new SidebarMenu
                    {
                        Type = SidebarMenuType.Link,
                        Name = "Categories",
                        IconClassName = "fas fa-th",
                        URLPath = "/Archone/Category",
                        LinkCounter = counter,
                    };
                case Module.Tags:
                    return new SidebarMenu
                    {
                        Type = SidebarMenuType.Link,
                        Name = "Tags",
                        IconClassName = "far fa-circle",
                        URLPath = "/Archone/Tag",
                        LinkCounter = counter,
                    };
                case Module.Templates:
                    return new SidebarMenu
                    {
                        Type = SidebarMenuType.Link,
                        Name = "Templates",
                        IconClassName = "far fa-image",
                        URLPath = "/Archone/Template",
                        LinkCounter = counter,
                    };
                case Module.Enquiries:
                    return new SidebarMenu
                    {
                        Type = SidebarMenuType.Link,
                        Name = "Enquiries",
                        IconClassName = "fa-circle",
                        URLPath = "/Archone/Contact",
                        LinkCounter = counter,
                    };
                case Module.EmailSubscriber:
                    return new SidebarMenu
                    {
                        Type = SidebarMenuType.Link,
                        Name = "Email Subscriber",
                        IconClassName = "far fa-circle",
                        URLPath = "/Archone/EmailSubscribers",
                        LinkCounter = counter,
                    };
                case Module.UserLogs:
                    return new SidebarMenu
                    {
                        Type = SidebarMenuType.Link,
                        Name = "User Logs",
                        IconClassName = "far fa-circle",
                        URLPath = "/Archone/UserAudit",
                        LinkCounter = counter,
                    };
                default:
                    break;
            }
            return null;
        }
        public static SidebarMenu AddClientModule(ClientModule module, Tuple<int, int, int> counter = null)
        {
            if (counter == null)
                counter = Tuple.Create(0, 0, 0);

            switch (module)
            {
                case ClientModule.Home:
                    return new SidebarMenu
                    {
                        Type = SidebarMenuType.Link,
                        Name = "Home",
                        IconClassName = "fas fa-tachometer-alt",
                        URLPath = "/",
                        LinkCounter = counter,
                    };
                case ClientModule.Categories:
                    return new SidebarMenu
                    {
                        Type = SidebarMenuType.Link,
                        Name = "Category",
                        IconClassName = "fas fa-tachometer-alt",
                        URLPath = "/trending/meme-template-categories",
                        LinkCounter = counter,
                    };
                case ClientModule.Tags:
                    return new SidebarMenu
                    {
                        Type = SidebarMenuType.Link,
                        Name = "Tags",
                        IconClassName = "fas fa-tachometer-alt",
                        URLPath = "/tags",
                        LinkCounter = counter,
                    };
                case ClientModule.AboutUs:
                    return new SidebarMenu
                    {
                        Type = SidebarMenuType.Link,
                        Name = "About",
                        IconClassName = "fas fa-tachometer-alt",
                        URLPath = "/about-us",
                        LinkCounter = counter,
                    };
                case ClientModule.Contact:
                    return new SidebarMenu
                    {
                        Type = SidebarMenuType.Link,
                        Name = "Contact",
                        IconClassName = "fas fa-tachometer-alt",
                        URLPath = "/contact",
                        LinkCounter = counter,
                    };
                case ClientModule.Disclaimer:
                    return new SidebarMenu
                    {
                        Type = SidebarMenuType.Link,
                        Name = "Disclaimer",
                        IconClassName = "fas fa-tachometer-alt",
                        URLPath = "/disclaimer",
                        LinkCounter = counter,
                    };
                case ClientModule.PrivacyPolicy:
                    return new SidebarMenu
                    {
                        Type = SidebarMenuType.Link,
                        Name = "Privacy Policy",
                        IconClassName = "fas fa-tachometer-alt",
                        URLPath = "/privacy-policy",
                        LinkCounter = counter,
                    };
                case ClientModule.Search:
                    return new SidebarMenu
                    {
                        Type = SidebarMenuType.Link,
                        Name = "Search",
                        IconClassName = "fas fa-tachometer-alt",
                        URLPath = "/search-meme-templates",
                        LinkCounter = counter,
                    };
                default:
                    break;
            }

            return null;
        }
    }
}
