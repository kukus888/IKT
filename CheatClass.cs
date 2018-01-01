using System;

namespace IKT
{
	public class CheatClass
	{
		public CheatClass (string[,] inventar, int zlato, int odolnost, bool neviditelnost_pred_monstry, int delka_neviditelnosti)
		{
			int poziceKurzoruX = 0; //CELY TENTO DOKUMENT SE NEPOUZIVA!!! POUZE NA VYVOJ
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

			bool incheatmode = true;
			while (incheatmode = true) {
				Console.Clear ();
				Console.SetCursorPosition (2,1);
				Console.Write ("Zlato: {0}  [E] +10  [R] +100",zlato);
				Console.SetCursorPosition (2,2);
				Console.Write ("Zivoty: {0} [F] +10  [G] +100",odolnost);
				Console.SetCursorPosition (2,3);
				Console.Write ("Neviditelnost pred monstry: {0}",neviditelnost_pred_monstry);
				Console.SetCursorPosition (2,4);
				Console.Write ("Delka neviditelnosti pred monstry: {0}",delka_neviditelnosti);
				Console.SetCursorPosition (2,5);
				Console.Write (" [X] aktivovat neviditelnost na 100 tahu");
				Console.SetCursorPosition (2,7);
				Console.Write (" [ESC] Zpet");

				Console.SetCursorPosition (poziceKurzoruX,poziceKurzoruY);
				ConsoleKeyInfo vstup = Console.ReadKey ();
				if (vstup.Key == ConsoleKey.Escape) { incheatmode = false; break; }
				if (vstup.Key == ConsoleKey.E) { zlato = zlato + 10; }
				if (vstup.Key == ConsoleKey.F) { odolnost = odolnost + 10; }
				if (vstup.Key == ConsoleKey.R) { zlato = zlato + 100; }
				if (vstup.Key == ConsoleKey.G) { odolnost = odolnost + 100; }
				if (vstup.Key == ConsoleKey.X) { neviditelnost_pred_monstry = true; delka_neviditelnosti = 100; }
			}

		}
	}
}

