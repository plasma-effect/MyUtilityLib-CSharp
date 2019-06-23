//Copyright (c) 2019 plasma_effect
//This source code is under MIT License. See ./LICENSE
using System;
using System.Collections.Generic;
using System.Text;

namespace UtilityLibrary
{
    [Serializable]
    public struct Expected<T>
    {
        T value;
        Exception exception;
        bool flag;
        
        public static implicit operator bool(in Expected<T> exp)
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

        public Exception GetException()
        {
            if (this.flag)
            {
                return null;
            }
            else
            {
                if (this.exception is null)
                {
                    this.exception = Expected.DefaultException;
                }
                return this.exception;
            }
        }

        internal Expected(T value, Exception exception, bool flag)
        {
            this.value = value;
            this.exception = exception;
            this.flag = flag;
        }
    }

    public static class Expected
    {
        internal static NullReferenceException DefaultException { get; }
        public static Expected<T> Success<T>(T value)
        {
            return new Expected<T>(value, null, true);
        }
        public static Expected<T> Failure<T>(Exception exception)
        {
            return new Expected<T>(default, exception, false);
        }
        static Expected()
        {
            DefaultException = new NullReferenceException("This Expected value has no value.");
        }
    }

}
