using clientes.Context;
using clientes.Models;
using clientes.Paginacao;

namespace clientes.Repository
{
    public class LogradouroRepository : Repository<Logradouro>, ILogradouroRepository
    {
        public LogradouroRepository(AppDbContext context): base(context) { }
        public async Task<ListaPagina<Logradouro>> GetLogradouros(LogradouroParametros logradouroParametros)
        {
            return await ListaPagina<Logradouro>.ParaListaPagina(Get().OrderBy(lo => lo.Id), logradouroParametros.NumeroPagina, logradouroParametros.TamanhoPagina);
        }
    }
}
