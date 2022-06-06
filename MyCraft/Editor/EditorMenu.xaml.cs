using MyGameEngine.Core;
using MyGameEngine.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace MyCraft.Editor
{
    /// <summary>
    /// Interaction logic for EditorMenu.xaml
    /// </summary>
    public partial class EditorMenu : Window
    {
        IEditManager _editManager;
        public EditorMenu()
        {
            InitializeComponent();

            _editManager = ServiceProvider.EditManager;

            foreach (var option in _editManager.BlockTypes)
            {
                var btn = new Button
                {
                    Content = option.Key
                };

                btn.Click += (s, e) => SetEditBlock(option);

                spBlocks.Children.Add(btn);
            }
        }

        private void SetEditBlock(KeyValuePair<string, Type> option)
        {
            _editManager.SetEditBlock(option);
        }
    }
}
