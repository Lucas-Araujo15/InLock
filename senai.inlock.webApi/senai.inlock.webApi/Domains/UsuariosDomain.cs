using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace senai.inlock.webApi.Domains
{
    public class UsuariosDomain
    {
        public int IdUsuario { get; set; }

        [Required]
        public string NomeUsuario { get; set; }     

        [Required]
        public int IdTipoUsuario { get; set; }

        [Required(ErrorMessage = "Informe o seu e-mail")]
        public string email { get; set; }

        [Required(ErrorMessage = "Informe a sua senha")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Sua senha deverá ser composta por no mínimo 5 e no máximo 20 caractéres")]
        public string senha { get; set; }

        public TiposUsuariosDomain TipoUsuario { get; set; }
    }
}
