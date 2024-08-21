using GetPush_Api.Domain.Commands.Interface;
using GetPush_Api.Domain.Commands.Results;
using GetPush_Api.Domain.Commands.Signature;
using GetPush_Api.Domain.Repositories;

namespace GetPush_Api.Domain.Commands.Handlers
{
    public class CalculosCommandHandler : ICalculosCommandHandler
    {
        private readonly ICalculosRepository _repository;

        public CalculosCommandHandler(ICalculosRepository repository)
        {
            _repository = repository;
        }

        public async Task<CalculoCDBResult> CalculoCDB(CalculoCDBSignature signature)
        {
            var valorFinal = await _repository.CalcularValorFinal(signature);
            var imposto = _repository.CalcularImposto(signature);
            var valorLiquido = valorFinal - imposto;

            return new CalculoCDBResult
            {
                resultadoLiquido = Math.Round(valorLiquido, 2),
                resultadoBruto = Math.Round(valorFinal, 2)
            };
        }
    }
}
