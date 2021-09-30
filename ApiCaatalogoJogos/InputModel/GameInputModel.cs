using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCaatalogoJogos.InputModel
{
    public class GameInputModel
    {
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome do jogo deve conter entre 3 a 100 caracteres")]
        public string Name { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome do jogo deve conter entre 3 a 100 caracteres")]
        public string Producer { get; set; }
        [Required]
        [Range(1,1000, ErrorMessage = "O preço do jogo deve estar entre no mínimo 1 real e no máximo 1000 reais")]
        public double Price { get; set; }
    }
}
