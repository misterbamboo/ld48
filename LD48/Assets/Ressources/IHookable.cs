public interface IHookable
{
    string Key { get; }

    void Hook(Hook hook);

    void Collect();
}
