using System.Text.Json;


public abstract class AccesoADatos
{
    public abstract List<Cadete> CargarCadetes(string archivo);
    public abstract List<Pedido> CargarPedidos(string archivo);
    public abstract void GuardarCadetes(string archivo, List<Cadete> cadetes);
    public abstract void GuardarPedidos(string archivo, List<Pedido> pedidos);
}

public class AccesoADatosJson : AccesoADatos
{
    public override List<Cadete> CargarCadetes(string archivo)
    {
        if (!File.Exists(archivo))
            return new List<Cadete>();

        string json = File.ReadAllText(archivo);
        return JsonSerializer.Deserialize<List<Cadete>>(json) ?? new List<Cadete>();
    }

    public override List<Pedido> CargarPedidos(string archivo)
    {
        if (!File.Exists(archivo))
            return new List<Pedido>();

        string json = File.ReadAllText(archivo);
        return JsonSerializer.Deserialize<List<Pedido>>(json) ?? new List<Pedido>();
    }

    public override void GuardarCadetes(string archivo, List<Cadete> cadetes)
    {
        var opciones = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(cadetes, opciones);
        File.WriteAllText(archivo, json);
    }

    public override void GuardarPedidos(string archivo, List<Pedido> pedidos)
    {
        var opciones = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(pedidos, opciones);
        File.WriteAllText(archivo, json);
    }
}

public class AccesoADatosCSV : AccesoADatos
{
    public override List<Cadete> CargarCadetes(string archivo)
    {
        var cadetes = new List<Cadete>();
        if (!File.Exists(archivo))
            return cadetes;

        var lineas = File.ReadAllLines(archivo);
        for (int i = 1; i < lineas.Length; i++)
        {
            var campos = lineas[i].Split(',');
            int id = int.Parse(campos[0]);
            string nombre = campos[1];
            string direccion = campos[2];
            string telefono = campos[3];

            cadetes.Add(new Cadete(id, nombre, direccion, telefono));
        }

        return cadetes;
    }

    public override List<Pedido> CargarPedidos(string archivo)
    {
        var pedidos = new List<Pedido>();
        if (!File.Exists(archivo))
            return pedidos;

        var lineas = File.ReadAllLines(archivo);
        for (int i = 1; i < lineas.Length; i++) //salta encabezado
        {
            var campos = lineas[i].Split(',');
            int nro = int.Parse(campos[0]);
            string obs = campos[1];
            string nombre = campos[2];
            string direccion = campos[3];
            string telefono = campos[4];
            string referencia = campos[5];
            bool estado = bool.Parse(campos[6]);

            var cliente = new Cliente(nombre, direccion, telefono, referencia);
            pedidos.Add(new Pedido(nro, obs, cliente));
        }
        return pedidos;
    }
    
    public override void GuardarCadetes(string archivo, List<Cadete> cadetes)
    {
        var lineas = new List<string>();
        lineas.Add("Id,Nombre,Direccion,Telefono"); // encabezado
        foreach (var c in cadetes)
        {
            lineas.Add($"{c.IdCadete},{c.nombreCadete},{c.direccionCadete},{c.telefonoCadete}");
        }
        File.WriteAllLines(archivo, lineas);
    }

    public override void GuardarPedidos(string archivo, List<Pedido> pedidos)
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
