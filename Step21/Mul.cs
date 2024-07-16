using dezero;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NumSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Step22
{
    internal class Mul : Function
    {
        public override NDArray?[]? Backward(params NDArray[] gy)
        {
            var x0 = inputs?[0]?.data;
            var x1 = inputs?[1]?.data;
            return [gy[0] * x1, gy[0] * x0];
        }

        public override NDArray[] Forward(params NDArray?[]? x)
        {
            var y = x?[0] * x?[1];
            return [y];
        }
    }
}
