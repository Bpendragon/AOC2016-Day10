using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace day10
{
	class Program
	{
		static void Main(string[] args)
		{
			System.IO.StreamReader file = new System.IO.StreamReader("..\\..\\input.txt");
			string line;

			//switch this based on which part you're solving for
			bool part1 = false;

			//set these as the cips you've been given in part 1
			int chip1 = 17;
			int chip2 = 61;

			//set these as the outputs given in part 2
			int output1 = 0;
			int output2 = 1;
			int output3 = 2;

			Dictionary<int, bot> bots = new Dictionary<int, bot>();
			while ((line = file.ReadLine()) != null)
			{
				var tokens = line.Split();
				if (tokens[0] == "bot")
				{
					bot temp = createBot(int.Parse(tokens[1]));


					int low = int.Parse(tokens[6]);
					if (tokens[5] == "output") low *= -1;
					if (!bots.ContainsKey(low))
					{
						bots.Add(low, createBot(low));
					}

					temp.lowOut = bots[low];

					int high = int.Parse(tokens[11]);
					if (tokens[10] == "output") high *= -1;
					if (!bots.ContainsKey(high))
					{
						bots.Add(high, createBot(high));
					}
					temp.highOut = bots[high];

					if (!bots.ContainsKey(temp.Serial))
					{
						bots.Add(temp.Serial, temp);
					}
					else
					{
						bots[temp.Serial].highOut = temp.highOut;
						bots[temp.Serial].lowOut = temp.lowOut;
					}


				}
				else
				{
					int serial = int.Parse(tokens[5]);
					if (!bots.ContainsKey(serial))
					{
						bots.Add(serial, createBot(serial));
					}

					bots[serial].vals.Add(int.Parse(tokens[1]), 0);

				}
			}


			while (notComplete(bots))
			{
				foreach (KeyValuePair<int, bot> x in bots)
				{
					if (x.Value.vals.Count() == 2 && !x.Value.hasGiven)
					{
						x.Value.highOut.vals.Add(x.Value.vals.Last().Key, 0);
						x.Value.lowOut.vals.Add(x.Value.vals.First().Key, 0);
						x.Value.hasGiven = true;
					}
				}
			}

			if (part1)
			{
				bot output = (from x in bots
							  where (x.Value.vals.First().Key == chip1 && x.Value.vals.Last().Key == chip2)
							  select x.Value
							  ).First();

				Console.WriteLine(output.Serial);
				Console.ReadKey();
			}
			else
			{
				bot[] output = (from x in bots
								where (x.Value.Serial == -(output1) || x.Value.Serial == -(output2) || x.Value.Serial == -(output3))
								select x.Value
							  ).ToArray();

				int out2 = 1;
				foreach (bot x in output)
				{
					out2 *= x.vals.First().Key;
				}
				Console.WriteLine(out2);
				Console.ReadKey();
			}
		}

		private static bool notComplete(Dictionary<int, bot> bots)
		{
			bool outp = false;
			foreach (KeyValuePair<int, bot> x in bots)
			{
				if (x.Value.vals.Count() != 2 && x.Value.Serial > 0)
				{
					outp = true;
					break;
				}
			}
			return outp;
		}

		private static bot createBot(int Serial)
		{
			return new bot(Serial);
		}
	}
}
