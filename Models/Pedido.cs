public class Pedido
{

    private int Nro { get; set; }

    private string Observacion { get; set; }

    private Cliente Cliente { get; set; }

    private bool Estado { get; set; }

    private Cadete Cadete { get; set; }
    public Pedido(int nro, string observacion, Cliente cliente)
    {
        Nro = nro;
        Observacion = observacion;
        Cliente = cliente;
        Estado = false;
    }

    public bool EstadoPedido
    {
        get => Estado;
        set => Estado = value;
    }

    public int NroPedido
    {
        get => Nro;
        set => Nro = value;
    }

    public Cadete CadeteAsignado
    {
        get => Cadete;
        set => Cadete = value;
    }

    public string observacionPedido => Observacion;

    public Cliente clientePedido => Cliente;
    //METODOS



}