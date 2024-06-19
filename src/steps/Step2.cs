using System.Diagnostics.CodeAnalysis;
using NumSharp;

namespace dezero
{
    public abstract class Function
    {
        public NDArray? x;
        public Variable? y;

        public Variable? input;
        public Variable? ouput;
        public Variable Call(Variable input)
        {
            x = input.data;
            y = Forward(input);
            var ouput = y;
            ouput.setCreator(this);
            this.input = input;
            this.ouput=ouput;
            return ouput;
        }
        public Variable Forward(NDArray x)
        {
            return Forward(new Variable(x));
        }
        public NDArray Backward(NDArray x)
        {
            return Backward(new Variable(x)).data;
        }
        
        public abstract Variable Forward(Variable x);
        public abstract Variable Backward(Variable gy);
    }
    public delegate Variable FORWARD(NDArray x);
    public class Square : Function
    {

        public override Variable Backward(Variable gy)
        {
            var x = input.data;
            var gx= np.array(2);
            gx =gx.dot(x).dot(gy.data);
            return new Variable(gx);
        }

        public override Variable Forward(Variable x)
        {
            return new Variable(x.data.matrix_power(2));
        }


    }

    public class Exp : Function
    {
        public override Variable Backward(Variable gy)
        {
            var x = input.data;
            var gx = np.exp(x).dot(gy.data);
            return new Variable(gx);
        }

        public override Variable Forward(Variable x)
        {
            return new Variable(np.exp(x.data));
        }
    }
}