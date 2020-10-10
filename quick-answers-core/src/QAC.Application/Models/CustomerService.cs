using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QAC.Application.Models
{
    public class CustomerService
    {
        [Key]
        public int CustomerServiceId { get; set; }

        [Required]
        public string Identifier { get; set; }

        [Required]
        [ForeignKey("IdentificationValidator")]
        public int ValidatorId { get; set; }

        public IdentificationValidator Validator { get; set; }

        [ForeignKey("Typification")]
        public int TypificationId { get; set; }

        public Typification Typification { get; set; }

        public DateTime StartService { get; set; }

        public DateTime EndService { get; set; }

    }
}
