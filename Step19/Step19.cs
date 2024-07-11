// See https://aka.ms/new-console-template for more information
using NumSharp;
using System.Runtime.InteropServices;

namespace dezero
{
    public class Step19
    {
        public static Variable Add(object x0, object x1)
        {
             new Add().Call([(Variable)(x0), (Variable)x1])[0].TryGetTarget(out Variable? v);
             return v!;
        }
        public static Variable Square(Variable x0)
        {
            new Square().Call([x0])[0].TryGetTarget(out Variable? v);
            return v!; 
        }
        public static string getMemory(object o) // 获取引用类型的内存地址方法    
        {
            GCHandle h = GCHandle.Alloc(o, GCHandleType.WeakTrackResurrection);

            IntPtr addr = GCHandle.ToIntPtr(h);

            return "0x" + addr.ToString("X");
        }
        public static void Main()
        {
            var x = new Variable(np.array([[1, 2, 3], [4, 5, 6]]));
            x.name = "x";

            Console.WriteLine(x.name);
            Console.WriteLine(x.Shape);
            Console.WriteLine(x);
        }
    }

}

