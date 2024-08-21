using GetPush_Api.Domain.Commands.Results;
using GetPush_Api.Domain.Commands.Signature;
using GetPush_Api.Domain.Repositories;

namespace GetPush_Api.Infra.Repositories
{
    public class CalculosRepository : ICalculosRepository
    {
        private const decimal TaxaCDI = 0.009m;
        private const decimal TaxaBancaria = 1.08m;
        public Task<CalculoCDBResult> CalculoCDB(CalculoCDBSignature signature)
        {
            throw new NotImplementedException();
        }

        public async Task<decimal> CalcularValorFinal(CalculoCDBSignature signature)
        {
            var valorInicial = signature.valorMonetario;
            var valorFinal = signature.valorMonetario;

            for (int i = 0; i < signature.prazoMeses; i++)
            {
                valorFinal = valorInicial * (1 + (TaxaCDI * TaxaBancaria));
                valorInicial = valorFinal;
            }

            return await Task.FromResult(valorFinal);
        }

        public decimal CalcularImposto(CalculoCDBSignature signature)
        {
            decimal aliquota;

            if (signature.prazoMeses <= 6)
                aliquota = 0.225m;
            else if (signature.prazoMeses <= 12)
                aliquota = 0.20m;
            else if (signature.prazoMeses <= 24)
                aliquota = 0.175m;
            else
                aliquota = 0.15m;

            var imposto = signature.valorMonetario * aliquota;

            return imposto;
        }
    }
}
