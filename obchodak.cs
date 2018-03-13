using System;

namespace IKT
{
	public class obchodniclassa
	{
		public int obchodak(string[,,] obchod,string[,] inventar,int zlato,int odolnost)
		{
			int poziceKurzoruX = 0;
			int poziceKurzoruY = 0;
			int nejvetsisirka = 80;
			int nejvetsivyska = 24;
			Random rnd = new Random ();
			string systemOS = Environment.OSVersion.ToString ();
			if (systemOS.Contains ("Windows")) {
				nejvetsisirka = 80;
				nejvetsivyska = 24;
				poziceKurzoruX = 0;
				poziceKurzoruY = 0;
			}
			if (systemOS.Contains ("Unix")) {
				nejvetsisirka = Console.LargestWindowWidth;
				nejvetsivyska = Console.LargestWindowHeight;
				poziceKurzoruX = nejvetsisirka - 1;
				poziceKurzoruY = nejvetsivyska - 1;
			}

			bool uvnitrvesnice = true;
			string[] nazvymest = {
				"Bily prusmyk",
				"Jitrenka",
				"Ledohrad",
				"Morthal",
				"Riften",
				"Samota",
				"Vetrny zleb",
				"Vorarov",
				"Helgen"
			};
			string[] obchody = new string[] { "Hokynarstvi", "Kovarna", "Lekarna", "Doly", "Hospoda"};
			string nazevmesta = nazvymest [rnd.Next (0, 8)];
			int volbamesto = 0;
			while (uvnitrvesnice == true) {
				Console.Clear ();
				Console.SetCursorPosition (2, 1);
				Console.Write ("Vitej v meste jmenem {0}", nazevmesta);
				for (int x = 0; x <= 4; x++) {
					Console.SetCursorPosition (2, x + 2);
					if (x == volbamesto) {
						Console.Write ("> ");
					}
					Console.Write (obchody [x]);
				}
				int mestovysvetlivkyoffset = 1;
				Console.SetCursorPosition (2,7+mestovysvetlivkyoffset);
				Console.Write ("[E] - Inventar");
				Console.SetCursorPosition (2,8+mestovysvetlivkyoffset);
				Console.Write ("[Enter] - Projit si obchod");
				Console.SetCursorPosition (2,9+mestovysvetlivkyoffset);
				Console.Write ("[ESC] - Zpet");
				Console.SetCursorPosition (2,10+mestovysvetlivkyoffset);
				Console.Write ("[T] - Pregenerovat nabidky v obchode");
				Console.SetCursorPosition (poziceKurzoruX, poziceKurzoruY);
				ConsoleKeyInfo vesnicevolba = Console.ReadKey ();
				if (vesnicevolba.Key == ConsoleKey.Escape || vesnicevolba.Key == ConsoleKey.E) {
					uvnitrvesnice = false;
				}
				if (vesnicevolba.Key == ConsoleKey.W) {
					volbamesto--;
				}
				if (vesnicevolba.Key == ConsoleKey.S) {
					volbamesto++;
				}
				if (vesnicevolba.Key == ConsoleKey.T) {//regeneruje obchod
					//new generaceobchodu(obchod, hodnoty);
				}
				if (volbamesto <= -1) { volbamesto = (obchody.Length-1); }
				if (volbamesto >= (obchody.Length)) { volbamesto = 0; }
				if (vesnicevolba.Key == ConsoleKey.E) {
					new tridainventar ();
					//new tridainventar (inventar, zlato, odolnost);
				}
				if (vesnicevolba.Key == ConsoleKey.Enter) {
					int obchodvolba = 0;
					bool vobchode = true;
					while (vobchode) {
						Console.Clear ();
						Console.SetCursorPosition (2, 1);
						Console.Write (obchody [volbamesto]);
						for (int i = 0; i <= 9; i++) {
							Console.SetCursorPosition (2, i + 2);
							if (i == obchodvolba) {
								Console.Write ("> ");
							}
							if (obchod [volbamesto, i, 0] == "0") {
								Console.Write ("nic");
							} else {
								Console.Write ("{0} - {1} zlata", obchod [volbamesto, i, 0], obchod [volbamesto, i, 5]);
							}
						}
						if (obchod [volbamesto, obchodvolba, 0] == "0") {
						} else {
							Console.SetCursorPosition (2, 13);
							Console.Write (obchod [volbamesto, obchodvolba, 1]);
							if (obchod [volbamesto, obchodvolba, 2] == "zbran") {
								Console.SetCursorPosition (2, 14);
								Console.Write ("Attack: {0}", obchod [volbamesto, obchodvolba, 3]);
								Console.SetCursorPosition (2, 15);
								Console.Write ("Výdrž:", obchod [volbamesto, obchodvolba, 4]);
							}
							if (obchod [volbamesto, obchodvolba, 2] == "brneni") {
								Console.SetCursorPosition (2, 14);
								Console.Write ("Obrana: {0}", obchod [volbamesto, obchodvolba, 3]);
								Console.SetCursorPosition (2, 15);
								Console.Write ("Výdrž:", obchod [volbamesto, obchodvolba, 4]);
							}
							if (obchod [volbamesto, obchodvolba, 2] == "jidlo") {
								Console.SetCursorPosition (2, 14);
								Console.Write ("Doplni {0} zivotu.", obchod [volbamesto, obchodvolba, 3]);
							}
							if (obchod [volbamesto, obchodvolba, 2] == "lektvar-zivot") {
								Console.SetCursorPosition (2, 14);
								Console.Write ("Doplni {0} zivotu.", obchod [volbamesto, obchodvolba, 3]);
							}
							if (obchod [volbamesto, obchodvolba, 2] == "lektvar-otrava") {
								Console.SetCursorPosition (2, 14);
								Console.Write ("Ubere {0} zivotu.", obchod [volbamesto, obchodvolba, 3]);
							}
							if (obchod [volbamesto, obchodvolba, 2] == "lektvar-neviditelnost") {
								Console.SetCursorPosition (2, 14);
								Console.Write ("Schova te pred nepriteli na {0} tahu.", obchod [volbamesto, obchodvolba, 3]);
							}
							Console.SetCursorPosition (2, 15);
							Console.Write ("{0} kusy", obchod [volbamesto, obchodvolba, 4]);
						}
						Console.SetCursorPosition (poziceKurzoruX, poziceKurzoruY);
						ConsoleKeyInfo vstupobchod = Console.ReadKey ();
						if (vstupobchod.Key == ConsoleKey.W) {
							obchodvolba--;
						}
						if (vstupobchod.Key == ConsoleKey.S) {
							obchodvolba++;
						}
						if (vstupobchod.Key == ConsoleKey.Enter) { //koupi ho
							if (zlato >= Int32.Parse (obchod [volbamesto, obchodvolba, 5])) { //pokud ma dostatek penez
								for (int i = 0; i <= 10; i++) {
									if (inventar [i, 0] == obchod [volbamesto, obchodvolba, 0] && inventar [i, 2] == obchod [volbamesto, obchodvolba, 2]) { // je uz v inv
										inventar [i, 4] = (Int32.Parse (inventar [i, 4]) + Int32.Parse (obchod [volbamesto, obchodvolba, 4])).ToString ();
										zlato = zlato - Int32.Parse (obchod [volbamesto, obchodvolba, 5]);
										for (int y = 0; y <= 5; y++) {
											obchod [volbamesto, obchodvolba, y] = "0";
										}
										break;
									}
									if (inventar [i, 0] == "0") {//najde volne misto
										inventar [i, 0] = obchod [volbamesto, obchodvolba, 0];
										inventar [i, 1] = obchod [volbamesto, obchodvolba, 1];
										inventar [i, 2] = obchod [volbamesto, obchodvolba, 2];
										inventar [i, 3] = obchod [volbamesto, obchodvolba, 3];
										inventar [i, 4] = obchod [volbamesto, obchodvolba, 4];
										inventar [i, 5] = obchod [volbamesto, obchodvolba, 5];

										zlato = zlato - Int32.Parse (obchod [volbamesto, obchodvolba, 5]);
										for (int y = 0; y <= 5; y++) {
											obchod [volbamesto, obchodvolba, y] = "0";
										}
										break;
									}
								}
							}
						}
						if (vstupobchod.Key == ConsoleKey.Escape || vstupobchod.Key == ConsoleKey.E) {
							vobchode = false;
						}
						if (obchodvolba <= -1) {
							obchodvolba = 9;
						}
						if (obchodvolba >= 10) {
							obchodvolba = 0;
						}
					}
				}
			}
			return zlato;
		}
	}
}