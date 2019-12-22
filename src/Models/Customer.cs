using System.ComponentModel.DataAnnotations;

namespace frontend.Models
{
    public class Customer
    {
        public int Id { get; set; }
        [Required] [StringLength(200)] public string Name { get; set; }
        [Required] [StringLength(16)] public string Vat { get; set; }
        public bool Enabled { get; set; }
    }
}
