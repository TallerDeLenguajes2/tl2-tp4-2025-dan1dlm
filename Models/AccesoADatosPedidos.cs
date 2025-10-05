using System.Text.Json;

public class AccesoADastosPedido
{
    public List<Pedido> Obtener(string archivo)
    {
        if (!File.Exists(archivo))
            return new List<Pedido>();

        string json = File.ReadAllText(archivo);
        return JsonSerializer.Deserialize<List<Pedido>>(json) ?? new List<Pedido>();
    }
    public void Guardar(List<Pedido> pedidos, string archivo)
    {
        var lineas = new List<string>();
        lineas.Add("Nro,Obs,Nombre,Direccion,Telefono,Referencia,Estado"); // encabezado
        foreach (var p in pedidos)
        {
            lineas.Add($"{p.NroPedido},{p.observacionPedido},{p.clientePedido.NombreCliente},{p.clientePedido.DireccionCliente},{p.clientePedido.TelefonoCliente},{p.clientePedido.DatosReferenciaDireccionCliente},{p.EstadoPedido},{p.CadeteAsignado.IdCadete}");
        }
        File.WriteAllLines(archivo, lineas);
    }
}