using OnlineBookstore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineBookstore.DAL
{
    public class MenuTypeAccess
    {
        OnlineBookstoreDbContext context = new OnlineBookstoreDbContext();

        public List<MenuTypes> ReadMenuTypes()
        {
            List<MenuTypes> menuTypes = context.MenuTypes.Where(x => x.Status != 0).ToList();
            return menuTypes;
        }

        public MenuTypes GetMenuTypeById(int id)
        {
            return context.MenuTypes.FirstOrDefault(x => x.Id == id);
        }

        public void Status(int id, string modifiedBy)
        {
            MenuTypes menuType = GetMenuTypeById(id);
            menuType.Status = (menuType.Status == 1) ? 2 : 1;
            menuType.ModifiedBy = modifiedBy;
            menuType.ModifiedDate = DateTime.Now;
            context.SaveChanges();
        }
    }
}