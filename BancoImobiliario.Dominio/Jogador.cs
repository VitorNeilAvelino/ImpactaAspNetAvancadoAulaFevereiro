namespace BancoImobiliario.Dominio
{
    public class Jogador
    {
        public Jogador(int numeroCor)
        {
            Cor = (Cor)numeroCor;
        }

        public static int SaldoInicial { get; internal set; } = 1500;
        public Cor Cor { get; set; }
        public int Saldo { get; set; } = SaldoInicial;
    }
}