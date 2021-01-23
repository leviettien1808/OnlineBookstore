using OnlineBookstore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineBookstore.DAL
{
    public class CategoriesAccess
    {
        OnlineBookstoreDbContext context = new OnlineBookstoreDbContext();

        public int CreateCategory(Categories categories)
        {
            // kiểm tra danh mục đã tồn tại chưa
            var category = GetCategoryByName(categories.Name);
            if (category != null)
            {
                return 0;
            }
            context.Categories.Add(categories);
            context.SaveChanges();
            return categories.Id;
        }

        public List<Categories> ReadCategories()
        {
            List<Categories> categories = context.Categories.Where(x => x.Status != 0).OrderBy(x => x.DisplayOrder).ToList();
            return categories;
        }

        public Categories ReadCategory(int id)
        {
            Categories category = context.Categories.FirstOrDefault(x => x.Id == id);
            return category;
        }

        public bool UpdateCategory(Categories categories)
        {
            Categories updateCategory = context.Categories.FirstOrDefault(x => x.Id == categories.Id);
            if (updateCategory != null)
            {
                updateCategory.DisplayOrder = categories.DisplayOrder;
                updateCategory.ModifiedBy = categories.ModifiedBy;
                updateCategory.ModifiedDate = DateTime.Now;
                updateCategory.Name = categories.Name;
                updateCategory.ParentId = categories.ParentId;
                updateCategory.SeoTitle = categories.SeoTitle;
                updateCategory.SeoUrl = categories.SeoUrl;
                updateCategory.ShowOnHome = categories.ShowOnHome;
                updateCategory.Status = categories.Status;
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool DeleteCategory(int id, string modifiedBy)
        {
            Categories categories = context.Categories.FirstOrDefault(x => x.Id == id);
            if (categories != null)
            {
                // Move this user to trash
                categories.ModifiedBy = modifiedBy;
                categories.ModifiedDate = DateTime.Now;
                categories.Status = 0;
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public Categories GetCategoryByName(string name)
        {
            return context.Categories.FirstOrDefault(x => x.Name == name && x.Status != 0);
        }

        public Categories GetCategoryById(int id)
        {
            return context.Categories.FirstOrDefault(x => x.Id == id);
        }

        public List<Categories> GetCategoriesInTrash()
        {
            List<Categories> categories = context.Categories.Where(x => x.Status == 0).ToList();
            return categories;
        }

        public bool ReTrash(int id, string modifiedBy)
        {
            Categories categories = context.Categories.FirstOrDefault(x => x.Id == id);
            if (categories != null)
            {
                // Move this category out of the trash
                categories.ModifiedBy = modifiedBy;
                categories.ModifiedDate = DateTime.Now;
                categories.Status = 1;
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public void Status(int id, string modifiedBy)
        {
            var category = GetCategoryById(id);
            category.Status = (category.Status == 1) ? 2 : 1;
            category.ModifiedBy = modifiedBy;
            category.ModifiedDate = DateTime.Now;
            context.SaveChanges();
        }
    }
}