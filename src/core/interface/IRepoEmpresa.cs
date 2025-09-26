using System.Security.Cryptography.X509Certificates;

namespace core.interfaces;

public interface IRepoEmpresa : IRepoAlta<Empresa>, IRepoListado<Empresa>, IRepoDetalle<Empresa, uint>
{
    /// <summary>
    /// Permite a una empresa iniciar sesión.
    /// </summary>
    /// <param name="nombre">Nombre de la empresa.</param>
    /// <param name="contrasena">Contraseña de la empresa.</param>
    /// <returns>La empresa autenticada, o null si las credenciales no son válidas.</returns>
    public Task<Empresa?> LoguearseAsync(string nombre, string contrasena);

    /// <summary>
    /// Elimina una empresa por su Id.
    /// </summary>
    /// <param name="id">Identificador de la empresa.</param>
    /// <returns>True si fue eliminada, False si no existe.</returns>
    public Task<bool> EliminarEmpresaAsync(uint id);

    /// <summary>
    /// Obtiene los datos de la empresa por su nombre.
    /// </summary>
    /// <param name="nombre">Nombre de la empresa.</param>
    /// <returns>La empresa correspondiente al nombre, o null si no existe.</returns>
    public Task<Empresa?> ObtenerPorNombreAsync(string nombre);
}

//Lo que se va a mostrar en el index, son los paquetes de la empresa, hecho en el repo de paquete.
//Lo del login y registro de administradores, condutores y vehiculos en sus respectoivos repos y Interfaces.
