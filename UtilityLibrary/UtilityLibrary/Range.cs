//Copyright (c) 2019 plasma_effect
//This source code is under MIT License. See ./LICENSE
using System.Collections;
using System.Collections.Generic;
using System;
using UtilityLibrary.EnumeratorClass;

namespace UtilityLibrary
{
    namespace EnumeratorClass
    {
        public abstract class IntegerEnumerator<T> : IEnumerator<T>
            where T : struct, IComparable<T>
        {
            protected T start, step;
            protected T? now;

            protected T min;
            protected T max;

            internal bool valid;

            protected IntegerEnumerator(T start, T step, T min, T max, bool valid)
            {
                this.start = start;
                this.step = step;
                this.min = min;
                this.max = max;
                this.valid = valid;
            }

            public T Current => this.now ?? throw new NullReferenceException();

            object IEnumerator.Current => this.Current;

            public void Dispose()
            {

            }

            public abstract bool MoveNext();

            public void Reset()
            {
                this.now = null;
            }
        }

        public class IntEnumerator : IntegerEnumerator<int>
        {
            public IntEnumerator(int start, int step, int min, int max, bool valid) : base(start, step, min, max, valid)
            {

            }

            public override bool MoveNext()
            {
                if (!this.valid)
                {
                    return false;
                }
                if (this.now is int now)
                {
                    if (this.step < 0 && now <= this.min - this.step)
                    {
                        return false;
                    }
                    if (0 < this.step && this.max - this.step <= now)
                    {
                        return false;
                    }
                    this.now += this.step;
                }
                else
                {
                    this.now = this.start;
                }
                return true;
            }
        }

        public class LongEnumerator : IntegerEnumerator<long>
        {
            public LongEnumerator(long start, long step, long min, long max, bool valid) : base(start, step, min, max, valid)
            {

            }

            public override bool MoveNext()
            {
                if (!this.valid)
                {
                    return false;
                }
                if (this.now is long now)
                {
                    if (this.step < 0 && now <= this.min - this.step)
                    {
                        return false;
                    }
                    if (0 < this.step && this.max - this.step <= now)
                    {
                        return false;
                    }
                    this.now += this.step;
                }
                else
                {
                    this.now = this.start;
                }
                return true;
            }
        }

        public abstract class IntegerEnumerable<T> : IEnumerable<T>
            where T : struct, IComparable<T>
        {
            internal T start, step;

            protected IntegerEnumerable(T start, T step)
            {
                this.start = start;
                this.step = step;
                this.Valid = true;
            }

            public T MinValue { get; internal set; }
            public T MaxValue { get; internal set; }
            internal bool Valid { get; set; }

            public abstract IEnumerator<T> GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            internal abstract IntegerEnumerable<T> DeepCopy();
            public abstract int Count();
            public abstract long LongCount();
            public T[] ToArray()
            {
                var ret = new T[Count()];
                var index = -1;
                foreach (var v in this)
                {
                    ret[++index] = v;
                }
                return ret;
            }
            public List<T> ToList()
            {
                var ret = new List<T>(Count());
                foreach (var v in this)
                {
                    ret.Add(v);
                }
                return ret;
            }
            public abstract IntegerEnumerable<T> Reverse();
        }

        public class IntEnumerable : IntegerEnumerable<int>
        {
            internal IntEnumerable(int start, int step) : base(start, step)
            {
                this.MinValue = int.MinValue;
                this.MaxValue = int.MaxValue;
            }

            public override IEnumerator<int> GetEnumerator()
            {
                return new IntEnumerator(this.start, this.step, this.MinValue, this.MaxValue, this.Valid);
            }

            public override int Count()
            {
                var last = this.step < 0 ? this.MinValue : this.MaxValue;
                var count = (last - this.start) / this.step;
                if (count * this.step + this.start == last)
                {
                    return count;
                }
                else
                {
                    return count + 1;
                }
            }

            public override long LongCount()
            {
                return Count();
            }

