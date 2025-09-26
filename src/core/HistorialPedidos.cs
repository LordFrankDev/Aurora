using System;

namespace core;

public class HistorialPedidos
{
    public uint IdHistorialPedido { get; set; }
    public uint IdPedido { get; set; }
    public DateTime FechaCambio { get; set; }
    public required string EstadoAnterior { get; set; }
    public required string EstadoNuevo { get; set; }
}
