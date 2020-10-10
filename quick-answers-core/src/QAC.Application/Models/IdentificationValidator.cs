using System;
using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QAC.Application.Models
{
    public class IdentificationValidator
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(255, ErrorMessage = "O campo Nome deve conter de 3 a 255 caracteres")]
        [MinLength(3, ErrorMessage = "O campo Nome deve conter de 3 a 255 caracteres")]
        public string Name { get; set; }

        [Required]
        [MaxLength(255, ErrorMessage = "O campo Nome deve conter de 3 a 255 caracteres")]
        [MinLength(3, ErrorMessage = "O campo Nome deve conter de 3 a 255 caracteres")]
        public string Example { get; set; }

        [Required]
        [MaxLength(255, ErrorMessage = "O campo Nome deve conter de 3 a 255 caracteres")]
        [MinLength(3, ErrorMessage = "O campo Nome deve conter de 3 a 255 caracteres")]
        public string Regex { get; set; }

        public bool Validate(string identification) 
        {
            return new Regex(Regex).IsMatch(identification);
        }

    }
}
