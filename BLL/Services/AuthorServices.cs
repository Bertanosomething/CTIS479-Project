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
    public interface IAuthorService
    {
        public Service Create(Author author);
        public Service Update(Author author);
        public Service Delete(int id);
        public IQueryable<AuthorModel> Query();
    }
    public class AuthorServices : Service, IAuthorService
    {
        public AuthorServices(DB db) : base(db)
        {
        }

        public Service Create(Author author)
        {
            if (_db.Authors.Any(a => a.Name.ToUpper() == author.Name.ToUpper().Trim()))
                return Error("Author with the same name already exists!");

            // Add the new author to the Authors table
            _db.Authors.Add(author);
            _db.SaveChanges();

            return Success("Author created successfully.");
        }

        public Service Delete(int id)
        {
            var author = _db.Authors.Include(a => a.Books).SingleOrDefault(a => a.Id == id);
            if (author == null)
                return Error("Author not found!");

            // Optionally, you could check if this author has any books associated with them
            // and handle the deletion accordingly (e.g., you might want to delete or reassign the books)
            _db.Books.RemoveRange(author.Books);  // Example if cascade delete is not set

            // Remove the author from the database
            _db.Authors.Remove(author);
            _db.SaveChanges();

            return Success("Author deleted successfully.");
        }

        public IQueryable<AuthorModel> Query()
        {
            return _db.Authors.Include(s => s.Books).Select(s => new AuthorModel() { Record = s });
        }

        public Service Update(Author author)
        {
            var existingAuthor = _db.Authors.SingleOrDefault(a => a.Id == author.Id);
            if (existingAuthor == null)
                return Error("Author not found!");

            // Update the properties of the existing author
            existingAuthor.Name = author.Name?.Trim();

            // Save changes to the database
            _db.SaveChanges();

            return Success("Author updated successfully.");
        }
    }
}
