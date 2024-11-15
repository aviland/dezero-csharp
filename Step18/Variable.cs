﻿using NumSharp;
using System.Collections;
using System.Runtime.CompilerServices;

namespace dezero
{
    public class Variable
    {
        public Variable(NDArray? data,bool RetainGrad=false)
        {
            this.data = data;
            this.RetainGrad = RetainGrad;
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

        public NDArray? data;
        public NDArray? grad;
        public Function? creator;
        internal int Generation = 0;
        bool RetainGrad;

        public void ClearGrad()
        {
            this.grad = null;
        }

        public void SetCreator(Function v)
        {
            this.creator = v;
            this.Generation = v.Generation + 1;
        }

        private  void AddFunc( Function f)
        {
            if (!SeenSet.Contains(f))
            {
                fs.Add(f);
                SeenSet.Add(f);
                fs.Sort(delegate(Function? x, Function? y)  { return x!.Generation!.CompareTo(y!.Generation); });

            }
        }

        private readonly List<Function?> fs = [];
        private readonly HashSet<Function?> SeenSet = [];
        public void BackWard()
        {
            if (this.grad == null)
            {
                this.grad = np.ones_like(this.data);
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
                    gys[i] = o.grad;
                }

                var gxs = f.Backward(gys);

                for (var i = 0; i < f!.inputs!.Length; i++)
                {
                    if (f.inputs[i].grad == null)
                    {
                        f.inputs[i].grad = gxs[i];
                    }
                    else
                    {
                        //不必担心in-place运算问题
                        f.inputs[i].grad = f.inputs[i].grad+ gxs[i];
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
    }
}
