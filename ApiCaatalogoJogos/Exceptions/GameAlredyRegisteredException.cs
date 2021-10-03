using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCaatalogoJogos.Exceptions
{
    public class GameAlredyRegisteredException : Exception
    {
        public GameAlredyRegisteredException() : base("Este jogo já está cadastrado") { }
    }
}
