using OnlineBookstore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineBookstore.DAL
{
    public class BooksAccess
    {
        OnlineBookstoreDbContext context = new OnlineBookstoreDbContext();

        public int CreateBook(Books books)
        {
            // kiểm tra sách đã tồn tại chưa
            var book = GetBookByName(books.Name);
            if (book != null)
            {
                return 0;
            }
            context.Books.Add(books);
            context.SaveChanges();
            return books.Id;
        }

        public List<Books> ReadBooks()
        {
            List<Books> books = context.Books.Where(x => x.Status != 0).ToList();
            return books;
        }

        public Books ReadBook(int id)
        {
            Books book = context.Books.FirstOrDefault(x => x.Id == id);
            return book;
        }

        public bool UpdateBook(Books books)
        {
            Books updateBook = context.Books.FirstOrDefault(x => x.Id == books.Id);
            if (updateBook != null)
            {
                updateBook.Author = books.Author;
                updateBook.CategoryId = books.CategoryId;
                updateBook.Code = books.Code;
                updateBook.Cover = books.Cover;
                updateBook.Description = updateBook.Description;
                updateBook.Detail = updateBook.Detail;
                updateBook.Image = books.Image;
                updateBook.IncludedVAT = books.IncludedVAT;
                updateBook.ModifiedBy = books.ModifiedBy;
                updateBook.ModifiedDate = DateTime.Now;
                updateBook.MoreImages = books.MoreImages;
                updateBook.Name = books.Name;
                updateBook.PageSize = books.PageSize;
                updateBook.PromotionPrice = books.PromotionPrice;
                updateBook.Publisher = books.Publisher;
                updateBook.PurchasePrice = books.PurchasePrice;
                updateBook.Quantity = books.Quantity;
                updateBook.SalePrice = books.SalePrice;
                updateBook.SeoTitle = books.SeoTitle;
                updateBook.SeoUrl = books.SeoUrl;
                updateBook.Sold = books.Sold;
                updateBook.Status = books.Status;
                updateBook.Supplier = books.Supplier;
                updateBook.TopHot = books.TopHot;
                updateBook.TotalPage = books.TotalPage;
                updateBook.Translator = books.Translator;
                updateBook.ViewCount = books.ViewCount;
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool DeleteBook(int id, string modifiedBy)
        {
            Books books = context.Books.FirstOrDefault(x => x.Id == id);
            if (books != null)
            {
                // Move this book to trash
                books.ModifiedBy = modifiedBy;
                books.ModifiedDate = DateTime.Now;
                books.Status = 0;
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public Books GetBookByName(string name)
        {
            return context.Books.FirstOrDefault(x => x.Name == name && x.Status != 0);
        }

        public Books GetBookById(int id)
        {
            return context.Books.FirstOrDefault(x => x.Id == id);
        }

        public List<Books> GetBooksInTrash()
        {
            List<Books> books = context.Books.Where(x => x.Status == 0).ToList();
            return books;
        }

        public bool ReTrash(int id, string modifiedBy)
        {
            Books books = context.Books.FirstOrDefault(x => x.Id == id);
            if (books != null)
            {
                // Move this book out of the trash
                books.ModifiedBy = modifiedBy;
                books.ModifiedDate = DateTime.Now;
                books.Status = 1;
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public void Status(int id, string modifiedBy)
        {
            var book = GetBookById(id);
            book.Status = (book.Status == 1) ? 2 : 1;
            book.ModifiedBy = modifiedBy;
            book.ModifiedDate = DateTime.Now;
            context.SaveChanges();
        }
    }
}