using SistemaCadeteria;
using System.Text.Json;

public class AccesoADastosCadeteria
{
    public Cadeteria Obtener(string archivo)
    {
        if (!File.Exists(archivo))
            return new Cadeteria("SinNombre", "00000");

        string json = File.ReadAllText(archivo);
        return JsonSerializer.Deserialize<Cadeteria>(json) ?? new Cadeteria("SinNombre", "00000");
    }
}