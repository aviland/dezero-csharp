using NumSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dezero
{
    internal class Neg : Function
    {
        public override NDArray?[]? Backward(params NDArray[] gy)
        {
            return [0 - gy[0]];
        }

        public override NDArray[] Forward(params NDArray?[]? x)
        {
            return  [0-x[0]];
        }
    }
}
