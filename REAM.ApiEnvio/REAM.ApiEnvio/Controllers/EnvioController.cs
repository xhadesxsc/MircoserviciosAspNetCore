using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using REAM.ApiEnvio.Helper;
using REAM.ApiEnvio.Model;

namespace REAM.ApiEnvio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EnvioController : ControllerBase
    {
        private readonly EnvioTopicHelper topicHelper;

        public EnvioController(EnvioTopicHelper topicHelper)
        {
            this.topicHelper = topicHelper;
        }


        [HttpPost]
        public async Task<IActionResult> RecibirPedido([FromBody] CompraRequest request)
        {

            if (string.IsNullOrEmpty(request.NombreProveedor))
            {
                return BadRequest("Debe enviar el nombre de Articulo");
            }
            await topicHelper.SendMessage(request);
            return Ok("Datos Enviados correctamente");
        }
    }
}