using dezero;
using NumSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dezero
{
    internal class Sub : Function
    {
        public override NDArray?[]? Backward(params NDArray[] gy)
        {
            var gx0 = gy[0];
            var gx1 = 0-gy[0];
            return [gx0, gx1];
        }

        public override NDArray[] Forward(params NDArray?[]? x)
        {
            var y = x[0] - x[1];
            return [y];
        }
    }
}
