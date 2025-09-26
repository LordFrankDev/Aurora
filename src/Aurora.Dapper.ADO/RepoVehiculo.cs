using core;
using core.interfaces;
using Dapper;
using System.Data;
#pragma warning restore format


namespace Aurora.Dapper.ADO;

public class RepoVehiculo : RepoGenerico, IRepoVehiculo
{
    public RepoVehiculo(IDbConnection conexion) : base(conexion) { }

    public Task<IEnumerable<Vehiculo>> ObtenerAsync => Obtener();
    public async Task<IEnumerable<Vehiculo>> Obtener()
    {
        var sql = "SELECT * FROM Vehiculos";
        return await Conexion.QueryAsync<Vehiculo>(sql);
    }

    public async Task AltaAsync(Vehiculo elemento)
    {
        var parametros = new DynamicParameters();
        parametros.Add("xidVehiculo", dbType: DbType.Int32, direction: ParameterDirection.Output);
        parametros.Add("xTipo", elemento.Tipo);
        parametros.Add("xEstado", elemento.Estado);
        parametros.Add("xCapacidadMax", elemento.CapacidadMax);
        parametros.Add("xMatricula", elemento.Matricula);

        try
        {
            await Conexion.ExecuteAsync("SPCrearVehiculo", parametros, commandType: CommandType.StoredProcedure);

            var nuevo_idvehiculo = parametros.Get<int>("xidVehiculo");
        }
        catch (System.Exception)
        {
            throw new Exception(@"Error al agregar el vehiculo");
        }
    }

    public async Task<Vehiculo>? DetalleAsync(int indiceABuscar)
    {
        string query = @"
            SELECT *
            FROM Vehiculo 
            WHERE idVehiculo = @vehiculoId";

        var vehiculo = await Conexion.QueryFirstOrDefaultAsync<Vehiculo>(query, new { vehiculoId = indiceABuscar });
        return vehiculo; 
    }

    public async Task<bool> EliminarVehiculo(int _idvehiculo)
    {
        var parametros = new DynamicParameters();
        parametros.Add("xidVehiculo", _idvehiculo);
        try
        {
            if (Convert.ToBoolean(Conexion.Execute("SPDelVehiculo", parametros)))
                return true;
            else
                return false;
        }
        catch (System.Exception)
        {
            throw new Exception("Error al eliminar el vehiculo");
        }
    }

    public async Task<Vehiculo> Loguearse(string nombre, string contrasena)
    {
        var funtionLogin = "SELECT FLoginLoguearse(@xNombre, @xContrasena);"; // Crear FLoginLoguearse
        var result = await Conexion.ExecuteScalarAsync<Vehiculo>(funtionLogin, new { xNombre = nombre , xContrasena = contrasena});
        return result;
    }

}
