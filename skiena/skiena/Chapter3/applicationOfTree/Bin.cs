using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skiena.Chapter3.applicationOfTree
{
    public class Bin : IEquatable<Bin>, IComparable<Bin>
    {
        private double space = 1;
        public static double TRESHOLD = 0.000001;

        public bool canPack(double weight) 
        {
            return space - weight >= -TRESHOLD;
        }
        public double getSpace() 
        {
            return space;
        }
        public bool pack(double weight) 
        {
            if (space - weight < -TRESHOLD) 
            {
                return false;
            }
            space -= weight;
            return true;
        }
        public int CompareTo(Bin? other)
        {
            return space.CompareTo(other?.space);
        }

        public bool Equals(Bin? other)
        {
            if(other == null) return false;
            return space == other.space;
        }
    }
}
