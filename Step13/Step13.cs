// See https://aka.ms/new-console-template for more information
using NumSharp;

namespace Step13
{
    public class Step13
    {
        public static Variable add(object x0, object x1)
        {
            return (Variable)new Add().Call([(Variable)(x0), (Variable)x1]);
        }
        public static object square(Variable x0)
        {
            return new Square().Call([x0]);
        }

        public static void Main()
        {
            var x = new Variable(np.array(2));
            var y = new Variable(np.array(3));
            var z = add(square(x), square(y));
            z.BackWard();
            Console.WriteLine(z.data.GetValue());
            Console.WriteLine(x.grad.GetValue());
            Console.WriteLine(y.grad.GetValue());
        }
    }

}

