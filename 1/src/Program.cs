using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Text.Unicode;
using System.Threading;

namespace AoC;

public class Program
{
    private static void Main(string[] args)
    {
        Regex regex = new Regex(@"(?<left>[0-9]+)\s+(?<right>[0-9]+)", RegexOptions.Compiled);

        string lines = File.ReadAllText("cfg/input.txt");

        MatchCollection matches = regex.Matches(lines);
        
        var left = matches.Where(match => match.Groups["left"].Value != null && int.TryParse(match.Groups["left"].Value, out int _)).Select(match => 
        {
            int.TryParse(match.Groups["left"].Value, out int i);
            return i;
        }).ToArray();
        var right = matches.Where(match => match.Groups["right"].Value != null && int.TryParse(match.Groups["right"].Value, out int _)).Select(match => 
        {
            int.TryParse(match.Groups["right"].Value, out int i);
            return i;
        }).ToArray();

        // Console.WriteLine(Dist(l, r));
        Console.WriteLine(Similarity(left, right));
    }

    private static void RunBaseTest()
    {
        int[] left = new int[]{3, 4, 2, 1, 3, 3};
        int[] right = new int[]{4, 3, 5, 3, 9, 3};

        int? found = Dist(left, right);
        if(found != 11)
        {
            throw new Exception("Failed base test");
        }

        found = Similarity(left, right);
        if(found != 31)
        {
            throw new Exception("Failed base test");
        }
    }

    public static int Similarity(in int[] left, in int[] right)
    {
        var length = left.Length;
        var result = -1;

        for (int i = 0; i < length; i++)
        {
            var target = left[i];
            var occurances = right.Where(x => x.Equals(target)).Count();

            var calc = (target * occurances);

            result = (i <= 0) ? calc : result + calc;
        }

        return result;
    }

    public static int Dist(in int[] left, in int[] right)
    {
        if(left.Length != right.Length)
        {
            throw new Exception("Cannot run function on two arrays that are not the same length!");
        }

        var length = left.Length;

        var left_mutable = left.ToList();
        var right_mutable = right.ToList();

        var result = -1;

        for (int i = 0; i < length; i++)
        {
            if(!FindSmallestNumber(left_mutable.ToArray(), out int left_smallest) || !FindSmallestNumber(right_mutable.ToArray(), out int right_smallest))
            {
                break;
            }

            var curr_dist = (left_smallest > right_smallest) ? left_smallest - right_smallest : right_smallest - left_smallest;

            left_mutable.Remove(left_smallest);
            right_mutable.Remove(right_smallest);

            result = (i <= 0) ? curr_dist : result + curr_dist;
        }

        return result;
    }

    private static bool FindSmallestNumber(int[] array, out int smallest)
    {
        smallest = int.MaxValue;
        foreach(int num in array)
        {
            if(num < smallest)
                smallest = num;
        }
        
        return smallest != int.MaxValue;
    }

}