using System.Data;
using core.interfaces;
using core.models;
using Dapper;

namespace Aurora.Dapper.ADO;

public class RepoVehiculoConductor : RepoGenerico, IRepoVehiculoConductor
{
    public RepoVehiculoConductor(IDbConnection conexion) : base(conexion) { }

    public Task<IEnumerable<_conductorVehiculo>> ObtenerAsync => throw new NotImplementedException();

    public async Task AltaAsync(_conductorVehiculo elemento)
    {
        var parametros = new DynamicParameters();
        parametros.Add("XidConductor", elemento.idConductor);
        parametros.Add("XidVehiculo", elemento.idVehiculo);
        parametros.Add("FechaAsignacion", elemento.fechaAsignacion);

        try
        {
            await Conexion.ExecuteAsync("AsignarVehiculoAConductor", parametros, commandType: CommandType.StoredProcedure);
        }
        catch (System.Exception e)
        {
            throw new Exception("Error al asignar vehiculo a conductor", e);
        }    }

    public async Task AsignarConductorAVehiculo(int idVehiculo, int idConductor)
    {
        var parametros = new DynamicParameters();
        parametros.Add("xidConductor", idConductor);
        parametros.Add("xidVehiculo", idVehiculo);

        try
        {
            await Conexion.ExecuteAsync("AsignarVehiculoAConductor", parametros, commandType: CommandType.StoredProcedure);

            
        }
        catch (System.Exception)
        {
            throw new Exception("No se pudo Asignar el vehiculo al conductor");
        }    
    }

    public async Task<IEnumerable<_conductorVehiculo>> consultaVehiculoConductor()
    {
        var query = @"SELECT 
            c.idConductor, c.Name AS NombreConductor, c.Licencia, c.Disponibilidad,
            v.idVehiculo, v.Matricula, v.Tipo, v.CapacidadMax, v.Estado AS EstadoVehiculo,
            cv.FechaAsignado
            FROM Conductor c
            LEFT JOIN Conductor_has_Vehiculo cv ON c.idConductor = cv.idConductor
            LEFT JOIN Vehiculo v ON cv.idVehiculo = v.idVehiculo

            UNION

            SELECT 
            NULL AS idConductor, NULL AS NombreConductor, NULL AS Licencia, NULL AS Disponibilidad,
            v.idVehiculo, v.Matricula, v.Tipo, v.CapacidadMax, v.Estado AS EstadoVehiculo,
            NULL AS FechaAsignado
            FROM Vehiculo v
            WHERE v.idVehiculo NOT IN (SELECT idVehiculo FROM Conductor_has_Vehiculo);";

        return await Conexion.QueryAsync<_conductorVehiculo>(query);
    }

    public async Task<bool> DesasignarConductorDeVehiculo(int idVehiculo, int idConductor)
    {
        var parametros = new DynamicParameters();
        parametros.Add("xidConductor", idConductor);
        parametros.Add("xidVehiculo", idVehiculo);

        try
        {
            await Conexion.ExecuteAsync("SPDesasignarVehiculoAConductor", parametros);
            return true;
        }
        catch (System.Exception)
        {
            throw new Exception("Error al querer cancelar la asignacion al conductor");
        }        }

    public async Task<_conductorVehiculo>? DetalleAsync(int indiceABuscar)
    {
        throw new NotImplementedException();
    }

}
