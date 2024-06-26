// See https://aka.ms/new-console-template for more information
using NumSharp;

namespace dezero
{

    public class Variable(NDArray data)
    {
        public NDArray data = data;

        public static void Main()
        {
            var x = new Variable(np.array(10));
            var f = new Square();
            var y = f.Forward(x);
            Console.WriteLine(y.GetType());
            Console.WriteLine(y.data.ToString());
        }
    }

    public abstract class Function
    {
        public NDArray? x;
        public Variable? y;

        public Variable? input;
        public Variable? ouput;

        public Variable Call(Variable input)
        {
            x = input.data;
            y = Forward(new Variable(x));
            var ouput = new Variable(y.data);
            return ouput;
        }
        public abstract Variable Forward(Variable x);
    }
    public class Square : Function
    {
        public override Variable Forward(Variable  x)
        {
            return new Variable(x.data.matrix_power(2));
        }
    }
}

