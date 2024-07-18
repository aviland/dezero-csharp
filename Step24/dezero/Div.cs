using NumSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dezero
{
    internal class Div : Function
    {
        public override NDArray?[]? Backward(params NDArray[] gy)
        {
            Variable x0 = this.inputs[0];
         Variable x1   =this.inputs[1];
            var gx0 = gy[0] / x1.data;
            var gx1 = gy * (-x0.data/ Variable.Pow(x1,2).data);

            return [gx0, gx1];
        }

        public override NDArray[] Forward(params NDArray?[]? x)
        {
            var y = x[0] / x[1];
            return [y];
        }
    }
}
