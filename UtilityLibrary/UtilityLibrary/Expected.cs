//Copyright (c) 2019 plasma_effect
//This source code is under MIT License. See ./LICENSE
using System;
using System.Collections.Generic;
using System.Text;

namespace UtilityLibrary
{
    [Serializable]
    public struct Expected<T, E>
        where E:Exception
    {
        T value;
        E exception;
        bool flag;

        public static implicit operator bool(in Expected<T, E> exp)
        {
            return exp.flag;
        }

        public bool TryGet(out T value)
        {
            if (this.flag)
            {
                value = this.value;
                return true;
            }
            else
            {
                value = default;
                return false;
            }
        }

        public E GetException()
        {
            if (this.flag)
            {
                return null;
            }
            else
            {
                return this.exception;
            }
        }

        internal Expected(T value, E exception, bool flag)
        {
            this.value = value;
            this.exception = exception;
            this.flag = flag;
        }
    }

    public static class Expected<E>
        where E : Exception
    {
        public static Expected<T, E> Success<T>(T value)
        {
            return new Expected<T, E>(value, null, true);
        }
        public static Expected<T, E> Failure<T>(E exception)
        {
            return new Expected<T, E>(default, exception, false);
        }
    }
    public static class Expected
    {
        public static Expected<T, Exception> Success<T>(T value)
        {
            return new Expected<T, Exception>(value, null, true);
        }
        public static Expected<T, Exception> Failure<T>(Exception exception)
        {
            return new Expected<T, Exception>(default, exception, false);
        }
    }

}
