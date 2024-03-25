using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace clientes.Models
{
    public class Logradouro
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string? Nome { get; set; }
        [Required]
        [StringLength(4)]
        public string? Numero { get; set; }
        [Required]
        [StringLength(100)]
        public string? Complemento { get; set; }
        [Required]
        [StringLength(50)]
        public string? Cidade { get; set; }
        [Required]
        [StringLength(2)]
        public string? Uf { get; set; }
        [Required]
        public int ClienteId { get; set; }
        [JsonIgnore]
        public Cliente? Cliente { get; set; }

    }
}
