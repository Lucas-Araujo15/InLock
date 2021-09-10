using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace senai.inlock.webApi.Domains
{
    public class JogosDomain
    {
        public int IdJogo { get; set; }

        [Required]
        public int IdEstudio { get; set; }

        [Required]
        public string NomeJogo { get; set; }

        [Required]
        public int DescricaoJogo { get; set; }

        [Required]
        public DateTime DataLancamento { get; set; }

        [Required]
        public decimal ValorJogo { get; set; }

        public EstudiosDomain Estudio { get; set; }
    }
}
