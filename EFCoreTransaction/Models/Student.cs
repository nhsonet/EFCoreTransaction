using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace EFCoreTransaction.Models
{
    public class Student
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        [Required]
        public string Name { get; set; }

        [DataType(DataType.Date)]
        [Column(TypeName = "datetime2")]
        [Required]
        public DateTime CreatedAt { get; set; }

        [DataType(DataType.Date)]
        [Column(TypeName = "datetime2")]
        public DateTime? ModifiedAt { get; set; }

        [Column(TypeName = "bit")]
        [Required]
        [DefaultValue(false)]
        public bool IsActive { get; set; }
    }
}
