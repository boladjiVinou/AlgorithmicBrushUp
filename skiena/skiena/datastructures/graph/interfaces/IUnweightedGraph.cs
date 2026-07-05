using skiena.datastructures.graph.basegraph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skiena.datastructures.graph.interfaces
{
    public interface IUnweightedGraph<T> : IGraph<T> where T : IEquatable<T>
    {

        public IUnweightedGraph<T> connect(T n1, T n2);

        public IUnweightedGraph<T> disconnect(T n1, T n2);
    }
}
