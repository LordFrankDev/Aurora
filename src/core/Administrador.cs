namespace core;

public class Administrador
{
    public uint IdAdministrador { get; set; }
    public uint IdEmpresa { get; set; }
    public required string Nombre { get; set; }
    public required string Contrasena { get; set; }
}
