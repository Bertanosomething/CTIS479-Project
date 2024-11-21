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

        public decimal? Price => Record.Price;

        public int Quantity => Record.Quantity ?? 0;

        public Author Author => Record.Author;

        [DisplayName("Publisher")]
        public Publisher Publisher => Record.Publisher;

        [DisplayName("Genre")]
        public Genre Genre => Record.Genre;


    }
}
