namespace dezero_test;
using dezero;
using NumSharp;
using Xunit;
using Xunit.Abstractions;

public class Step1Test(ITestOutputHelper output)
{
    protected readonly   ITestOutputHelper output = output;

    [Fact]
    public void Test1()
    {
        var d = new Variable(np.array(1));
         output.WriteLine(d.data.ToString());
    }
}