using OnlineBookstore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineBookstore.DAL
{
    public class FooterAccess
    {
        OnlineBookstoreDbContext context = new OnlineBookstoreDbContext();

        public Footers ReadFooters()
        {
            return context.Footers.FirstOrDefault(x => x.Status != 2);
        }

        public Footers GetFooterById(string id)
        {
            return context.Footers.FirstOrDefault(x => x.Id == id);
        }

        public void Status(string id)
        {
            var menu = GetFooterById(id);
            menu.Status = (menu.Status == 1) ? 2 : 1;
            context.SaveChanges();
        }
    }
}