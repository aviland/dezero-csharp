// See https://aka.ms/new-console-template for more information
using NumSharp;
using Step18;
using System.Runtime.InteropServices;

namespace dezero
{
    public class Step18
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
            //x = Variable(np.random.randn(10000))  # big data
            //y = square(square(square(x)))
            var x0 = new Variable(np.array(1));
            var x1 = new Variable(np.array(1));
            var t = add(x0, x1);
            var y = add(x0, t);
            y.BackWard();
            Console.WriteLine(y?.grad?.GetValue());
            Console.WriteLine(t?.grad?.GetValue());
            Console.WriteLine(x0?.grad?.GetValue());
            Console.WriteLine(x1?.grad?.GetValue());

            Config.EnableBackprop = true;
            var x = new Variable(np.ones((100, 100, 100)));
            y = square(square(square(x)));
            y.BackWard();

            Config.EnableBackprop = false;
             x = new Variable(np.ones((100, 100, 100)));
            y = square(square(square(x)));
        }
    }

}

