using NumSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dezero
{
    internal class Sin:Function
    {
      
public override NDArray[] Forward(params NDArray?[]? x)
        {
            var y = np.sin(x);
            return [y];
        }

        public override NDArray?[]? Backward(params NDArray[] gy)
        {
            var x = inputs[0];
            var gx = gy[0] * Variable.Cos(x);
            return [gx.data];
        }

     
    }
}
