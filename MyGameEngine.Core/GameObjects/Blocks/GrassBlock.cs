using MyGameEngine.Shared.Records;

namespace MyGameEngine.Core.GameObjects.Blocks
{
    public class GrassBlock : Block
    {
        protected override ColorRecord[,] GetBlockPixels()
        {
            var colors = new ColorRecord[DefaultBlockSize, DefaultBlockSize];

            for (int i = 0; i < DefaultBlockSize; i++)
            {
                for (int j = 0; j < DefaultBlockSize; j++)
                {
                    if (j < DefaultBlockSize / 3)
                        colors[i, j] = new ColorRecord(40, 200, 40, i, j);
                    else
                        colors[i, j] = new ColorRecord(200, 150, 40, i, j);
                }
            }

            return colors;
        }
    }
}
