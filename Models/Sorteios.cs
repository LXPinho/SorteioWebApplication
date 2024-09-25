using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

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
        [Display(Name = "Lista de Números Sorteados")]
        public string NumerosSorteados
        {
            get
            {
                string result = string.Empty;
                foreach (var item1 in ListaSorteios)
                    foreach (var item2 in item1.ListaNumeros)
                        result += (string.IsNullOrEmpty(result) ? "" : ", ") + item2.Numero.ToString();
                return result;
            }
        }
        public Sorteios()
        {
        }
    }
}
