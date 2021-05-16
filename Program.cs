using System;
using System.Collections.Generic;
using System.Linq;

namespace Experiment
{
	class Program
	{
		static readonly Random random = new Random();
		static void Main()
		{
			var starts = new List<int>() { 5000, 10000, 50000, 100000, 500000, 1000000, 2000000 };
			var count = new List<int>() { 25, 50, 100 };
			WriteRow("Средняя вероятность", "n=25", "n=50", "n=100");
			Console.WriteLine("-----------------------------------------------------");
			starts.ForEach(s =>
			{
				var t = count.Select(c => Experiment(s, c)).ToArray();
				WriteRow(string.Format("За {0} запусков", s), t[0], t[1], t[2]);
			}
			);
			Console.WriteLine("-----------------------------------------------------");
			Console.ReadKey();
		}
		static void WriteRow(params object[] cells) =>
			Console.WriteLine("{0,20}|{1,10}|{2,10}|{3,10}", cells);
		static double Experiment(int s, int c)
		{
			var startList = GenerateList(c);
			var count = 0;
			for (int i = 0; i < s; i++)
				if (AnyMathc(startList, Shuffle(new List<int>(startList))))
					count++;
			return (double)count / s;
		}
		static List<int> GenerateList(int n)
		{
			var l = new List<int>();
			for (int i = 0; i < n; i++)
				l.Add(i);
			return l;
		}
		static bool AnyMathc<T>(List<T> first, List<T> second)
		{
			for (int i = Math.Min(first.Count, second.Count) - 1; i >= 0; i--)
				if (first[i].Equals(second[i]))
					return true;
			return false;
		}
		static List<T> Shuffle<T>(List<T> list)
		{
			int n = list.Count;
			while (n > 1)
			{
				n--;
				int k = random.Next(n + 1);
				T value = list[k];
				list[k] = list[n];
				list[n] = value;
			}
			return list;
		}
	}
}
