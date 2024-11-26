using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaCadastroLogin.Models
{
    [Table("usuario")] 
    public class Usuario
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; } = "Viewer";  // Define "Viewer" como padrão 
    }
}
