// See https://aka.ms/new-console-template for more information
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NumSharp;

namespace dezero
{
    public class Step12
    {
        public static object add(Variable x0, Variable x1)
        {
            return new Add().Call([x0, x1]);
        }
  
            public static void Main()
        {
            var x0 = new Variable(np.array(2));
            var x1 = new Variable(np.array(3));
            var y = add(x0, x1);
            if (y is Variable v)
            {
                Console.WriteLine(v.data.GetValue());
            }
           

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
                var x = f?.inputs;
                var y = f?.ouputs;
                for(var i = 0; i < x!.Length; i++)
                {
                    x[i]!.grad = f!.Backward(y![i]!.grad);
                    if (x[i]!.creator != null)
                    {
                        fs.Push(x[i]!.creator);
                    }
                }
              
            }
        }
    }

    public abstract class Function
    {
        public NDArray?[]? xs;
        public NDArray?[]? ys;

        public Variable?[]? inputs;
        public Variable?[]? ouputs;
        public static NDArray? AsArray(NDArray? x)
        {
            if (np.isscalar(x))
            {
                return np.array(x!.GetDouble());
            }
            return x;
        }
        
        public object Call(params Variable?[]? inputs)
        {
            xs = new NDArray[inputs!.Length];
            for(int i=0;i<inputs.Length;i++)
            {
                xs[i] = inputs[i]!.data;
            }
        
            ys = Forward(xs);
            var ouputs = new Variable[ys.Length];
            for (int i = 0; i < ys.Length; i++)
            {
                ouputs[i]= new Variable(AsArray(ys[i]));
                ouputs[i].SetCreator(this);
            }
            this.inputs = inputs;
            this.ouputs = ouputs;
            if (ouputs.Length > 1)
            {
                return ouputs;
            }
            else
            {
                return ouputs[0];
            }
        }
        public abstract NDArray[] Forward(params NDArray?[]? x);
        public abstract NDArray?[]? Backward(params NDArray?[]? gy);

        public  NDArray[] Forward(NDArray x0, NDArray x1)
        {
            return Forward([x0, x1]);
        }
    }
    public class Add : Function
    {
        public override NDArray?[]? Backward(NDArray?[]? gy)
        {
            return null;
        }

        public override NDArray[] Forward(params NDArray?[]? x)
        {
            var y = x[0] + x[1];
            return [y];
        }
    }

}

