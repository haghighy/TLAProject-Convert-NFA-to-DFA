using System;
using System.Collections.Generic;
using System.Linq;

namespace question2
{
    class Program2
    {
        public static List<string> e_closure (List<string> input, List<string[]> AllTransitionRules,long TransitionCount)
        {
            List<string> result = new List<string>();
            foreach (string state in input)
            {
                List<string> curr_state = new List<string>();
                curr_state.Add(state);
                int k = 0;
                while(k < curr_state.Count)
                {
                    for (int j = 0; j < TransitionCount; j++)
                    {
                        if (AllTransitionRules[j][0] == curr_state[k] && AllTransitionRules[j][1] == "$" )
                        {
                            curr_state.Add(AllTransitionRules[j][2]);
                        }
                    }
                    k++;
                }
                for (int o = 0; o < curr_state.Count; o++)
                {
                    if (!result.Contains(curr_state[o]))
                        result.Add(curr_state[o]);
                }
            }
            return result;
        }

        public static List<string> move (List<string> input, string lable, List<string[]> AllTransitionRules,long TransitionCount)
        {
            List<string> result = new List<string>();
            foreach (string state in input)
            {
                for (int j = 0; j < TransitionCount; j++)
                {
                    if (AllTransitionRules[j][0] == state && AllTransitionRules[j][1] == lable)
                    {
                        result.Add(AllTransitionRules[j][2]);
                    }
                }
            }
            return result;
        }
        public static bool contain (List<List<string>> DFAStates, List<string> mclosur)
        {
            foreach (var state in DFAStates)
            {
                if (complist(state,mclosur))
                {
                    return true;
                }
            }
            return false;
        }
        public static bool complist(List<string> state , List<string> mclosur)
        {
            bool yeki = true;
            state.Sort();
            mclosur.Sort();
            if (state.Count == mclosur.Count)
            {
                for (int j = 0; j < mclosur.Count; j++)
                {
                    if (state[j] != mclosur[j])
                    {
                        yeki = false;
                        break;
                    }
                }
                if (yeki)
                {
                    return true;
                }
            }
            return false;
        }
        public static long NFAtoDFA(string[] AllStates, string[] Alphabet, List<string> FinalStates, List<string[]> AllTransitionRules,long TransitionCount)
        {
            List<List<string>> DFAStates = new List<List<string>>();
            List<string> input = new List<string>();
            input.Add(AllStates[0]);
            DFAStates.Add(e_closure(input, AllTransitionRules,TransitionCount));
            int k = 0;
            while (k< DFAStates.Count)
            {
                foreach (string i in Alphabet)
                {
                    List<string> m = move(DFAStates[k],i,AllTransitionRules,TransitionCount);
                    List<string> mclosur = e_closure(m,AllTransitionRules,TransitionCount);
                    if (!contain(DFAStates, mclosur))
                        DFAStates.Add(mclosur);
                }
                k++;
            }
            return DFAStates.Count;
        }
        static void Main(string[] args)
        {
            string[] AllStates = Console.ReadLine().Trim('}','{').Split(',');
            string[] Alphabet = Console.ReadLine().Trim('}','{').Split(',');
            List<string> FinalStates = Console.ReadLine().Trim('}','{').Split(',').ToList();
            long n = long.Parse(Console.ReadLine());
            List<string[]> AllTransitionRules = new List<string[]>();
            for (int i = 0; i < n; i++)
            {
                string[] transition = Console.ReadLine().Split(',');
                AllTransitionRules.Add(new string[3]{transition[0], transition[1], transition[2]});
            }
            System.Console.WriteLine( NFAtoDFA(AllStates, Alphabet, FinalStates, AllTransitionRules, n));
        }
    }
}