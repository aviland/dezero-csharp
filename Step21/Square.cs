using NumSharp;

namespace dezero
{
    internal class Square : Function
    {
        public override NDArray?[]? Backward(NDArray[] gy)
        {
            var x = this.inputs[0].data;
            var gx = np.array(2) * x * gy[0];
            return [gx];
        }

        public override NDArray[] Forward(params NDArray?[]? x)
        {
            var y = x![0] * x[0];
            return [y];
        }
    }
}
