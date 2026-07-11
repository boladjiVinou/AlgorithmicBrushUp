using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skiena.datastructures.graph.interfaces
{
    public interface IWeightedGraph<T,U> : IGraph<T>  where T : IEquatable<T> where U : IEquatable<U>
    {

        public IWeightedGraph<T, U> connect(T n1, T n2, U weight);

        public IWeightedGraph<T, U> disconnect(T n1, T n2);

        public void setWeight(T n1, T n2, U weight);
        public U getWeight(T n1, T n2);
    }
}
