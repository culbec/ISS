using Domain;

namespace Service.Utils;

public interface IObservable
{
    void Attach(IObserver observer);
    void Detach(IObserver observer);
    void Notify<TE>(Event<TE> e);
}