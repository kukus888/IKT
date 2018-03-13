using System;
using System.Threading;

namespace IKT
{
	public class souboj
	{
		public souboj ()
		{
		}
		public void bitva(string stojiciblok, bool exitgame, promenne hodnoty) {
			Random rnd = new Random ();
			grafika g = new grafika ();
			int pravdepodobnostmonstra = rnd.Next (0, 100);
			if (pravdepodobnostmonstra <= 10 && stojiciblok != "Vesnice" && stojiciblok != "Dungeon" && hodnoty.neviditelnost_pred_monstry == false) {
				int nahmonstrum = rnd.Next (0,hodnoty.monstra.GetLength(0)-1);//ZDE POZDEJI IMPLEMENTUJ SYSTEM EXP & LVL
				//Console.SetCursorPosition (1, hodnoty.vyskamapy + 2);
				//Console.WriteLine ("Potkal jsi příšeru jménem {0}! Budeš muset bojovat...", hodnoty.monstra[nahmonstrum,0]);
				//Console.SetCursorPosition (1, hodnoty.vyskamapy + 3);
				//Console.WriteLine ("Zmáčkni klávesu pro pokračování...");
				//Console.ReadKey ();

				bool utoceni = true;
				int volbautoceni = 0;
				int exppred = 0;
				int monstrumodolnostpred = 0;
				int monstrumodolnost = Int32.Parse(hodnoty.monstra [nahmonstrum, 2]);
				string[] moznosti = new string[] {
					"Zaútočit",
					"Bránit se",
					"Použít předmět - NEDOSTUPNÉ"
				};
				while (utoceni) {
					#region vykreslovani
					Console.BackgroundColor = ConsoleColor.Black;
					Console.ForegroundColor = ConsoleColor.White;
					Console.Clear ();
					Console.SetCursorPosition (2,1);
					Console.WriteLine ("{0} : Útok: {1}+{2}",hodnoty.charakter,hodnoty.sila, hodnoty.inventar[0,3]);
					g.HealthBar(hodnoty.odolnost, hodnoty.odolnostMax,2,2,20,true);
					Console.SetCursorPosition (2,3);
					Console.WriteLine ("{0} : {1}/{2}",hodnoty.monstra[nahmonstrum, 0],hodnoty.monstra[nahmonstrum,3], monstrumodolnost);
					Console.SetCursorPosition (2,4);
					Console.WriteLine ("   Útok / Životy");
					for (int a = 0; a <= moznosti.GetLength (0)-1; a++) {
						Console.SetCursorPosition (2,6+a);
						if (a == volbautoceni) {
							Console.Write ("> ");
						}
						Console.WriteLine (moznosti[a]);
					}
					#endregion
					ConsoleKeyInfo vstup = Console.ReadKey ();
					if (vstup.Key == ConsoleKey.Enter) {
						if (volbautoceni == 0) {//Zautocit
							Console.SetCursorPosition (2, 9);
							Console.WriteLine ("Útočíš na {0}...", hodnoty.monstra [nahmonstrum, 0]);
							Console.SetCursorPosition (2, 10);
							monstrumodolnostpred = monstrumodolnost;
							monstrumodolnost = monstrumodolnost - (rnd.Next (hodnoty.sila / 2, (hodnoty.sila-hodnoty.sila/3)) + Int32.Parse (hodnoty.inventar [0, 3]));
							Console.SetCursorPosition (2, 11);
							Console.WriteLine ("Ubral jsi nepříteli jménem {0} {1} životů!", hodnoty.monstra[nahmonstrum,0], monstrumodolnostpred - monstrumodolnost);
							hodnoty.inventar [0, 4] = (Int32.Parse (hodnoty.inventar [0, 4]) - 1).ToString ();
							if (Int32.Parse (hodnoty.inventar [0, 4]) <= 0 && hodnoty.inventar [0, 0] != "Zadna zbran") {//Rozbiti zbrane
								Console.SetCursorPosition (2, 12);
								Console.WriteLine ("Rozbila se ti zbran: {0}", hodnoty.inventar [0, 0]);
								hodnoty.inventar [0, 0] = "Zadna zbran";
								hodnoty.inventar [0, 1] = " ";
								hodnoty.inventar [0, 2] = "zbran";
								hodnoty.inventar [0, 3] = "0";
								hodnoty.inventar [0, 4] = "0";
							}
							if (monstrumodolnost <= 0) {//porazis monstrum
								Console.SetCursorPosition (2, 12);
								Console.WriteLine ("Vyhral jsi!");
								exppred = hodnoty.exp;
								hodnoty.exp = hodnoty.exp + rnd.Next (0, 10);
								Console.SetCursorPosition (2, 14);
								Console.WriteLine ("Bylo ti pricteno {0} EXP! Nyni mas {1} EXP!", hodnoty.exp - exppred, hodnoty.exp);
								utoceni = true;
								break;
							}
							Console.SetCursorPosition (2, 12);
							Console.WriteLine ("{0} útočí...", hodnoty.monstra [nahmonstrum, 0]);
							Console.SetCursorPosition (2, 13);
							int odolnostpred = hodnoty.odolnost;
							hodnoty.odolnost = hodnoty.odolnost - rnd.Next (Int32.Parse(hodnoty.monstra [nahmonstrum, 3]) / 3, Int32.Parse(hodnoty.monstra [nahmonstrum, 3])) + Int32.Parse (hodnoty.inventar [1, 3]);
							hodnoty.inventar [1, 4] = (Int32.Parse (hodnoty.inventar [1, 4]) - 1).ToString ();
							Console.WriteLine ("Nepřítel ti ubral {0} životů!", odolnostpred - hodnoty.odolnost);
							if (Int32.Parse (hodnoty.inventar [1, 4]) <= 0 && hodnoty.inventar [1, 0] != "Žádné brnění") {//Rozbiti brneni
								Console.SetCursorPosition(2,14);
								Console.WriteLine ("Rozbilo se ti brnění: {0}", hodnoty.inventar [1, 0]);
								hodnoty.inventar [1, 0] = "Žádné brnění";
								hodnoty.inventar [1, 1] = "";
								hodnoty.inventar [1, 2] = "brneni";
								hodnoty.inventar [1, 3] = "0";
								hodnoty.inventar [1, 4] = "0";
							}
							if (hodnoty.odolnost <= 0) {
								Console.SetCursorPosition (2, 15);
								Console.WriteLine ("{0} tě usmrtil... :( ", hodnoty.monstra [nahmonstrum, 0]);
								Console.SetCursorPosition (2, 16);
								Console.WriteLine ("Konec hry.");
								Console.ReadKey ();
								hodnoty.jenazivu = false;
								exitgame = true;
								utoceni = true;
								break;
							}
							Console.ReadKey ();
						}
						if (volbautoceni == 1) {//obrana
							Console.SetCursorPosition (2, 9);
							Console.WriteLine ("Snažíš se ubránit proti {0}...", hodnoty.monstra[nahmonstrum,0]);
							int obranarandom = rnd.Next (0, 10);
							if (obranarandom <= 7) {
								Console.SetCursorPosition (2, 10);
								Console.WriteLine ("Úspěšně ses ubránil...");
								int regenerace = rnd.Next (5, 10);
								Console.SetCursorPosition (2, 11);
								Console.WriteLine ("Regenerovaly se ti {0} bodů zdraví!", regenerace);
								hodnoty.odolnost = hodnoty.odolnost + regenerace;
								if (hodnoty.odolnost >= hodnoty.odolnostMax + 1) {
									hodnoty.odolnost = hodnoty.odolnostMax;
								}
							}
							if (obranarandom >= 8) {
								Console.SetCursorPosition (2, 10);
								Console.WriteLine ("Nedokázal ses ubránit!");
								Console.SetCursorPosition (2, 11);
								Console.WriteLine ("Nyní je na tahu {0}...", hodnoty.monstra[nahmonstrum,0]);
								Console.SetCursorPosition (2, 12);
								int odolnostpred = hodnoty.odolnost;
								hodnoty.odolnost = hodnoty.odolnost - rnd.Next (Int32.Parse(hodnoty.monstra[nahmonstrum,3]) / 3, Int32.Parse(hodnoty.monstra[nahmonstrum,3])) + Int32.Parse (hodnoty.inventar [1, 3]);
								hodnoty.inventar [1, 4] = (Int32.Parse (hodnoty.inventar [1, 4]) - 1).ToString ();
								Console.WriteLine ("Nepřítel tě lehce zranil! Ubral ti {0} životů...", odolnostpred - hodnoty.odolnost);
								if (Int32.Parse (hodnoty.inventar [1, 4]) <= 0 && hodnoty.inventar [1, 0] != "Žádné brnění") {//Rozbiti brneni
									Console.SetCursorPosition (2, 13);
									Console.WriteLine ("Rozbilo se ti brnění: {0}", hodnoty.inventar [1, 0]);
									hodnoty.inventar [1, 0] = "Žádné brnění";
									hodnoty.inventar [1, 1] = "";
									hodnoty.inventar [1, 2] = "brneni";
									hodnoty.inventar [1, 3] = "0";
									hodnoty.inventar [1, 4] = "0";
								}
								if (hodnoty.odolnost <= 0) {
									Console.SetCursorPosition (2, 14);
									Console.WriteLine ("{0} tě usmrtil... :( ", hodnoty.monstra [nahmonstrum, 0]);
									Console.SetCursorPosition (2, 15);
									Console.WriteLine ("Konec hry.");
									Console.ReadKey ();
									hodnoty.jenazivu = false;
									exitgame = true;
									utoceni = true;
									break;
								}
							}
							Console.ReadKey ();
						}
						if (volbautoceni == 2) {//Pouzit predmet

						}
					}
					if (vstup.Key == ConsoleKey.S) { volbautoceni++;}
					if (vstup.Key == ConsoleKey.W) { volbautoceni--;}
					if (volbautoceni >= moznosti.GetLength (0)) {
						volbautoceni = 0;
					}
					if (volbautoceni <= -1) {
						volbautoceni = moznosti.GetLength (0)-1;
					}
				}
			}
		}
	}
}
