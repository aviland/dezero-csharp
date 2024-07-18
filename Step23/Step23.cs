// See https://aka.ms/new-console-template for more information
using NumSharp;
using System.Runtime.InteropServices;
using dezero;

namespace Step
{
    public class Step23
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
            var y = Pow(x + 3,2);
            y.BackWard();
            print(y);
            print(x.grad);
        }

        private static Variable? Pow(Variable x, int v)
        {
            return new Pow(v).Call(x)[0];
        }

        private static void print(object obj)
        {
            Console.WriteLine(obj);
        }
    }

}

