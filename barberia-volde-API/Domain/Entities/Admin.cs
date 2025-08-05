using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Admin
    {   
        public int Id { get; set; }
        public string Usuario { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
    }
}