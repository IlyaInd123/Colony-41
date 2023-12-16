public interface ISlowable
{
    bool IsSlowed { get; }
    public void ApplySlow(float slowPercentage);
    public void RemoveSlow(float slowPercentage);
}