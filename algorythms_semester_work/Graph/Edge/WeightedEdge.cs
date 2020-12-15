using System;

namespace algorythms_semester_work
{
    public class WeightedEdge : Edge, IComparable, IComparable<WeightedEdge>
    {
        public double Weight { get; protected set; }

        public WeightedEdge(double weight, Node from, Node to)
            :base(from, to)
        {
            Weight = weight;
        }

        public static bool operator >(WeightedEdge first, WeightedEdge second)
            => first.CompareTo(second) > 0;

        public static bool operator <(WeightedEdge first, WeightedEdge second)
            => first.CompareTo(second) < 0;

        public override string ToString() 
            => base.ToString() + $" \twith Weight - { Weight }";

        public int CompareTo(WeightedEdge other)
            => Weight.CompareTo(other.Weight);

        public int CompareTo(object obj)
            => CompareTo(obj as WeightedEdge);
  
    }
}