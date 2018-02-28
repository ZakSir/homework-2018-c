using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Diagnostics;

namespace Homework.Intervals
{
    public static class IntervalCalculator
    {
        public static Interval[] CalculateMissingCoverage(Interval master, Interval[] additionalIntervals)
        {
            List<Interval> result = new List<Interval>() { master };

            Interval[] sortedAdditionalIntervals = additionalIntervals.OrderBy(i => i.Start).ToArray();

            Interval current;
            int b = 0;
            bool done = false;
            double end;
            for (int i = 0; i < sortedAdditionalIntervals.Length; i++)
            {
                // stackref
                current = sortedAdditionalIntervals[i];

                // special case
                if (current.Start < master.Start && current.End > master.End)
                {
                    return new Interval[0];
                }

                // move cursor
                while (current.Start > result[b].End && current.End > result[b].End)
                {
                    Debug.WriteLine("Move cursor forward");
                    b++;

                    if (b == result.Count)
                    {
                        Debug.WriteLine("At Final Result, breaking");
                        // we have nothing else to subtract against the origin interval.
                        // win
                        done = true;
                        break;
                    }
                }

                if (done)
                {
                    break;
                }

                if (current.End < result[b].Start)
                {
                    Debug.WriteLine($"Disregard {current} as it ends before anything we care about starts.");
                    // discard out of range
                    continue;
                }

                if (current.Start <= result[b].Start)
                {
                    Debug.WriteLine($"We know that {nameof(current)}.End is greater from previous method");
                    result[b] = new Interval(current.End, result[b].End);

                    // no more processing.
                    continue;
                }

                Debug.WriteLine($"Splitting current indexed result {result[b]} as {nameof(current)}.Start < {nameof(result)}[{nameof(b)}].Start and {nameof(current)}.End > {nameof(result)}[{nameof(b)}].End");
                bool needFullSplit = current.End < result[b].End;
                end = result[b].End;
                result[b] = new Interval(result[b].Start, current.Start);

                // by definition we know that current.Start > result[b].start
                // we now need to see if it ends before the end of the master
                if (needFullSplit)
                {
                    Debug.WriteLine("Adding additional object to the result array as it is a full cut");
                    result.Add(new Interval(current.End, end));
                }
            }

            return result.ToArray();
        }
    }
}