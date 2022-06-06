namespace MyGameEngine.Shared.Interfaces;

public interface IEditManager
{
    IBlock? GetSelectedBlock<T>();
    IReadOnlyDictionary<string, Type> BlockTypes { get; }
    void SetEditBlock(KeyValuePair<string, Type> option);
}
