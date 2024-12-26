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
   public class UserModel
    {
        public User Record { get; set; }

        public string Username => Record.Username; 
       
        public string Password => Record.Password;

        public string IsActive => Record.IsActive ? "Active" : "Not Active";
        public string Role => Record.Role?.Name;



    }
}
