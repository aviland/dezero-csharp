// See https://aka.ms/new-console-template for more information
using NumSharp;
using System.Runtime.InteropServices;
using dezero;
using static dezero.Variable;
namespace Step
{
    public class Step24
    {
        public static string getMemory(object o) // 获取引用类型的内存地址方法    
        {
            GCHandle h = GCHandle.Alloc(o, GCHandleType.WeakTrackResurrection);

            IntPtr addr = GCHandle.ToIntPtr(h);

            return "0x" + addr.ToString("X");
        }
        public static void Main()
        {
            var x = new Variable(np.array(1.0));
            var y = new Variable(np.array(1.0));
            Variable z = Goldstein(x, y); //# sphere(x, y) / matyas(x, y)
            //Variable z = matyas(x, y);
            //Variable z =Sphere(x, y);
            z.BackWard();
            print(x.grad, y.grad);
        }


        public static void print(object obj, object obj2)
        {
            Console.WriteLine(obj);
            Console.WriteLine(obj2);
        }
        public static void print(object obj)
        {
            Console.WriteLine(obj);
        }

        public static Variable Sphere(Variable x, Variable y)
        {
            var z = Pow(x, 2) + Pow(y, 2);
            return z;
        }
        public static Variable Matyas(Variable x, Variable y)
        {
            var z = 0.26 * (Pow(x, 2) + Pow(y, 2)) - 0.48 * x * y;
            return z;
        }


        public static Variable Goldstein(Variable x, Variable y)
        {
            var  z =(1 + Pow(x + y + 1,2)*(19 - 14 * x + 3 * Pow(x ,2) - 14 * y + 6 * x * y + 3 * Pow(y, 2)))*
        (30 + Pow(2 * x - 3 * y, 2) * (18 - 32 * x + 12 * Pow(x, 2) + 48 * y - 36 * x * y + 27 * Pow(y, 2)));
            return z;
        }
    }

}

