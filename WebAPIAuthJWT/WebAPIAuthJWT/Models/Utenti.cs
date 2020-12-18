using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebAPIAuthJWT.Models
{
    public class Utenti
    {
        public string Username { get; set; }

        public string Password { get; set; }
    }
}