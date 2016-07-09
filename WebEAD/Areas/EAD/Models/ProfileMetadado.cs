using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebEAD.Areas.EAD.Models
{
    public class ProfileMetadado
    {
        public string al_ra { get; set; }
        public string al_nome { get; set; }
        public string turma { get; set; }
        [Required(ErrorMessage = "O E-mail é obrigatório!")]
        [EmailAddress(ErrorMessage = "E-mail em formato inválido.")]
        public string email { get; set; }
        [RegularExpression(@"^(\([0-9]{2}\))\s([9]{1})?([0-9]{4})-([0-9]{4})$", ErrorMessage = "Telefone inválido")]
        public string telefone { get; set; }
        [Required(ErrorMessage = "O celular é obrigatório!")]
        [RegularExpression(@"^(\([0-9]{2}\))\s([9]{1})?([0-9]{4})-([0-9]{4})$", ErrorMessage = "Celular inválido")]
        public string celular { get; set; }

        public string turno { get; set; }
    }

    public class ProfilePasswordMetadado
    {
        [Display(Name = "RA")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Somente são aceitos números.")]
        [Required(ErrorMessage = "Informe o RA", AllowEmptyStrings = false)]
        public string p_eus_login { get; set; }

        [Display(Name = "Senha")]
        [Required(ErrorMessage = "Informe a senha", AllowEmptyStrings = false)]
        [StringLength(12, ErrorMessage = "A {0} deve ter pelo menos {2} caracteres.", MinimumLength = 6)]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Password)]
        public string p_eus_senha { get; set; }

        [Display(Name = "Confirmar Senha")]
        [Required(ErrorMessage = "Informe a confirmação da senha", AllowEmptyStrings = false)]
        [StringLength(12, ErrorMessage = "A {0} deve ter pelo menos {2} caracteres.", MinimumLength = 6)]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Password)]
        public string confirm_eus_senha { get; set; }
    }

    public class SubjectsMetadado
    {
        [Key]
        public long dp_id { get; set; }
        public string dp_descricao { get; set; }
    }
}