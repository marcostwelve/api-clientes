namespace clientes.Paginacao
{
    public abstract class QueryStringParametro
    {
        const int paginaMaxima = 50;
        public int NumeroPagina { get; set; } = 1;
        private int _tamanhoPagina;

        public int TamanhoPagina { 
            get 
            {
                return _tamanhoPagina;
            }

            set
            {
                _tamanhoPagina = (value > paginaMaxima) ? paginaMaxima : value;
            }
        }
    }
}
