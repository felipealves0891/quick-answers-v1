using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QAC.Application.Models
{
    public class Typification
    {
        [Key]
        public int Id { get; private set; }

        [Required]
        [MaxLength(255, ErrorMessage = "O campo Nome deve conter de 3 a 255 caracteres")]
        [MinLength(3, ErrorMessage = "O campo Nome deve conter de 3 a 255 caracteres")]
        public string Name { get; set; }

        [Required]
        [MaxLength(255, ErrorMessage = "O campo Nome deve conter de 3 a 255 caracteres")]
        [MinLength(3, ErrorMessage = "O campo Nome deve conter de 3 a 255 caracteres")]
        public string Nickname { get; set; }

        [Required]
        [MaxLength(255, ErrorMessage = "O campo Nome deve conter de 3 a 255 caracteres")]
        [MinLength(3, ErrorMessage = "O campo Nome deve conter de 3 a 255 caracteres")]
        public string Description { get; set; }

        [MaxLength(30, ErrorMessage = "O campo Nome deve conter de 3 a 30 caracteres")]
        [MinLength(3, ErrorMessage = "O campo Nome deve conter de 3 a 30 caracteres")]
        public string ExternalId { get; set; }
        
        [ForeignKey("Typification")]
        public int ParentId { get; set; }

        public Typification Parent { get; set; }


    }

}
