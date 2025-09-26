using System.Data;
using core;
using core.interfaces;
using Dapper;

namespace Aurora.Dapper.ADO;

public class RepoConductor : RepoGenerico, IRepoConductor
{
    public RepoConductor(IDbConnection conexion) : base(conexion)
    {
    }

    public Task<IEnumerable<Conductor>> ObtenerAsync => throw new NotImplementedException();

    public async Task AltaAsync(Conductor elemento)
    {
        var parametros = new DynamicParameters();
        parametros.Add("xidConductor", dbType: DbType.Int32, direction: ParameterDirection.Output);
        parametros.Add("xName", elemento.Nombre);
        parametros.Add("xLicencia", elemento.Licencia);
        parametros.Add("xDisponibilidad", elemento.Dispobilidad);

        try
        {
            await Conexion.ExecuteAsync("SPNuevoConductor", parametros, commandType: CommandType.StoredProcedure);
            elemento.IdConductor = parametros.Get<uint>("xidConductor");
        }
        catch (System.Exception e)
        {
            throw new Exception("Conductor ya registrado",  e);
        }    
    }

    public async Task<Conductor>? DetalleAsync(int _idConductor)
    {
        var Query = @"Select idConductor, Name, Licencia, Disponibilidad From Conductor Where idConductor = @indice;";
        var resultados = await Conexion.QueryFirstOrDefaultAsync<Conductor>(Query, new {indice = _idConductor});
        return resultados;
    }

    public async Task<bool> EliminarConductor(int _idConductor)
    {
        var parametros = new DynamicParameters();
        parametros.Add("xidConductor", _idConductor);

        try
        {
            await Conexion.ExecuteAsync("SPDelConductor", parametros);
            return true;
        }
        catch (System.Exception)
        {
            throw new Exception("Error al eliminar al conductor");
        }
    }

    public Task<List<Conductor>> ListarConductoresLibres()
    {
        var query = @"SELECT * FROM conductor WHERE Disponibilidad = 1;";
        return Conexion.QueryAsync<Conductor>(query).ContinueWith(task => task.Result.AsList()); 
   }

    public async Task<Conductor?> Loguearse(string nombre, string licencia)
    {
        var funtionLogin = "SELECT FLoginConductor(@xNombre, @xLicencia);"; // Crear en bd la funcion FLoginConductor
        var result = await Conexion.ExecuteScalarAsync<Conductor>(funtionLogin, new { xNombre = nombre, xLicencia = licencia });
        return result;
    }

}
