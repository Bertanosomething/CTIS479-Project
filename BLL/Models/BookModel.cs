using Azure.Identity;
using BLL.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#nullable disable

namespace BLL.Models
{
    public class BookModel
    {
        public Book Record { get; set; }

        [DisplayName("Book Name")]
        public string Name => Record.Name;

        public string Price => Record.Price.HasValue ? Record.Price.Value.ToString("C2") : string.Empty;

        public int Quantity => Record.Quantity ?? 0;

        public string PublishDate => Record.PublishDate.HasValue ? Record.PublishDate.Value.ToShortDateString() : string.Empty;
        public string Author => Record.Author?.Name;


        public string Publisher => Record?.Publisher?.Name;


        //public string Genre => Record?.Genre?.Name;
        public string Genres => string.Join("<br>", Record.BookGenres?.Select(ps => ps.Genre?.Name));
        //[DisplayName("Books Published by this Publisher")]
        [DisplayName("Genres")]
        public List<int> GenreIds
        {
            get => Record.BookGenres?.Select(bg => bg.GenreId).ToList();
            set => Record.BookGenres = value.Select(v => new BookGenre() { GenreId = v }).ToList();
            
        }

        // Display Genre Names (optional, for convenience)
        //[DisplayName("Genres")]
        /*public string Genres => Record.BookGenres != null
            ? string.Join(", ", Record.BookGenres.Select(bg => bg.Genre.Name))
            : string.Empty;*/
    }


}


