using MyGameEngine.Shared.Interfaces;
using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace MyCraft.GameObjects.Blocks
{
    /// <summary>
    /// Interaction logic for Block.xaml
    /// </summary>
    public abstract partial class Block : IBlock
    {
        private Rectangle[,] _pixels;

        public int UnitWidth_pixels { get; set; } = 32;
        public int UnitHeight_pixels { get; set; } = 32;
        public virtual int BlockWidth { get; set; } = 1;
        public virtual int BlockHeight { get; set; } = 1;
        public Color[,]? ColorMatrix { get; set; }
        public int XPosition { get; private set; } = 0;
        public int YPosition { get; private set; } = 0;
        public int Width { get; init; }
        public int Height { get; init; }
        public int ZIndex { get; init; }

        /// <summary>
        /// Creates and assigns to ColorMatrix a matrix of colors that spans the height and width of the block
        /// </summary>
        protected abstract void CreateColorMatrix();

        public Block()
        {
            Width = UnitWidth_pixels * BlockWidth;
            Height = UnitHeight_pixels * BlockHeight;

            _pixels = new Rectangle[Width, Height];

            CreateColorMatrix();
            ApplyColorMatrix();
        }

        private void ApplyColorMatrix()
        {
            if (ColorMatrix is null)
                throw new Exception("ColorMatrix must be set prior to applying the color matrix");

            var matrixWidth = ColorMatrix.GetLength(0);
            var matrixHeight = ColorMatrix.GetLength(1);

            if (matrixWidth > Width || Width % matrixWidth != 0 || matrixHeight > Height || Height % matrixHeight != 0)
                throw new Exception("ColorMatrix dimensions must be at most equal to and evenly divisible by the size of the block;");

            int x_multiple = Width / matrixWidth;
            int y_multiple = Height / matrixHeight;

            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    var x_matrix = (int)Math.Floor(x * 1.0 / x_multiple);
                    var y_matrix = (int)Math.Floor(y * 1.0 / y_multiple);

                    var color = ColorMatrix[x_matrix, y_matrix];

                    var rect = new Rectangle()
                    {
                        Width = 1,
                        Height = 1,
                        Fill = new SolidColorBrush(color)
                    };

                    Canvas.SetTop(rect, YPosition + y);
                    Canvas.SetLeft(rect, XPosition + x);

                    _pixels[x, y] = rect;
                }
            }
        }

        public void Place(int x, int y)
        {
        }

        public bool ShouldRender()
        {
            throw new NotImplementedException();
        }

        public void Render()
        {
            throw new NotImplementedException();
        }
    }
}
