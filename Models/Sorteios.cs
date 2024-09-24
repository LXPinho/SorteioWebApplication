using System.Collections.Generic;

namespace SorteioWebApplication.Models
{
    public class Sorteios
    {
        public int NumeroDoSorteio { get; set; } = 0;
        public int QtdeNumerosSorteados { get; set; } = 0;
        public int VibesAculumadas { get; set; } = 0;
        public List<ListaNumerosSorteados> ListaSorteios { get; set; } = new List<ListaNumerosSorteados>();
        public string Texto { get; set; } = string.Empty;
        public Sorteios() 
        {
        }
    }
}
