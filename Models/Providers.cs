using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FairyBE.Models
{
    public class Providers
    {
        [Key]
        public string Id { get; set; }

        public string Type { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public bool IsMyCompany { get; set; }

        public string? Description { get; set; }

        public bool IsConfirmated { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedDate { get; set; }

        public string IsActive { get; set; } = "ACTIVO";

        // Foreign keys
        public int? CompanyId { get; set; }
        public string? CompanyName { get; set; }
        public int CreatedUserId { get; set; }
        public int? PeopleId { get; set; }
        public string? PeopleName { get; set; }
        public int? UpdatedUserId { get; set; }
        public string TypePeople { get; set; }

        // Opcional: Propiedades de navegación (recomendado)
        //[ForeignKey("CompanyId")]
        //public virtual Company Company { get; set; }

        //[ForeignKey("CreatedUserId")]
        //public virtual User CreatedUser { get; set; }

        //[ForeignKey("UpdatedUserId")]
        //public virtual User? UpdatedUser { get; set; }

        //[ForeignKey("PeopleId")]
        //public virtual Person Person { get; set; }
    }
}
