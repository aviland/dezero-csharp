// See https://aka.ms/new-console-template for more information
using NumSharp;
using System.Runtime.InteropServices;

namespace dezero
{
    public class Step16
    {
        public static Variable add(object x0, object x1)
        {
            return (Variable)new Add().Call([(Variable)(x0), (Variable)x1]);
        }
        public static Variable square(Variable x0)
        {
            return (Variable)new Square().Call([x0]);
        }
        public static string getMemory(object o) // 获取引用类型的内存地址方法    
        {
            GCHandle h = GCHandle.Alloc(o, GCHandleType.WeakTrackResurrection);

            IntPtr addr = GCHandle.ToIntPtr(h);

            return "0x" + addr.ToString("X");
        }
        public static void Main()
        {
            var x = new Variable(np.array(2));
            var a =  square(x);
            var y = add(square(a), square(a));
            y.BackWard();
            Console.WriteLine(y.data.GetValue());
            Console.WriteLine(x.grad.GetValue());
        }
    }

}

