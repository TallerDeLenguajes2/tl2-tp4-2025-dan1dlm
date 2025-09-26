public class Cliente
{
    private string Nombre { get; set; }
    private string Direccion { get; set; }
    private string Telefono { get; set; }
    private string DatosReferenciaDireccion { get; set; }
    public Cliente(string nombre, string direccion, string telefono, string datosReferencia)
    {
        Nombre = nombre;
        Direccion = direccion;
        Telefono = telefono;
        DatosReferenciaDireccion = datosReferencia;
    }

    public string NombreCliente => Nombre;
    public string DireccionCliente => Direccion;
    public string TelefonoCliente => Telefono;
    public string DatosReferenciaDireccionCliente => DatosReferenciaDireccion;
}
