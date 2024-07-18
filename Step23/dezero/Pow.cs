using dezero;
using NumSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dezero
{
    internal class Pow : Function
    {
        private int i;
        public Pow(int i)
        {
            this.i = i;
        }
        public override NDArray?[]? Backward(params NDArray[] gy)
        {
            var x = this.inputs;
            var c = i;
            var gx = np.array(c) * (x[0].data.matrix_power(c - 1)) * gy[0];
        return [gx];
        }

        public override NDArray[] Forward(params NDArray?[]? x)
        {
            var z = x[0]?.matrix_power(i);
            return [z];
        }
    }
}
