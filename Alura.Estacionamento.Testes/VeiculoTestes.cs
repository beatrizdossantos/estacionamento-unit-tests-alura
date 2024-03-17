using Alura.Estacionamento.Alura.Estacionamento.Modelos;
using Alura.Estacionamento.Modelos;
using System;
using Xunit;
using Xunit.Abstractions;

namespace Alura.Estacionamento.Testes
{
    public class VeiculoTestes : IDisposable
    {
        private Veiculo veiculo;
        public ITestOutputHelper SaidaConsoleTest;

        // Setup
        public VeiculoTestes(ITestOutputHelper _saidaConsoleTest)
        {
            SaidaConsoleTest = _saidaConsoleTest;
            SaidaConsoleTest.WriteLine("Construtor invocado.");

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
        public void TestaVeiculoAcelerarComParametro10()
        {

            //Act
            veiculo.Acelerar(10);

            //Asset
            Assert.Equal(100, veiculo.VelocidadeAtual);
        }

        [Fact]
        public void TestaVeiculoFrearComParametro10()
        {
            //Act
            veiculo.Frear(10);

            //Asset
            Assert.Equal(-150, veiculo.VelocidadeAtual);
        }

        [Fact]
        public void TestaTipoDoVeiculo()
        {
            //Act
            veiculo.Tipo = TipoVeiculo.Motocicleta;

            //Asset
            Assert.Equal(TipoVeiculo.Motocicleta, veiculo.Tipo);
        }

        [Fact]
        public void TestaVeiculoTipoPadrao()
        {
            //Asset
            Assert.Equal(TipoVeiculo.Automovel, veiculo.Tipo);
        }

        [Fact(Skip = "Teste não implementado")]
        public void ValidaNomeProprietarioDoVeiculo()
        {

        }

        [Fact]
        public void TestaFichaInformacaoDoVeiculo()
        {
            // Act
            string dados = veiculo.ToString();

            // Asset
            Assert.Contains("Tipo do veículo: Automovel", dados);
        }

        [Fact]
        public void TestaNomeProprietarioVeiculoComCaracteresInsuficientes() {

            // Arrange
            string nomeProprietario = "Ab";

            // Assert
            Assert.Throws<System.FormatException> (

                // Act
                () => new Veiculo(nomeProprietario)
            );
        }

        [Fact]
        public void TestaPlacaVeiculoComFormatoInvalido()
        {
            // Arrange
            string placa = "ASDF8888";

            // Assert
            var ex = Assert.Throws<System.FormatException>(

                // Act
                () => new Veiculo().Placa = placa
            );

            // Assert
            Assert.Equal("O 4° caractere deve ser um hífen", ex.Message);
        }

        [Fact]
        public void TestePlacaUltimosCaracteresTendoLetras()
        {
            // Arrange
            string placa = "ASD-88A8";

            // Assert
            var ex = Assert.Throws<System.FormatException>(

                // Act
                () => new Veiculo().Placa = placa
            );

            // Assert
            Assert.Equal("Do 5º ao 8º caractere deve-se ter apenas números!", ex.Message);

        }
    }
}