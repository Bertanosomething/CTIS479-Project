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
    public interface IPublisherService
    {
        Service Create(Publisher publisher);
        Service Update(Publisher publisher);
        Service Delete(int id);
        IQueryable<PublisherModel> Query();
    }
    public class PublisherService : Service, IPublisherService
    {
        public PublisherService(DB db) : base(db)
        {
        }

        public Service Create(Publisher publisher)
        {
            if (_db.Publishers.Any(p => p.Name.ToUpper() == publisher.Name.ToUpper().Trim()))
                return Error("Publisher with the same name already exists!");

            _db.Publishers.Add(publisher);
            _db.SaveChanges();

            return Success("Publisher created successfully.");
        }

        public Service Delete(int id)
        {
            var publisher = _db.Publishers.SingleOrDefault(p => p.Id == id);
            if (publisher == null)
                return Error("Publisher not found!");

            // Check if any books are associated with the publisher
            if (_db.Books.Any(b => b.PublisherId == id))
                return Error("Publisher cannot be deleted because it is associated with one or more books.");

            _db.Publishers.Remove(publisher);
            _db.SaveChanges();

            return Success("Publisher deleted successfully.");
        }

        public IQueryable<PublisherModel> Query()
        {
            //return _db.Publishers.AsQueryable();
            return _db.Publishers.Include(s => s.Books).Select(s => new PublisherModel { Record = s });
            
        }

        public Service Update(Publisher publisher)
        {
            var existingPublisher = _db.Publishers.SingleOrDefault(p => p.Id == publisher.Id);
            if (existingPublisher == null)
                return Error("Publisher not found!");

            existingPublisher.Name = publisher.Name?.Trim();
            _db.SaveChanges();

            return Success("Publisher updated successfully.");
        }
    }
}
