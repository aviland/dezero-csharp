namespace dezero_test;
using dezero;
using NumSharp;
using Xunit;
using Xunit.Abstractions;

public class Step2Test(ITestOutputHelper output)
{
    protected readonly   ITestOutputHelper output = output;

    [Fact]
    public void Test()
    {
        var x = new Variable(np.array(10));
        var f=new Square();
        var y=f.Call(x);
        output.WriteLine(y.GetType().ToString());
         output.WriteLine(y.data.ToString());
    }
}