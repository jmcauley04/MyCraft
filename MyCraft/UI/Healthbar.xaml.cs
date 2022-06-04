using MyGameEngine.Core;
using MyGameEngine.Shared.Interfaces;
using System;
using System.ComponentModel;
using System.Windows.Controls;

namespace MyCraft.UI
{
    /// <summary>
    /// Interaction logic for Healthbar.xaml
    /// </summary>
    public partial class Healthbar : UserControl, INotifyPropertyChanged, IDisposable
    {
        IResourcePool _healthBar;
        double _maxHealth = 100;
        double _health = 80;
        string _healthDisplay = string.Empty;

        public double MaxHealth
        {
            get => _maxHealth;
            set { _maxHealth = value; OnPropertyChanged(); UpdateHealthDisplay(); }
        }

        public double Health
        {
            get => _health;
            set { _health = value; OnPropertyChanged(); UpdateHealthDisplay(); }
        }

        public string HealthDisplay
        {
            get => _healthDisplay;
            set { _healthDisplay = value; OnPropertyChanged(); }
        }

        public Healthbar()
        {
            InitializeComponent();

            var player = ServiceProvider.GameManager.GetPlayer();
            _healthBar = player.HealthPool;

            Health = player.Health;
            MaxHealth = player.Health;

            _healthBar.OnMaxResourceChanged += SetMaxHealth;
            _healthBar.OnResourceChanged += SetHealth;
        }

        public void Dispose()
        {
            _healthBar.OnMaxResourceChanged -= SetMaxHealth;
            _healthBar.OnResourceChanged -= SetHealth;
            GC.SuppressFinalize(this);
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

        public void SetMaxHealth(double health)
        {
            MaxHealth = health;
            UpdateHealthDisplay();
        }

        public void SetHealth(double health)
        {
            Health = health;
            UpdateHealthDisplay();
        }

        void UpdateHealthDisplay()
        {
            HealthDisplay = $"{Health} / {MaxHealth}";
        }

        public bool ShouldRender() => false;
    }
}
