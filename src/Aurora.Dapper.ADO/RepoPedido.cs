using System.Data;
using core;
using core.interfaces;
using Dapper;

namespace Aurora.Dapper.ADO;

public class RepoPedido : RepoGenerico, IRepoPedido
{
    public RepoPedido(IDbConnection conexion) : base(conexion)
    {
    }

    public Task<IEnumerable<Pedido>> ObtenerAsync => ObtenerAsync;

    public async Task<bool> ActualizarEstadoPedidoPorAdmin(int idPedido, string nuevoEstado)
    {
        var parametetros = new DynamicParameters();
        parametetros.Add("xidPedido",idPedido);
        parametetros.Add("xNuevoEstado",nuevoEstado);
        try
        {
            await Conexion.ExecuteAsync("SPUpdateEstadoPedido", parametetros, commandType: CommandType.StoredProcedure);
            return true;
        }
        catch (System.Exception)
        {
            throw new Exception("No se pudo actualizar el estado del pedido");
        }
    }

    public async Task<bool> ActualizarEstadoPedidoPorConductor(int idPedido, string nuevoEstado)
    {
        var parametetros = new DynamicParameters();
        parametetros.Add("xidPedido",idPedido);
        parametetros.Add("xNuevoEstado",nuevoEstado);
        try
        {
            await Conexion.ExecuteAsync("SPUpdateEstadoPedido", parametetros, commandType: CommandType.StoredProcedure);
            return true;
        }
        catch (System.Exception)
        {
            throw new Exception("No se pudo actualizar el estado del pedido");
        }
    }

    public async Task AltaAsync(Pedido _nuevoPedido)
    {
        var parametros = new DynamicParameters();
        parametros.Add("xidPedido", dbType: DbType.Int32, direction: ParameterDirection.Output);
        parametros.Add("xName",_nuevoPedido.Nombre);
        parametros.Add("xVolumen",_nuevoPedido.Volumen);
        parametros.Add("xPeso", _nuevoPedido.Peso);
        parametros.Add("xEstadoPedido", _nuevoPedido.Estado);
        parametros.Add("xFechaDespacho", _nuevoPedido.FechaDespacho);
        parametros.Add("xAdministrador_idAdministrador", _nuevoPedido.IdAdministrador);
        parametros.Add("xEmpresaDestino", _nuevoPedido.IdEmpresa);
        parametros.Add("xRuta_idRuta", _nuevoPedido.IdRuta);
        parametros.Add("xidVehiculo", _nuevoPedido.IdVehiculo);

        try
        {
            await Conexion.ExecuteAsync("SPCrearPedido", parametros, commandType: CommandType.StoredProcedure);
            _nuevoPedido.IdPedido = parametros.Get<int>("xidPedido");
        }
        catch (System.Exception)
        {
            throw new Exception("Error al intentar crear un pedido");
        }    
    }

    public async Task<IEnumerable<Pedido>> Obtener()
    {
        var Query=@"Select * from Pedido";
        var repuesta = await Conexion.QueryAsync<Pedido>(Query);
        return repuesta;
    }

    public async Task<Pedido>? DetalleAsync(uint indiceABuscar)
    {
        var Query=@"Select * from Pedido where idPedido = @xidPedido";
        var repuesta = await Conexion.QueryFirstOrDefaultAsync<Pedido>(Query, new {xidPedido = indiceABuscar});
        return repuesta;
    }

    public async Task<Pedido> ObtenerDetalleCompletoDePedido(int idPedido) //Este mth va en el index de empresa
    {
        throw new NotImplementedException();
    }

    public Task<List<Pedido>> ObtenerPedidosPorEmpresa(int idEmpresa)
    {
        throw new NotImplementedException(); //armar un repo especial
    }

    public Task<List<Pedido>> ObtenerPedidosPorPaquete(int idPaquete)
    {
        throw new NotImplementedException(); //armar un repo especial
    }

}
