namespace ClickBytez.SKRAPE.Engine
{
    public interface ISkrapeEngine
    {
        bool Initialized { get; }
        SkrapeEngine Initialize();
        SkrapeEngine Start();
    }
}