namespace dezero_test;
using dezero;
using NumSharp;
using Xunit;
using Xunit.Abstractions;

public class Step7Test(ITestOutputHelper output)
{
    protected readonly ITestOutputHelper output = output;

    [Fact]
    public void Test()
    {
        var A = new Square();
        var B = new Exp();
        var C = new Square();
        var x = new Variable(np.array(0.5));
        var a = A.Call(x);
        var b = B.Call(a);
        var y = C.Call(b);

        Assert.Equal(y.creator, C);
        Assert.Equal(y.creator.input, b);
        Assert.Equal(y.creator.input.creator, B);
        Assert.Equal(y.creator.input.creator.input, a);
        Assert.Equal(y.creator.input.creator.input.creator, A);
        Assert.Equal(y.creator.input.creator.input.creator.input, x);
    }
}