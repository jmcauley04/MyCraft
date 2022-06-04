namespace MyGameEngine.Shared.Interfaces;

public interface IResourcePool
{
    void SetResource(double resource);
    void SetMaxResource(double resource);
    Action<double>? OnMaxResourceChanged { get; set; }
    Action<double>? OnResourceChanged { get; set; }
}
