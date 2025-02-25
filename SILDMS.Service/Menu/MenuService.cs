using SILDMS.DataAccessInterface.Menu;
using SILDMS.Model.SecurityModule;
using SILDMS.Utillity;
using SILDMS.Utillity.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.Service.Menu
{
    public class MenuService : IMenuService
    {
        #region Fields

        private readonly IMenuDataService menuDataService;
        private readonly ILocalizationService localizationService;
        private string errorNumber = "";
        #endregion

        #region Constractor
        public MenuService(
             IMenuDataService repository,
             ILocalizationService localizationService
            )
        {
            this.menuDataService = repository;
            this.localizationService = localizationService;
        }

        #endregion


        public ValidationResult GetMenu(string ownerID, string menuID, string action, out List<SEC_Menu> menuList)
        {
            menuList = menuDataService.GetMenu(ownerID, menuID, action, out errorNumber);
            if (errorNumber.Length > 0)
            {
                return new ValidationResult(errorNumber, localizationService.GetResource(errorNumber));
            }
            return ValidationResult.Success;
        }

        public ValidationResult AddMenu(SEC_Menu menu, string action, out string status)
        {
            menuDataService.AddMenu(menu, action, out status);
            if (status.Length > 0)
            {
                return new ValidationResult(status, localizationService.GetResource(status));
            }
            return ValidationResult.Success;
        }

        public ValidationResult EditMenu(SEC_Menu menu, string action, out string status)
        {
            throw new NotImplementedException();
        }

        public ValidationResult DeleteMenu(SEC_Menu menu, string action, out string status)
        {
            throw new NotImplementedException();
        }

        public StringBuilder GenerateMenu(List<SEC_Menu> lstMenu)
        {
            int i = 0;
            StringBuilder sb = new StringBuilder();
            sb.Append("<ul class='sidebar-menu'><li class='header' style='padding-bottom: 15px'><small class='label bg-yellow'> Current Session will Expired On: " + lstMenu[0].SessionExpiredOn + "</small></li>");
            lstMenu = lstMenu.Where(ob => ob.PermissionClass == "").ToList();

            List<Child> child = new List<Child>();
            var root = (from t in lstMenu where t.ParentMenuID == "0" select t).ToList();

            sb.Append("<li class='header'>MAIN NAVIGATION</li>");

            foreach (var item in root)
            {
                sb.Append("<li class='treeview active'><a href='" + item.MenuUrl + "'><i class='" + item.MenuIcon + "'></i> <span>" + item.MenuTitle + "</span><i class='fa fa-angle-left pull-right'></i></a>");
                GetChild(lstMenu, item.MenuID, sb, i);
                sb.Append("</li>");
            }
            sb.Append("</ul>");
            return sb;
        }
        public StringBuilder GetChild(List<SEC_Menu> lstMenuSetup, string parentId, StringBuilder sb, int i)
        {
            var hasChild = (from c in lstMenuSetup where c.ParentMenuID == parentId select c).ToList();
            if (hasChild.Count > 0)
            {// menu-open
                if (i == 0)
                { sb.Append("<ul class='treeview-menu  menu-open' style='display: block;'>"); i++; }
                else
                { sb.Append("<ul class='treeview-menu  menu-open' style='display: none;'>"); }
                foreach (var item in hasChild)
                {

                    var hasChild2 = (from c in lstMenuSetup where c.ParentMenuID == item.MenuID select c).ToList();
                    if (hasChild2.Count > 0)
                    {
                        sb.Append("<li><a href='" + item.MenuUrl + "'><i class='" + item.MenuIcon + "'></i> " + item.MenuTitle + "<i class='fa fa-angle-left pull-right'></i></a>");
                        GetChild(lstMenuSetup, item.MenuID, sb, i);
                    }
                    else
                    {
                        sb.Append(" <li><a href='" + item.MenuUrl + "'><i class='" + item.MenuIcon + "'></i> " + item.MenuTitle + "</a>");
                    }
                    sb.Append("</li>");
                }

                sb.Append("</ul>");
            }

            return sb;
        }

        //public StringBuilder GenerateMenu(List<SEC_Menu> lstMenu)
        //{
        //    StringBuilder sb = new StringBuilder();

        //    // Add session expiration info and main navigation header
        //    sb.Append("<ul class='sidebar-menu'>");
        //    sb.Append($"<li class='header' style='padding-bottom: 15px'><small class='label bg-yellow'> Current Session will Expired On: {lstMenu[0].SessionExpiredOn}</small></li>");
        //    sb.Append("<li class='header'>MAIN NAVIGATION</li>");

        //    // Filter root menus (ParentMenuID = "0")
        //    var rootMenus = lstMenu.Where(menu => menu.ParentMenuID == "0").ToList();

        //    foreach (var rootMenu in rootMenus)
        //    {
        //        sb.Append($"<li class='treeview'><a href='{rootMenu.MenuUrl}'><i class='{rootMenu.MenuIcon}'></i> <span>{rootMenu.MenuTitle}</span><i class='fa fa-angle-left pull-right'></i></a>");
        //        GenerateSubMenu(lstMenu, rootMenu.MenuID, sb); // Generate child menus
        //        sb.Append("</li>");
        //    }

        //    sb.Append("</ul>");
        //    return sb;
        //}

        private void GenerateSubMenu(List<SEC_Menu> lstMenu, string parentId, StringBuilder sb)
        {
            // Get child menus for the given parent
            var childMenus = lstMenu.Where(menu => menu.ParentMenuID == parentId).ToList();

            if (childMenus.Any())
            {
                // All menus are initially closed (display: none)
                sb.Append("<ul class='treeview-menu' style='display: none;'>");

                foreach (var childMenu in childMenus)
                {
                    // Check if the current child has further children
                    var hasSubMenu = lstMenu.Any(menu => menu.ParentMenuID == childMenu.MenuID);

                    if (hasSubMenu)
                    {
                        // Recursive call for submenus
                        sb.Append($"<li><a href='{childMenu.MenuUrl}'><i class='{childMenu.MenuIcon}'></i> {childMenu.MenuTitle} <i class='fa fa-angle-left pull-right'></i></a>");
                        GenerateSubMenu(lstMenu, childMenu.MenuID, sb);
                    }
                    else
                    {
                        // Leaf node menu
                        sb.Append($"<li><a href='{childMenu.MenuUrl}'><i class='{childMenu.MenuIcon}'></i> {childMenu.MenuTitle}</a></li>");
                    }
                }

                sb.Append("</ul>");
            }
        }


    }
}
