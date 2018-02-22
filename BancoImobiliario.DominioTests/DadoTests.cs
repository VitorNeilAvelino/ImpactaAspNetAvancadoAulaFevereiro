using Microsoft.VisualStudio.TestTools.UnitTesting;
using BancoImobiliario.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoImobiliario.Dominio.Tests
{
    [TestClass()]
    public class DadoTests
    {
        [TestMethod()]
        public void DadoTest()
        {
            var dado = new Dado(2);

            Assert.IsTrue(dado.Resultados.Count() == 2);

            Console.WriteLine(dado.Resultados[0]);
            Console.WriteLine(dado.Resultados[1]);
        }
    }
}