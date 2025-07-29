using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CadastroEmpresasApp.Models
{
    public class Empresa
    {
        [Key]
        public int emp_id { get; set; }

        [Required]
        public string emp_cnpj { get; set; }

        public string emp_nome_empresarial { get; set; }
        public string emp_nome_fantasia { get; set; }
        public string emp_situacao { get; set; }
        public string emp_abertura { get; set; }
        public string emp_tipo { get; set; }
        public string emp_natureza_juridica { get; set; }
        public string emp_atividade_principal { get; set; }

        public string emp_logradouro { get; set; }
        public string emp_numero { get; set; }
        public string emp_complemento { get; set; }
        public string emp_bairro { get; set; }
        public string emp_municipio { get; set; }
        public string emp_uf { get; set; }
        public string emp_cep { get; set; }

        [ForeignKey("Usuario")]
        public int emp_usu_id { get; set; }
        public Usuario Usuario { get; set; }
    }
}
