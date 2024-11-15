// See https://aka.ms/new-console-template for more information
using NumSharp;
using System.Runtime.InteropServices;

namespace dezero
{
    public class Step17
    {
        public static Variable add(object x0, object x1)
        {
            Variable v;
             new Add().Call([(Variable)(x0), (Variable)x1])[0].TryGetTarget(out v);
            return v;
        }
        public static Variable square(Variable x0)
        {
            Variable v;
             new Square().Call([x0])[0].TryGetTarget(out v);
            return v; 
        }
        public static string getMemory(object o) // 获取引用类型的内存地址方法    
        {
            GCHandle h = GCHandle.Alloc(o, GCHandleType.WeakTrackResurrection);

            IntPtr addr = GCHandle.ToIntPtr(h);

            return "0x" + addr.ToString("X");
        }
        public static void Main()
        {
           for(int i = 0; i <= 10; i++)
            {
                //x = Variable(np.random.randn(10000))  # big data
                //y = square(square(square(x)))
               var  x = new Variable(np.random.randn(10000000));
                var y = square(square(square(x)));
                y.BackWard();
                Console.WriteLine(y.grad.GetValue());
            }
        }
    }

}

