using Microsoft.EntityFrameworkCore;

namespace clientes.Paginacao
{
    public class ListaPagina<T> : List<T>
    {
        public int PaginaAtual { get; set; }
        public int TotalPagina { get; set; }
        public int TamanhoPagina { get; set; }
        public int ContagemTotal {  get; set; }

        public bool PaginaAnterior => PaginaAtual > 1;
        public bool ProximaPagina => PaginaAtual < TotalPagina;

        public ListaPagina(List<T> itens, int contador, int numeroPagina, int tamanhoPagina)
        {
            ContagemTotal = contador;
            TamanhoPagina = tamanhoPagina;
            PaginaAtual = numeroPagina;
            ContagemTotal = (int)Math.Ceiling(contador / (double)tamanhoPagina);

            AddRange(itens);
        }

        public async static Task<ListaPagina<T>> ParaListaPagina(IQueryable<T> source, int numeroPagina, int tamanhoPagina)
        {
            var contador = source.Count();
            var itens = await source.Skip((numeroPagina - 1) * tamanhoPagina).Take(tamanhoPagina).ToListAsync();

            return new ListaPagina<T>(itens, contador, numeroPagina, tamanhoPagina);
        }
        
    }
}
