namespace core.interfaces;

public interface IRepoVehiculo : IRepoAlta<Vehiculo>, IRepoListado<Vehiculo>, IRepoDetalle<Vehiculo, int>
{
    /// <summary>
    /// Permite a un vehículo iniciar sesión (por matrícula o credencial).
    /// </summary>
    /// <param name="nombre">Nombre o matrícula del vehículo.</param>
    /// <param name="contrasena">Contraseña asociada.</param>
    /// <returns>El vehículo autenticado, o null si no existe.</returns>
    public Task<Vehiculo> Loguearse(string nombre, string contrasena);

    /// <summary>
    /// Elimina un vehículo según su Id.
    /// </summary>
    /// <param name="id">Identificador del vehículo.</param>
    /// <returns>True si fue eliminado, False si no existe.</returns>
    public Task<bool> EliminarVehiculo(int id);

}
