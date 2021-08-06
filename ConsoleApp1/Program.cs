using System.Collections.Generic;

namespace ConsoleApp1
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var dictionary = new Dictionary<string, Dictionary<string, List<string>>>();
            switch (dictionary.TryGetValue("", out var value))
            {
                case true:
                    switch (value!.TryGetValue("", out var list))
                    {
                        case true:
                            list!.Add(string.Empty);
                            break;
                        case false:
                            value.Add(string.Empty, new List<string> {string.Empty});
                            break;
                    }

                    break;
                case false:
                    dictionary.Add("",
                        new Dictionary<string, List<string>>
                        {
                            {string.Empty, new List<string> {string.Empty}}
                        });
                    break;
            }
        }
    }

    internal class A : IA
    {
    }

    internal interface IA
    {
    }
}
