using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;

namespace SorteioWebApplication.Models
{
    public class NumeroSorteado
    {
        [Display(Name = "Id do Sorteio")]
        public int IdSorteio { get; set; }
        [Display(Name = "Número do Sorteio")]
        public int Numero { get; set; }
        public NumeroSorteado( int idSorteio, int numero)
        {
            IdSorteio = idSorteio;
            Numero = numero;
        }
    }
}
