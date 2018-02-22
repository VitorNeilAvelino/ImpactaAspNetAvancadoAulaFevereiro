using System;
using System.Collections.Generic;

namespace BancoImobiliario.Dominio
{
    public class Mesa
    {
        public Mesa(int quantidadeJogadores)
        {
            InicializarJogo(quantidadeJogadores);
        }

        private void InicializarJogo(int quantidadeJogadores)
        {
            if (quantidadeJogadores < 2 || quantidadeJogadores > 6)
            {
                throw new ArgumentOutOfRangeException(nameof(quantidadeJogadores), $"O campo {nameof(quantidadeJogadores)} deve estar entre 2 e 6.");
            }

            Tabuleiro = new Tabuleiro();
            Banco = new Banco();
            Jogadores = new List<Jogador>();

            for (int i = 1; i <= quantidadeJogadores; i++)
            {
                Jogadores.Add(new Jogador(i));
                Banco.Saldo -= Jogador.SaldoInicial;
            }
        }

        public Tabuleiro Tabuleiro { get; set; }
        public Banco Banco { get; set; }
        public List<Jogador> Jogadores { get; set; }

        public Dado[] Dados { get; set; }
    }
}