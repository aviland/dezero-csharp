// See https://aka.ms/new-console-template for more information
using NumSharp;
using System.Runtime.InteropServices;

namespace dezero
{
    public class Step22
    {
        public static string getMemory(object o) // 获取引用类型的内存地址方法    
        {
            GCHandle h = GCHandle.Alloc(o, GCHandleType.WeakTrackResurrection);

            IntPtr addr = GCHandle.ToIntPtr(h);

            return "0x" + addr.ToString("X");
        }
        public static void Main()
        {
            var x = new Variable(np.array(2.0));

            var y = -x;
            Console.WriteLine(y);
            var y1 = 2.0 - x;
            var y2 = x - 1.0;
            Console.WriteLine(y1);
            Console.WriteLine(y2);
            y = 3.0/x + 1.0;
            Console.WriteLine(y);

           // y = x ** 3;
            //Console.WriteLine(y);
        }
    }

}

