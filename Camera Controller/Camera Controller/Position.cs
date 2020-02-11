using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coordinates_Controller
{
	[Serializable]
	public class Position
	{
		public float x,y,z;
		public string name;

		public override string ToString()
		{
			return this.name;
		}
	}
}
