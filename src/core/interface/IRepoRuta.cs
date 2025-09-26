namespace core.interfaces;

public interface IRepoRuta : IRepoAlta<Ruta>, IRepoListado<Ruta>, IRepoDetalle<Ruta, uint>
{
    /// <summary>
    /// Obtiene una ruta según parámetros de origen, destino e identificador.
    /// </summary>
    /// <param name="origen">Ciudad o punto de origen.</param>
    /// <param name="destino">Ciudad o punto de destino.</param>
    /// <param name="idRuta">Identificador de la ruta.</param>
    /// <returns>La ruta encontrada, o null si no existe.</returns>
    public Task<Ruta?> ObtenerRutaPorParametros(string origen, string destino, int idRuta);

}
