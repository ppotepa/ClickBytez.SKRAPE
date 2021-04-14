namespace ClickBytez.SKRAPE.Engine
{
    public interface ISkrapeEngine
    {
        bool Initialized { get; }
        void Initialize();
        void Start();
    }
}