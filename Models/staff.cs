using System;
using System.Collections.Generic;

#nullable disable

namespace GymMGT.Models
{
    public partial class staff
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Image { get; set; }
        public  string Poste{ get; set; }
        public string Salaire { get; set; }
    }
}
