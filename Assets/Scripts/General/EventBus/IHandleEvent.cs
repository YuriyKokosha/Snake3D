namespace EventsSystem
{
    public interface IHandleEvent<T>
    {
        void Handle(object sender, T data);
    }
}