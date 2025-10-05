using System;
using System.Data.Common;
using System.IO.Compression;
using System.Runtime.InteropServices;

namespace SistemaCadeteria
{
    public class Cadeteria
    {
        private string Nombre { get; set; }
        private string Telefono { get; set; }
        private List<Cadete> ListadoCadetes = new List<Cadete>();
        private List<Pedido> ListadoTotalPedidos = new List<Pedido>();

        public Cadeteria(string nombre, string telefono)
        {
            this.Nombre = nombre;
            this.Telefono = telefono;
        }

        public void agregarListaCadetes(List<Cadete> cadetes)
        {
            this.ListadoCadetes = cadetes;
        }

        public void agregarListaPedidos(List<Pedido> pedidos)
        {
            this.ListadoTotalPedidos = pedidos;
        }

        public List<Cadete> listadoCadetes => ListadoCadetes;
        public List<Pedido> listadoPedidos => ListadoTotalPedidos;

        //AGREGAR-BORRAR OBJETOS
        public bool AgregarCadete(Cadete c)
        {
            if (!ListadoCadetes.Contains(c))
            {
                c.IdCadete = ListadoCadetes.Any() ? ListadoCadetes.Max(p => p.IdCadete) + 1 : 1;
                ListadoCadetes.Add(c);

                return true;
            }

            return false;
        }

        public bool AgregarPedido(Pedido p)
        {
            if (!ListadoTotalPedidos.Contains(p))
            {
                p.NroPedido = ListadoTotalPedidos.Any() ? ListadoTotalPedidos.Max(p => p.NroPedido) + 1 : 1;
                ListadoTotalPedidos.Add(p);

                return true;
            }

            return false;
        }

        public bool EliminarPedido(int id)
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

        public Pedido BuscarPedido(int id)
        {
            return ListadoTotalPedidos.FirstOrDefault(x => x.NroPedido == id);
        }

        public Cadete BuscarCadete(int id)
        {
            return ListadoCadetes.FirstOrDefault(x => x.IdCadete == id);
        }

        public bool AsignarPedido(int idPedido, int idCadete)
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

        public bool ReasignarPedido(int idPedido, int idCadeteNuevo)
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

        public bool CambiarEstadoPedido(int idPedido)
        {
            var pedidoCambiarEstado = ListadoTotalPedidos.FirstOrDefault(x => x.NroPedido == idPedido);
            if (pedidoCambiarEstado != null)
            {
                pedidoCambiarEstado.EstadoPedido = !pedidoCambiarEstado.EstadoPedido;
                return true;
            }

            return false;
        }


        public int JornalAPagar(int idCadete)
        {
            var pedidosDelCadete = ListadoTotalPedidos.Where(x => x.CadeteAsignado.IdCadete == idCadete);
            var pedidosEntregados = pedidosDelCadete.Where(x => x.EstadoPedido);
            return (500 * pedidosEntregados.Count());
        }

    }

}