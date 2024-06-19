namespace dezero_test;
using dezero;
using NumSharp;
using Xunit;
using Xunit.Abstractions;

public class Step6Test(ITestOutputHelper output)
{
    protected readonly   ITestOutputHelper output = output;

    [Fact]
    public void Test()
    {
        var A=new Square();
        var B=new Exp();
        var C=new Square();
        var x=new Variable(np.array(0.5));
        var a=A.Call(x);
var b=B.Call(a);
var y=C.Call(b);

y.grad=np.array(1.0);
b.grad=C.Backward(y.grad);
a.grad=B.Backward(b.grad);
x.grad=A.Backward(a.grad);

 output.WriteLine(x.grad.ToString());

        
    }
}