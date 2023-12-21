namespace VGSS.Application;
public class ValidationFailed(params string[] reasons)
{
    public string[] Reasons { get; } = reasons;
}