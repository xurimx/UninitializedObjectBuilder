using Moq;
using Builder.Sample.Interfaces;
using Builder.Sample.Providers;
using UninitializedObjectBuilder;

namespace Builder.Sample.Tests;

public class SampleTests
{
    private readonly Mock<IADependency> _aMock;
    private readonly Mock<IBDependency> _bMock;
    private readonly Mock<ICDependency> _cMock;

    public SampleTests()
    {
        _aMock = new Mock<IADependency>();
        _bMock = new Mock<IBDependency>();
        _cMock = new Mock<ICDependency>();
    }
    
    [Fact]
    public void DoStuff_WithAssignedFields_ReturnsExpectedResult()
    {
        //Arrange
        _aMock.Setup(x => x.DoWork());
        _cMock.Setup(x => x.DoWork()).Returns("hello");
        _bMock.Setup(x => x.DoWork()).Returns("hello from mock b");
        
        var builder = new UninitializedObjectBuilder<Provider>();

        builder.InjectFields(_aMock.Object, _bMock.Object, _cMock.Object);
        
        var sut = builder.Build();
        
        //Act
        var result = sut.DoStuff();
        
        //Assert
        Assert.Equal("hello from mock b", result);
    }
}