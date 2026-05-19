using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skiena.datastructures.graph
{
    public interface IVisitable<T> where T : IEquatable<T>
    {
        void accept(INodeVisitor<T> visitor);
    }
}
