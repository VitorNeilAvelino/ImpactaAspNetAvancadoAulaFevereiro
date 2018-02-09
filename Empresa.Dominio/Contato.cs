namespace Empresa.Dominio
{
    public class Contato
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Assunto { get; set; }
        public string Mensagem { get; set; }
    }
}