using GetPush_Api.Domain.Commands.Interface;
using GetPush_Api.Domain.Commands.Signature;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace GetPush_Api.Controllers
{
    [ApiController]
    [Route("v1")]
    public class CalculosController : BaseController
    {
        private readonly ICalculosCommandHandler _handler;

        public CalculosController(ICalculosCommandHandler handler)
        {
            _handler = handler;
        }

        [HttpPost]
        [Route(nameof(CalculoCDB))]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [SwaggerOperation(Summary = "Calculo", Description = "Faz calculo de CDB")]
        [AllowAnonymous]
        public async Task<IActionResult> CalculoCDB([FromBody] CalculoCDBSignature signature)
        {
            try
            {
                if (signature.valorMonetario < 0)
                    return BadRequest("Valor monetário não pode ser negativo.");

                var result = await _handler.CalculoCDB(signature);

                return ApiResponse(true, "Executado com sucesso", result);
            }
            catch(Exception ex)
            {
                return ErrorResponse($"Erro: {ex.Message}");
            }
        }
    }
}
