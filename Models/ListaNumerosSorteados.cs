namespace SorteioWebApplication.Models
{
    public class ListaNumerosSorteados
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public List<NumeroSorteado> listaNumerosSorteados { get; set; } = new List<NumeroSorteado>();
        public ListaNumerosSorteados()
        {
        }
    }
}
