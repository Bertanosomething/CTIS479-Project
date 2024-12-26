using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DAL;
#nullable disable

namespace BLL.Models
{
    public class AuthorModel
    {
        public Author Record { get; set; }

        [DisplayName("Author Name")]
        public string Name => Record.Name;

        [DisplayName("Books")]
        public string Books => string.Join("<br>", Record.Books.Select(b => b.Name));

    }
}
