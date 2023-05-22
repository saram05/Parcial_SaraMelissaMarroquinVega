using System.ComponentModel.DataAnnotations;

namespace Parcial_SaraMelissaMarroquinVega.DAL.Entities
{
    public class Ticket
    {
        [Key]
        [Display(Name = "Id de la boleta")]
        public Guid Id { get; set; }

        [Display(Name = "Fecha de uso")]
        public DateTime? UseDate { get; set; }

        [Display(Name = "Boleta usada")]
        public bool IsUsed { get; set; }

        [Display(Name = "Portería de entrada")]

        [MaxLength(20)]
        public string? EntranceGate { get; set; }
        public string? Message { get; set; }

    }
}
