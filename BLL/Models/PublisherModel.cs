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
    public class PublisherModel
    {
        public Publisher Record { get; set; }

        [DisplayName("Publisher Name")]
        public string Name => Record.Name;

        [DisplayName("Books Published by this Publisher")]
        public List<int> BookIds
        {
            get => Record.Books?.Select(ps => ps.Id).ToList();
            set => Record.Books = value.Select(v => new Book() { Id = v }).ToList();
        }
    }
}
