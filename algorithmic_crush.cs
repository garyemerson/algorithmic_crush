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
        
        List<Group> groups = new List<Group>() { new Group(0, n - 1, 0) };
        performUpdates(groups, m);
        
        Console.WriteLine(groups.Select<Group, long>(g => g.Val).Max<long>());
    }
    
    static void performUpdates(List<Group> groups, int numUpdates) {
        for (int i = 0; i < numUpdates; i++) {
            // Console.WriteLine("mark1");
            string[] abk = Console.ReadLine().Split(' ');
            int a = int.Parse(abk[0]) - 1;
            int b = int.Parse(abk[1]) - 1;
            long k = long.Parse(abk[2]);
            // Console.WriteLine("mark2");

            int groupIndexA = getGroupIndex(groups, a);
            int groupIndexB = getGroupIndex(groups, b);
            // Console.WriteLine("a: {0}; b: {1}\ngroups:\n{2}\ngroupIndexA: {3}; groupIndexB: {4}; #groups: {5}", a, b, string.Join("\n", groups.OrderBy<Group, int>(g => g.Start).Select<Group, string>(g => string.Format("{0}-{1}", g.Start, g.End))), groupIndexA, groupIndexB, groups.Count);
            // Console.WriteLine("mark2a");
            Group groupA = groups[groupIndexA];
            // Console.WriteLine("mark2b");
            Group groupB = groups[groupIndexB];
            // Console.WriteLine("mark3");

            
            if (groupA == groupB) {
                if (a == groupA.Start && b == groupA.End) {
                    groupA.Val += k;
                } else if (a == groupA.Start) {
                    Group g1 = new Group(a, b, groupA.Val + k);
                    Group g2 = new Group(b + 1, groupA.End, groupA.Val);
                    groups.RemoveAt(groupIndexA);
                    groups.Add(g1);
                    groups.Add(g2);
                } else if (b == groupA.End) {
                    Group g1 = new Group(a, b, groupA.Val + k);
                    Group g2 = new Group(groupA.Start, a - 1, groupA.Val);
                    groups.RemoveAt(groupIndexA);
                    groups.Add(g1);
                    groups.Add(g2);
                } else {
                    Group g1 = new Group(groupA.Start, a - 1, groupA.Val);
                    Group g2 = new Group(a, b, groupA.Val + k);
                    Group g3 = new Group(b + 1, groupA.End, groupA.Val + k);
                    groups.RemoveAt(groupIndexA);
                    groups.Add(g1);
                    groups.Add(g2);
                    groups.Add(g3);
                }
            } else {
            // Console.WriteLine("mark4");
                List<Group> inbetweenGroups = getInbetweenGroups(a, b, groups);
                foreach (Group g in inbetweenGroups) {
                    g.Val += k;
                }
            // Console.WriteLine("mark5");

                List<int> indicesToRemove = new List<int>();
                if (a == groupA.Start) {
                    groupA.Val += k;
                } else {
                    Group g1 = new Group(groupA.Start, a - 1, groupA.Val);
                    Group g2 = new Group(a, groupA.End, groupA.Val + k);
                    indicesToRemove.Add(groupIndexA);
                    // groups.RemoveAt(groupIndexA);
                    groups.Add(g1);
                    groups.Add(g2);
                }
                if (b == groupB.End) {
                    groupB.Val += k;
                } else {
                    Group g1 = new Group(groupB.Start, b, groupB.Val + k);
                    Group g2 = new Group(b + 1, groupB.End, groupB.Val);
                    indicesToRemove.Add(groupIndexB);
                    // groups.RemoveAt(groupIndexB);
                    groups.Add(g1);
                    groups.Add(g2);
                }

                foreach (int idx in indicesToRemove.OrderByDescending<int, int>(n => n)) {
                    groups.RemoveAt(idx);
                }
            }
        }
    }
    
    private static List<Group> getInbetweenGroups(int a, int b, List<Group> groups) {
        List<Group> inbetweenGroups = new List<Group>();
        int groupIndexA = getGroupIndex(groups, a);
        int groupIndexB = getGroupIndex(groups, b);
        for (int i = 0; i < groups.Count; i++) {
            Group g = groups[i];
            if (g.Start >= a && g.End <= b && i != groupIndexA && i != groupIndexB) {
                inbetweenGroups.Add(groups[i]);
            }
        }
        return inbetweenGroups;
    }

    private static int getGroupIndex(List<Group> groups, int index) {
        for (int i = 0; i < groups.Count; i++) {
            Group g = groups[i];
            if (index >= g.Start && index <= g.End) {
                return i;
            }
        }
        
        return -1; // should never get here
    }
}
