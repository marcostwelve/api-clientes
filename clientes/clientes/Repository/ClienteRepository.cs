using clientes.Context;
using clientes.Models;
using clientes.Paginacao;
using Microsoft.EntityFrameworkCore;

namespace clientes.Repository
{
    public class ClienteRepository : Repository<Cliente>, IClienteRepository
    {
        public ClienteRepository(AppDbContext context) : base(context) { }
        public async Task<ListaPagina<Cliente>> GetClientes(ClienteParametros clienteParametros)
        {
            return await ListaPagina<Cliente>.ParaListaPagina(Get().OrderBy(cl => cl.Id),
                clienteParametros.NumeroPagina, clienteParametros.TamanhoPagina);
        }

        public async Task<bool> GetClientesEmail(string email)
        {
            var clienteExistente = await Get().AnyAsync(e => e.Email == email);
            return clienteExistente;
        }
        public async Task<IEnumerable<Cliente>> GetClientesLogradouros()
        {
            return await Get().Include(x => x.Logradouro).ToListAsync();
        }
    }
}
