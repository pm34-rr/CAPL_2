using System;
using System.IO;
using System.Diagnostics;

namespace CAPL_2
{
	class Program
	{
		public static string[]		words;

		public static int			uniqueCount;
		public static string		uniqueWord;
		public static int			wordsCount;

		public static int delta( string s )
		{
			int count = 0;
			for (int i = 0, n = Math.Min(s.Length, uniqueWord.Length); i < n; ++i)
			{
				if (uniqueWord[i] == s[i] )
					++count;
			}
			return s.Length - count;
		}

		public static void readParameters()
		{
			Console.WriteLine("Write unique word");
			uniqueWord = Console.ReadLine();

			Console.WriteLine("Write words count");
			wordsCount = int.Parse(Console.ReadLine());
		}

		public static void readText( string path )
		{
			using (StreamReader reader = new StreamReader(path))
			{
				string fullText = reader.ReadToEnd();
				words = fullText.Split(' ', '\n');
			}			
		}

		public static void makeUnique()
		{
			Array.Sort(words);
			int posToSwap	= 0;
			uniqueCount		= 1;
			for ( int i = 0, n = words.Length - 1; i < n; ++i )
			{
				if ( words[i].CompareTo( words[i+1] ) != 0 )
				{
					++posToSwap;
					if (posToSwap != (i + 1))
						words[posToSwap] = words[i + 1];
					++uniqueCount;
				}
			}
			Array.Resize(ref words, uniqueCount);
		}

		public static void getResult()
		{
			Array.Sort( words, (s1, s2)=>delta(s1)-delta(s2) );
		}

		public static void writeAnswer()
		{
			for (int i = 0, n = Math.Min(wordsCount, words.Length); i < n; ++i)
			{
				Console.WriteLine(words[i]);
			}
		}

		public static void Main(string[] args)
		{
			readParameters();
			int count = 1;
			Stopwatch timer = new Stopwatch();
			StreamWriter writer = new StreamWriter( "timeCSharp.txt" );
			for ( int i = 0; i < 4; ++i )
			{
				string path = "strings" + count.ToString() + "000000.txt";
				readText(path);
				timer.Start();
				makeUnique();
				getResult();
				long end = timer.ElapsedMilliseconds;
				writer.WriteLine(count + "000000" + ' ' + end);
				writeAnswer();
				count *= 2;
			}
			writer.Close();
		}
	}
}
