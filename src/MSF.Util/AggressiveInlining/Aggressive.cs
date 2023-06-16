using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace MSF.Util.AggressiveInlining
{
    public static class Aggressive
    {
        // Substitutes a method call with the method’s body
        // Useful for small, frequently-called methods where the overhead of a method call can be a performance bottleneck
        // Can improve performance by reducing the overhead of method calls
        // It’s only a suggestion to the JIT compiler, not a guarantee!

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Add(int x, int y)
        {
            return x + y;
        }
    }
}
