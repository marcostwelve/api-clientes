using clientes.Models;
using clientes.Paginacao;

namespace clientes.Repository
{
    public interface IClienteRepository : IRepository<Cliente>
    {
        Task<ListaPagina<Cliente>> GetClientes(ClienteParametros clienteParametros);

        Task<bool> GetClientesEmail(string email);
        Task<IEnumerable<Cliente>> GetClientesLogradouros();
    }
}
