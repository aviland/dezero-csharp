using NumSharp;

namespace Step13
{
    public class Variable
    {
        public Variable(object? data)
        {
            if (data != null)
            {
                if (data is NDArray nd)
                {
                    this.data = nd;
                }
                else
                {
                    throw new IncorrectTypeException(data.GetType().ToString() + " is not supported");
                }
            }
        }

        public NDArray? data;
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
                //var x = f?.inputs;
                var gys = new NDArray[f.ouputs.Length];
                for (var i = 0; i < f.ouputs.Length; i++)
                {
                    gys[i] = f.ouputs[i].grad;
                }

                var gxs = f.Backward(gys);

                for (var i = 0; i < f.inputs.Length; i++)
                {
                    f.inputs[i].grad = gxs[i];
                    if (f.inputs[i].creator != null)
                    {
                        fs.Push(f.inputs[i]!.creator);
                    }
                }


            }
        }
    }
}