            internal override IntegerEnumerable<int> DeepCopy()
            {
                return new IntEnumerable(this.start, this.step)
                {
                    MinValue = this.MinValue,
                    MaxValue = this.MaxValue,
                    Valid = this.Valid
                };
            }

            public override IntegerEnumerable<int> Reverse()
            {
                var last = this.step < default(int) ? this.MinValue : this.MaxValue;
                return new IntEnumerable((last - this.start) / this.step * this.step + this.start - this.step, -this.step);
            }
        }

        public class LongEnumerable : IntegerEnumerable<long>
        {
            public LongEnumerable(long start, long step) : base(start, step)
            {
                this.MinValue = long.MinValue;
                this.MaxValue = long.MaxValue;
            }

            public override int Count()
            {
                return (int)LongCount();
            }

            public override long LongCount()
            {
                var last = this.step < 0 ? this.MinValue : this.MaxValue;
                var count = (last - this.start) / this.step;
                if (count * this.step + this.start == last)
                {
                    return count;
                }
                else
                {
                    return count + 1;
                }
            }

            public override IEnumerator<long> GetEnumerator()
            {
                return new LongEnumerator(this.start, this.step, this.MinValue, this.MaxValue, this.Valid);
            }

            internal override IntegerEnumerable<long> DeepCopy()
            {
                return new LongEnumerable(this.start, this.step)
                {
                    MinValue = this.MinValue,
                    MaxValue = this.MaxValue,
                    Valid = this.Valid
                };
            }

            public override IntegerEnumerable<long> Reverse()
            {
                var last = this.step < default(long) ? this.MinValue : this.MaxValue;
                return new LongEnumerable((last - this.start) / this.step * this.step + this.start - this.step, -this.step);
            }
        }
    }
    public static class IntegerEnumerable
    {
        public static IntEnumerable Range(int first, int last, int step)
        {
            if (step == 0)
            {
                throw new ArgumentException(@"""step"" must not be 0.");
            }
            else if (step < 0 && first < last)
            {
                throw new ArgumentException(@"if ""step"" is less than 0, ""first"" must be more than or equal to ""last"".");
            }
            else if (0 < step && last < first)
            {
                throw new ArgumentException(@"if ""step"" is more than 0, ""first"" must be less than or equal to ""last"".");
            }
            var count = (last - first) / step;
            if (count == 0)
            {
                return new IntEnumerable(first, step)
                {
                    MinValue = first,
                    MaxValue = first,
                    Valid = false
                };
            }
            else
            {
                return new IntEnumerable(first, step)
                {
                    MinValue = Math.Min(first, last),
                    MaxValue = Math.Max(first, last)
                };
            }
        }

        public static IntEnumerable Range(int first, int last)
        {
            return Range(first, last, last < first ? -1 : 1);
        }

        public static IntEnumerable Range(int last)
        {
            return Range(default, last);
        }

        public static LongEnumerable Range(long first, long last, long step)
        {
            if (step == 0)
            {
                throw new ArgumentException(@"""step"" must not be 0.");
            }
            else if (step < 0 && first < last)
            {
                throw new ArgumentException(@"if ""step"" is less than 0, ""first"" must be more than or equal to ""last"".");
            }
            else if (0 < step && last < first)
            {
                throw new ArgumentException(@"if ""step"" is more than 0, ""first"" must be less than or equal to ""last"".");
            }
            var count = (last - first) / step;
            if (count == 0)
            {
                return new LongEnumerable(first, step)
                {
                    MinValue = first,
                    MaxValue = first,
                    Valid = false
                };
            }
            else
            {
                return new LongEnumerable(first, step)
                {
                    MinValue = Math.Min(first, last),
                    MaxValue = Math.Max(first, last)
                };
            }
        }

        public static LongEnumerable Range(long first, long last)
        {
            return Range(first, last, last < first ? -1 : 1);
        }

        public static LongEnumerable Range(long last)
        {
            return Range(default, last);
        }
    }
}
