using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static UtilityLibrary.IntegerEnumerable;

namespace UtilityLibrary
{
    public interface IRandomizerSet<T>
    {
        T Get();
        T Pop();
        void Add(T value);
        int Count { get; }
    }

    [Serializable]
    public class RandomizerSet<T> : IRandomizerSet<T>
    {
        List<T> datas;
        List<int> segTree;
        Random random;
        private void SegTreeReset()
        {
            var u = this.datas.Count;
            var newSegTree = new List<int>(2 * u);
            newSegTree = new List<int>(2 * u);
            newSegTree.AddRange(Range(u).Select(v => 0));
            newSegTree.AddRange(Range(u / 2).Select(i => this.segTree[u / 2 + i]));
            newSegTree.AddRange(Range(u / 2).Select(_ => 0));
            this.segTree = newSegTree;
            SegTreeCalcAll();
        }

        private void SegTreeCalcAll()
        {
            for (var d = this.datas.Count / 2; d > 0; d /= 2)
            {
                foreach (var i in Range(d))
                {
                    this.segTree[d + i] =
                        this.segTree[2 * (d + i)] +
                        this.segTree[2 * (d + i) + 1];
                }
            }
        }

        private int GetIndex(Func<int, int, int, bool> func)
        {
            var i = 0;
            for (var d = 1; d < this.datas.Count; d <<= 1)
            {
                if (func(d + i, 2 * (d + i), 2 * (d + i) + 1))
                {
                    i *= 2;
                    ++i;
                }
                else
                {
                    i *= 2;
                }
            }
            return i;
        }

        public RandomizerSet(IEnumerable<T> ts)
        {
            this.random = new Random();
            this.datas = new List<T>(ts);
            var s = this.datas.Count;
            var u = 1;
            while (u < s)
            {
                u <<= 1;
            }
            while (this.datas.Count != u)
            {
                this.datas.Add(default);
            }
            this.segTree = new List<int>(Range(this.datas.Count).Select(_ => 0));
            foreach(var _ in ts)
            {
                this.segTree.Add(1);
            }
            while (this.segTree.Count != 2 * this.datas.Count)
            {
                this.segTree.Add(0);
            }
            SegTreeCalcAll();
        }
        public void Add(T v)
        {
            if (this.segTree[1] == this.datas.Count)
            {
                this.datas.AddRange(Range(this.datas.Count).Select(_ => default(T)));
                SegTreeReset();
            }
            bool inner(int parent,int child0,int child1)
            {
                return this.segTree[child1] < this.segTree[child0];
            }
            var i = GetIndex(inner);
            this.segTree[this.datas.Count + i] = 1;
            this.datas[i] = v;
            SegTreeCalc(i);
        }

        private (T ret, int index) GetInside()
        {
            if (this.segTree[1] == 0)
            {
                throw new Exception("this randomizer have no elem");
            }
            bool inner(int parent, int child0, int child1)
            {
                return this.random.Next(this.segTree[parent]) < this.segTree[child1];
            }
            var i = GetIndex(inner);
            var ret = this.datas[i];
            return (ret, i);
        }
        public T Get()
        {
            return GetInside().ret;
        }

        private void SegTreeCalc(int i)
        {
            i /= 2;
            for (var d = this.datas.Count / 2; d > 0; d /= 2, i /= 2)
            {
                this.segTree[d + i] =
                    this.segTree[2 * (d + i)] +
                    this.segTree[2 * (d + i) + 1];
            }
        }

        public T Pop()
        {
            var (ret, i) = GetInside();
            this.datas[i] = default;
            this.segTree[i + this.datas.Count] = 0;
            SegTreeCalc(i);
            return ret;
        }

        public int Count
        {
            get
            {
                return this.segTree[1];
            }
        }
    }

}
