using System;

class Program
{
    static void Main()
    {
        int[] nums = new int[5];
        nums[0] = 1;
        nums[1] = 3;
        nums[2] = 5;
        nums[3] = 7;
        nums[4] = 11;

        for (int i=0; i<nums.Length; i++)
        {
            Console.WriteLine(nums[i]);
        }
    }
}