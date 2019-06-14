//Copyright (c) 2019 plasma_effect
//This source code is under MIT License. See ./LICENSE
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using static UtilityLibrary.IntegerEnumerable;

namespace UtilityLibrary
{
    [Serializable]
    public struct StringView : IComparable<StringView>, IEquatable<StringView>, IEnumerable<char>
    {
        private string str;
        private int start;
        public int Length { get; }

        public StringView(string str)
        {
            this.str = str;
            this.start = 0;
            this.Length = str.Length;
        }

        private StringView(string str, int start, int length)
        {
            this.str = str;
            this.start = start;
            this.Length = length;
        }

        public StringView Substring(int startIndex)
        {
            if (startIndex < 0 || this.Length < startIndex)
            {
                throw new ArgumentOutOfRangeException("startIndex is less than zero or greater than the length of this instance.");
            }
            return new StringView(this.str, this.start + startIndex, this.Length - startIndex);
        }

        public StringView Substring(int startIndex, int length)
        {
            if (startIndex < 0 || length < 0)
            {
                throw new ArgumentOutOfRangeException("startIndex or length is less than zero.");
            }
            if (this.Length < startIndex + length)
            {
                throw new ArgumentOutOfRangeException("startIndex plus length indicates a position not within this instance.");
            }
            return new StringView(this.str, this.start + startIndex, length);
        }

        public bool Contains(StringView view)
        {
            if (view.Length == 0)
            {
                return true;
            }
            for (var i = 0; i + view.Length <= this.Length; ++i)
            {
                var f = true;
                foreach (var n in Range(0, view.Length))
                {
                    if (At(i + n) != view.At(n))
                    {
                        f = false;
                        break;
                    }
                }
                if (f)
                {
                    return true;
                }
            }
            return false;
        }
        public bool Contains(string value)
        {
            if (value.Length == 0)
            {
                return true;
            }
            for (var i = 0; i + value.Length <= this.Length; ++i)
            {
                var f = true;
                foreach (var n in Range(0, value.Length))
                {
                    if (At(i + n) != value[n])
                    {
                        f = false;
                        break;
                    }
                }
                if (f)
                {
                    return true;
                }
            }
            return false;
        }

        public bool Equals(StringView other)
        {
            if (this.Length != other.Length)
            {
                return false;
            }
            foreach (var i in Range(0, this.Length))
            {
                if (At(i) != other.At(i))
                {
                    return false;
                }
            }
            return true;
        }
        public bool Equals(string other)
        {
            if (this.Length != other.Length)
            {
                return false;
            }
            foreach (var i in Range(0, this.Length))
            {
                if (At(i) != other[i])
                {
                    return false;
                }
            }
            return true;
        }
        public override bool Equals(object obj)
        {
            return
                obj is StringView view ? Equals(view) :
                obj is String str ? Equals(str) : false;
        }

        public override string ToString()
        {
            return this.str.Substring(this.start, this.Length);
        }

        public int CompareTo(StringView other)
        {
            foreach (var i in Range(0, Math.Min(this.Length, other.Length)))
            {
                var v = At(i).CompareTo(other.At(i));
                if (v != 0)
                {
                    return v;
                }
            }
            return this.Length.CompareTo(other.Length);
        }

        public IEnumerator<char> GetEnumerator()
        {
            foreach (var i in Range(this.start, this.Length))
            {
                yield return this.str[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public char this[int index]
        {
            get
            {
                if (index < 0 || this.Length <= index)
                {
                    throw new IndexOutOfRangeException("index is greater than or equal to the length of this object or less than zero.");
                }
                return this.str[this.start + index];
            }
        }
        private char At(int index)
        {
            return this.str[this.start + index];
        }

        public override int GetHashCode()
        {
            var hashCode = -1860133252;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.str);
            hashCode = hashCode * -1521134295 + this.start.GetHashCode();
            hashCode = hashCode * -1521134295 + this.Length.GetHashCode();
            return hashCode;
        }

        public static bool operator ==(StringView lhs, StringView rhs)
        {
            return lhs.Equals(rhs);
        }
        public static bool operator !=(StringView lhs, StringView rhs)
        {
            return !lhs.Equals(rhs);
        }
    }
}
