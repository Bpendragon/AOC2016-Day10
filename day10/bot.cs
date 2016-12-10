using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace day10
{
	class bot
	{
		public int Serial { get; set; }
		public SortedList<int, int> vals { get; set; }
		public bot lowOut { get; set; }
		public bot highOut { get; set; }
		public bool hasGiven { get; set; }



		public bot(int Serial)
		{
			this.Serial = Serial;
			vals = new SortedList<int, int>();
			this.hasGiven = false;
		}
	}
}
