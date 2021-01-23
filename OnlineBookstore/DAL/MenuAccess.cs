using OnlineBookstore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineBookstore.DAL
{
    public class MenuAccess
    {
        OnlineBookstoreDbContext context = new OnlineBookstoreDbContext();

        public int CreateMenu(Menus menus)
        {
            // kiểm tra menu đã tồn tại chưa
            var menu = GetMenuByName(menus.Name);
            if (menu != null)
            {
                return 0;
            }
            context.Menus.Add(menus);
            context.SaveChanges();
            return menus.Id;
        }

        public List<Menus> ReadMenus()
        {
            List<Menus> menus = context.Menus.Where(x => x.Status != 0).ToList();
            return menus;
        }

        public Menus ReadMenu(int id)
        {
            Menus menu = context.Menus.FirstOrDefault(x => x.Id == id);
            return menu;
        }

        public bool UpdateMenu(Menus menu)
        {
            Menus updateMenu = context.Menus.FirstOrDefault(x => x.Id == menu.Id);
            if (updateMenu != null)
            {
                updateMenu.DisplayOrder = menu.DisplayOrder;
                updateMenu.Link = menu.Link;
                updateMenu.MenuTypeId = menu.MenuTypeId;
                updateMenu.Name = menu.Name;
                updateMenu.Status = menu.Status;
                updateMenu.Target = menu.Target;
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool DeleteMenu(int id)
        {
            Menus menu = context.Menus.FirstOrDefault(x => x.Id == id);
            if (menu != null)
            {
                // Move this menu to trash
                menu.Status = 0;
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public Menus GetMenuByName(string name)
        {
            return context.Menus.FirstOrDefault(x => x.Name == name && x.Status != 0);
        }

        public Menus GetMenuById(int id)
        {
            return context.Menus.FirstOrDefault(x => x.Id == id);
        }
        
        public List<Menus> GetMenusInTrash()
        {
            List<Menus> menus = context.Menus.Where(x => x.Status == 0).ToList();
            return menus;
        }

        public bool ReTrash(int id)
        {
            Menus menu = context.Menus.FirstOrDefault(x => x.Id == id);
            if (menu != null)
            {
                // Move this menu out of the trash
                menu.Status = 2;
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public void Status(int id)
        {
            var menu = GetMenuById(id);
            menu.Status = (menu.Status == 1) ? 2 : 1;
            context.SaveChanges();
        }

        public List<Menus> ReadMenusByMenuTypeId(int id)
        {
            return context.Menus.Where(x => x.MenuTypeId == id && x.Status == 1).OrderBy(x => x.DisplayOrder).ToList();
        }
    }
}