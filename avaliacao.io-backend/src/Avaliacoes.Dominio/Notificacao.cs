using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avaliacoes.Dominio
{
    public class Notificacao
    {
        public Notificacao(string mensagem, string propriedade)
        {
            Mensagem = mensagem;
            Propriedade = propriedade;
        }

        public string Mensagem { get; set; }
        public string Propriedade { get; set; }
    }
}
