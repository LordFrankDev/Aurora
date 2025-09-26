namespace core.interfaces;

public interface IRepoPedido : IRepoAlta<Pedido>, IRepoListado<Pedido>, IRepoDetalle<Pedido, uint>
{
    /// <summary>
    /// Actualiza el estado de un pedido según la acción de un administrador.
    /// </summary>
    /// <param name="idPedido">Identificador del pedido.</param>
    /// <param name="nuevoEstado">Nuevo estado del pedido.</param>
    /// <returns>True si se actualizó, False en caso contrario.</returns>
    public Task<bool> ActualizarEstadoPedidoPorAdmin(int idPedido, string nuevoEstado);

    /// <summary>
    /// Actualiza el estado de un pedido según la acción de un conductor.
    /// </summary>
    /// <param name="idPedido">Identificador del pedido.</param>
    /// <param name="nuevoEstado">Nuevo estado del pedido.</param>
    /// <returns>True si se actualizó, False en caso contrario.</returns>
    public Task<bool> ActualizarEstadoPedidoPorConductor(int idPedido, string nuevoEstado);

    /// <summary>
    /// Obtiene la lista de pedidos asociados a una empresa.
    /// </summary>
    /// <param name="idEmpresa">Identificador de la empresa.</param>
    /// <returns>Lista de pedidos de la empresa.</returns>
    public Task<List<Pedido>> ObtenerPedidosPorEmpresa(int idEmpresa);

    /// <summary>
    /// Obtiene la lista de pedidos asociados a un paquete.
    /// </summary>
    /// <param name="idPaquete">Identificador del paquete.</param>
    /// <returns>Lista de pedidos asociados al paquete.</returns>
    public Task<List<Pedido>> ObtenerPedidosPorPaquete(int idPaquete);

    /// <summary>
    /// Obtiene el detalle completo de un pedido, incluyendo información
    /// de empresa, paquete, ruta, historial y vehículo.
    /// </summary>
    /// <param name="idPedido">Identificador del pedido.</param>
    /// <returns>El detalle completo del pedido.</returns>
    public Task<Pedido> ObtenerDetalleCompletoDePedido(int idPedido);

}
