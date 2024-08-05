using Domain;

namespace Service.Utils;

public class Event<TE>(EventType eventType, TE? oldData, TE? newData)
{
    public EventType EventType => eventType;
    public TE? OldData => oldData;
    public TE? NewData => newData;

    public override string ToString()
    {
        return $"Event type: {eventType}, old data: {oldData}, new data: {newData}";
    }
}