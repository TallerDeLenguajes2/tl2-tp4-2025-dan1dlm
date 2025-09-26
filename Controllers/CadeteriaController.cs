using Microsoft.AspNetCore.Mvc;
using SistemaCadeteria;

namespace tl2_tp4_2025_dan1dlm.Controllers
{
    [ApiController]
    [Route("api/{controller}")]
    public class CadeteriaController : ControllerBase
    {
        AccesoADatos acceso = new AccesoADatosCSV();
        public CadeteriaController()
        {
            Cadeteria.Inicializar(acceso);
        }

        [HttpGet("obtenerPedidos")]
        public IActionResult GetPedidos()
        {
            return Ok(Cadeteria.listadoPedidos);
        }

        [HttpGet("obtenerCadetes")]

        public IActionResult GetCadetes()
        {
            return Ok(Cadeteria.listadoCadetes);
        }


        [HttpPost("agregarPedido")]
        public IActionResult AgregarPedido([FromBody] Pedido pedidoNuevo)
        {
            if (Cadeteria.AgregarPedido(pedidoNuevo))
                return Ok("Pedido agregado correctamente");
            else
                return BadRequest("No se pudo agregar el pedido");
        }


        [HttpPut("asignarPedido/{idPedido}/{idCadete}")]
        public IActionResult AsignarPedido(int idPedido, int idCadete)
        {
            if (Cadeteria.AsignarPedido(idPedido, idCadete))
                return Ok("Pedido asignado");
            else
                return BadRequest("No se pudo asignar el pedido");
        }

        [HttpPut("cambiarEstado/{idPedido}")]

        public IActionResult CambiarEstadoPedido(int idPedido)
        {
            if (Cadeteria.CambiarEstadoPedido(idPedido))
                return Ok("Cambio de estado correcto");
            else
                return BadRequest("No se pudo cambiar de estado");
        }

        [HttpPut("ReasignarPedido/{idPedido}/{idNuevoCadete}")]

        public IActionResult ReasignarPedido(int idPedido, int idNuevoCadete)
        {
            if (Cadeteria.ReasignarPedido(idPedido, idNuevoCadete))
                return Ok("Pedido reasignado");
            else
                return BadRequest("No se pudo reasignar el pedido");
        }

    }


}