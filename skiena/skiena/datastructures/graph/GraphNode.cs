using skiena.datastructures.lists;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skiena.datastructures.graph
{
    public class GraphNode<T> : IEquatable<GraphNode<T>> where T : IEquatable<T>
    {
        public T Value { get; set; }
        private Action<T> beforeVisitAction = (v)=> { };
        private Action<T> afterVisitAction = (v) => { };
        private Action<T> visitAction = (v) => { };

        public GraphNode(T value) 
        {
            this.Value = value;
        }

        public bool Equals(GraphNode<T>? other) 
        {
            if (other == null) 
            {
                return false;
            }
            return this.Value.Equals(other.Value);
        }

        public void setBeforeVisitAction(Action<T> beforeVisitAction) 
        {
            this.beforeVisitAction = beforeVisitAction;
        }
        public void setVisitAction(Action<T> action)
        {
            this.visitAction = action;
        }
        public void setAfterVisitAction(Action<T> afterVisitAction)
        {
            this.afterVisitAction = afterVisitAction;
        }
        public void beforeVisit() 
        {
            beforeVisitAction.Invoke(Value);
        }
        public void onVisit() 
        {
            visitAction.Invoke(Value);
        }
        public void afterVisit() 
        {
            afterVisitAction.Invoke(Value);
        }
    }
}
