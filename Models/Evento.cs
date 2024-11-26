using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaCadastroLogin.Models
{
     [Table("Eventos")] 
   public class Evento
{
    public int Id { get; set; }  
    public string NomeEvento { get; set; }
    public string TipoEvento { get; set; }
    public string DescricaoEvento { get; set; }
    public DateTime DiaEvento { get; set; }
    public string DiaSemanaEvento { get; set; }
    public decimal ValorEvento { get; set; }
}

}
