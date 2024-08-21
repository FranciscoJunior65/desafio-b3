using GetPush_Api.Domain.Commands.Results;
using GetPush_Api.Domain.Commands.Signature;

namespace GetPush_Api.Domain.Commands.Interface
{
    public interface ICalculosCommandHandler
    {
        Task<CalculoCDBResult> CalculoCDB(CalculoCDBSignature signature);
    }
}
