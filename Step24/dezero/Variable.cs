using NumSharp;

namespace dezero
{
    public class Variable
    {
        public Variable(NDArray? data, string? name = null)
        {
            this.data = data;
            this.name = name;
        }
        public Variable(object? data)
        {
            if (data != null)
            {
                if (data is NDArray nd)
                {
                    new Variable(nd);
                }
                else
                {
                    throw new IncorrectTypeException(data.GetType().ToString() + " is not supported");
                }
            }
        }

        public override string ToString()
        {
            //if self.data is None:
            //  return 'variable(None)'
            //   p = str(self.data).replace('\n', '\n' + ' ' * 9)
            //   return 'variable(' + p + ')'
            if (data is null)
            {
                return "Variable(None)";
            }
            var p = data.ToString().Replace("\n", "\n" + "         ");
            return $"Variable({p})";
        }

        public NDArray? data;
        public Variable? grad;
        public Function? creator;
        internal int generation = 0;
        public string? name;



        public int Ndim
        {
            get
            {
                return data!.ndim;
            }
        }
        public int Size
        {
            get
            {
                return data!.size;
            }
        }

        public Type Dtype
        {
            get
            {
                return data!.dtype;
            }
        }

        public string Shape
        {
            get
            {
                return $"[{string.Join(",", data!.shape)}]";
            }
        }

        public void ClearGrad()
        {
            grad = null;
        }

        public void SetCreator(Function v)
        {
            creator = v;
            generation = v.generation + 1;
        }

        private void AddFunc(Function f)
        {
            if (!SeenSet.Contains(f))
            {
                fs.Add(f);
                SeenSet.Add(f);
                fs.Sort(delegate (Function? x, Function? y) { return x!.generation!.CompareTo(y!.generation); });

            }
        }

        private readonly List<Function?> fs = [];
        private readonly HashSet<Function?> SeenSet = [];
        public void BackWard(bool RetainGrad = false)
        {
            if (grad == null)
            {
                grad = new Variable(np.ones_like(data));
            }

            fs.Clear();
            SeenSet.Clear();

            AddFunc(creator!);
            while (fs?.Count > 0)
            {
                var f = fs.Last();
                fs.Remove(f);
                //var x = f?.inputs;
                var gys = new NDArray[f.ouputs.Length];
                for (var i = 0; i < f.ouputs.Length; i++)
                {
                    Variable o;
                    f.ouputs[i].TryGetTarget(out o);
                    gys[i] = o.grad.data;
                }

                var gxs = f.Backward(gys);

                for (var i = 0; i < f!.inputs!.Length; i++)
                {
                    if (f.inputs[i].grad == null)
                    {
                        f.inputs[i].grad = new Variable(gxs[i]);
                    }
                    else
                    {
                        //不必担心in-place运算问题
                        f.inputs[i].grad = f.inputs[i].grad + gxs[i];
                    }
                    if (f.inputs[i].creator != null)
                    {
                        AddFunc(f!.inputs[i]!.creator!);
                    }
                }
                if (RetainGrad == false)
                {
                    foreach (var item in f.ouputs)
                    {
                        item!.TryGetTarget(out var g);
                        g!.grad = null;
                    }
                }
            }
        }
        public static Variable operator +(Variable b, Variable c)
        {
            return new Add().Call([b, c])[0];
        }
        public static Variable operator +(Variable b, int c)
        {
            return new Add().Call([b, new Variable(c)])[0];
        }
        public static Variable operator +(Variable b, double c)
        {
            return new Add().Call([b, new Variable(np.array(c))])[0];
        }
        public static Variable operator +(Variable b, NDArray c)
        {
            return new Add().Call([b, new Variable(c)])[0];
        }
        public static Variable operator +(NDArray b, Variable c)
        {
              return new Add().Call([new Variable(b), c])[0];
        }


        public static Variable operator *(Variable b, Variable c)
        {
            return new Mul().Call([b, c])[0];
        }

        public static Variable operator *(double b, Variable c)
        {
            return new Mul().Call([new Variable(np.array(b)), c])[0];
        }

        public static Variable operator *(Variable b, double c)
        {
            return new Mul().Call([b, new Variable(np.array(c))])[0];
        }

        public static Variable operator -(Variable b,Variable c)
        {
            return new Sub().Call([b, c])[0];
        }
        public static Variable operator -(NDArray b, Variable c)
        {
            return new Sub().Call([new Variable(b), c])[0];
        }
        public static Variable operator -(Variable b)
        {
            return new Neg().Call([b])[0];
        }
        public static Variable operator -(Variable b, NDArray c)
        {
            return new Sub().Call([b, new Variable(c)])[0];
        }

        public static Variable operator /(Variable b, NDArray c)
        {
            return new Div().Call([b, new Variable(c)])[0];
        }
        public static Variable operator /(NDArray b, Variable c)
        {
            return new Div().Call([new Variable(b),c])[0];
        }

        public static Variable Pow(Variable x, int v)
        {
            return new Pow(v).Call(x)[0];
        }

        public static Variable Cos(Variable x)
        {
            return new Cos().Call(x)[0];
        }

        public static Variable Sin(Variable x)
        {
            return new Sin().Call(x)[0];
        }
    }
}
