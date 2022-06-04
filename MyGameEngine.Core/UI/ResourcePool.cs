using MyGameEngine.Shared.Interfaces;

namespace MyGameEngine.Core.UI
{
    public class ResourcePool : IResourcePool
    {
        double _resource;
        double _maxResource;

        public Action<double>? OnMaxResourceChanged { get; set; }
        public Action<double>? OnResourceChanged { get; set; }

        public void SetMaxResource(double resource)
        {
            _maxResource = resource;
            OnMaxResourceChanged?.Invoke(_maxResource);
        }

        public void SetResource(double resource)
        {
            _resource = resource;
            OnResourceChanged?.Invoke(_resource);
        }
    }
}
