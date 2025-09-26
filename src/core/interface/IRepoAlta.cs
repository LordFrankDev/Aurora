namespace core.interfaces;

public interface IRepoAlta<T>
{
    Task AltaAsync(T elemento);
}
