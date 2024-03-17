using Alura.Estacionamento.Alura.Estacionamento.Modelos;
using Alura.Estacionamento.Modelos;
using System;
using Xunit;
using Xunit.Abstractions;

namespace Alura.Estacionamento.Testes
{
    public class PatioTestes : IDisposable
    {
        private Veiculo veiculo;
        private Patio patio;
        public ITestOutputHelper SaidaConsoleTest;

        // Setup
        public PatioTestes(ITestOutputHelper _saidaConsoleTest)
        {
            SaidaConsoleTest = _saidaConsoleTest;
            SaidaConsoleTest.WriteLine("Construtor invocado.");

            patio = new Patio();
            patio.OperadorPatio = new Operador();
            patio.OperadorPatio.Nome = "Pedro Fagundes";

            veiculo = new Veiculo()
            {
                Tipo = TipoVeiculo.Automovel,
                Proprietario = "Beatriz Silva",
                Cor = "Azul",
                Modelo = "Fusca",
                Placa = "ASD-9999"
            };
        }

        // Cleanup
        public void Dispose()
        {
            SaidaConsoleTest.WriteLine("Dispose invocado.");
        }

        [Fact]
        public void TestaFaturamentoDoEstacionamentoComUmVeiculo()
        {
            //Arrange
            patio.RegistrarEntradaVeiculo(veiculo);
            patio.RegistrarSaidaVeiculo(veiculo.Placa);

            //Act
            double faturamento = patio.TotalFaturado();

            //Asset
            Assert.Equal(2, faturamento);
        }

        [Theory]
        [InlineData("João Silva", "TST-9999", "Preto", "Gol")]
        [InlineData("Maurício Silva", "ABC-9999", "Vermelho", "Fusca")]
        [InlineData("Amanda Silva", "AAF-9999", "Prata", "Opala")]
        public void TestaFaturamentoDoEstacionamentoComVariosVeiculos(string proprietario, string placa, string cor, string modelo)
        {
            //Arrange
            patio.RegistrarEntradaVeiculo(veiculo);
            patio.RegistrarSaidaVeiculo(veiculo.Placa);

            //Act
            double faturamento = patio.TotalFaturado();

            //Asset
            Assert.Equal(2, faturamento);
        }


        [Fact]
        public void TestaLocalizaVeiculoNoPatioComBaseNaTicket()
        {
            // Arrange
            var veiculo = new Veiculo();           

            veiculo.Proprietario = "João Silva";
            veiculo.Placa = "TST-9999";
            veiculo.Cor = "Preto";
            veiculo.Modelo = "Gol";
            patio.RegistrarEntradaVeiculo(veiculo);



            // Act
            var consultado = patio.LocalizaVeiculo(veiculo.IdTicket);

            // Assert
            Assert.Contains(veiculo.IdTicket, consultado.Ticket);
        }

        [Fact]
        public void TestaAlteraDadosDoVeiculo()
        {
            // Arrange
            patio.Veiculos.Add(veiculo);

            var veiculoAlterado = new Veiculo() {
                Cor = "Prata",
                Modelo = "Fusca",
                Proprietario = "Beatriz Silva",
                Placa = "ASD-9999",
                Tipo = TipoVeiculo.Automovel
            };

            // Act
            var alterado = patio.AlterarDadosVeiculo(veiculoAlterado);

            // Arrange
            Assert.Equal(alterado.Cor, veiculoAlterado.Cor);
        }
    }
}
