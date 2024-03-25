using clientes.Models;

namespace clientes.DTO_s
{
    public class ClienteDTO
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public string? Logotipo { get; set; }
        public ICollection<Logradouro>? Logradouro  {get; set;}
    }
}
