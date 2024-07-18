using NumSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dezero
{
    internal class Cos : Function
    {
        public override NDArray?[]? Backward(params NDArray[] gy)
        {
            var x = inputs[0];
            var gx = gy[0] * (-Variable.Sin(x));
            return [gx.data];
        }

        public override NDArray[] Forward(params NDArray?[]? x)
        {
            var y = np.cos(x?[0]);
            return [y];
        }
    }
}
