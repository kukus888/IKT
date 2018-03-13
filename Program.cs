using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace IKT
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Console.CursorVisible = false;

			grafika map = new grafika();
			promenne hodnoty = new promenne();

			int poziceKurzoruX = 0;
			int poziceKurzoruY = 0;
			int nejvetsisirka = 80;
			int nejvetsivyska = 24;
			string systemOS = Environment.OSVersion.ToString ();
			if(systemOS.Contains("Windows")) {
				nejvetsisirka = 80;
				nejvetsivyska = 24;
				poziceKurzoruX = 0;
				poziceKurzoruY = 0;
			}
			if(systemOS.Contains("Unix")) {
				nejvetsisirka = Console.LargestWindowWidth;
				nejvetsivyska = Console.LargestWindowHeight;
				poziceKurzoruX = nejvetsisirka-1;
				poziceKurzoruY = nejvetsivyska-1;
			}
			string[] volbymenu = new string[] {
				"Hrát",
				"Editor mapy",
				"Achievementy",
				"Nastavení",
				"Konec"
			};
			hodnoty.RP = null;
			hodnoty.RP = hodnoty.RP_basic;
			int volba = 0;
			while (true) {
				Console.BackgroundColor = ConsoleColor.Blue;
				Console.ForegroundColor = ConsoleColor.White;
				Console.Clear ();
				Console.SetCursorPosition (2, 1);
				Console.WriteLine ("IKT v 0.5");
				for (int i = 0; i <= volbymenu.GetLength (0)-1; i++) {
					Console.SetCursorPosition (2, 3+i);
					if (i == volba) {
						Console.Write ("> ");
					}
					Console.Write (volbymenu[i]);
				}
				Console.SetCursorPosition (poziceKurzoruX,poziceKurzoruY);
				ConsoleKeyInfo vstup = Console.ReadKey ();
				if (vstup.Key == ConsoleKey.W) {
					volba--;
				}
				if (vstup.Key == ConsoleKey.S) {
					volba++;
				}
				if (vstup.Key == ConsoleKey.Enter ) {
					if (volba == 0) {
						//HRA
						Console.Clear();
						Console.SetCursorPosition (nejvetsisirka/2-16,nejvetsivyska/2-2);
						Console.WriteLine ("Varovani! Hra se neda ukladat!");
						Console.SetCursorPosition (nejvetsisirka/2-13,nejvetsivyska/2-1);
						Console.WriteLine ("Nedohrajes, nenactes :)");
						Console.SetCursorPosition (nejvetsisirka/2-23,nejvetsivyska/2);
						Console.WriteLine ("Zmackni libovolnou klavesu pro pokracovani...");
						Console.SetCursorPosition (poziceKurzoruX,poziceKurzoruY);
						Console.ReadKey ();
						hra ();
					}
					if (volba == 1) {//Editor mapy
						if (hodnoty.RP == null) {
							hodnoty.RP = hodnoty.RP_basic;
						}
						map.NakresliMapu();
					}
					if (volba == 2) {//achievementy
					}
					if (volba == 3) {//nastaveni
						int volbanastaveni = 0;
						bool nastavovani = true;
						string[] volbynastaveni = new string[]{ 
							"Resource pack -- K NIČEMU",
							"Zpět"
						};
						while(nastavovani){
							Console.Clear ();
							Console.SetCursorPosition (2,1);
							Console.Write ("⚙ Nastavení");
							for (int i = 0; i <= volbynastaveni.GetLength (0) - 1; i++) {
								Console.SetCursorPosition (2,i+2);
								if(i==volbanastaveni){Console.Write ("> ");}
								Console.Write (volbynastaveni[i]);
							}
							ConsoleKeyInfo vstupnastaveni = Console.ReadKey ();
							if (vstupnastaveni.Key == ConsoleKey.Enter) {
								if (volbanastaveni == 0) {//ResourcePack
									Console.Clear();
									Console.SetCursorPosition(2,1);
									Console.WriteLine("Vyberte resource pack:");
									Console.SetCursorPosition(2,2);
									Console.WriteLine("1 - základní");
									Console.SetCursorPosition(2,3);
									Console.WriteLine("2 - sněžný");
									ConsoleKeyInfo vstupRP = Console.ReadKey ();
									if (vstupRP.Key == ConsoleKey.D1) {
										hodnoty.RP = null;
										hodnoty.RP = hodnoty.RP;
									}
									if (vstupRP.Key == ConsoleKey.D2) {
										hodnoty.RP = null;
										hodnoty.RP = hodnoty.RP_snow;
									}
								}
								if (volbanastaveni == 1) {//Zpět
									nastavovani = false;
								}
							}
							if (vstupnastaveni.Key == ConsoleKey.W) {
								volbanastaveni++;
							}
							if (vstupnastaveni.Key == ConsoleKey.S) {
								volbanastaveni--;
							}
							if(volbanastaveni <= -1){
								volbanastaveni = volbynastaveni.GetLength (0) - 1;
							}
							if(volbanastaveni >= volbynastaveni.GetLength (0)){
								volbanastaveni = 0;
							}
						}
					}
					if (volba == 4) {//ZDE SE SKONCI
						break;
					}	
				}
				if (volba >= volbymenu.GetLength(0)) {
					volba = 0;
				}
				if (volba <= -1) {
					volba = volbymenu.GetLength(0)-1;
				}
			}
		}
		public static void hra() {
			Random rnd = new Random ();
			grafika map = new grafika();
			promenne hodnoty = new promenne();
			tridainventar inv = new tridainventar();
			mramor fungeon = new mramor ();
			mapPresets DefinovaneKrajiny = new mapPresets ();

			hodnoty.init ();
			if (hodnoty.RP == null) {
				hodnoty.RP = hodnoty.RP_basic;
			}

			Console.BackgroundColor = ConsoleColor.Black;
			Console.Clear ();

			hodnoty.mapa = new map[32,16];


			string[] volbyhra = new string[]{
				"Nová mapa",
				"Načíst mapu z editoru"
			};
			int vyber = map.Menu ("Hra",volbyhra);

			if (vyber == 1) {
				int volbanacteni = 0;
				bool nacteni = true;
				while (nacteni) {
					Console.Clear ();
					Console.SetCursorPosition (2, 1);
					Console.WriteLine ("Vyber soubor k načtení:");
					string[] cestanac = Directory.GetFiles (AppDomain.CurrentDomain.BaseDirectory);
					for (int i = 0; i <= cestanac.GetLength (0) - 1; i++) {
						Console.SetCursorPosition (2, i + 2);
						if (i == volbanacteni) {
							Console.Write ("> ");
						}
						Console.WriteLine (cestanac [i]);
					}
					Console.CursorVisible = false;
					ConsoleKeyInfo vstupnacteni = Console.ReadKey ();
					if (vstupnacteni.Key == ConsoleKey.Enter) {
						hodnoty.mapa = null;
						hodnoty.mapa = map.NactiMapu(cestanac[volbanacteni]);
						Console.Clear ();
						Console.SetCursorPosition (2, 1);
						Console.WriteLine ("Úspěšně načteno!");
						Console.ReadKey ();
						nacteni = false;
					}
					if (vstupnacteni.Key == ConsoleKey.W) {
						volbanacteni--;
					}
					if (vstupnacteni.Key == ConsoleKey.S) {
						volbanacteni++;
					}
					if (volbanacteni >= cestanac.GetLength (0)) {
						volbanacteni = 0;
					}
					if (volbanacteni <= -1) {
						volbanacteni = cestanac.GetLength (0) - 1;
					}
					if (vstupnacteni.Key == ConsoleKey.Escape) {
						hodnoty.mapa = new map[32,16];
						for (int i = 0; i <= hodnoty.mapa.GetLength (0)-1; i++) {
							for (int x = 0; x <= hodnoty.mapa.GetLength (1)-1; x++) {
								hodnoty.mapa [i, x] = null;
							}
						}
						hodnoty.jenazivu = false;
						nacteni = false;
						break;
					}
				}
			}
			if (vyber == 0) {
				hodnoty.mapa = map.VygenerujMapu (32,16);
			}

			Console.CursorVisible = false;
			//Zde by se mel vybirat hrdina
			int mana = 0;
			hodnoty.exp = 0;
			Console.Clear ();
			bool exithrdina = false;
			int volbahrdiny = 0;
			string[,] inventar = new string[11, 10];//Generace inventare
			hodnoty.inventar = new string[11,10];
			hodnoty.vymazat ();
			string[,] hrdinove = {//Jmeno, sila, odolnost
				{"Bojovník", "120", "80"},
				{"Čaroděj", "50", "20"}
			};

			for (int i = 0; i <= 10; i++) {
				for (int j = 0; j <= 9; j++) {
					inventar [i, j] = "0";
				}
			}
			while (exithrdina == false) {
				Console.Clear ();
				Console.SetCursorPosition (1, 1);
				Console.WriteLine ("Nyní si vyber hrdinu:");
				Console.CursorVisible = false;
				for(int a = 0;a<= hrdinove.GetLength(0)-1;a++) {
					Console.SetCursorPosition(1,2+a);
					if(a==volbahrdiny) {Console.Write("> ");}
					Console.Write(hrdinove[a,0]);
				}
				Console.SetCursorPosition(1,hrdinove.GetLength(0)+3);
				Console.Write ("Síla: {0}",hrdinove[volbahrdiny,1]);
				Console.SetCursorPosition(1,hrdinove.GetLength(0)+4);
				Console.Write ("Životy: {0}",hrdinove[volbahrdiny,2]);
				Console.CursorVisible = false;
				ConsoleKeyInfo vstup = Console.ReadKey ();
				if (vstup.Key == ConsoleKey.W) {
					volbahrdiny--;
				}
				if (vstup.Key == ConsoleKey.S) {
					volbahrdiny++;
				}
				if (vstup.Key == ConsoleKey.Enter) {
					if (volbahrdiny == 0) {
						hodnoty.charakter = "Bojovník";
						hodnoty.sila = 120;
						hodnoty.odolnost = 80;
						hodnoty.odolnostMax = 80;
						hodnoty.mana = 10;
						hodnoty.inventar [0, 0] = "Železný meč";
						hodnoty.inventar [0, 1] = "Základní meč. Nedává moc poškození.";
						hodnoty.inventar [0, 2] = "zbran";
						hodnoty.inventar [0, 3] = "10";
						hodnoty.inventar [0, 4] = "20";
						hodnoty.inventar [1, 0] = "Drátěná košile";
						hodnoty.inventar [1, 1] = "Základní brnění";
						hodnoty.inventar [1, 2] = "brneni";
						hodnoty.inventar [1, 3] = "10";
						hodnoty.inventar [1, 4] = "30";
						hodnoty.inventar [2, 0] = "Chleba";
						hodnoty.inventar [2, 1] = "Klasický pšeničný chléb.";
						hodnoty.inventar [2, 2] = "jidlo";
						hodnoty.inventar [2, 3] = "10";
						hodnoty.inventar [2, 4] = "2";
					}
					if (volbahrdiny == 1) {
						hodnoty.charakter = "Čaroděj";
						hodnoty.sila = 50;
						hodnoty.odolnost = 80;
						hodnoty.odolnostMax = 80;
						hodnoty.mana = 100;
						hodnoty.inventar [0, 0] = "Dubová hůlka";
						hodnoty.inventar [0, 1] = "Základní hůlka. Nedává moc poškození.";
						hodnoty.inventar [0, 2] = "zbran";
						hodnoty.inventar [0, 3] = "5";
						hodnoty.inventar [0, 4] = "1000000";//1M
						hodnoty.inventar [1, 0] = "Lněný plášť";
						hodnoty.inventar [1, 1] = "Základní plášť.";
						hodnoty.inventar [1, 2] = "brneni";
						hodnoty.inventar [1, 3] = "10";
						hodnoty.inventar [1, 4] = "30";
						hodnoty.inventar [2, 0] = "Chleba";
						hodnoty.inventar [2, 1] = "Klasický pšeničný chléb.";
						hodnoty.inventar [2, 2] = "jidlo";
						hodnoty.inventar [2, 3] = "10";
						hodnoty.inventar [2, 4] = "2";
					}
					exithrdina = true;
				}
				if (volbahrdiny >= hrdinove.GetLength(0)) {
					volbahrdiny = hrdinove.GetLength(0)-1;
				}
				if (volbahrdiny <= -1) {
					volbahrdiny = 0;
				}
			}
			//Vysvetlivky
			map.Vysvetlivky();
			Console.ReadKey ();

			//Hra by mela zapocat
			bool exitgame = false;
			int charakterX = 0;
			int charakterY = 0;
			for (int x = 0; x <= hodnoty.mapa.GetLength (0) - 1; x++) {
				for (int y = 0; y <= hodnoty.mapa.GetLength (1) - 1; y++) {
					if (hodnoty.mapa [x, y].IsWalkable == true) {
						charakterX = x;
						charakterY = y;
						break;
					}
				}
			}
			int pocetDungeonu = 0;
			int maxPocetDungeonu = 0;
			hodnoty.cheat = false;
			hodnoty.neviditelnost_pred_monstry = false;
			hodnoty.delka_neviditelnosti = 0;
			string[,,] obchod = new string[5, 10, 6];//vytvori obchod
			for (int a = 0; a <= 4; a++) {//vymaze obchod
				for (int b = 0; b <= 9; b++) {
					for (int c = 0; c <= 5; c++) {
						obchod [a, b, c] = "0";
					}
				}
			}
			new generaceobchodu (obchod,hodnoty);
			souboj mob = new souboj ();
			hodnoty.jenazivu = true;

			Console.CursorVisible = true;
			bool movePlayer = false;
			maxPocetDungeonu = (hodnoty.mapa.GetLength(0)*hodnoty.mapa.GetLength(1))/(hodnoty.mapa.GetLength(1)*4);
			if (maxPocetDungeonu <= 0) {
				maxPocetDungeonu = 1;
			}
			while (exitgame == false) {
				Console.BackgroundColor = ConsoleColor.Black;
				Console.ForegroundColor = ConsoleColor.White;
				Console.Clear ();

				map.VykresliMapu(hodnoty.mapa,1,1);

				Console.BackgroundColor = hodnoty.mapa[charakterX,charakterY].TileBackground;
				Console.ForegroundColor = hodnoty.mapa[charakterX,charakterY].PlayerColor;
				Console.SetCursorPosition (charakterX+1, charakterY+1);
				Console.WriteLine ("\uC637");
				Console.ForegroundColor = ConsoleColor.White;
				Console.BackgroundColor = ConsoleColor.Black;
				map.HealthBar (hodnoty.odolnost, hodnoty.odolnostMax,hodnoty.mapa.GetLength(0) + 2,1,20,true);//health bar
				Console.SetCursorPosition (hodnoty.mapa.GetLength(0) + 2 + 7, 1);
				Console.BackgroundColor = ConsoleColor.Black;
				Console.SetCursorPosition (hodnoty.mapa.GetLength(0) + 2, 2);//NA CEM STOJIS
				Console.Write ("Stojíš na: ");
				string stojiciblok = "nic";
				string stojiciblok1 = "nic";
				try{
					stojiciblok = hodnoty.mapa[charakterX,charakterY].Name;
					stojiciblok1 = hodnoty.mapa[charakterX+1,charakterY].Name;
				} catch {
				}
				Console.Write ("{0} / {1}", stojiciblok, stojiciblok1);
				Console.SetCursorPosition (hodnoty.mapa.GetLength(0) + 2, 3);//Info o charakteru
				Console.WriteLine ("{0}: {1}/{2}/{3}", hodnoty.charakter, hodnoty.exp, hodnoty.sila, mana);
				Console.SetCursorPosition (hodnoty.mapa.GetLength(0) + 3, 4);
				Console.WriteLine ("Zkušenosti/Síla/Mana");
				if (hodnoty.neviditelnost_pred_monstry == true) {
					Console.SetCursorPosition (hodnoty.mapa.GetLength(0) + 2, 5);
					Console.Write ("Monstra si te nevsimaji ({0} tahu zbyva)",hodnoty.delka_neviditelnosti);
				}
				Console.SetCursorPosition (hodnoty.mapa.GetLength(0) + 2, 6);
				Console.WriteLine ("Zlato: {0}",hodnoty.zlato);
				Console.SetCursorPosition (hodnoty.mapa.GetLength(0) + 2, 7);
				//MOB
				if (movePlayer == true) {
					mob.bitva (stojiciblok, exitgame, hodnoty);
					movePlayer = false;
				}
				if (hodnoty.odolnost <= 0 || hodnoty.jenazivu == false) {//check 4 die
					exitgame = true;
					break;
				}
				if (hodnoty.neviditelnost_pred_monstry == true) {//cheat anti-monster
					hodnoty.delka_neviditelnosti = hodnoty.delka_neviditelnosti - 1;
					if (hodnoty.delka_neviditelnosti <= 0) {
						hodnoty.neviditelnost_pred_monstry = false;
						hodnoty.delka_neviditelnosti = 0;
					}
				}
				int pravdepodobnostdungeonu = rnd.Next (0, 100);//Nahodne generovani dungeonu, 2% sance na dungeon
				if (pravdepodobnostdungeonu <= 2 && pocetDungeonu <= maxPocetDungeonu) {//vygeneruje dungeon
					int randomDungeonX = rnd.Next (1, hodnoty.mapa.GetLength (0) - 1);
					int randomDungeonY = rnd.Next (1, hodnoty.mapa.GetLength (1) - 1);
					try {
						if (hodnoty.mapa [randomDungeonX, randomDungeonY].Replaceable == true/*hodnoty.mapa [randomDungeonX, randomDungeonY].Type != "City" && hodnoty.mapa [randomDungeonX, randomDungeonY].Type != "River" && hodnoty.mapa [randomDungeonX, randomDungeonY].Type != "Dungeon" && hodnoty.mapa [randomDungeonX, randomDungeonY].Type != "Bridge"*/) {
							hodnoty.mapa [randomDungeonX, randomDungeonY] = DefinovaneKrajiny.Dungeon;
							pocetDungeonu++;
						}
					} catch (System.NullReferenceException) {
					}
				}
				hodnoty.charakterX = charakterX;
				hodnoty.charakterY = charakterY;
				Console.CursorVisible = false;
				ConsoleKeyInfo vstup = Console.ReadKey ();/////////////////////////////////////////////////////////READS INPUT
				bool nahorevoda = false;
				bool dolevoda = false;
				bool vlevovoda = false;
				bool vpravovoda = false;
				try{ if(hodnoty.mapa[charakterX,charakterY-1].IsWalkable == false) { nahorevoda = true;} } catch(System.IndexOutOfRangeException) {}catch(System.NullReferenceException) {}
				try{ if(hodnoty.mapa[charakterX,charakterY+1].IsWalkable == false) { dolevoda = true;} } catch(System.IndexOutOfRangeException) {}catch(System.NullReferenceException) {}
				try{ if(hodnoty.mapa[charakterX-1,charakterY].IsWalkable == false) { vlevovoda = true;} } catch(System.IndexOutOfRangeException) {}catch(System.NullReferenceException) {}
				try{ if(hodnoty.mapa[charakterX+1,charakterY].IsWalkable == false) { vpravovoda = true;} } catch(System.IndexOutOfRangeException) {}catch(System.NullReferenceException) {}
				if (vstup.Key == ConsoleKey.W && nahorevoda == false) {
					charakterY--;
					movePlayer = true;
				} 
				if (vstup.Key == ConsoleKey.A && vlevovoda == false) {
					charakterX--;
					movePlayer = true;
				} 
				if (vstup.Key == ConsoleKey.S && dolevoda == false) {
					charakterY++;
					movePlayer = true;
				}
				if (vstup.Key == ConsoleKey.D && vpravovoda == false) {
					charakterX++;
					movePlayer = true;
				}
				if (vstup.Key == ConsoleKey.F) {//Specialni fce, sebrat drevo
					if (stojiciblok == "Větve") {
						int nah = rnd.Next (0,4);
						for (int i = 0; i <= hodnoty.inventar.GetLength (0) - 1; i++) {
							if (hodnoty.inventar [i, 0] == "Větve") { // je uz v inv
								hodnoty.inventar [i, 4] = (Int32.Parse (hodnoty.inventar [i, 4]) + nah).ToString ();
								break;
							}
							if (hodnoty.inventar [i, 0] == "0") {//najde volne misto
								hodnoty.inventar [i, 0] = "Větve";
								hodnoty.inventar [i, 1] = "Kus dřeva";
								hodnoty.inventar [i, 2] = "větve";
								hodnoty.inventar [i, 4] = nah.ToString();
								hodnoty.inventar [i, 5] = "1";//cena za 1 kus vetve
								break;
							}
						}
						int removeVetve = rnd.Next (0,100);
						if (removeVetve >= 95) {//5% chance to clear větve
							hodnoty.mapa[charakterX,charakterY].Name = "NOIDEAHOWTODOIT";//TOTO BYS MĚL TAKY SPRAVIT :D
						}
					}
				}
				#region Město
				if (vstup.Key == ConsoleKey.E && stojiciblok == "Vesnice") {
					bool uvnitrvesnice = true;
					string[] nazvymest = {
						"Bílý průsmyk",
						"Jitřenka",
						"Ledohrad",
						"Morthal",
						"Riften",
						"Samota",
						"Větrný žleb",
						"Vorařov",
						"Helgen"
					};
					string[] obchody = new string[] { "Hokynářstvi", "Kovárna", "Lékarna", "Doly", "Hospoda"};
					string nazevmesta = nazvymest [rnd.Next (0, 8)];
					int volbamesto = 0;
					while (uvnitrvesnice == true) {
						Console.Clear ();
						Console.SetCursorPosition (2, 1);
						Console.Write ("Vítej v městě jménem {0}", nazevmesta);
						for (int x = 0; x <= 4; x++) {
							Console.SetCursorPosition (2, x + 2);
							if (x == volbamesto) {
								Console.Write ("> ");
							}
							Console.Write (obchody [x]);
						}
						int mestovysvetlivkyoffset = 1;
						Console.SetCursorPosition (2,7+mestovysvetlivkyoffset);
						Console.Write ("[E] - Inventář");
						Console.SetCursorPosition (2,8+mestovysvetlivkyoffset);
						Console.Write ("[Enter] - Projít si obchod");
						Console.SetCursorPosition (2,9+mestovysvetlivkyoffset);
						Console.Write ("[ESC] - Zpět");
						Console.SetCursorPosition (2,10+mestovysvetlivkyoffset);
						Console.Write ("[T] - Přegenerovat nabídky v obchodech");
						Console.SetCursorPosition (2,11+mestovysvetlivkyoffset);
						Console.Write ("[Q] - Prodat věci z inventáře");
						Console.SetCursorPosition (hodnoty.poziceKurzoruX, hodnoty.poziceKurzoruY);
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
						if (vesnicevolba.Key == ConsoleKey.T) {
							new generaceobchodu(obchod,hodnoty);
						}
						if (volbamesto <= -1) { volbamesto = (obchody.Length-1); }
						if (volbamesto >= (obchody.Length)) { volbamesto = 0; }
						if (vesnicevolba.Key == ConsoleKey.E) {
							inv.action (hodnoty);
						}
						if (vesnicevolba.Key == ConsoleKey.Q) {//Prodat
							bool prodava = true;
							int volbainventar = 0;
							while(prodava) {
								if (Int32.Parse (hodnoty.inventar [volbainventar, 4]) <= 0) {//Kdyz dojde item
									for (int i = 0; i <= 9; i++) {
										hodnoty.inventar [volbainventar, i] = "0";
									}
								}
								Console.Clear ();
								Console.SetCursorPosition (1, 1);
								for (int i = 0; i <= 10; i++) {
									if (volbainventar == i) {
										Console.SetCursorPosition (2, i + 2);
										Console.Write ("> ");
										if (hodnoty.inventar [i, 0] == "0") {
											Console.Write ("Prázdné místo");
										} else {
											Console.Write ("{0} - {1}x",hodnoty.inventar [i, 0],hodnoty.inventar [i, 4]);
										}
										if (hodnoty.inventar [volbainventar, 1] != "0") {
											Console.SetCursorPosition (2, 14);
											Console.Write (hodnoty.inventar [volbainventar, 1]);
										}
										Console.SetCursorPosition (2, 15);
										if (hodnoty.inventar [volbainventar, 2] == "zbran") {
											Console.Write ("Poškození: ");
											Console.Write (hodnoty.inventar [volbainventar, 3]);
										}
										if (hodnoty.inventar [volbainventar, 2] == "brneni") {
											Console.Write ("Odolnost: ");
											Console.Write (hodnoty.inventar [volbainventar, 3]);
										}
										if (hodnoty.inventar [volbainventar, 2] == "jidlo") {
											Console.Write ("Doplni životů: ");
											Console.Write (hodnoty.inventar [volbainventar, 3]);
										}
										if (hodnoty.inventar [volbainventar, 2] == "lektvar-zdravi") {
											Console.Write ("Doplni životů: ");
											Console.Write (hodnoty.inventar [volbainventar, 3]);
										}
										if (hodnoty.inventar [volbainventar, 2] == "lektvar-otrava") {
											Console.Write ("Ubere životů: ");
											Console.Write (hodnoty.inventar [volbainventar, 3]);
										}
										if (hodnoty.inventar [volbainventar, 2] == "lektvar-neviditelnost") {
											Console.Write ("Na {0} tahů tě skryje před veškerou havětí.",hodnoty.inventar [volbainventar, 3]);
										}
										Console.SetCursorPosition (2, 16);
										if (hodnoty.inventar [volbainventar, 2] == "zbran") {
											Console.Write ("Výdrž: ");
											Console.Write (hodnoty.inventar [volbainventar, 4]);
										}
										if (hodnoty.inventar [volbainventar, 2] == "brneni") {
											Console.Write ("Výdrž: ");
											Console.Write (hodnoty.inventar [volbainventar, 4]);
										}
										if (hodnoty.inventar [volbainventar, 2] == "lektvar-zivot" || hodnoty.inventar [volbainventar, 2] == "lektvar-otrava" || hodnoty.inventar [volbainventar, 2] == "jidlo" || hodnoty.inventar [volbainventar, 2] == "větve" || hodnoty.inventar [volbainventar, 2] == "Most") {
											Console.Write ("V zásobě: ");
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
								Console.SetCursorPosition (2, 1);
								Console.Write ("Mas {0} zlataku.", hodnoty.zlato);
								Console.SetCursorPosition(2,18);
								Console.WriteLine("[Enter] - prodat 1 kus");
								ConsoleKeyInfo vstuppro = Console.ReadKey();
								if(vstuppro.Key == ConsoleKey.Enter) {//Prodá 1
									hodnoty.inventar[volbainventar,4] = (Int32.Parse(hodnoty.inventar[volbainventar,4])-1).ToString();
									hodnoty.zlato = hodnoty.zlato + Int32.Parse(hodnoty.inventar[volbainventar,5]);
								}
								if (vstuppro.Key == ConsoleKey.W) {
									volbainventar--;
								}
								if (vstuppro.Key == ConsoleKey.S) {
									volbainventar++;
								}
								if(vstuppro.Key == ConsoleKey.Escape || vstuppro.Key == ConsoleKey.E) {
									prodava = false;
								}
								if (volbainventar <= -1) {
									volbainventar = hodnoty.inventar.GetLength(0)-1;
								}
								if (volbainventar >= hodnoty.inventar.GetLength(0)) {
									volbainventar = 0;
								}
							}
						}
						if (vesnicevolba.Key == ConsoleKey.Enter) {
							int obchodvolba = 0;
							bool vobchode = true;
							while (vobchode) {
								Console.Clear ();
								Console.SetCursorPosition (2, 1);
								Console.Write ("{0} - Máš {1} zlaťáků",obchody [volbamesto], hodnoty.zlato);
								for (int i = 0; i <= 9; i++) {
									if(hodnoty.zlato >= Int32.Parse(obchod[volbamesto,i,5])){
										Console.BackgroundColor = ConsoleColor.Black;
										Console.ForegroundColor = ConsoleColor.Green;
									} else {
										Console.BackgroundColor = ConsoleColor.Black;
										Console.ForegroundColor = ConsoleColor.Red;
									}
									Console.SetCursorPosition (2, i + 2);
									if (i == obchodvolba) {
										Console.Write ("> ");
									}
									if (obchod [volbamesto, i, 0] == "0") {
										Console.Write ("-");
									} else {
										Console.Write ("{0} - {1} zlata", obchod [volbamesto, i, 0], obchod [volbamesto, i, 5]);
									}
								}
								Console.BackgroundColor = ConsoleColor.Black;
								Console.ForegroundColor = ConsoleColor.White;
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
										Console.Write ("Doplní {0} životů.", obchod [volbamesto, obchodvolba, 3]);
									}
									if (obchod [volbamesto, obchodvolba, 2] == "lektvar-zdravi") {
										Console.SetCursorPosition (2, 14);
										Console.Write ("Doplni {0} životů.", obchod [volbamesto, obchodvolba, 3]);
									}
									if (obchod [volbamesto, obchodvolba, 2] == "lektvar-otrava") {
										Console.SetCursorPosition (2, 14);
										Console.Write ("Ubere {0} životů.", obchod [volbamesto, obchodvolba, 3]);
									}
									if (obchod [volbamesto, obchodvolba, 2] == "lektvar-neviditelnost") {
										Console.SetCursorPosition (2, 14);
										Console.Write ("Schová tě před nepřáteli na {0} tahů.", obchod [volbamesto, obchodvolba, 3]);
									}
									Console.SetCursorPosition (2, 15);
									Console.Write ("{0} kusy", obchod [volbamesto, obchodvolba, 4]);
								}
								Console.SetCursorPosition (hodnoty.poziceKurzoruX, hodnoty.poziceKurzoruY);
								ConsoleKeyInfo vstupobchod = Console.ReadKey ();
								if (vstupobchod.Key == ConsoleKey.W) {
									obchodvolba--;
								}
								if (vstupobchod.Key == ConsoleKey.S) {
									obchodvolba++;
								}
								if (vstupobchod.Key == ConsoleKey.Enter) { //koupi ho
									if (hodnoty.zlato >= Int32.Parse (obchod [volbamesto, obchodvolba, 5])) { //pokud ma dostatek penez
										for (int i = 0; i <= hodnoty.inventar.GetLength(0)-1; i++) {
											if (hodnoty.inventar [i, 0] == obchod [volbamesto, obchodvolba, 0] && hodnoty.inventar [i, 2] == obchod [volbamesto, obchodvolba, 2]) { // je uz v inv
												hodnoty.inventar [i, 4] = (Int32.Parse (hodnoty.inventar [i, 4]) + Int32.Parse (obchod [volbamesto, obchodvolba, 4])).ToString ();
												hodnoty.zlato = hodnoty.zlato - Int32.Parse (obchod [volbamesto, obchodvolba, 5]);
												for (int y = 0; y <= 5; y++) {
													obchod [volbamesto, obchodvolba, y] = "0";
												}
												break;
											}
											if (hodnoty.inventar [i, 0] == "0") {//najde volne misto
												hodnoty.inventar [i, 0] = obchod [volbamesto, obchodvolba, 0];
												hodnoty.inventar [i, 1] = obchod [volbamesto, obchodvolba, 1];
												hodnoty.inventar [i, 2] = obchod [volbamesto, obchodvolba, 2];
												hodnoty.inventar [i, 3] = obchod [volbamesto, obchodvolba, 3];
												hodnoty.inventar [i, 4] = obchod [volbamesto, obchodvolba, 4];
												hodnoty.inventar [i, 5] = obchod [volbamesto, obchodvolba, 5];

												hodnoty.zlato = hodnoty.zlato - Int32.Parse (obchod [volbamesto, obchodvolba, 5]);
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
				} else {
					if (vstup.Key == ConsoleKey.E) { //OTEVRIT INVENTAR
						inv.action (hodnoty);
					}
				}
				#endregion
				if (vstup.Key == ConsoleKey.Enter && stojiciblok == "Dungeon") {//DUNGEON
					Console.SetCursorPosition (1, hodnoty.vyskamapy + 1);
					Console.WriteLine ("Vstupujes do dungeonu...");
					Console.SetCursorPosition (1, hodnoty.vyskamapy + 2);
					Console.WriteLine ("Stiskni libovolnou klávesu pro vstup...");
					Console.ReadKey ();
					fungeon.Dungeon (hodnoty);
					pocetDungeonu--;
					if (hodnoty.jenazivu == false) {
						exitgame = true;
					}
				}
				if (vstup.Key == ConsoleKey.Escape) {
					bool exitESCmenu = false;
					int menuvolba = 0;
					string[] menumoznosti = new string[]{
						"Zpět", "Uložit", "Konec"
					};
					while (exitESCmenu == false) {
						Console.BackgroundColor = ConsoleColor.Black;
						Console.ForegroundColor = ConsoleColor.White;
						Console.Clear ();
						Console.SetCursorPosition (1, 1);
						Console.WriteLine ("Menu");
						for (int i = 0; i <= menumoznosti.GetLength (0) - 1; i++) {
							Console.SetCursorPosition (1,i+2);
							if (menuvolba == i) {
								Console.Write ("> ");
							}
							Console.Write (menumoznosti[i]);
						}
						Console.SetCursorPosition (hodnoty.poziceKurzoruX, hodnoty.poziceKurzoruY);
						ConsoleKeyInfo vstupESC = Console.ReadKey ();
						//CHEATY ON pluskem
						if (vstupESC.Key == ConsoleKey.Add) {
							bool incheatmode = true;
							while (incheatmode == true) {
								Console.Clear ();
								Console.SetCursorPosition (2,1);
								Console.Write ("Zlato: {0}  [E] +10  [R] +100 [T] +1k [Z] +1M",hodnoty.zlato);
								Console.SetCursorPosition (2,2);
								Console.Write ("Zivoty: {0} [F] +10  [G] +100 [H] +1k [J] +1M",hodnoty.odolnost);
								Console.SetCursorPosition (2,3);
								Console.Write ("Zkusenosti: {0} [V] +10  [B] +100 [N] +1k",hodnoty.exp);
								Console.SetCursorPosition (2,4);
								Console.Write ("Neviditelnost pred monstry: {0}",hodnoty.neviditelnost_pred_monstry);
								Console.SetCursorPosition (2,5);
								Console.Write ("Delka neviditelnosti pred monstry: {0}",hodnoty.delka_neviditelnosti);
								Console.SetCursorPosition (2,6);
								Console.Write (" [X] aktivovat neviditelnost na 100 tahu");
								Console.SetCursorPosition (2,7);
								Console.Write ("");
								Console.SetCursorPosition (1,8);
								Console.Write (" [ESC] Zpet");

								Console.SetCursorPosition (hodnoty.poziceKurzoruX,hodnoty.poziceKurzoruY);
								ConsoleKeyInfo vstupcheat = Console.ReadKey ();
								if (vstupcheat.Key == ConsoleKey.Escape) { incheatmode = false; break; }
								if (vstupcheat.Key == ConsoleKey.E) { hodnoty.zlato = hodnoty.zlato + 10; }
								if (vstupcheat.Key == ConsoleKey.F) { hodnoty.odolnost = hodnoty.odolnost + 10; hodnoty.odolnostMax = hodnoty.odolnostMax + 10; }
								if (vstupcheat.Key == ConsoleKey.R) { hodnoty.zlato = hodnoty.zlato + 100; }
								if (vstupcheat.Key == ConsoleKey.G) { hodnoty.odolnost = hodnoty.odolnost + 100; hodnoty.odolnostMax = hodnoty.odolnostMax + 100; }
								if (vstupcheat.Key == ConsoleKey.T) { hodnoty.zlato = hodnoty.zlato + 1000; }
								if (vstupcheat.Key == ConsoleKey.H) { hodnoty.odolnost = hodnoty.odolnost + 1000; hodnoty.odolnostMax = hodnoty.odolnostMax + 1000; }
								if (vstupcheat.Key == ConsoleKey.Z) { hodnoty.zlato = hodnoty.zlato + 1000000; }
								if (vstupcheat.Key == ConsoleKey.J) { hodnoty.odolnost = hodnoty.odolnost + 1000000; hodnoty.odolnostMax = hodnoty.odolnostMax + 1000000; }
								if (vstupcheat.Key == ConsoleKey.V) { hodnoty.exp = hodnoty.exp + 10; }
								if (vstupcheat.Key == ConsoleKey.B) { hodnoty.exp = hodnoty.exp + 100; }
								if (vstupcheat.Key == ConsoleKey.N) { hodnoty.exp = hodnoty.exp + 1000; }
								if (vstupcheat.Key == ConsoleKey.M) { //MAP EDITOR

								}
								if (vstupcheat.Key == ConsoleKey.C) { //INV EDITOR

								}
								if (vstupcheat.Key == ConsoleKey.X) { hodnoty.neviditelnost_pred_monstry = true; hodnoty.delka_neviditelnosti = 100; }
							}
							Console.Clear ();
						}
						if (vstupESC.Key == ConsoleKey.W) {
							menuvolba--;
						}
						if (vstupESC.Key == ConsoleKey.S) {
							menuvolba++;
						}
						if (vstupESC.Key == ConsoleKey.Enter && menuvolba == 0) {
							//Zpet
							exitESCmenu = true;
						}
						if (vstupESC.Key == ConsoleKey.Enter && menuvolba == 1) {
							//Uložit
							int volbaulozeni = 0;
							bool ukladani = true;
							while (ukladani) {
								Console.Clear ();
								Console.SetCursorPosition (2, 1);
								Console.WriteLine ("Vyber soubor k uložení:");
								string[] cesta = Directory.GetFiles (AppDomain.CurrentDomain.BaseDirectory);
								for (int i = 0; i <= cesta.GetLength (0) - 1; i++) {
									Console.SetCursorPosition (2, i + 2);
									if (i == volbaulozeni) {
										Console.Write ("> ");
									}
									Console.WriteLine (cesta [i]);
								}
								Console.SetCursorPosition (2,cesta.GetLength(0)+3);
								Console.WriteLine ("[N] - Nový soubor");
								Console.SetCursorPosition (2,cesta.GetLength(0)+4);
								Console.WriteLine ("[ESC] - Zpět");
								Console.CursorVisible = false;
								ConsoleKeyInfo vstupulozeni = Console.ReadKey ();
								if (vstupulozeni.Key == ConsoleKey.Enter) {
									map.UlozMapu (hodnoty.mapa,cesta[volbaulozeni]);
									Console.Clear ();
									Console.SetCursorPosition (2,1);
									Console.WriteLine ("Úspěšně uloženo!");
									Console.ReadKey ();
									break;
								}
								if (vstupulozeni.Key == ConsoleKey.N) {
									Console.Clear ();
									Console.CursorVisible = true;
									Console.SetCursorPosition (2,1);
									Console.WriteLine ("Zadejte nazev souboru: ");
									Console.SetCursorPosition (2,2);
									string nazev = Console.ReadLine ();
									Console.CursorVisible = false;
									map.UlozMapu (hodnoty.mapa ,Environment.CurrentDirectory+"/"+nazev);
									Console.Clear ();
									Console.SetCursorPosition (2,2);
									Console.WriteLine ("Úspěšně uloženo!");
									Console.ReadKey ();
									break;
								}
								if (vstupulozeni.Key == ConsoleKey.W) {
									volbaulozeni--;
								}
								if (vstupulozeni.Key == ConsoleKey.S) {
									volbaulozeni++;
								}
								if (volbaulozeni >= cesta.GetLength (0)) {
									volbaulozeni = 0;
								}
								if (volbaulozeni <= -1) {
									volbaulozeni = cesta.GetLength (0) - 1;
								}
								if (vstupulozeni.Key == ConsoleKey.Escape) {
									ukladani = false;
									break;
								}
							}
							exitESCmenu = true;
						}
						if (vstupESC.Key == ConsoleKey.Enter && menuvolba == 2) {
							//Konec
							exitESCmenu = true;
							exitgame = true;
						}
						if (menuvolba <= -1 || menuvolba >= menumoznosti.GetLength(0)) {
							menuvolba = 0;
						}
					}
				}
				if (charakterX <= -1) {
					charakterX = 0;
				}
				if (charakterX >= hodnoty.mapa.GetLength(0)) {
					charakterX = hodnoty.mapa.GetLength(0)-1;
				}
				if (charakterY <= -1) {
					charakterY = 0;
				}
				if (charakterY >= hodnoty.mapa.GetLength(1)) {
					charakterY = hodnoty.mapa.GetLength(1)-1;
				}
			}
		}
	}
}