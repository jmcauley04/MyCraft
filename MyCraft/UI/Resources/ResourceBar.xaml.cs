using MyGameEngine.Shared.Interfaces;
using System;
using System.ComponentModel;
using System.Windows.Controls;

namespace MyCraft.UI
{
    /// <summary>
    /// Interaction logic for ResourceBar.xaml
    /// </summary>
    public abstract partial class ResourceBar : UserControl, INotifyPropertyChanged, IDisposable
    {
        double _maxValue = 100;
        double _currentValue = 80;
        string _valueDisplayy = string.Empty;

        protected IResourcePool? _resourcePool;
        protected abstract void SetResourcePool();
        protected abstract void InitializeValues();
        public abstract string PrimaryColor { get; }
        public abstract string GradientColor { get; }


        public double MaxResourceValue
        {
            get => _maxValue;
            set { _maxValue = value; OnPropertyChanged(); UpdateHealthDisplay(); }
        }

        public double ResourceValue
        {
            get => _currentValue;
            set { _currentValue = value; OnPropertyChanged(); UpdateHealthDisplay(); }
        }

        public string ResourceValueDisplay
        {
            get => _valueDisplayy;
            set { _valueDisplayy = value; OnPropertyChanged(); }
        }

        public ResourceBar()
        {
            InitializeComponent();


            SetResourcePool();
            InitializeValues();

            if (_resourcePool is not null)
            {
                _resourcePool.OnMaxResourceChanged += SetMaxResource;
                _resourcePool.OnResourceChanged += SetResource;
            }
        }

        public void Dispose()
        {
            if (_resourcePool is not null)
            {
                _resourcePool.OnMaxResourceChanged -= SetMaxResource;
                _resourcePool.OnResourceChanged -= SetResource;
                GC.SuppressFinalize(this);
            }
        }

        public void SetMaxResource(double resourceValue)
        {
            MaxResourceValue = resourceValue;
            UpdateHealthDisplay();
        }

        public void SetResource(double resourceValue)
        {
            ResourceValue = resourceValue;
            UpdateHealthDisplay();
        }

        void UpdateHealthDisplay()
        {
            ResourceValueDisplay = $"{ResourceValue} / {MaxResourceValue}";
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = "")
        {
            if (this.PropertyChanged != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                this.PropertyChanged(this, e);
            }
        }
    }
}
