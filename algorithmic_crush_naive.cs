// === ======= ======== =====
//  |              |

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
class Solution {
    private class Group {
        public int Start;
        public int End;
        public long Val;
        
        public Group(int s, int e, long v) {
            this.Start = s;
            this.End = e;
            this.Val = v;
        }
    }
    
    static void Main(String[] args) {
        string[] nm = Console.ReadLine().Split(' ');
        int n = int.Parse(nm[0]);
        int m = int.Parse(nm[1]);
        
        long[] nums = new long[n];
        performUpdates(nums, m);
        Console.WriteLine(nums.Max<long>());
    }
    
    static void performUpdates(long[] nums, int numUpdates) {
        for (int i = 0; i < numUpdates; i++) {
            string[] abk = Console.ReadLine().Split(' ');
            int a = int.Parse(abk[0]) - 1;
            int b = int.Parse(abk[1]) - 1;
            int k = int.Parse(abk[2]);

            for (int j = a; j <= b; j++) {
                nums[j] += k;
            }
        }
    }
}
