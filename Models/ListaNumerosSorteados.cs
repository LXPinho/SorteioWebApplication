using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SorteioWebApplication.Models
{
    public class ListaNumerosSorteados
    {
        [Display(Name = "Id")]
        public int Id { get; set; }
        [Display(Name = "Data")]
        public DateTime Data { get; set; }
        [Display(Name = "Lista de Números Sorteados")]
        public List<NumeroSorteado> ListaNumeros { get; set; } = new List<NumeroSorteado>();
        public ListaNumerosSorteados()
        {
        }
    }
}
