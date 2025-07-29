using System.ComponentModel.DataAnnotations;

namespace CadastroEmpresasApp.Models
{
    public class Usuario
    {
        [Key]
        public int usu_id { get; set; }

        [Required]
        public string usu_nome { get; set; }

        [Required]
        [EmailAddress]
        public string usu_email { get; set; }

        [Required]
        public string usu_senha_hash { get; set; }
    }
}
