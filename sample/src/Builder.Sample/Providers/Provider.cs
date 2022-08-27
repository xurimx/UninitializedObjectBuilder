using Builder.Sample.Interfaces;
using Builder.Sample.Services;

namespace Builder.Sample.Providers;

public class Provider : BaseProvider
{
    public static Guid Id => throw new Exception();

    private readonly ICDependency _cDependency;
    
    public Provider() : this(new CDependency()){ }

    private Provider(ICDependency cDependency)
    {
        _cDependency = cDependency;
    }

    public string DoStuff()
    {
        _cDependency.DoWork();
        return InnerWork();
    }
}