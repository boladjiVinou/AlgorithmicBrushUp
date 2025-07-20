using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace skiena.algorithms.sorting
{
    public interface IDivisibleByInt<T> :
       IDivisionOperators<T, int, T>,
       IDivisionOperators<T, long, T>,
       IDivisionOperators<T, short, T>,
       IDivisionOperators<T, ulong, T>,
       IDivisionOperators<T, uint, T>,
       IModulusOperators<T, int, T>,
       IModulusOperators<T, long, T>,
       IModulusOperators<T, short, T>,
       IModulusOperators<T, ulong, T>,
       IModulusOperators<T, uint, T>
       where T : IDivisibleByInt<T>
    {
    }
}
