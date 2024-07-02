using NumSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Step13
{
    internal class Add : Function
    {
        public override NDArray?[]? Backward(NDArray[] gy)
        {
            return [gy[0], gy[0]];
        }

        public override NDArray[] Forward(params NDArray?[]? x)
        {
            var y = x[0] + x[1];
            return [y];
        }
    }
}
