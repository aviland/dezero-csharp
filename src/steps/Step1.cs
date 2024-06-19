// See https://aka.ms/new-console-template for more information
using NumSharp;

namespace dezero
{

    public class Variable(NDArray data)
    {
        public NDArray data = data;
       public NDArray grad=null;

       public Function creator;

     public  void setCreator(Function func){
        creator=func;
       }
        public static void Main(string[] args)
        {
        }
    }

}

