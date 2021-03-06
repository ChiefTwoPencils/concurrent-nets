﻿using System;
using System.Threading;

namespace CSharpFuncStructs
{
    public sealed class Atom<T> where T : class
    {
        private volatile T _value;
        public T Value => _value;

        public Atom(T value)
        {
            _value = value;
        }

        public T Swap(Func<T, T> factory)
        {
            T original, temp = _value;
            do
            {
                original = _value;
                temp = factory(original);
            } while (Interlocked.CompareExchange(ref _value, temp, original) != original);
            return original;
        }
    }
}
