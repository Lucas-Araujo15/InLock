using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace senai.inlock.webApi.Domains
{
    public class EstudiosDomain
    {
        public int IdEstudio { get; set; }

        [Required]
        public string NomeEstudio { get; set; }

        public List<JogosDomain> listaDeJogos { get; set; }
    }
}
