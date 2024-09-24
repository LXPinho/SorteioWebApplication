using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SorteioWebApplication.Models
{
    public class Sorteios
    {
        [Display(Name = "Número do Sorteio")]
        public int NumeroDoSorteio { get; set; } = 0;
        [Display(Name = "Qtde de Números Sorteados")]
        public int QtdeNumerosSorteados { get; set; } = 0;
        [Display(Name = "Vibes Acululadas")]
        public int VibesAculumadas { get; set; } = 0;
        [Display(Name = "Lista de Números Sorteados")]
        public List<ListaNumerosSorteados> ListaSorteios { get; set; } = new List<ListaNumerosSorteados>();
        public string Texto { get; set; } = string.Empty;
        public Sorteios() 
        {
        }
    }
}
