using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SorteioWebApplication.Models
{
    public interface IBaseModel
    {
        [Column("Id")]
        [Display(Name = "Código")]
        public int Id { get; set; }
    }
}
