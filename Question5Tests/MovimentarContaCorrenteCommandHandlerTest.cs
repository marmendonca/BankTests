using NSubstitute;
using TestQuestion5.Application.Commands;
using TestQuestion5.Application.Handlers;
using TestQuestion5.Domain.Entities;
using TestQuestion5.Domain.Enumerators;
using TestQuestion5.Domain.Exceptions;
using TestQuestion5.Domain.Interfaces.Repositories;

namespace Question5Tests
{
    public class MovimentarContaCorrenteCommandHandlerTest
    {
        [Fact]
        public void MovimentacaoContaCorrente_Handler_ValidRequest_ReturnaIdMovimento()
        {
            // Arrange
            var contaCorrenteRepository = Substitute.For<IContaCorrenteRepository>();

            var contaCorrente = new ContaCorrente(
                "B6BAFC09 -6967-ED11-A567-055DFA4A16C9",
                123,
                "",
                true);

            contaCorrenteRepository
                .GetByIdAsync("B6BAFC09 -6967-ED11-A567-055DFA4A16C9")
                .ReturnsForAnyArgs(contaCorrente);

            var movimentoRepository = Substitute.For<IMovimentoRepository>();
            var idempotenciaRepository = Substitute.For<IIdempotenciaRepository>();
            var handler = new MovimentarContaCorrenteCommandHandler(
                contaCorrenteRepository,
                movimentoRepository,
                idempotenciaRepository);

            var command = new MovimentarContaCorrenteCommand
            {
                IdRequisicao = "1",
                IdContaCorrente = "123",
                Valor = 100,
                TipoMovimento = TipoMovimento.C
            };

            // Act
            var result = handler.Handle(command, default).Result;

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public void MovimentacaoContaCorrente_Handler_RequisicaoInvalida_ThrowsException_INVALID_ACCOUNT()
        {
            // Arrange
            var contaCorrenteRepository = Substitute.For<IContaCorrenteRepository>();
            var movimentoRepository = Substitute.For<IMovimentoRepository>();
            var idempotenciaRepository = Substitute.For<IIdempotenciaRepository>();

            var handler = new MovimentarContaCorrenteCommandHandler(contaCorrenteRepository,
                movimentoRepository,
                idempotenciaRepository);

            var command = new MovimentarContaCorrenteCommand
            {
                IdRequisicao = "123",
                IdContaCorrente = "789",
                Valor = 0,
                TipoMovimento = TipoMovimento.D
            };

            // Act & Assert
            var exception = Assert.ThrowsAsync<DomainException>(() => handler.Handle(command, default));

            Assert.Same("INVALID_ACCOUNT", exception.Result.Type);
        }

        [Fact]
        public void MovimentacaoContaCorrente_Handler_RequisicaoInvalida_ThrowsException_INVALID_VALUE()
        {
            // Arrange
            var contaCorrenteRepository = Substitute.For<IContaCorrenteRepository>();

            var contaCorrente = new ContaCorrente(
                "B6BAFC09 -6967-ED11-A567-055DFA4A16C9",
                123,
                "",
                true);

            contaCorrenteRepository
                .GetByIdAsync("B6BAFC09 -6967-ED11-A567-055DFA4A16C9")
                .ReturnsForAnyArgs(contaCorrente);

            var movimentoRepository = Substitute.For<IMovimentoRepository>();
            var idempotenciaRepository = Substitute.For<IIdempotenciaRepository>();

            var handler = new MovimentarContaCorrenteCommandHandler(contaCorrenteRepository,
                movimentoRepository,
                idempotenciaRepository);

            var command = new MovimentarContaCorrenteCommand
            {
                IdRequisicao = "123",
                IdContaCorrente = "789",
                Valor = -1,
                TipoMovimento = TipoMovimento.D
            };

            // Act & Assert
            var exception = Assert.ThrowsAsync<DomainException>(() => handler.Handle(command, default));

            Assert.Same("INVALID_VALUE", exception.Result.Type);
        }

        [Fact]
        public void MovimentacaoContaCorrente_Handler_RequisicaoInvalida_ThrowsException_INACTIVE_ACCOUNT()
        {
            // Arrange
            var contaCorrenteRepository = Substitute.For<IContaCorrenteRepository>();

            var contaCorrente = new ContaCorrente(
                "B6BAFC09 -6967-ED11-A567-055DFA4A16C9",
                123,
                "",
                false);

            contaCorrenteRepository
                .GetByIdAsync("B6BAFC09 -6967-ED11-A567-055DFA4A16C9")
                .ReturnsForAnyArgs(contaCorrente);

            var movimentoRepository = Substitute.For<IMovimentoRepository>();
            var idempotenciaRepository = Substitute.For<IIdempotenciaRepository>();

            var handler = new MovimentarContaCorrenteCommandHandler(contaCorrenteRepository,
                movimentoRepository,
                idempotenciaRepository);

            var command = new MovimentarContaCorrenteCommand
            {
                IdRequisicao = "123",
                IdContaCorrente = "789",
                Valor = 1,
                TipoMovimento = TipoMovimento.D
            };

            // Act & Assert
            var exception = Assert.ThrowsAsync<DomainException>(() => handler.Handle(command, default));

            Assert.Same("INACTIVE_ACCOUNT", exception.Result.Type);
        }
    }
}