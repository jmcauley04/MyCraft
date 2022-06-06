using MyGameEngine.Core.GameObjects.Blocks;
using MyGameEngine.Shared.Interfaces;

namespace MyGameEngine.Core;

public class EditManager : IEditManager
{
    KeyValuePair<string, Type>? _selectedEditBlock;
    public IReadOnlyDictionary<string, Type> BlockTypes => _blockTypes;

    static Dictionary<string, Type> _blockTypes = new Dictionary<string, Type>()
    {
        {"Dirt", typeof(DirtBlock) },
        {"Grass", typeof(GrassBlock) }
    };

    public void SetEditBlock(KeyValuePair<string, Type> option)
    {
        _selectedEditBlock = option;
    }

    public IBlock? GetSelectedBlock<T>()
    {
        if (_selectedEditBlock is null)
            return default;

        var selectedType = _selectedEditBlock?.Value;

        if (selectedType is null)
            return default;

        return Activator.CreateInstance(selectedType) as IBlock;
    }
}
