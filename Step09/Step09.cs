// See https://aka.ms/new-console-template for more information
using NumSharp;

namespace dezero
{
    public class Step09
    {
        public static Variable Square(Variable? x)
        {
            return new Square().Call(x);
        }

        public static Variable Exp(Variable? x)
        {
            return new Exp().Call(x);
        }

        public static void Main()
        {
            var x = new Variable(np.array(0.5));

            var a = Square(x);
            var b =Exp(a);
            var y = Square(b);

             y.grad = np.array(1.0);
            y.BackWard();

            /*b.grad = C.Backward(y.grad);
            a.grad = B.Backward(b.grad);
            x.grad = A.Backward(a.grad);*/
            Console.WriteLine(x?.grad?.ToString());

            x = new Variable(np.array(0.5));
            y = Square(Exp(Square(x)));
            y.grad = np.array(1.0);
            y.BackWard();
            Console.WriteLine(x?.grad?.ToString());

            x = new Variable(np.array(0.5));
            y = Square(Exp(Square(x)));
            y.BackWard();
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
    public class Variable
    {
        public Variable(object? data)
        {
            if (data != null)
            {
                if(data is  NDArray nd)
                {
                    this.data = nd;
                }
                else
                {
                    throw new IncorrectTypeException(data.GetType().ToString() + " is not supported");
                }
            }
           

        }
        public NDArray? data ;
        public NDArray? grad;
        public Function? creator;

        public void SetCreator(Function v)
        {
            this.creator = v;
        }

        public void BackWard()
        {
            if (this.grad == null)
            {
                this.grad = np.ones_like(this.data);
            }
            var fs = new Stack<Function?>();
            fs.Push(this.creator);
            while (fs?.Count > 0)
            {
                var f = fs.Pop();
                var x = f?.input;
                var y = f?.ouput;
                x!.grad = f?.Backward(y?.grad);
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
        public static NDArray? AsArray(object? x)
        {
            if (x is NDArray nd){
                return nd;
            }
            return np.array((double?)x);
        }
        
        public Variable Call(Variable? input)
        {
            x = input?.data;
            y = Forward(new Variable(x));
            var ouput = new Variable(AsArray(y.data));
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
        public override NDArray Backward(NDArray? gy)
        {
            var x = this?.input?.data;
            var gx = 2 * x * gy;
            return gx;
        }

        public override Variable Forward(Variable x)
        {
            return new Variable(x?.data?.matrix_power(2));
        }
    }
    public class Exp : Function
    {
        public override NDArray Backward(NDArray? gy)
        {
            var x = this?.input?.data;
            var gx =np.exp(x)* gy;
            return gx;
        }

        public override Variable Forward(Variable x)
        {
            return new Variable(np.exp(x.data));
        }

    }
}

