using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Empresa.Dominio
{
    public class Contato
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Assunto { get; set; }
        public string Mensagem { get; set; }
    }
}
