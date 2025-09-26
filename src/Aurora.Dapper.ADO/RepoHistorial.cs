using System.Data;
using core;
using core.interfaces;
using Dapper;

namespace Aurora.Dapper.ADO;

public class RepoHistorial : RepoGenerico, IRepoHistorial
{
    public RepoHistorial(IDbConnection conexion) : base(conexion)
    {

    }

    public Task<IEnumerable<HistorialPedidos>> ObtenerAsync => Obtener();

    public Task<IEnumerable<HistorialPedidos>> Obtener()
    {
        var Query = "SELECT * FROM HistorialPedido ORDER BY FechaCambio DESC";
        var Repuesta = Conexion.QueryAsync<HistorialPedidos>(Query);
        return Repuesta;
    }



    public async Task<HistorialPedidos>? DetalleAsync(uint indiceABuscar)
    {
        var Query = "SELECT * FROM HistorialPedido WHERE Pedido_idPedido = @xpedidoid ORDER BY FechaCambio DESC";
        var Repuesta = await Conexion.QueryAsync<HistorialPedidos>(Query);
        return (HistorialPedidos)Repuesta;
    }

    public Task AltaAsync(HistorialPedidos elemento)
    {
        //No Implementa
        throw new NotImplementedException();
    }


    public Task<HistorialPedidos?> ObtenerDetalleDeHistorial(int idHistorial)
    {
        //No Implementa
        throw new NotImplementedException();
    }

    public Task<List<HistorialPedidos>> ObtenerHistorialesDeEmpresa(int idEmpresa)
    {
        //No Implementa
        throw new NotImplementedException();
    }
}
