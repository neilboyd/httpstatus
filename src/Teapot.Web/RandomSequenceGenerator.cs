using System;
using System.Linq;

namespace Teapot.Web;

public class RandomSequenceGenerator
{
    private readonly Random _random = new();
    private int[] Range { get; set; }

    public static bool TryParse(string range, out RandomSequenceGenerator generator)
    {
        try
        {
            generator = new RandomSequenceGenerator
            {
                // copied from https://stackoverflow.com/a/37213725/260221
                Range = range.Split(',')
                             .Select(x => x.Split('-'))
                             .Select(p => new { First = int.Parse(p.First()), Last = int.Parse(p.Last()) })
                             .SelectMany(x => Enumerable.Range(x.First, x.Last - x.First + 1))
                             .OrderBy(z => z)
                             .ToArray()
            };
            return true;
        }
        catch
        {
            generator = null;
            return false;
        }
    }

    public int Next => Range[_random.Next(Range.Length)];
}
