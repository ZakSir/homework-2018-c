using System;
using System.Collections.Generic;
using System.Text;

namespace Homework.Intervals
{
    public struct Interval : IEquatable<Interval>
    {
        public double Start { get; private set; }
        public double End { get; private set; }

        public Interval(double Start, double End)
        {
            if (Start > End)
            {
                throw new ArgumentException("Start cannot be greater than End");
            }

            this.Start = Start;
            this.End = End;
        }

        public static bool operator == (Interval a, Interval b)
        {
            return a.Start == b.Start && a.End == b.End;
        }

        public static bool operator !=(Interval a, Interval b)
        {
            return !(a.Start == b.Start && a.End == b.End);
        }

        public override bool Equals(object obj)
        {
            if(object.ReferenceEquals(null, obj))
            {
                return false;
            }

            if(object.ReferenceEquals(this, obj))
            {
                return true;
            }

            if(obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Interval i = (Interval)obj;

            return this.Equals(i);
        }

        public override string ToString()
        {
            return $"{{{this.Start}, {this.End}}}";
        }

        public bool Equals(Interval other)
        {
            return this.Start == other.Start && this.End == other.End;
        }
    }
}
