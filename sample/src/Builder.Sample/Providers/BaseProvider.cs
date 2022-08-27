using Builder.Sample.Interfaces;
using Builder.Sample.Services;

namespace Builder.Sample.Providers;

public abstract class BaseProvider
{
    private readonly IADependency _aDependency;
    private readonly IBDependency _bDependency;

    protected BaseProvider() : this(new ADependency(), new BDependency()) { }

    private BaseProvider(IADependency aDependency, IBDependency bDependency)
    {
        _aDependency = aDependency;
        _bDependency = bDependency;
    }

    protected string InnerWork()
    {
        _aDependency.DoWork();
        return _bDependency.DoWork();
    }
}