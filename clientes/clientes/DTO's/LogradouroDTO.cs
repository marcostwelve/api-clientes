namespace clientes.DTO_s
{
    public class LogradouroDTO
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Numero { get; set; }
        public string? Complemento { get; set; }
        public string? Cidade { get; set;}
        public string? Uf { get; set;}
        public int ClienteId {  get; set; }
    }
}
