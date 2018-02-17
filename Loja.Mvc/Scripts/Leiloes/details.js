// Kyle Simpson - object literals
var Details = {
    leilaoHub: {},
    produtoId: 0,
    connectionId: "",
    nomeParticipante: "",

    inicializar: function (produtoId) {
        this.produtoId = produtoId;
        this.conectarLeilaoHub();
        this.vincularEventos();
    },

    conectarLeilaoHub: function () {
        const self = this;
        const connection = $.hubConnection();
        this.leilaoHub = connection.createHubProxy("LeilaoHub");

        //hub.on("atualizarOfertas", function () { document.location.reload(); });

        connection.start().done(function () {
            self.connectionId = connection.id;
        });
    },

    vincularEventos: function () {
        const self = this;

        $("#entrarButton").on("click", function () { self.entrarLeilao(); });

        this.leilaoHub.on("adicionarMensagem", function (nomeParticipante, connectionId, mensagem) {
            self.adicionarMensagem(nomeParticipante, connectionId, mensagem);
        });
    },

    entrarLeilao: function () {
        this.nomeParticipante = $("#nomeParticipante").val();

        this.leilaoHub.invoke("Participar", this.nomeParticipante, this.produtoId);

        $("#participanteDiv").hide();
        $("#lanceDiv").show();
        $("#valorLance").focus();
    },

    adicionarMensagem: function (nomeParticipante, connectionId, mensagem) {
        $("#lancesRealizadosTable").append(this.montarMensagem(nomeParticipante, connectionId, mensagem));
    },

    montarMensagem: function (nomeRemetente, connectionId, mensagem) {
        var tr = "<tr>";
        tr += "<td>" + nomeRemetente + "</td>";
        tr += "<td>" + mensagem + "</td>";

        var like = "<a data-connection-id='" + connectionId + "' href='#'>" +
                    "<span class='glyphicon glyphicon-thumbs-up' style='font-size:18px'></span></a>";
        var enviadaPorMim = this.connectionId === connectionId;

        tr += "<td>" + (enviadaPorMim ? "" : like) + "</td>";

        tr += "</tr>";

        return tr;
    }
};