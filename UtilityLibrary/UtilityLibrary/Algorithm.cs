//Copyright (c) 2019 plasma_effect
//This source code is under MIT License. See ./LICENSE
using System;
using System.Collections.Generic;
using System.Text;

namespace UtilityLibrary
{
    public static class RangeAlgorithm
    {
        public static bool Equal<T>(IEnumerable<T> lhs, IEnumerable<T> rhs)
        {
            using(var left = lhs.GetEnumerator())
            {
                using(var right = rhs.GetEnumerator())
                {
                    while (left.MoveNext())
                    {
                        if (!right.MoveNext())
                        {
                            return false;
                        }
                        if (!left.Current.Equals(right.Current))
                        {
                            return false;
                        }
                    }
                    if(right.MoveNext())
                    {
                        return false;
                    }
                }
            }
            return true;
        }

    }
}
