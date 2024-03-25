namespace clientes.DTO_s
{
    public class UsuarioToken
    {
        public bool Autenticado { get; set; }
        public DateTime DataExpirar { get; set; }
        public string? Token { get; set; }
        public string Mensagem {  get; set; }
    }
}
