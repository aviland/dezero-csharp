// See https://aka.ms/new-console-template for more information
using NumSharp;

namespace dezero
{
    public class F : Function
    {
        public override Variable Forward(Variable x)
        {
            var A = new Square();
            var B = new Exp();
            var C = new Square();
            return C.Call(B.Call(A.Call(x)));
        }
    }
    public class Step04
    {
        public static void Main()
        {
            var f = new Square();

            var x = new Variable(np.array(2.0));
            var dy = NumericalDiff(f, x);
            Console.WriteLine(dy);

            x = new Variable(np.array(0.5));
            dy = NumericalDiff(new F(), x);
            Console.WriteLine(dy);

        }

        private readonly static double eps = 1e-4;
        public static double NumericalDiff(Function f, Variable x)
        {
            Variable x0 = new Variable(x.data - eps);
            Variable x1 = new Variable(x.data + eps);
            Variable y0 = f.Call(x0);
            Variable y1 = f.Call(x1);
            double z1 = y1.data - y0.data;
            double z2 = 2.0 * eps;
            return z1 / z2;
        }
    }
    public class Variable(NDArray data)
    {
        public NDArray data = data;


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
        public override Variable Forward(Variable x)
        {
            return new Variable(x.data.matrix_power(2));
        }
    }
    public class Exp : Function
    {
        public override Variable Forward(Variable x)
        {
            return new Variable(np.exp(x.data));
        }

    }
}

