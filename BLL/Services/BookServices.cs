using BLL.DAL;
using BLL.Models;
using BLL.Services.Bases;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public interface IBookService
    {
        public Service Create(Book book);
        public Service Update(Book book);
        public Service Delete(int id);
        public IQueryable<BookModel> Query();
    }
    public class BookServices : Service, IBookService
    {
        public BookServices(DB db) : base(db)
        {
        }

        public Service Create(Book book)
        {
            if (_db.Books.Any(p => p.Name.ToUpper() == book.Name.ToUpper().Trim()))
                return Error("A Book with the same name exists!");
            if (book.Quantity.HasValue && book.Quantity.Value < 0)
                return Error("Stock amount must be a positive number!");
            book.Name = book.Name?.Trim();
            book.Quantity = book.Quantity.Value;
            book.Price=book.Price.Value;
            _db.Books.Add(book);
            _db.SaveChanges();
            return Success("Book created successfully.");
        }

        public Service Delete(int id)
        {
            var book = _db.Books.Include(b => b.Author).SingleOrDefault(b => b.Id == id);
            if (book == null)
                return Error("Book not found!");
            _db.Books.Remove(book);

            // Save changes to the database
            _db.SaveChanges();

            // Return success message
            return Success("Book deleted successfully.");

        }

        public IQueryable<BookModel> Query()
        {
            return _db.Books.Include(b => b.Author).Include(b => b.Genre).Include(b => b.Publisher).OrderByDescending(b => b.Quantity).ThenBy(b => b.Name).Select(b => new BookModel { Record = b});
        }

        public Service Update(Book book)
        {
            if (_db.Books.Any(b => b.Id != book.Id && b.Name.ToUpper() == book.Name.ToUpper().Trim()))
                return Error("Book with the same name exists!");

          
            if (book.Quantity.HasValue && book.Quantity.Value < 0)
                return Error("Quantity must be a positive number!");

            
            var entity = _db.Books.Include(b => b.Author) 
                                  .SingleOrDefault(b => b.Id == book.Id);

           
            if (entity == null)
                return Error("Book not found!");

            
            entity.Name = book.Name?.Trim(); 
            entity.Price = book.Price;
            entity.Quantity = book.Quantity;
            entity.AuthorId = book.AuthorId;  
            //entity.Author = book.Author;
            //DELETE BELOW IF IT DOESN'T WORK
            if (book.Author != null)
            {
                _db.Authors.Attach(book.Author); 
                entity.Author = book.Author;
            }

            _db.Books.Update(entity);

           
            _db.SaveChanges();

            
            return Success("Book updated successfully.");
        }
    }
}
