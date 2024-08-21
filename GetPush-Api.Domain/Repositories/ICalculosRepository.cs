using GetPush_Api.Domain.Commands.Results;
using GetPush_Api.Domain.Commands.Signature;

namespace GetPush_Api.Domain.Repositories
{
    public interface ICalculosRepository
    {
        Task<CalculoCDBResult> CalculoCDB(CalculoCDBSignature signature);

        Task<decimal> CalcularValorFinal(CalculoCDBSignature signature);
        decimal CalcularImposto(CalculoCDBSignature signature);
    }
}
