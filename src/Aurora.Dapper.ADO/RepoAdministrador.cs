using System.Data;
using core;
using core.interfaces;
using Dapper;

namespace Aurora.Dapper.ADO;

public class RepoAdministrador : RepoGenerico, IRepoAdministrador
{
    public RepoAdministrador(IDbConnection conexion) : base(conexion)
    {
        
    }

    public Task<IEnumerable<Administrador>> ObtenerAsync => Obtener();

    public async Task<IEnumerable<Administrador>> Obtener()
    {
        var query = @"Select * From Administrador";
        var Resultado = await Conexion.QueryAsync<Administrador>(query);
        return Resultado;
    }

    public async Task AltaAsync(Administrador _nuevoAdministrador)
    {
        var parametros = new DynamicParameters();
        parametros.Add("xidAdministrador", dbType: DbType.Int32, direction: ParameterDirection.Output);
        parametros.Add("xName", _nuevoAdministrador.Nombre);
        parametros.Add("xPassword", _nuevoAdministrador.Contrasena);
        parametros.Add("xidEmpresa", _nuevoAdministrador.IdEmpresa);

        try
        {
            await Conexion.ExecuteAsync("SPNuevoAdministrador", parametros, commandType: CommandType.StoredProcedure);
        }
        catch (System.Exception)
        {
            throw new Exception("Error al agregar un nuevo administrador");
        }
    }

    public async Task<Administrador>? DetalleAsync(int _idadministrador)
    {
        var Query = @"SELECT idAdministrador, Nombre, idEmpresa, Contrasena FROM Administrador where idAdministrador = @xidAdmin";
        var repuesta = await Conexion.QueryFirstOrDefaultAsync<Administrador>(Query, new { xidAdmin = _idadministrador});
        return repuesta;    }

    public async Task<bool> EliminarAsync(int _idadministrador)
    {
        var parametros = new DynamicParameters();
        parametros.Add("xidAdministrador", _idadministrador);
        try
        {
            await Conexion.ExecuteAsync("SPDelAdministrador", parametros);
            return true;
        }
        catch (System.Exception)
        {
            throw new Exception("¡Error al eliminar al administrador!");
        }
    }

    public async Task<bool> LoguearseAsync(string nombre, string contrasena)
    {
        var funtionLogin = "SELECT FLoginAdministrador(@xNombre, @xPassword);";
        var result = await Conexion.ExecuteScalarAsync<bool>(funtionLogin, new { xNombre = nombre, xPassword = contrasena });
        return result;
    }

    public async Task<List<Administrador>> ObtenerPorEmpresaAsync(int _idEmpresa)
    {
        var query = @"Select * From Administrador where idEmpresa = @IdEmpresa;";
        var Resultado = await Conexion.QueryAsync<Administrador>(query, new { IdEmpresa = _idEmpresa });
        return (List<Administrador>)Resultado;
    }
}
