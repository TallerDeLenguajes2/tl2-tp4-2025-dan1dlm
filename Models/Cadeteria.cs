using System;
using System.Data.Common;
using System.IO.Compression;
using System.Runtime.InteropServices;

namespace SistemaCadeteria
{
    public static class Cadeteria
    {
        private static AccesoADatos Acceso;
        private static string Nombre { get; set; }
        private static string Telefono { get; set; }
        private static List<Cadete> ListadoCadetes = new List<Cadete>();
        private static List<Pedido> ListadoTotalPedidos = new List<Pedido>();
        
        public static void Inicializar(AccesoADatos acceso)
        {
            Acceso = acceso;
            ListadoCadetes = acceso.CargarCadetes("Datos/cadetes.csv");
            ListadoTotalPedidos = acceso.CargarPedidos("Datos/pedidos.csv");
        }

        public static List<Cadete> listadoCadetes => ListadoCadetes;
        public static List<Pedido> listadoPedidos => ListadoTotalPedidos;

        //AGREGAR-BORRAR OBJETOS
        public static bool AgregarCadete(Cadete c)
        {
            if (!ListadoCadetes.Contains(c))
            {
                c.IdCadete = ListadoCadetes.Any() ? ListadoCadetes.Max(p => p.IdCadete) + 1 : 1;
                ListadoCadetes.Add(c);
                Acceso.GuardarCadetes("Informes/ResultadoCadetes.csv", listadoCadetes);

                return true;
            }

            return false;
        }

        public static bool AgregarPedido(Pedido p)
        {
            if (!ListadoTotalPedidos.Contains(p))
            {
                p.NroPedido = ListadoTotalPedidos.Any() ? ListadoTotalPedidos.Max(p => p.NroPedido) + 1 : 1;
                ListadoTotalPedidos.Add(p);
                Acceso.GuardarPedidos("Informes/Resultadopedidos.csv", listadoPedidos);
 
                return true;
            }

            return false;
        }

        public static bool EliminarPedido(int id)
        {
            var pedido = ListadoTotalPedidos.FirstOrDefault(x => x.NroPedido == id);
            if (pedido != null)
            {
                ListadoTotalPedidos.Remove(pedido);
                return true;
            }

            return false;
        }

        //METODOS DE LÓGICA

        public static Pedido BuscarPedido(int id)
        {
            return ListadoTotalPedidos.FirstOrDefault(x => x.NroPedido == id);
        }

        public static Cadete BuscarCadete(int id)
        {
            return ListadoCadetes.FirstOrDefault(x => x.IdCadete == id);
        }

        public static bool AsignarPedido(int idPedido, int idCadete)
        {
            var pedidoAsignado = ListadoTotalPedidos.FirstOrDefault(x => x.NroPedido == idPedido);
            var CadeteAsignado = ListadoCadetes.FirstOrDefault(x => x.IdCadete == idCadete);
            if (pedidoAsignado != null && CadeteAsignado != null)
            {
                pedidoAsignado.CadeteAsignado = CadeteAsignado;
                return true;
            }

            return false;
        }

        public static bool ReasignarPedido(int idPedido, int idCadeteNuevo)
        {
            var pedidoReasignar = ListadoTotalPedidos.FirstOrDefault(x => x.NroPedido == idPedido);
            var cadeteNuevo = ListadoCadetes.FirstOrDefault(x => x.IdCadete == idCadeteNuevo);
            if (pedidoReasignar != null)
            {
                pedidoReasignar.CadeteAsignado = cadeteNuevo;
                return true;
            }

            return false;
        }

        public static bool CambiarEstadoPedido(int idPedido)
        {
            var pedidoCambiarEstado = ListadoTotalPedidos.FirstOrDefault(x => x.NroPedido == idPedido);
            if (pedidoCambiarEstado != null)
            {
                pedidoCambiarEstado.EstadoPedido = !pedidoCambiarEstado.EstadoPedido;
                return true;
            }

            return false;
        }


        public static int JornalAPagar(int idCadete)
        {
            var pedidosDelCadete = ListadoTotalPedidos.Where(x => x.CadeteAsignado.IdCadete == idCadete);
            var pedidosEntregados = pedidosDelCadete.Where(x => x.EstadoPedido);
            return (500 * pedidosEntregados.Count());
        }

    }

}