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
    public class GenreModel
    {
        public Genre Record { get; set; }

        [DisplayName("Genre Name")]
        public string Name => Record.Name;

        [DisplayName("Books Written in this Genre")]
        public List<int> BookIds
        {
            get => Record.Books.Select(ps => ps.Id).ToList();
            set => Record.Books = value.Select(v => new Book() { Id = v }).ToList();
        }
    }
}
