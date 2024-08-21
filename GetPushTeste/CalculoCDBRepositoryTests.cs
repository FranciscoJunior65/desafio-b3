using GetPush_Api.Domain.Commands.Handlers;
using GetPush_Api.Domain.Commands.Signature;
using GetPush_Api.Domain.Repositories;
using Moq;

namespace GetPushTeste
{
    public class CalculoCDBRepositoryTests
    {
        public class CalculoCDBHandlerTests
        {
            private readonly Mock<ICalculosRepository> _mockRepository;
            private readonly CalculosCommandHandler _handler;

            public CalculoCDBHandlerTests()
            {
                _mockRepository = new Mock<ICalculosRepository>();
                _handler = new CalculosCommandHandler(_mockRepository.Object);
            }

            [Fact]
            public async Task CalcularCDB_DeveCalcularCorretamenteParaUmMes()
            {
                var signature = new CalculoCDBSignature
                {
                    valorMonetario = 1000m,
                    prazoMeses = 1
                };

                _mockRepository.Setup(repo => repo.CalcularValorFinal(signature)).ReturnsAsync(1009.72m);
                _mockRepository.Setup(repo => repo.CalcularImposto(signature)).Returns(2.18m);

                var result = await _handler.CalculoCDB(signature);

                Assert.Equal(1009.72m, result.resultadoBruto);
                Assert.Equal(1007.54m, result.resultadoLiquido);
            }

            [Fact]
            public async Task CalcularCDB_DeveRetornarZeroParaValorInicialZero()
            {
                var signature = new CalculoCDBSignature
                {
                    valorMonetario = 0m,
                    prazoMeses = 12
                };

                _mockRepository.Setup(repo => repo.CalcularValorFinal(signature)).ReturnsAsync(0m);
                _mockRepository.Setup(repo => repo.CalcularImposto(signature)).Returns(0m);

                var result = await _handler.CalculoCDB(signature);

                Assert.Equal(0m, result.resultadoBruto);
                Assert.Equal(0m, result.resultadoLiquido);
            }

            [Fact]
            public async Task CalcularCDB_DeveCalcularCorretamentePara80Meses()
            {
                var signature = new CalculoCDBSignature
                {
                    valorMonetario = 1000m,
                    prazoMeses = 80
                };

                _mockRepository.Setup(repo => repo.CalcularValorFinal(signature)).ReturnsAsync(1800m);
                _mockRepository.Setup(repo => repo.CalcularImposto(signature)).Returns(200m);

                var result = await _handler.CalculoCDB(signature);

                Assert.Equal(1800m, result.resultadoBruto);
                Assert.Equal(1600m, result.resultadoLiquido);
            }

            [Fact]
            public async Task CalcularCDB_DeveLancarExcecaoParaValorInicialNegativo()
            {
                var signature = new CalculoCDBSignature
                {
                    valorMonetario = -1000m,
                    prazoMeses = 12
                };

                _mockRepository.Setup(repo => repo.CalcularValorFinal(signature))
                    .ThrowsAsync(new ArgumentException("Valor monetário não pode ser negativo."));

                var exception = await Assert.ThrowsAsync<ArgumentException>(() => _handler.CalculoCDB(signature));

                Assert.Equal("Valor monetário não pode ser negativo.", exception.Message);
            }

            [Fact]
            public async Task CalcularCDB_DeveLancarExcecaoParaPrazosNegativos()
            {
                var signature = new CalculoCDBSignature
                {
                    valorMonetario = 1000m,
                    prazoMeses = -12
                };

                _mockRepository.Setup(repo => repo.CalcularValorFinal(signature))
                    .ThrowsAsync(new ArgumentException("Prazo não pode ser negativo."));

                var exception = await Assert.ThrowsAsync<ArgumentException>(() => _handler.CalculoCDB(signature));

                Assert.Equal("Prazo não pode ser negativo.", exception.Message);
            }

            [Fact]
            public async Task CalcularCDB_DeveCalcularCorretamenteParaDoisMeses()
            {
                var signature = new CalculoCDBSignature
                {
                    valorMonetario = 1000m,
                    prazoMeses = 2
                };

                _mockRepository.Setup(repo => repo.CalcularValorFinal(signature)).ReturnsAsync(1020m);
                _mockRepository.Setup(repo => repo.CalcularImposto(signature)).Returns(4m);

                var result = await _handler.CalculoCDB(signature);

                Assert.Equal(1020m, result.resultadoBruto);
                Assert.Equal(1016m, result.resultadoLiquido);
            }

            [Fact]
            public async Task CalcularCDB_DeveCalcularCorretamenteParaValoresMuitoPequenos()
            {
                var signature = new CalculoCDBSignature
                {
                    valorMonetario = 0.01m,
                    prazoMeses = 12
                };

                _mockRepository.Setup(repo => repo.CalcularValorFinal(signature)).ReturnsAsync(0.02m);
                _mockRepository.Setup(repo => repo.CalcularImposto(signature)).Returns(0.005m);
                              
                var result = await _handler.CalculoCDB(signature);

                Assert.Equal(Math.Round(0.02m, 2), result.resultadoBruto);
                Assert.Equal(Math.Round(0.015m, 2), result.resultadoLiquido);
            }


            [Fact]
            public async Task CalcularCDB_DeveCalcularCorretamenteParaValorMonetarioMuitoGrande()
            {
                var signature = new CalculoCDBSignature
                {
                    valorMonetario = 1_000_000_000m, // 1 bilhão
                    prazoMeses = 12
                };

                _mockRepository.Setup(repo => repo.CalcularValorFinal(signature)).ReturnsAsync(1_090_000_000m); // Exemplo de valor final
                _mockRepository.Setup(repo => repo.CalcularImposto(signature)).Returns(90_000_000m); // Exemplo de imposto

                var result = await _handler.CalculoCDB(signature);

                Assert.Equal(1_090_000_000m, result.resultadoBruto);
                Assert.Equal(1_000_000_000m, result.resultadoLiquido);
            }

            [Fact]
            public async Task CalcularCDB_DeveCalcularCorretamenteParaPrazoDeMesesMuitoGrande()
            {
                var signature = new CalculoCDBSignature
                {
                    valorMonetario = 1000m,
                    prazoMeses = 1000
                };

                _mockRepository.Setup(repo => repo.CalcularValorFinal(signature)).ReturnsAsync(2_500_000m); // Exemplo de valor final após 1000 meses
                _mockRepository.Setup(repo => repo.CalcularImposto(signature)).Returns(1_000_000m); // Exemplo de imposto

                var result = await _handler.CalculoCDB(signature);

                Assert.Equal(2_500_000m, result.resultadoBruto);
                Assert.Equal(1_500_000m, result.resultadoLiquido);
            }
        }
    }
}
