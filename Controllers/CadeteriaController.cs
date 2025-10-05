using Microsoft.AspNetCore.Mvc;
using SistemaCadeteria;

namespace tl2_tp4_2025_dan1dlm.Controllers
{
    [ApiController]
    [Route("api/{controller}")]
    public class CadeteriaController : ControllerBase
    {
        private Cadeteria cadeteria;
        private AccesoADastosCadeteria ADCadeteria;
        private AccesoADastosCadete ADCadete;
        private AccesoADastosPedido ADPedido;
        AccesoADatos acceso = new AccesoADatosCSV();
        public CadeteriaController()
        {
            ADCadeteria = new AccesoADastosCadeteria();
            ADCadete = new AccesoADastosCadete();
            ADPedido = new AccesoADastosPedido();

            cadeteria = ADCadeteria.Obtener("");
            cadeteria.agregarListaCadetes(ADCadete.Obtener(""));
            cadeteria.agregarListaPedidos(ADPedido.Obtener(""));
        }

        [HttpGet("obtenerPedidos")]
        public IActionResult GetPedidos()
        {
            return Ok(cadeteria.listadoPedidos);
        }

        [HttpGet("obtenerCadetes")]

        public IActionResult GetCadetes()
        {
            return Ok(cadeteria.listadoCadetes);
        }


        [HttpPost("agregarPedido")]
        public IActionResult AgregarPedido([FromBody] Pedido pedidoNuevo)
        {
            if (cadeteria.AgregarPedido(pedidoNuevo)) {
                ADPedido.Guardar(cadeteria.listadoPedidos, "");
                return Ok("Pedido agregado correctamente");
            }
            else
                return BadRequest("No se pudo agregar el pedido");
        }

    


        [HttpPut("asignarPedido/{idPedido}/{idCadete}")]
        public IActionResult AsignarPedido(int idPedido, int idCadete)
        {
            if (cadeteria.AsignarPedido(idPedido, idCadete)) {
                ADPedido.Guardar(cadeteria.listadoPedidos, "");
                return Ok("Pedido asignado");
            }
            else
                return BadRequest("No se pudo asignar el pedido");
        }

        [HttpPut("cambiarEstado/{idPedido}")]

        public IActionResult CambiarEstadoPedido(int idPedido)
        {
            if (cadeteria.CambiarEstadoPedido(idPedido)) {
                ADPedido.Guardar(cadeteria.listadoPedidos, "");                
                return Ok("Cambio de estado correcto");
            }
            else
                return BadRequest("No se pudo cambiar de estado");
        }

        [HttpPut("ReasignarPedido/{idPedido}/{idNuevoCadete}")]
        public IActionResult ReasignarPedido(int idPedido, int idNuevoCadete)
        {
            if (cadeteria.ReasignarPedido(idPedido, idNuevoCadete)) {
                ADPedido.Guardar(cadeteria.listadoPedidos, "");                
                return Ok("Pedido reasignado");
            }
            else
                return BadRequest("No se pudo reasignar el pedido");
        }

    }

    


}