namespace core;

public class Pedido
{
    public int IdPedido { get; set; }
    public int IdAdministrador { get; set; }
    public int IdRuta { get; set; }
    public int IdEmpresa { get; set; }
    public int IdVehiculo { get; set;}
    public int IdVehiculo { get; set;}
    public required string Nombre { get; set; }
    public required double Peso { get; set; } // Expresado en kilogramos
    public required double Volumen { get; set; } //Expresado en metros cúbicos
    public required string Estado { get; set; } // "Despachado", "En viaje", "Entregado", "Recibido"
    public required DateTime FechaDespacho { get; set; }
}
