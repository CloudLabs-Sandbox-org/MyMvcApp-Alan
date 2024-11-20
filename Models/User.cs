using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyMvcApp.Models // Asegúrate de que el namespace sea correcto
{
    public class User
    {
        private int id;
        private string name;
        private string email;
        private string password;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }
    }
}