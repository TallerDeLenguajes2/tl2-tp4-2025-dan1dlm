public class Cadete
{
    private int Id { get; set; }

    private string Nombre { get; set; }

    private string Direccion { get; set; }

    private string Telefono { get; set; }



    public Cadete(int id, string nombre, string direccion, string telefono)
    {
        Id = id;
        Nombre = nombre;
        Direccion = direccion;
        Telefono = telefono;
    }
    public int IdCadete
    {
        get => Id;
        set => Id = value;
    }
    public string nombreCadete => Nombre;

    public string direccionCadete => Direccion;

    public string telefonoCadete => Telefono;

}