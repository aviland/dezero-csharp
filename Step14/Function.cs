using NumSharp;

namespace dezero
{
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
                var z = x!.GetValue();
                return np.array(Convert.ToDouble(z));
            }
            return x;
        }

        public object Call(params Variable?[]? inputs)
        {
            xs = new NDArray[inputs!.Length];
            for (int i = 0; i < inputs.Length; i++)
            {
                xs[i] = inputs[i]!.data;
            }

            ys = Forward(xs);
            var ouputs = new Variable[ys.Length];
            for (int i = 0; i < ys.Length; i++)
            {
                ouputs[i] = new Variable(AsArray(ys[i]));
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
        public abstract NDArray?[]? Backward(params NDArray[] gy);

        public NDArray[] Forward(NDArray x0, NDArray x1)
        {
            return Forward([x0, x1]);
        }
    }
}
