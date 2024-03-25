namespace clientes.Repository
{
    public interface IUnitOfWork
    {
        IClienteRepository ClienteRepository { get; }
        ILogradouroRepository LogradouroRepository { get; }

        Task Commit();
    }
}
