using clientes.Context;

namespace clientes.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }
        private IClienteRepository _clientesRepo;
        private ILogradouroRepository _logradouroRepo;
        public AppDbContext _context;
        

        public IClienteRepository ClienteRepository
        {
            get
            {
                return _clientesRepo = _clientesRepo ?? new ClienteRepository(_context);
            }
        }

        public ILogradouroRepository LogradouroRepository
        {
            get 
            {
                return _logradouroRepo = _logradouroRepo ?? new LogradouroRepository(_context);
            }
        }

        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
