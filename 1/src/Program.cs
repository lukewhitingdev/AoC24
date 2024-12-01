using System;
using System.Threading;

namespace AoC;

public class Program
{
    private static void Main(string[] args)
    {
        // First test the base case.
        int[] left = new int[]{3, 4, 2, 1, 3, 3};
        int[] right = new int[]{4, 3, 5, 3, 9, 3};

        RunTest(left, right, 11);
    }

    private static void RunTest(in int[] left, in int[] right, int expected_dist)
    {
        int found = Dist(left, right);
        if(found != expected_dist)
        {
            throw new Exception("Failed test");
        }
    }

    public static int Dist(in int[] left, in int[] right)
    {
        return -1;
    }
}