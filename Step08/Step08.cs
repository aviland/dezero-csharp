// See https://aka.ms/new-console-template for more information
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NumSharp;

namespace dezero
{
    public class Step06
    {
        public static void Main()
        {
            var A = new Square();
            var B = new Exp();
            var C = new Square();
            var x = new Variable(np.array(0.5));

            var a = A.Call(x);
            var b = B.Call(a);
            var y = C.Call(b);

             y.grad = np.array(1.0);
            y.BackWard();

            /*b.grad = C.Backward(y.grad);
            a.grad = B.Backward(b.grad);
            x.grad = A.Backward(a.grad);*/
            Console.WriteLine(x?.grad?.ToString());

        }

        private readonly static double eps=1e-4;
        public static double NumericalDiff(Function f, Variable x)
        {
            Variable x0 = new(x.data - eps);
            Variable x1 = new(x.data + eps);
            Variable y0 = f.Call(x0);
            Variable y1 = f.Call(x1);
            double z1 = y1.data - y0.data;
            double z2 = 2.0 * eps;
            return z1/ z2;
        }
    }
    public class Variable(NDArray data)
    {
        public NDArray data = data;
        public NDArray? grad;
        public Function? creator;

        public void SetCreator(Function v)
        {
            this.creator = v;
        }

        public void BackWard()
        {
            var fs = new Stack<Function?>();
            fs.Push(this.creator);
            while (fs?.Count > 0)
            {
                var f = fs.Pop();
                var x = f?.input;
                var y = f?.ouput;
                x.grad = f?.Backward(y?.grad);
                if (x.creator != null)
                {
                    fs?.Push(x.creator);
                }
            }
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
            ouput.SetCreator(this);
            this.input = input;
            this.ouput = ouput;
            return ouput;
        }
        public abstract Variable Forward(Variable x);
        public abstract NDArray Backward(NDArray? gy);
    }
    public class Square : Function
    {
        public override NDArray Backward(NDArray gy)
        {
            var x = this?.input?.data;
            var gx = 2 * x * gy;
            return gx;
        }

        public override Variable Forward(Variable x)
        {
            return new Variable(x.data.matrix_power(2));
        }
    }
    public class Exp : Function
    {
        public override NDArray Backward(NDArray gy)
        {
            var x = this.input.data;
            var gx =np.exp(x)* gy;
            return gx;
        }

        public override Variable Forward(Variable x)
        {
            return new Variable(np.exp(x.data));
        }

    }
}

