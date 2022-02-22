using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop2022.Components
{
    public class AdminMenu : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var menuItems = new List<AdminMenuItem> { new AdminMenuItem()
            {
                DisplayValue = "User Management",
                ControllerValue = "UserRoles"
            },
            new AdminMenuItem()
            {
                DisplayValue = "Role Management",
                ControllerValue = "RolesManager"
            },
            new AdminMenuItem()
            {
                DisplayValue = "Category Management",
                ControllerValue = "Category"
            }};

            return View(menuItems);
        }
    }

    public class AdminMenuItem
    {
        public string DisplayValue { get; set; }
        public string ControllerValue { get; set; }
    }
}
