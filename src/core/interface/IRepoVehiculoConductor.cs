using core.models;

namespace core.interfaces;

public interface IRepoVehiculoConductor : IRepoAlta<_conductorVehiculo>, IRepoListado<_conductorVehiculo>, IRepoDetalle<_conductorVehiculo, int>
{
    /// <summary>
    /// Asigna un conductor a un vehículo.
    /// </summary>
    /// <param name="idVehiculo">Id del vehículo.</param>
    /// <param name="idConductor">Id del conductor.</param>
    /// <returns>El registro de asignación creado.</returns>
    /// <exception cref="InvalidOperationException">Si el vehículo o conductor no existen, o si ya está asignado.</exception>
    public Task AsignarConductorAVehiculo(int idVehiculo, int idConductor);
    public Task<bool> DesasignarConductorDeVehiculo(int idVehiculo, int idConductor);
    public Task<IEnumerable<_conductorVehiculo>> consultaVehiculoConductor();
}
