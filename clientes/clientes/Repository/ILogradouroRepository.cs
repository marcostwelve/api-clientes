using clientes.Models;
using clientes.Paginacao;

namespace clientes.Repository
{
    public interface ILogradouroRepository : IRepository<Logradouro>
    {
        Task<ListaPagina<Logradouro>> GetLogradouros(LogradouroParametros logradouroParametros);
    }
}
