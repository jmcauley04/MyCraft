using MyGameEngine.Shared.Interfaces;
using MyGameEngine.Shared.Records;

namespace MyGameEngine.Core.GameObjects.Blocks
{
    public abstract class Block : IBlock
    {
        protected int DefaultBlockSize = 3;

        ColorRecord[,] _pixels { get; init; }
        public int XPosition { get; private set; } = 0;
        public int YPosition { get; private set; } = 0;
        public int Width { get; init; }
        public int Height { get; init; }
        public int ZIndex { get; init; }

        /// <summary>
        /// Creates and assigns to ColorMatrix a matrix of colors that spans the height and width of the block
        /// </summary>
        protected abstract ColorRecord[,] GetBlockPixels();

        public Block()
        {
            _pixels = GetBlockPixels();
            Width = _pixels.GetLength(0);
            Height = _pixels.GetLength(1);
        }

        public bool ShouldRender()
        {
            throw new NotImplementedException();
        }

        public void Render()
        {
            throw new NotImplementedException();
        }

        public ColorRecord[,] GetPixels() => _pixels;

        public void Place(int x, int y)
        {
            XPosition = x;
            YPosition = y;
        }
    }
}
