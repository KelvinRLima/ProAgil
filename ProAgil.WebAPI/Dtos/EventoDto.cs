using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProAgil.WebAPI.Dtos
{
    public class EventoDto
    {
        public int ID { get; set; }
        public string Local { get; set; }
        public string DataEvento { get; set; }

        [StringLength(100, MinimumLength = 1, ErrorMessage = "A quantidade de caracteres deve ser entre 10 e 100!")]
        [Required(ErrorMessage = "O Tema deve ser preenchido!")]
        public string Tema { get; set; }

        [Range(10, 500)]
        public int QtdPessoas { get; set; }
        public string ImagemURL { get; set; }
        public string Telefone { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        public List<LoteDto> Lotes { get; set; }
        public List<RedeSocialDto> RedesSociais { get; set; }
        public List<PalestranteDto> Palestrantes { get; set; }
    }
}
