namespace Service.Utils;

public interface IObserver
{
    void Update<TE>(Event<TE> e);
}