const Index = {
    viewModel: {
        produtos: ko.observableArray()
    },

    inicializar: function () {
        this.conectarLeilaoHub();
        ko.applyBindings(this.viewModel);
    },

    conectarLeilaoHub: function () {
        const self = this;
        const connection = $.hubConnection();
        const hub = connection.createHubProxy("LeilaoHub");

        hub.on("atualizarOfertas", function () { self.obterOfertas(); });

        connection.start();
    },

    obterOfertas: function () {
        this.viewModel.produtos.push({
            id: 3,
            nome: "Batom",
            preco: 22.03,
            estoque: 3,
            categoriaNome: "Cosméticos"
        });
    }
};