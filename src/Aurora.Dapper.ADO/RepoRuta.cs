using System.Data;
using core;
using core.interfaces;
using Dapper;

namespace Aurora.Dapper.ADO;

public class RepoRuta : RepoGenerico, IRepoRuta
{
    public RepoRuta(IDbConnection contexto) : base(contexto)
    {
    }

    public Task<IEnumerable<Ruta>> ObtenerAsync => ObtenerAsync;
    public async Task<IEnumerable<Ruta>> Obtener()
    {
        var query = @"Select * From Ruta";
        var Resultado = await Conexion.QueryAsync<Ruta>(query);
        return Resultado;
    }

    public async Task AltaAsync(Ruta elemento)
    {
        var parametros = new DynamicParameters();
        parametros.Add("xidRuta", dbType: DbType.Int32, direction: ParameterDirection.Output);
        parametros.Add("xOrigen", elemento.Origen);
        parametros.Add("xDestino", elemento.Destino);

        try
        {
            await Conexion.ExecuteAsync("SPCrearRuta", parametros, commandType: CommandType.StoredProcedure);
            elemento.IdRuta = parametros.Get<int>("xidRuta");
        }
        catch (Exception)
        {
            throw new Exception("¡Error al generar la ruta!");
        }
    }

    public async Task<Ruta>? DetalleAsync(uint _idruta)
    {
        var query = @"Select * from Ruta where idRuta = @_idruta";
        var Resultado = await Conexion.QueryFirstOrDefaultAsync<Ruta>(query, new {_idruta});
        return Resultado;    }

    public async Task<Ruta?> ObtenerRutaPorParametros(string origen, string destino, int idRuta)
    {
        var query = @"Select * from Ruta where idRuta = @Indice or Origen = @xOrigen or Destino = @xDestino";
        var Resultado = await Conexion.QueryFirstOrDefaultAsync<Ruta>(query, new {Indice = idRuta, xOrigen = origen, xDestino = destino});
        return Resultado;
    }
}
