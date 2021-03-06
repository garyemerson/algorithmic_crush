// === ======= ======== =====
//  |              |

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
class Solution {
    static void Main(String[] args) {
        string[] nm = Console.ReadLine().Split(' ');
        int n = int.Parse(nm[0]);
        int m = int.Parse(nm[1]);
        
        long[] nums = new long[n];
        performUpdates(nums, m);
        
        Console.WriteLine(maxSum(nums));
    }
    
    static long maxSum(long[] nums) {
        long sum = 0;
        long max = 0;
        foreach (long n in nums) {
            sum += n;
            if (sum > max) {
                max = sum;
            }
        }
        return  max;
    }

    static void performUpdates(long[] nums, int numUpdates) {
        for (int i = 0; i < numUpdates; i++) {
            string[] abk = Console.ReadLine().Split(' ');
            int a = int.Parse(abk[0]) - 1;
            int b = int.Parse(abk[1]) - 1;
            long k = long.Parse(abk[2]);

            nums[a] += k;
            if (b + 1 < nums.Length) {
                nums[b + 1] -= k;
            }
        }
    }
}
