// See https://aka.ms/new-console-template for more information
using NumSharp;
using System.Runtime.InteropServices;

namespace dezero
{
    public class Step20
    {
        public static string getMemory(object o) // 获取引用类型的内存地址方法    
        {
            GCHandle h = GCHandle.Alloc(o, GCHandleType.WeakTrackResurrection);

            IntPtr addr = GCHandle.ToIntPtr(h);

            return "0x" + addr.ToString("X");
        }
        public static void Main()
        {
            var a = new Variable(np.array(3.0));
            var b = new Variable(np.array(2.0));
            var c = new Variable(np.array(1.0));

// y = add(mul(a, b), c)
            var y = a * b + c;
            y.BackWard();

            Console.WriteLine(y);
            Console.WriteLine(a.grad.GetValue());
 Console.WriteLine(b.grad.GetValue());
        }
    }

}

