using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;

namespace Loja.Mvc.Hubs
{
    public class LeilaoHub : Hub
    {
        public async Task Participar(string nomeParticipante, string produtoId)
        {
            await Groups.Add(Context.ConnectionId, produtoId);
            Clients.Group(produtoId).adicionarMensagem(nomeParticipante, Context.ConnectionId, "Bom leilão a todos!");
        }

        public void EnviarLance(string nomeParticipante, string valor, string produtoId)
        {
            Clients.Group(produtoId).adicionarMensagem(nomeParticipante, Context.ConnectionId, valor);
        }

        public void EnviarLike(string nomeParticipante, string connectionIdDestinatario)
        {
            Clients.Client(connectionIdDestinatario).receberLike(nomeParticipante);
        }
    }
}