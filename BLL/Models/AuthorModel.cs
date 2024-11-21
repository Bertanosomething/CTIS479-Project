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

        [DisplayName("Books Written by the Author")]
        /*public List<int> BookIds
        {
            get => Record.Books?.Select(ps => ps.Id).ToList();
            set => Record.Books = value.Select(v => new Book() { Id = v }).ToList();
        }*/

        
        public List<string> BookNames { get; set; }
        //public string UnitPrice => Record.UnitPrice.ToString("C2");
    }
}
