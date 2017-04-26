using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lab_9.Models
{
    public class Pet
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Укажите имя питомца")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Укажите породу питомца")]
        public string Breed { get; set; }
    }
}
