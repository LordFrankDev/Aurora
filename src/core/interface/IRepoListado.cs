namespace core.interfaces;

public interface IRepoListado<T>
{
    Task<IEnumerable<T>> ObtenerAsync { get; }
}
