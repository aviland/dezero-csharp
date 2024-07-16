// See https://aka.ms/new-console-template for more information
using NumSharp;
using System.Runtime.InteropServices;

namespace dezero
{
    public class Step21
    {
        public static string getMemory(object o) // ��ȡ�������͵��ڴ��ַ����    
        {
            GCHandle h = GCHandle.Alloc(o, GCHandleType.WeakTrackResurrection);

            IntPtr addr = GCHandle.ToIntPtr(h);

            return "0x" + addr.ToString("X");
        }
        public static void Main()
        {
            var x = new Variable(np.array(2.0));

            var y = x + np.array(3.0);
            Console.WriteLine(y);
            y = x + 3.0;
            Console.WriteLine(y);
            y = 3.0 * x + 1.0;
            Console.WriteLine(y);
        }
    }

}

