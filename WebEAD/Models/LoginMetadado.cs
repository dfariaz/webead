using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebEAD.Models
{
    public class LoginMetadado
    {
        [Display(Name = "Nome de Usuário")]
        [Required(ErrorMessage = "Informe o nome de usuário", AllowEmptyStrings = false)]
        public string eus_login { get; set; }

        [Display(Name = "Senha")]
        [Required(ErrorMessage = "Informe a senha", AllowEmptyStrings = false)]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Password)]
        public string eus_senha { get; set; }
    }

    public class ForgotPasswordMetadado
    {
        [Display(Name = "Nome de Usuário")]
        [Required(ErrorMessage = "Informe o nome de usuário", AllowEmptyStrings = false)]
        public string eus_login { get; set; }
    }
}