// See https://aka.ms/new-console-template for more information
using NumSharp;

namespace dezero
{

    public class Variable(NDArray data)
    {
        public NDArray data = data;

        public static void Main()
        {
            var d = np.array(1.0);
            var x = new Variable(d);
            Console.WriteLine(x.data.ToString());

            x.data = np.array(2);
            Console.WriteLine(x.data.ToString());
        }
    }

}

