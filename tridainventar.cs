using System;

namespace IKT
{
	public class tridainventar
	{
		public tridainventar ()
		{
			
		}
		public void action(promenne hodnoty) {
			if (hodnoty.inventar == null) {
				return;
			}
			int poziceKurzoruX = 0;
			int poziceKurzoruY = 0;
			int nejvetsisirka = 80;
			int nejvetsivyska = 24;
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

			bool otevrenyinventar = true;
			int volbainventar = 0;
			while (otevrenyinventar) {
				Console.Clear ();
				Console.SetCursorPosition (1, 1);
				for (int i = 0; i <= 10; i++) {
					if (volbainventar == i) {
						Console.SetCursorPosition (2, i + 2);
						Console.Write ("> ");
						if (hodnoty.inventar [i, 0] == "0") {
							Console.Write ("Prazdne misto");
						} else {
							Console.Write ("{0} - {1}x",hodnoty.inventar [i, 0],hodnoty.inventar [i, 4]);
						}
						if (hodnoty.inventar [volbainventar, 1] != "0") {
							Console.SetCursorPosition (2, 14);
							Console.Write (hodnoty.inventar [volbainventar, 1]);
						}
						Console.SetCursorPosition (2, 15);
						if (hodnoty.inventar [volbainventar, 2] == "zbran") {
							Console.Write ("Poskozeni: ");
							Console.Write (hodnoty.inventar [volbainventar, 3]);
						}
						if (hodnoty.inventar [volbainventar, 2] == "brneni") {
							Console.Write ("Odolnost: ");
							Console.Write (hodnoty.inventar [volbainventar, 3]);
						}
						if (hodnoty.inventar [volbainventar, 2] == "jidlo") {
							Console.Write ("Doplni zivotu: ");
							Console.Write (hodnoty.inventar [volbainventar, 3]);
						}
						if (hodnoty.inventar [volbainventar, 2] == "lektvar-zivot") {
							Console.Write ("Doplni zivotu: ");
							Console.Write (hodnoty.inventar [volbainventar, 3]);
						}
						if (hodnoty.inventar [volbainventar, 2] == "lektvar-otrava") {
							Console.Write ("Ubere zivotu: ");
							Console.Write (hodnoty.inventar [volbainventar, 3]);
						}
						if (hodnoty.inventar [volbainventar, 2] == "lektvar-neviditelnost") {
							Console.Write ("Na {0} tahu te skryje pred veskerou haveti.",hodnoty.inventar [volbainventar, 3]);
						}
						Console.SetCursorPosition (2, 16);
						if (hodnoty.inventar [volbainventar, 2] == "zbran") {
							Console.Write ("Vydrz: ");
							Console.Write (hodnoty.inventar [volbainventar, 4]);
						}
						if (hodnoty.inventar [volbainventar, 2] == "brneni") {
							Console.Write ("Vydrz: ");
							Console.Write (hodnoty.inventar [volbainventar, 4]);
						}
						if (hodnoty.inventar [volbainventar, 2] == "lektvar-zivot" || hodnoty.inventar [volbainventar, 2] == "lektvar-otrava" || hodnoty.inventar [volbainventar, 2] == "jidlo" || hodnoty.inventar [volbainventar, 2] == "větve" || hodnoty.inventar [volbainventar, 2] == "Most") {
							Console.Write ("V zasobe: ");
							Console.Write (hodnoty.inventar [volbainventar, 4]);
						}
					}
					if (volbainventar != i) {
						if (hodnoty.inventar [i, 0] == "0") {
							Console.SetCursorPosition (2, i + 2);
							Console.Write ("-");
						} else {
							Console.SetCursorPosition (2, i + 2);
							Console.Write ("{0} - {1}x",hodnoty.inventar [i, 0],hodnoty.inventar [i, 4]);
						}
					}
				}
				Console.SetCursorPosition (2, 18);
				Console.Write ("[C] - Vyrábět");
				Console.SetCursorPosition (2, 1);
				Console.Write ("Mas {0} zlataku.", hodnoty.zlato);
				Console.SetCursorPosition (poziceKurzoruX, poziceKurzoruY);
				ConsoleKeyInfo vstupinventar = Console.ReadKey ();
				if (vstupinventar.Key == ConsoleKey.W) {
					volbainventar--;
				}
				if (vstupinventar.Key == ConsoleKey.S) {
					volbainventar++;
				}
				if (vstupinventar.Key == ConsoleKey.Enter) { //menu pro item
					if (hodnoty.inventar [volbainventar, 2] == "zbran") {//zjisti typ zbrane a nabidne moznosti
						//NWM
					}
					if (hodnoty.inventar [volbainventar, 2] == "brneni") {
						//NWM
					}
					if (hodnoty.inventar [volbainventar, 2] == "jidlo") {
						bool invpouzit = true;
						int volbainvpouzit = 0;
						while (invpouzit) {
							if (Int32.Parse (hodnoty.inventar [volbainventar, 4]) <= 0) {//Kdyz dojde item
								Console.Clear ();
								for (int i = 0; i <= 9; i++) {
									hodnoty.inventar [volbainventar, i] = "0";
								}
								invpouzit = false;
								break;
							}
							Console.Clear ();
							Console.SetCursorPosition (2, 1);
							Console.Write (hodnoty.inventar [volbainventar, 0]);
							Console.SetCursorPosition (2, 2);
							Console.Write ("Doplni {0} zivotu.", hodnoty.inventar [volbainventar, 3]);
							Console.SetCursorPosition (2, 3);
							Console.Write ("Mas jich {0} v zasobe.", hodnoty.inventar [volbainventar, 4]);
							Console.SetCursorPosition (2, 4);
							Console.Write ("Mas {0} zivotu.", hodnoty.odolnost);
							if (volbainvpouzit == 0) {
								Console.SetCursorPosition (2, 5);
								Console.Write ("> Snist");
								Console.SetCursorPosition (2, 6);
								Console.Write ("Jit zpet");
							}
							if (volbainvpouzit == 1) {
								Console.SetCursorPosition (2, 5);
								Console.Write ("Snist");
								Console.SetCursorPosition (2, 6);
								Console.Write ("> Jit zpet");
							}
							Console.SetCursorPosition (poziceKurzoruX, poziceKurzoruY);
							ConsoleKeyInfo vstupinvpouzit = Console.ReadKey ();
							if (vstupinvpouzit.Key == ConsoleKey.W) {
								volbainvpouzit--;
							}
							if (vstupinvpouzit.Key == ConsoleKey.S) {
								volbainvpouzit++;
							}
							if (vstupinvpouzit.Key == ConsoleKey.Escape) {
								invpouzit = false;
							}
							if (vstupinvpouzit.Key == ConsoleKey.Enter && volbainvpouzit == 0) {//Snist
								hodnoty.odolnost = hodnoty.odolnost + Int32.Parse (hodnoty.inventar [volbainventar, 3]);
								hodnoty.inventar [volbainventar, 4] = (Int32.Parse (hodnoty.inventar [volbainventar, 4]) - 1).ToString ();
								if (hodnoty.odolnost >= hodnoty.odolnostMax + 1) {
									hodnoty.odolnost = hodnoty.odolnostMax;
								}
							}
							if (vstupinvpouzit.Key == ConsoleKey.Enter && volbainvpouzit == 1) {
								invpouzit = false;
							}//zpet
							if (volbainvpouzit <= -1) {
								volbainvpouzit = 1;
							}
							if (volbainvpouzit >= 2) {
								volbainvpouzit = 0;
							}
						}
					}
					if (hodnoty.inventar [volbainventar, 2] == "lektvar-zivot") {
						bool invpouzit = true;
						int volbainvpouzit = 0;
						while (invpouzit) {
							if (Int32.Parse (hodnoty.inventar [volbainventar, 4]) <= 0) {//Kdyz dojde item
								Console.Clear ();
								for (int i = 0; i <= 9; i++) {
									hodnoty.inventar [volbainventar, i] = "0";
								}
								invpouzit = false;
								break;
							}
							Console.Clear ();
							Console.SetCursorPosition (2, 1);
							Console.Write (hodnoty.inventar [volbainventar, 0]);
							Console.SetCursorPosition (2, 2);
							Console.Write ("Doplni {0} zivotu.", hodnoty.inventar [volbainventar, 3]);
							Console.SetCursorPosition (2, 3);
							Console.Write ("Mas jich {0} v zasobe.", hodnoty.inventar [volbainventar, 4]);
							Console.SetCursorPosition (2, 4);
							Console.Write ("Mas {0} zivotu.", hodnoty.odolnost);
							if (volbainvpouzit == 0) {
								Console.SetCursorPosition (2, 5);
								Console.Write ("> Vypit");
								Console.SetCursorPosition (2, 6);
								Console.Write ("Jit zpet");
							}
							if (volbainvpouzit == 1) {
								Console.SetCursorPosition (2, 5);
								Console.Write ("Vypit");
								Console.SetCursorPosition (2, 6);
								Console.Write ("> Jit zpet");
							}
							Console.SetCursorPosition (poziceKurzoruX, poziceKurzoruY);
							ConsoleKeyInfo vstupinvpouzit = Console.ReadKey ();
							if (vstupinvpouzit.Key == ConsoleKey.W) {
								volbainvpouzit--;
							}
							if (vstupinvpouzit.Key == ConsoleKey.S) {
								volbainvpouzit++;
							}
							if (vstupinvpouzit.Key == ConsoleKey.Escape) {
								invpouzit = false;
							}
							if (vstupinvpouzit.Key == ConsoleKey.Enter && volbainvpouzit == 0) {//vypit
								hodnoty.odolnost = hodnoty.odolnost + Int32.Parse (hodnoty.inventar [volbainventar, 3]);
								hodnoty.inventar [volbainventar, 4] = (Int32.Parse (hodnoty.inventar [volbainventar, 4]) - 1).ToString ();
								if (hodnoty.odolnost >= hodnoty.odolnostMax + 1) {
									hodnoty.odolnost = hodnoty.odolnostMax;
								}
							}
							if (vstupinvpouzit.Key == ConsoleKey.Enter && volbainvpouzit == 1) {
								invpouzit = false;
							}//zpet
							if (volbainvpouzit <= -1) {
								volbainvpouzit = 1;
							}
							if (volbainvpouzit >= 2) {
								volbainvpouzit = 0;
							}
						}
					}
					if (hodnoty.inventar [volbainventar, 2] == "lektvar-otrava") {
						bool invpouzit = true;
						int volbainvpouzit = 0;
						while (invpouzit) {
							if (Int32.Parse (hodnoty.inventar [volbainventar, 4]) <= 0) {//Kdyz dojde item
								Console.Clear ();
								for (int i = 0; i <= 9; i++) {
									hodnoty.inventar [volbainventar, i] = "0";
								}
								invpouzit = false;
								break;
							}
							Console.Clear ();
							Console.SetCursorPosition (2, 1);
							Console.Write (hodnoty.inventar [volbainventar, 0]);
							Console.SetCursorPosition (2, 2);
							Console.Write ("Doplni {0} zivotu.", hodnoty.inventar [volbainventar, 3]);
							Console.SetCursorPosition (2, 3);
							Console.Write ("Mas jich {0} v zasobe.", hodnoty.inventar [volbainventar, 4]);
							Console.SetCursorPosition (2, 4);
							Console.Write ("Mas {0} zivotu.", hodnoty.odolnost);
							if (volbainvpouzit == 0) {
								Console.SetCursorPosition (2, 5);
								Console.Write ("> Vypit");
								Console.SetCursorPosition (2, 6);
								Console.Write ("Aplikovat");
								Console.SetCursorPosition (2, 7);
								Console.Write ("Jit zpet");
							}
							if (volbainvpouzit == 1) {
								Console.SetCursorPosition (2, 5);
								Console.Write ("Vypit");
								Console.SetCursorPosition (2, 6);
								Console.Write ("> Aplikovat");
								Console.SetCursorPosition (2, 7);
								Console.Write ("Jit zpet");
							}
							if (volbainvpouzit == 2) {
								Console.SetCursorPosition (2, 5);
								Console.Write ("Vypit");
								Console.SetCursorPosition (2, 6);
								Console.Write ("Aplikovat");
								Console.SetCursorPosition (2, 7);
								Console.Write ("> Jit zpet");
							}
							Console.SetCursorPosition (poziceKurzoruX, poziceKurzoruY);
							ConsoleKeyInfo vstupinvpouzit = Console.ReadKey ();
							if (vstupinvpouzit.Key == ConsoleKey.W) {
								volbainvpouzit--;
							}
							if (vstupinvpouzit.Key == ConsoleKey.S) {
								volbainvpouzit++;
							}
							if (vstupinvpouzit.Key == ConsoleKey.Escape) {
								invpouzit = false;
							}
							if (vstupinvpouzit.Key == ConsoleKey.Enter && volbainvpouzit == 0) {//vypit
								hodnoty.odolnost = hodnoty.odolnost - Int32.Parse (hodnoty.inventar [volbainventar, 3]);
								hodnoty.inventar [volbainventar, 4] = (Int32.Parse (hodnoty.inventar [volbainventar, 4]) - 1).ToString ();
							}
							if (vstupinvpouzit.Key == ConsoleKey.Enter && volbainvpouzit == 0) {//aplikovat
								hodnoty.inventar [0, 3] = (Int32.Parse (hodnoty.inventar [0, 3]) + Int32.Parse (hodnoty.inventar [volbainventar, 3])).ToString ();
								hodnoty.inventar [volbainventar, 4] = (Int32.Parse (hodnoty.inventar [volbainventar, 4]) - 1).ToString ();
							}
							if (vstupinvpouzit.Key == ConsoleKey.Enter && volbainvpouzit == 1) {
								invpouzit = false;
							}//zpet
							if (volbainvpouzit <= -1) {
								volbainvpouzit = 1;
							}
							if (volbainvpouzit >= 3) {
								volbainvpouzit = 0;
							}
						}
					}
					if (hodnoty.inventar [volbainventar, 2] == "lektvar-neviditelnost") {
						bool invpouzit = true;
						int volbainvpouzit = 0;
						while (invpouzit) {
							if (Int32.Parse (hodnoty.inventar [volbainventar, 4]) <= 0) {//Kdyz dojde item
								Console.Clear ();
								for (int i = 0; i <= 9; i++) {
									hodnoty.inventar [volbainventar, i] = "0";
								}
								invpouzit = false;
								break;
							}
							Console.Clear ();
							Console.SetCursorPosition (2, 1);
							Console.Write (hodnoty.inventar [volbainventar, 0]);
							Console.SetCursorPosition (2, 2);
							Console.Write ("Skryje te pred moby na {0} tahu.", hodnoty.inventar [volbainventar, 3]);
							Console.SetCursorPosition (2, 3);
							Console.Write ("Mas jich {0} v zasobe.", hodnoty.inventar [volbainventar, 4]);
							Console.SetCursorPosition (2, 4);
							Console.Write ("Momentalne jses skryty na {0} tahu.", hodnoty.delka_neviditelnosti);
							if (volbainvpouzit == 0) {
								Console.SetCursorPosition (2, 5);
								Console.Write ("> Pouzit");
								Console.SetCursorPosition (2, 6);
								Console.Write ("Jit zpet");
							}
							if (volbainvpouzit == 1) {
								Console.SetCursorPosition (2, 5);
								Console.Write ("Pouzit");
								Console.SetCursorPosition (2, 6);
								Console.Write ("> Jit zpet");
							}
							Console.SetCursorPosition (poziceKurzoruX, poziceKurzoruY);
							ConsoleKeyInfo vstupinvpouzit = Console.ReadKey ();
							if (vstupinvpouzit.Key == ConsoleKey.W) {
								volbainvpouzit--;
							}
							if (vstupinvpouzit.Key == ConsoleKey.S) {
								volbainvpouzit++;
							}
							if (vstupinvpouzit.Key == ConsoleKey.Escape) {
								invpouzit = false;
							}
							if (vstupinvpouzit.Key == ConsoleKey.Enter && volbainvpouzit == 0) {//Pouzit
								hodnoty.delka_neviditelnosti = hodnoty.delka_neviditelnosti + Int32.Parse (hodnoty.inventar [volbainventar, 3]);
								hodnoty.inventar [volbainventar, 4] = (Int32.Parse (hodnoty.inventar [volbainventar, 4]) - 1).ToString ();
							}
							if (vstupinvpouzit.Key == ConsoleKey.Enter && volbainvpouzit == 1) {
								invpouzit = false;
							}//zpet
							if (volbainvpouzit <= -1) {
								volbainvpouzit = 1;
							}
							if (volbainvpouzit >= 2) {
								volbainvpouzit = 0;
							}
						}
					}
					if (hodnoty.inventar [volbainventar, 2] == "Most") {
						bool invpouzit = true;
						int volbainvpouzit = 0;
						while (invpouzit) {
							if (Int32.Parse (hodnoty.inventar [volbainventar, 4]) <= 0) {//Kdyz dojde item
								Console.Clear ();
								for (int i = 0; i <= 9; i++) {
									hodnoty.inventar [volbainventar, i] = "0";
								}
								invpouzit = false;
								break;
							}
							Console.Clear ();
							Console.SetCursorPosition (2, 1);
							Console.Write (hodnoty.inventar [volbainventar, 0]);
							Console.SetCursorPosition (2, 2);
							Console.Write (hodnoty.inventar [volbainventar, 1]);
							Console.SetCursorPosition (2, 3);
							Console.Write ("Mas jich {0} v zasobe.", hodnoty.inventar [volbainventar, 4]);
							if (volbainvpouzit == 0) {
								Console.SetCursorPosition (2, 5);
								Console.Write ("> Postavit");
								Console.SetCursorPosition (2, 6);
								Console.Write ("Jit zpet");
							}
							if (volbainvpouzit == 1) {
								Console.SetCursorPosition (2, 5);
								Console.Write ("Postavit");
								Console.SetCursorPosition (2, 6);
								Console.Write ("> Jit zpet");
							}
							Console.SetCursorPosition (poziceKurzoruX, poziceKurzoruY);
							ConsoleKeyInfo vstupinvpouzit = Console.ReadKey ();
							if (vstupinvpouzit.Key == ConsoleKey.W) {
								volbainvpouzit--;
							}
							if (vstupinvpouzit.Key == ConsoleKey.S) {
								volbainvpouzit++;
							}
							if (vstupinvpouzit.Key == ConsoleKey.Escape) {
								invpouzit = false;
							}
							if (vstupinvpouzit.Key == ConsoleKey.Enter && volbainvpouzit == 0) {//Postavit
								bool placedmost = false;
								bool pokracovatmost = true;
								try {
									if (hodnoty.mapa [hodnoty.charakterX - 1, hodnoty.charakterY].Type == "River" && pokracovatmost == true) {
										hodnoty.mapa [hodnoty.charakterX - 1, hodnoty.charakterY].Type = "Bridge";
										placedmost = true;
										pokracovatmost = false;
									}
								} catch (System.IndexOutOfRangeException) {
								}
								try {
									if (hodnoty.mapa [hodnoty.charakterX + 1, hodnoty.charakterY].Type == "River" && pokracovatmost == true) {
										hodnoty.mapa [hodnoty.charakterX + 1, hodnoty.charakterY].Type = "Bridge";
										placedmost = true;
										pokracovatmost = false;
									}
								} catch (System.IndexOutOfRangeException) {
								}
								try {
									if (hodnoty.mapa [hodnoty.charakterX, hodnoty.charakterY - 1].Type == "River" && pokracovatmost == true) {
										hodnoty.mapa [hodnoty.charakterX, hodnoty.charakterY - 1].Type = "Bridge";
										placedmost = true;
										pokracovatmost = false;
									}
								} catch (System.IndexOutOfRangeException) {
								}
								try {
									if (hodnoty.mapa [hodnoty.charakterX, hodnoty.charakterY + 1].Type == "River" && pokracovatmost == true) {
										hodnoty.mapa [hodnoty.charakterX, hodnoty.charakterY + 1].Type = "Bridge";
										placedmost = true;
										pokracovatmost = false;
									}
								} catch (System.IndexOutOfRangeException) {
								}
								if (placedmost == true) {
									hodnoty.inventar [volbainventar, 4] = (Int32.Parse (hodnoty.inventar [volbainventar, 4]) - 1).ToString ();//odecte most z inv
								}
							}
							if (vstupinvpouzit.Key == ConsoleKey.Enter && volbainvpouzit == 1) {
								invpouzit = false;
							}//zpet
							if (volbainvpouzit <= -1) {
								volbainvpouzit = 1;
							}
							if (volbainvpouzit >= 2) {
								volbainvpouzit = 0;
							}
						}
					}
					if (hodnoty.inventar [volbainventar, 2] == "0") { /*Prazdny slot*/
					}
				}
				if (vstupinventar.Key == ConsoleKey.C) {//Vyrobit
					int volbavyroby = 0;
					bool vyrabet = true;
					while (vyrabet) {
						Console.Clear ();
						for (int i = 0; i <= hodnoty.craft.GetLength (0)-1; i++) {
							Console.SetCursorPosition (2, i + 1);
							if (volbavyroby == i) {
								Console.Write ("> ");
							}
							Console.Write ("{0} [{1}x {2}]",hodnoty.craft [i, 0], hodnoty.craft[i,7], hodnoty.craft[i,6]);
						}
						ConsoleKeyInfo vstupvyroba = Console.ReadKey ();
						if (vstupvyroba.Key == ConsoleKey.E || vstupvyroba.Key == ConsoleKey.Escape) { vyrabet = false; }
						if (vstupvyroba.Key == ConsoleKey.W) { volbavyroby--; }
						if (vstupvyroba.Key == ConsoleKey.S) { volbavyroby++; }
						if (vstupvyroba.Key == ConsoleKey.Enter) {//vyrobi
							bool exitvyroba = false;
							for (int z = 0; z <= hodnoty.inventar.GetLength (0)-1; z++) {
								if (hodnoty.inventar [z, 2] == hodnoty.craft [volbavyroby, 6] && Int32.Parse(hodnoty.inventar [z, 4]) >= Int32.Parse(hodnoty.craft [volbavyroby, 7])) {
									for (int i = 0; i <= hodnoty.inventar.GetLength(0)-1; i++) {
										if (hodnoty.inventar [i, 2] == hodnoty.craft [volbavyroby, 2] && hodnoty.inventar [i, 0] == hodnoty.craft [volbavyroby, 0]  && hodnoty.inventar [i, 3] == hodnoty.craft [volbavyroby, 3]) { // je uz v inv
											hodnoty.inventar [i, 4] = (Int32.Parse (hodnoty.inventar [i, 4]) + 1).ToString ();
											exitvyroba = true;
											hodnoty.inventar [z, 4] = (Int32.Parse (hodnoty.inventar [z, 4]) - Int32.Parse (hodnoty.craft [volbavyroby, 7])).ToString ();
											break;
										}
										if (hodnoty.inventar [i, 0] == "0") {//najde volne misto
											hodnoty.inventar [i, 0] = hodnoty.craft [volbavyroby, 0];
											hodnoty.inventar [i, 1] = hodnoty.craft [volbavyroby, 1];
											hodnoty.inventar [i, 2] = hodnoty.craft [volbavyroby, 2];
											hodnoty.inventar [i, 3] = hodnoty.craft [volbavyroby, 3];
											hodnoty.inventar [i, 4] = (Int32.Parse(hodnoty.craft [volbavyroby, 4])+Int32.Parse(hodnoty.inventar[i,4])).ToString();
											hodnoty.inventar [i, 5] = hodnoty.craft [volbavyroby, 5];
											hodnoty.inventar [z, 4] = (Int32.Parse (hodnoty.inventar [z, 4]) - Int32.Parse (hodnoty.craft [volbavyroby, 7])).ToString ();
											exitvyroba = true;
											break;
										}
									}
								}
								if (exitvyroba == true) { break;}
							}
						}
						if (volbavyroby >= hodnoty.craft.GetLength (0)) { volbavyroby = hodnoty.craft.GetLength (0) - 1; }
						if (volbavyroby <= -1) { volbavyroby = 0; }
					}
				}
				if (volbainventar <= -1) {
					volbainventar = hodnoty.inventar.GetLength(0)-1;
				}
				if (volbainventar >= hodnoty.inventar.GetLength(0)) {
					volbainventar = 0;
				}
				if (vstupinventar.Key == ConsoleKey.Escape || vstupinventar.Key == ConsoleKey.E) {
					otevrenyinventar = false;
				}
			}
		}
	}
}

