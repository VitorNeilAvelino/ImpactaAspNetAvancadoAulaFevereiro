using System;

namespace BancoImobiliario.Dominio
{
    public class Dado
    {
        public Dado(int quantidadeDados)
        {
            Lancar(quantidadeDados);
        }
        private void Lancar(int quantidadeDados)
        {
            Resultados = new int[quantidadeDados];

            for (int i = 0; i < quantidadeDados; i++)
            {
                Resultados[i] = new Random(Guid.NewGuid().GetHashCode()).Next(1, 7);
                Soma += Resultados[i];
            }
        }

        public int Soma { get; private set; }
        public int[] Resultados { get; set; }
    }
}