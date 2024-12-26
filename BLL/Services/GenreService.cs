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
    public interface IGenreService
    {
        Service Create(Genre genre);
        Service Update(Genre genre);
        Service Delete(int id);
        IQueryable<GenreModel> Query();
    }
    public class GenreService : Service, IGenreService
    {
        public GenreService(DB db) : base(db)
        {
        }

        public Service Create(Genre genre)
        {
            if (_db.Genres.Any(g => g.Name.ToUpper() == genre.Name.ToUpper().Trim()))
                return Error("Genre with the same name already exists!");

            _db.Genres.Add(genre);
            _db.SaveChanges();

            return Success("Genre created successfully.");
        }

        public Service Delete(int id)
        {
            var genre = _db.Genres.SingleOrDefault(g => g.Id == id);
            if (genre == null)
                return Error("Genre not found!");

            // Checks if any books are associated with the genre
            if (_db.BookGenres.Any(bg => bg.GenreId == id))
                return Error("Genre cannot be deleted because it is associated with one or more books.");

            _db.Genres.Remove(genre);
            _db.SaveChanges();

            return Success("Genre deleted successfully.");
        }

        public IQueryable<GenreModel> Query()
        {
            //return _db.Genres.Include(s => s.Name).Select(s => new GenreModel { Record = s });
            return _db.Genres.Select(s => new GenreModel { Record = s });
        }

        public Service Update(Genre genre)
        {
            var existingGenre = _db.Genres.SingleOrDefault(g => g.Id == genre.Id);
            if (existingGenre == null)
                return Error("Genre not found!");

            existingGenre.Name = genre.Name?.Trim();
            _db.SaveChanges();

            return Success("Genre updated successfully.");
        }
    }
}
