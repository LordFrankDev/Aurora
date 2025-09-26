namespace core.interfaces;

public interface IRepoHistorial : IRepoAlta<HistorialPedidos>, IRepoListado<HistorialPedidos>, IRepoDetalle<HistorialPedidos, uint>
{
    /// <summary>
    /// Obtiene los historiales de pedidos relacionados a una empresa,
    /// ya sea como origen o destino.
    /// </summary>
    /// <param name="idEmpresa">Identificador de la empresa.</param>
    /// <returns>Lista de historiales asociados a la empresa.</returns>
    public Task<List<HistorialPedidos>> ObtenerHistorialesDeEmpresa(int idEmpresa);

    /// <summary>
    /// Obtiene el detalle de un historial específico.
    /// </summary>
    /// <param name="idHistorial">Identificador del historial.</param>
    /// <returns>El historial solicitado, o null si no existe.</returns>
    public Task<HistorialPedidos?> ObtenerDetalleDeHistorial(int idHistorial);

}
