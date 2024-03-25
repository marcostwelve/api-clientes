using System.ComponentModel.DataAnnotations;

namespace clientes.Models
{
    public class Cliente
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(80)]
        public string? Nome { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "O e-mail está inválido")]
        public string? Email {  get; set; }
        [Required]
        [StringLength(300)]
        public string? Logotipo {  get; set; }
        public ICollection<Logradouro>? Logradouro { get; set; }

    }
}
