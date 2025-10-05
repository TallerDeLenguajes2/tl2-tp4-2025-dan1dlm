using SistemaCadeteria;
using System.Text.Json;

public class AccesoADastosCadete
{
    public List<Cadete> Obtener(string archivo)
    {
        if (!File.Exists(archivo))
            return new List<Cadete>();

        string json = File.ReadAllText(archivo);
        return JsonSerializer.Deserialize<List<Cadete>>(json) ?? new List<Cadete>();
    }
   
}