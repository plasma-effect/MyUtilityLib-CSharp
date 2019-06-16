//Copyright (c) 2019 plasma_effect
//This source code is under MIT License. See ./LICENSE
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace UtilityLibrary
{
    [Serializable]
    public class ShiftedArray1<T> : IEnumerable<T>
    {
        internal T[] ar;
        public int Min { get; }
        public int Max { get; }
        public ShiftedArray1(int min, int max)
        {
            if (max < min)
            {
                throw new ArgumentException(@"""min"" must be less than or equal to ""max"".");
            }
            this.ar = new T[max - min];
            this.Min = min;
            this.Max = max;
        }
        public T this[int index]
        {
            get
            {
                return this.ar[index - this.Min];
            }
            set
            {
                this.ar[index - this.Min] = value;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            IEnumerable<T> inner(T[] ar)
            {
                foreach(var v in ar)
                {
                    yield return v;
                }
            }
            return inner(this.ar).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int Length
        {
            get
            {
                return this.ar.Length;
            }
        }

        public long LongLength
        {
            get
            {
                return this.ar.LongLength;
            }
        }

        public int Rank
        {
            get
            {
                return this.ar.Rank;
            }
        }
    }

    [Serializable]
    public class ShiftedArray2<T> : IEnumerable<T>
    {
        internal T[,] ar;
        public int Dim0Length { get; }
        public int Dim0Min { get; }
        public int Dim0Max { get; }
        public int Dim1Length { get; }
        public int Dim1Min { get; }
        public int Dim1Max { get; }
        public int Size
        {
            get
            {
                return this.Dim0Length * this.Dim1Length;
            }
        }

        public ShiftedArray2((int Min, int Max) dim0, (int Min, int Max) dim1)
        {
            if (dim0.Max < dim0.Min)
            {
                throw new ArgumentException(@"""dim0.Min"" must be less than or equal to ""dim0.Max"".");
            }
            if (dim1.Max < dim1.Min)
            {
                throw new ArgumentException(@"""dim1.Min"" must be less than or equal to ""dim1.Max"".");
            }
            this.Dim0Min = dim0.Min;
            this.Dim0Max = dim0.Max;
            this.Dim1Min = dim1.Min;
            this.Dim1Max = dim1.Max;
            this.Dim0Length = dim0.Max - dim0.Min;
            this.Dim1Length = dim1.Max - dim1.Min;
            this.ar = new T[this.Dim0Length, this.Dim1Length];
        }
        public T this[int i,int j]
        {
            get
            {
                return this.ar[i - this.Dim0Min, j - this.Dim1Min];
            }
            set
            {
                this.ar[i - this.Dim0Min, j - this.Dim1Min] = value;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            IEnumerable<T> inner()
            {
                foreach(var i in IntegerEnumerable.Range(this.Dim0Length))
                {
                    foreach(var j in IntegerEnumerable.Range(this.Dim1Length))
                    {
                        yield return this.ar[i, j];
                    }
                }
            }
            return inner().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int Rank
        {
            get
            {
                return 2;
            }
        }
    }
}
