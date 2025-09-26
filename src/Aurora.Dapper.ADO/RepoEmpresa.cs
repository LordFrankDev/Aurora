using System.Data;
using core;
using core.interfaces;
using Dapper;

namespace Aurora.Dapper.ADO;

public class RepoEmpresa : RepoGenerico, IRepoEmpresa
{
    public RepoEmpresa(IDbConnection conexion) : base(conexion)
    {
        
    }

    public Task<IEnumerable<Empresa>> ObtenerAsync => Obtener();

    public async Task<IEnumerable<Empresa>> Obtener()
    {
        var query = @"Select * From Empresa";
        var Resultado = await Conexion.QueryAsync<Empresa>(query);
        return Resultado;
    }


    public async Task AltaAsync(Empresa _nuevaempresa)
    {
        var parametros = new DynamicParameters();
        parametros.Add("xidEmpresa", direction: ParameterDirection.Output);
        parametros.Add("xNombre", _nuevaempresa.Nombre);
        try
        {
            await Conexion.ExecuteAsync("PSCrearEmpresa", parametros, commandType: CommandType.StoredProcedure); // "PSCrearEmpresa" SP para crear empresa
            var _idempresanueva = (uint)parametros.Get<int>("xidEmpresa");
        }
        catch (System.Exception ex)
        {
            throw new Exception($"Error al crear empresa: {ex.Message}", ex);
        }
    }

    public async Task<Empresa>? DetalleAsync(uint _idempresa)
    {
        var query = @"Select * From Empresa where idEmpresa = @IndiceEmpresa;";
        var Resultado = await Conexion.QueryFirstOrDefaultAsync<Empresa>(query, new { IndiceEmpresa = _idempresa });
        return Resultado;
    }

    

    public async Task<bool> EliminarEmpresaAsync(uint _idempresa)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@xidEmpresa", _idempresa);
        try
        {
            await Conexion.ExecuteAsync("SPDelEmpresa", parametros);
            return true;
        }
        catch (System.Exception)
        {
            throw new Exception("¡Error al eliminar al administrador!");
        }
    }

    public async Task<Empresa?> LoguearseAsync(string nombre, string contrasena)
    {
        var funtionLogin = "SELECT FLoginEmpresa(@xNombre);";
        var result = await Conexion.ExecuteScalarAsync<Empresa>(funtionLogin, new { xNombre = nombre });
        return result;
    }

    public async Task<Empresa?> ObtenerPorNombreAsync(string _nombre)
    {
        var query = @"Select * From Empresa where Nombre = @NombreEmpresa;";
        var Resultado = await Conexion.QueryFirstOrDefaultAsync<Empresa>(query, new { NombreEmpresa = _nombre });
        return Resultado;
    }
}
