using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MovieShop.Core.Entities
{
    [Table("Role")]
    public class Role
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(20)]
        public string Name { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
    }
}
