namespace core;

public class Vehiculo
{
    public uint IdVehiculo { get; set; }
    public required string Tipo { get; set; }
    public required string Matricula { get; set; }
    public required string Estado { get; set; }
    public uint CapacidadMax { get; set; }
    
}
