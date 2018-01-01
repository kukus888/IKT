using System;

namespace IKT
{
	public class mramor
	{
		public mramor ()
		{
		}
		public void Dungeon(promenne hodnoty) {
			bool zijes = true;
			bool cleandungeon = false;
			while (zijes == true || cleandungeon == false) {
				Console.Clear ();
			}
			hodnoty.mapa [hodnoty.charakterX, hodnoty.charakterY] = 0;//pokud je vycisten, je znicen navzdy...
			hodnoty.jenazivu = zijes;
		}
	}
}

