using System.Collections.Generic;

namespace Loja.Dominio
{
    public class Categoria
    {
        public int Id { get; set; }
        public virtual List<Produto> Produtos { get; set; }
        public string Nome { get; set; }
    }
}