using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace IKT
{
	public class grafika
	{
		Random rnd = new Random();
		promenne hodnoty = new promenne();

		public grafika ()
		{
		}
		public void zasnezit(promenne hodnoty){
			hodnoty.RP = null;
			hodnoty.RP = hodnoty.RP_snow;
			for (int x = 0; x <= hodnoty.mapa.GetLength (0) - 1; x++) {
				for (int y = 0; y <= hodnoty.mapa.GetLength (1) - 1; y++) {
					if (hodnoty.mapa [x, y] == 2 && hodnoty.mapa [x + 1, y] != 17) {
						try{
						hodnoty.mapa [x, y + 1] = 17;
						} catch {
						}
					}
				}
			}
		}
		public int Menu(string titulek,string[] moznosti){
			int vracecihodnota;
			int volba = 0;
			while (true) {
				Console.Clear ();
				Console.SetCursorPosition (2,1);
				Console.Write (titulek);
				for (int i = 0; i <= moznosti.GetLength (0) - 1; i++) {
					Console.SetCursorPosition (2,2+i);
					if (volba == i) {
						Console.Write ("> ");
					}
					Console.Write (moznosti[i]);
				}
				ConsoleKeyInfo vstup = Console.ReadKey ();
				if (vstup.Key == ConsoleKey.W) {
					volba--;
				}
				if (vstup.Key == ConsoleKey.UpArrow) {
					volba--;
				}
				if (vstup.Key == ConsoleKey.S) {
					volba++;
				}
				if (vstup.Key == ConsoleKey.DownArrow) {
					volba++;
				}
				if (vstup.Key == ConsoleKey.Enter) {
					vracecihodnota = volba;
					break;
				}
				if (volba <= -1) {
					volba = moznosti.GetLength (0)-1;
				}
				if (volba >= moznosti.GetLength (0)) {
					volba = 0;
				}
			}
			return vracecihodnota;
		}
		public void VygenerujMapu(grafika map, promenne hodnoty) {
			bool exitmapa = false;
			while (exitmapa == false) {
				for (int dilky = 0; dilky <= hodnoty.vyskamapy - 1; dilky++) {
					for (int dilky1 = 0; dilky1 <= hodnoty.sirkamapy - 1; dilky1++) {//DODELAT VYGENEROVANI podle okoli !!!!!!!!!MOMENTALNE ROZJEBANE!!!!!!!!!
						hodnoty.mapa [dilky, dilky1] = 0;
					}
				}//GENERATOR MAPY, faze 2 zvedani terenu
				for (int dilky = 0; dilky <= hodnoty.vyskamapy - 1; dilky++) {
					for (int dilky1 = 0; dilky1 <= hodnoty.sirkamapy - 1; dilky1++) {
						int mnoznostpahorkatin = rnd.Next (0, 10);
						if (mnoznostpahorkatin <= 2) {
							hodnoty.mapa [dilky, dilky1] = 1;
						}
					}
				}//GENERATOR MAPY, faze 3 zvedani terenu podruhe
				for (int dilky = 0; dilky <= hodnoty.vyskamapy - 1; dilky++) {
					for (int dilky1 = 0; dilky1 <= hodnoty.sirkamapy - 1; dilky1++) {
						int mnoznosthor = rnd.Next (0, 10);
						if (hodnoty.mapa [dilky, dilky1] == 1 && mnoznosthor == 1) {
							hodnoty.mapa [dilky, dilky1] = 2;
						}
					}
				}
				bool pokus = false;
				while (pokus == false) {//GENERATOR MAPY, faze 4, generovani vodstva
					int mnoznostvodstva1 = rnd.Next (0, hodnoty.sirkamapy);
					int pozicevodstvy = mnoznostvodstva1;
					try {
						for (int vodstvo1 = 0; vodstvo1 <= hodnoty.sirkamapy - 1; vodstvo1++) {
							hodnoty.mapa [pozicevodstvy, vodstvo1] = 3;
						}
						pokus = true;
					} catch (System.IndexOutOfRangeException) {  // CS0168
						Console.WriteLine ("Ale toto je nepříjemné! Chyba: System.IndexOutOfRangeException ");
						Console.WriteLine ("Zmacknete libovolnou klavesu pro pokracovani...");
					}
				}//GENERATOR MAPY, faze 5 staveni domu
				for (int dilky = 0; dilky <= hodnoty.vyskamapy - 1; dilky++) {
					for (int dilky1 = 0; dilky1 <= hodnoty.sirkamapy - 1; dilky1++) {
						int mnoznostdomu = rnd.Next (0, 100);
						if (mnoznostdomu == 1 && hodnoty.mapa [dilky, dilky1] != 3 && hodnoty.mapa [dilky, dilky1] != 5 && hodnoty.mapa [dilky, dilky1] != 6) {
							hodnoty.mapa [dilky, dilky1] = 6;
						}
					}
				}//GENERATOR MAPY, faze 6 pohazovani vetvi
				for (int dilky = 0; dilky <= hodnoty.vyskamapy - 1; dilky++) {
					for (int dilky1 = 0; dilky1 <= hodnoty.sirkamapy - 1; dilky1++) {
						int mnoznostvetvi = rnd.Next (0, 10);
						if (mnoznostvetvi == 1 && hodnoty.mapa [dilky, dilky1] != 1 && hodnoty.mapa [dilky, dilky1] != 3 && hodnoty.mapa [dilky, dilky1] != 5 && hodnoty.mapa [dilky, dilky1] != 6) {
							hodnoty.mapa [dilky, dilky1] = 7;
						}
					}
				}
				Console.BackgroundColor = ConsoleColor.Black;
				Console.ForegroundColor = ConsoleColor.White;
				Console.Clear ();
				map.VykresliMapu (hodnoty.mapa, 1, 1, hodnoty.RP);

				Console.BackgroundColor = ConsoleColor.Black;
				Console.ForegroundColor = ConsoleColor.White;
				Console.SetCursorPosition (1, hodnoty.vyskamapy + 2);
				Console.WriteLine ("Chcete ponechat mapu? Y/n");
				Console.SetCursorPosition (1, hodnoty.vyskamapy + 3);
				ConsoleKeyInfo vstup = Console.ReadKey ();
				if (vstup.Key == ConsoleKey.Y) {
					exitmapa = true;
				}
				if (vstup.Key == ConsoleKey.N) {
					exitmapa = false;
				}
			}
		}
		public void Vysvetlivky() {
			string[] vysv = new string[] { "[WASD] - Pohyb","[Q] - Vyčkávat","[R] - Vstoupit do vesnice","[ESC] - Menu","[E] - Otevřít inventář","[F] - Sebrat věc z země"};
			Console.BackgroundColor = ConsoleColor.Black;
			Console.ForegroundColor = ConsoleColor.White;
			Console.Clear ();
			for(int i = 0;i<= vysv.GetLength(0)-1;i++) {
				Console.SetCursorPosition(1,i+1);
				Console.WriteLine(vysv[i]);
				if (i == vysv.GetLength (0) - 1) {
					Console.SetCursorPosition (1, i+3);
					Console.WriteLine ("Zmáčkni libovolnou klávesu k pokračování...");
				}
			}
		}
		public void VykresliMapu(int[,] mapa, int offsetX, int offsetY, string[,] resource) {
			int vykresliX = 1;//VYKRESLENI MAPY
			int zaklvykresliX = vykresliX;
			int radky = 1;
			for (int vykr = 0; vykr <= mapa.GetLength(0) - 1; vykr++) {
				for (int vykr1 = 0; vykr1 <= mapa.GetLength(1) - 1; vykr1++) {
					for (int z = 0; z <= resource.GetLength (0)-1; z++) {
						Console.SetCursorPosition (vykresliX, radky);
						if (mapa [vykr, vykr1] == Int32.Parse (resource [z, 0])) {
							Console.BackgroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), resource [z, 1]);
							Console.ForegroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), resource [z, 2]);
							Console.Write (resource [z, 3]);
						}
					}
					vykresliX++;
				}
				Console.WriteLine ("");
				radky++;
				vykresliX = zaklvykresliX;
			}
		}
		public void NakresliMapu() {
			if (hodnoty.RP == null) {
				hodnoty.RP = hodnoty.RP_basic;
			}
			Console.CursorVisible = false;
			string[] premenu = new string[] {"Malá mapa [12 * 12]","Standartní mapa [16 * 32]", "Velká mapa [20 * 56]", "Vlastní velikost mapy"};
			Console.BackgroundColor = ConsoleColor.Black;
			Console.ForegroundColor = ConsoleColor.White;
			Console.Clear ();
			bool vpresetu = true;
			int premoz = 0;
			int radky = 0;
			int sloupce = 0;
			while (vpresetu) {
				Console.Clear ();
				for (int x = 0; x <= premenu.GetLength (0) - 1; x++) {
					Console.SetCursorPosition (2, x + 1);
					if (x == premoz) {
						Console.Write ("> ");
					}
					Console.WriteLine (premenu [x]);
				}
				ConsoleKeyInfo vstuppre = Console.ReadKey ();
				if (vstuppre.Key == ConsoleKey.Enter) {
					if (premoz == 0) {
						radky = 12;
						sloupce = 12;
						vpresetu = false;
					}
					if (premoz == 1) {
						radky = 16;
						sloupce = 32;
						vpresetu = false;
					}
					if (premoz == 2) {
						radky = 20;
						sloupce = 56;
						vpresetu = false;
					}
					if (premoz == 3) {
						Console.Clear ();
						Console.CursorVisible = true;
						Console.SetCursorPosition (2,1);
						Console.WriteLine ("Vlastní velikost:");
						Console.SetCursorPosition (2,2);
						Console.WriteLine ("Kolik řádků má mít mapa?");
						Console.SetCursorPosition (2,3);
						radky = Int32.Parse(Console.ReadLine ());
						Console.SetCursorPosition (2,4);
						Console.WriteLine ("Kolik sloupců má mít mapa?");
						Console.SetCursorPosition (2,5);
						sloupce = Int32.Parse(Console.ReadLine ());
						vpresetu = false;
					}
				}
				if (vstuppre.Key == ConsoleKey.W) {
					premoz--;
				}
				if (vstuppre.Key == ConsoleKey.S) {
					premoz++;
				}
				if (premoz <= -1) {
					premoz = premenu.GetLength (0)-1;
				}
				if (premoz >= premenu.GetLength (0)) {
					premoz = 0;
				}
			}
			Console.CursorVisible = true;
			int[,] mapa = new int[radky,sloupce];
			bool kreslimapu = true;
			for (int i = 0; i <= mapa.GetLength (0) - 1; i++) {
				for (int a = 0; a <= mapa.GetLength (1) - 1; a++) {
					mapa[i,a] = 0;
				}
			}
			int posX = 0;
			int posY = 0;
			int[] buffer = {0,1,2,3,4,5,6,7,8,9};
			string[] moznosti = new string[] { "[WASD] - Pohyb po mapě","[ESC] - Menu","[E] - Vybrat stavěcí pole", "[Q] - Nástroje", "[F] - Zvýraznit kurzor"};
			string[] escmenu = new string[] { "Zpět do editoru","Uložit","Načíst","Spravovat soubory","Ukončit editor","Změna resource packu"};
			bool zvyraznitKurzor = false;
			while (kreslimapu) {
				Console.BackgroundColor = ConsoleColor.Black;
				Console.ForegroundColor = ConsoleColor.White;
				Console.Clear ();
				if (hodnoty.RP == null) {
					hodnoty.RP = hodnoty.RP_basic;
				}
				VykresliMapu (mapa,2,1, hodnoty.RP);
				Console.BackgroundColor = ConsoleColor.Black;
				Console.ForegroundColor = ConsoleColor.White;
				for (int x = 0; x <= moznosti.GetLength (0)-1; x++) {
					Console.SetCursorPosition (mapa.GetLength(1) + 2, 1+x);
					Console.WriteLine (moznosti[x]);
				}
				for (int i = 0; i <= buffer.GetLength (0)-1; i++) {
					Console.SetCursorPosition (mapa.GetLength(1) + 2,i+moznosti.GetLength(0)+2);
					Console.Write ("{0} - {1}",i,hodnoty.RP[buffer[i],4]);
				}
				if (zvyraznitKurzor == false) {
					Console.SetCursorPosition (posX + 1, posY + 1);
				}
				if (zvyraznitKurzor == true) {
					Console.SetCursorPosition (posX + 1, posY + 1);
					Console.BackgroundColor = ConsoleColor.Yellow;
					Console.ForegroundColor = ConsoleColor.Magenta;
					Console.Write ("X");
				}
				ConsoleKeyInfo vstup = Console.ReadKey ();
				if (vstup.Key == ConsoleKey.D0) { mapa [posY, posX] = buffer[0];}
				if (vstup.Key == ConsoleKey.D1) { mapa [posY, posX] = buffer[1];}
				if (vstup.Key == ConsoleKey.D2) { mapa [posY, posX] = buffer[2];}
				if (vstup.Key == ConsoleKey.D3) { mapa [posY, posX] = buffer[3];}
				if (vstup.Key == ConsoleKey.D4) { mapa [posY, posX] = buffer[4];}
				if (vstup.Key == ConsoleKey.D5) { mapa [posY, posX] = buffer[5];}
				if (vstup.Key == ConsoleKey.D6) { mapa [posY, posX] = buffer[6];}
				if (vstup.Key == ConsoleKey.D7) { mapa [posY, posX] = buffer[7];}
				if (vstup.Key == ConsoleKey.D8) { mapa [posY, posX] = buffer[8];}
				if (vstup.Key == ConsoleKey.D9) { mapa [posY, posX] = buffer[9];}
				if (vstup.Key == ConsoleKey.F) {//zvýraznit kurzor
					if (zvyraznitKurzor == false) {
						zvyraznitKurzor = true;
					} else {
						zvyraznitKurzor = false;
					}
				}
				if (vstup.Key == ConsoleKey.Q) {//Nástroje.
					Console.CursorVisible = false;
					int volbanastroje = 0;
					string[] nastroje = new string[]{
						"Vylít velou mapu...",
						"Zaměnit bloky",
						"Zpět"
					};
					while(true){
						volbanastroje = Menu ("Nástroje", nastroje);
						if (volbanastroje == 0) {//Vylít celou mapu
							int vylitblokem = 0;
							bool zmenaindexubufferu = true;
							int indexbufferu = 0;
							while(zmenaindexubufferu){
								Console.Clear ();
								Console.SetCursorPosition (2,1);
								Console.WriteLine ("Zvol blok na vylití celé mapy");
								try{
									for (int b = 0; b <= hodnoty.RP.GetLength (0) - 1; b++) {
										if (b == indexbufferu) {
											Console.SetCursorPosition (2,b+2);
											Console.Write (">");
										}
										Console.SetCursorPosition (3,b+2);
										Console.BackgroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), hodnoty.RP [b, 1]);
										Console.ForegroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), hodnoty.RP [b, 2]);
										Console.Write (hodnoty.RP [b, 3]);
										Console.BackgroundColor = ConsoleColor.Black;
										Console.ForegroundColor = ConsoleColor.White;
										Console.Write ("  {0}",hodnoty.RP [b, 4]);
									}
								} catch {
								}
								ConsoleKeyInfo vstupindexubuffer = Console.ReadKey ();
								if (vstupindexubuffer.Key == ConsoleKey.Escape) {
									zmenaindexubufferu = false;
								}
								if (vstupindexubuffer.Key == ConsoleKey.Enter) {
									vylitblokem = Int32.Parse(hodnoty.RP[indexbufferu,0]);
									for (int x = 0; x <= mapa.GetLength (0) - 1; x++) {
										for (int y = 0; y <= mapa.GetLength (1) - 1; y++) {
											mapa [x, y] = vylitblokem;
										}
									}
									zmenaindexubufferu = false;
								}
								if (vstupindexubuffer.Key == ConsoleKey.S) {
									indexbufferu++;
								}
								if (vstupindexubuffer.Key == ConsoleKey.W) {
									indexbufferu--;
								}
								if (indexbufferu <= -1) {
									indexbufferu = hodnoty.RP.GetLength(0)-1;
								}
								if (indexbufferu >= hodnoty.RP.GetLength(0)) {
									indexbufferu = 0;
								}
							}
						}
						if (volbanastroje == nastroje.GetLength (0) - 1) {
							break;
						}
					}
					Console.CursorVisible = true;
				}
				if (vstup.Key == ConsoleKey.E) {//Změna bufferu
					Console.CursorVisible = false;
					bool zmenabufferu = true;
					int vyberbufferu = 0;
					string nazevBuf = "";
					string[] moznostiZmenyBufferu = { "[ESC] - Zpět", "[Enter] - Změnit zkratku", "[W,D] - Pohyb"};
					while(zmenabufferu){
						Console.Clear ();
						Console.SetCursorPosition (2,1);
						Console.WriteLine ("Změna zkratek:");
						for (int i = 0; i <= 9; i++) {
							Console.SetCursorPosition (2,2+i);
							if (i == vyberbufferu) {
								Console.Write ("> ");
							}
							for (int v = 0; v <= hodnoty.RP.GetLength (0) - 1; v++) {
								if (v == buffer [i]) {
									nazevBuf = hodnoty.RP[v,4];
									break;
								}
							}
							Console.Write ("{0} - {1}",i,nazevBuf);
						}
						for (int y = 0; y <= moznostiZmenyBufferu.GetLength (0)-1; y++) {
							Console.SetCursorPosition (2, 13+y);
							Console.WriteLine (moznostiZmenyBufferu[y]);
						}
						ConsoleKeyInfo vstupbuffer = Console.ReadKey ();
						if (vstupbuffer.Key == ConsoleKey.Escape) {
							Console.CursorVisible = true;
							zmenabufferu = false;
						}
						if (vstupbuffer.Key == ConsoleKey.Enter) {
							bool zmenaindexubufferu = true;
							int indexbufferu = 0;
							while(zmenaindexubufferu){
								Console.Clear ();
								Console.SetCursorPosition (2,1);
								Console.WriteLine ("Změna zkratky:");
								Console.SetCursorPosition (2,2);
								Console.WriteLine ("{0} - {1}",buffer[vyberbufferu],hodnoty.RP[vyberbufferu,4]);
								Console.SetCursorPosition (2,3);
								Console.WriteLine ("Zvol nový blok:");
								try{
								for (int b = 0; b <= hodnoty.RP.GetLength (0) - 1; b++) {
									if (b == indexbufferu) {
										Console.SetCursorPosition (2,b+4);
										Console.Write (">");
									}
									Console.SetCursorPosition (3,b+4);
									Console.BackgroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), hodnoty.RP [b, 1]);
									Console.ForegroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), hodnoty.RP [b, 2]);
									Console.Write (hodnoty.RP [b, 3]);
									Console.BackgroundColor = ConsoleColor.Black;
									Console.ForegroundColor = ConsoleColor.White;
									Console.Write ("  {0}",hodnoty.RP [b, 4]);
									}
								} catch {
								}
								ConsoleKeyInfo vstupindexubuffer = Console.ReadKey ();
								if (vstupindexubuffer.Key == ConsoleKey.Escape) {
									Console.CursorVisible = true;
									zmenaindexubufferu = false;
								}
								if (vstupindexubuffer.Key == ConsoleKey.Enter) {
									buffer[vyberbufferu] = Int32.Parse(hodnoty.RP[indexbufferu,0]);
									zmenaindexubufferu = false;
								}
								if (vstupindexubuffer.Key == ConsoleKey.S) {
									indexbufferu++;
								}
								if (vstupindexubuffer.Key == ConsoleKey.W) {
									indexbufferu--;
								}
								if (indexbufferu <= -1) {
									indexbufferu = hodnoty.RP.GetLength(0)-1;
								}
								if (indexbufferu >= hodnoty.RP.GetLength(0)) {
									indexbufferu = 0;
								}
							}
						}
						if (vstupbuffer.Key == ConsoleKey.S) {
							vyberbufferu++;
						}
						if (vstupbuffer.Key == ConsoleKey.W) {
							vyberbufferu--;
						}
						if (vyberbufferu <= -1) {
							vyberbufferu = 9;
						}
						if (vyberbufferu >= 10) {
							vyberbufferu = 0;
						}
					}
				}
				if (vstup.Key == ConsoleKey.W) {
					posY--;
				}
				if (vstup.Key == ConsoleKey.A) {
					posX--;
				}
				if (vstup.Key == ConsoleKey.S) {
					posY++;
				}
				if (vstup.Key == ConsoleKey.D) {
					posX++;
				}
				if (vstup.Key == ConsoleKey.Escape) {//MENU
					Console.CursorVisible = false;
					bool escm = true;
					int escmoz = 0;
					while (escm) {
						Console.Clear ();
						for (int x = 0; x <= escmenu.GetLength (0) - 1; x++) {
							Console.SetCursorPosition (2, x + 1);
							if (x == escmoz) {
								Console.Write ("> ");
							}
							Console.WriteLine (escmenu [x]);
						}
						ConsoleKeyInfo vstupesc = Console.ReadKey ();
						if (vstupesc.Key == ConsoleKey.Enter) {
							if (escmoz == 0) {
								escm = false;
							}
							if (escmoz == 1) {//Uložit -- JE TU DVAKRÁT!!!!
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
										UlozMapu (mapa,cesta[volbaulozeni]);
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
										UlozMapu (mapa,Environment.CurrentDirectory+"/"+nazev);
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
							}
							if (escmoz == 2) {//Načíst
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
										mapa = null;
										mapa = NactiMapu (cestanac[volbanacteni]);
										Console.Clear ();
										Console.SetCursorPosition (2,1);
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
										nacteni = false;
										break;
									}
								}
							}
							if (escmoz == 3) {//spravovat mapu
								int volbasprava = 0;
								bool sprava = true;
								while (sprava) {
									Console.Clear ();
									Console.SetCursorPosition (2, 1);
									Console.WriteLine ("Vyber soubor a zmáčkněte příslušnou klávesu:");
									string[] cestaspr = Directory.GetFiles (AppDomain.CurrentDomain.BaseDirectory);
									for (int i = 0; i <= cestaspr.GetLength (0) - 1; i++) {
										Console.SetCursorPosition (2, i + 2);
										if (i == volbasprava) {
											Console.Write ("> ");
										}
										Console.WriteLine (cestaspr [i]);
									}
									Console.SetCursorPosition (2,cestaspr.GetLength(0)+3);
									Console.WriteLine ("[R] - Smazat soubor");
									Console.SetCursorPosition (2,cestaspr.GetLength(0)+4);
									Console.WriteLine ("[ESC] - Zpět");
									Console.CursorVisible = false;
									ConsoleKeyInfo vstupnacteni = Console.ReadKey ();
									if (vstupnacteni.Key == ConsoleKey.R) {//Smazat Soubor
										File.Delete(cestaspr[volbasprava]);
										cestaspr = Directory.GetFiles (AppDomain.CurrentDomain.BaseDirectory);
									}
									if (vstupnacteni.Key == ConsoleKey.W) {
										volbasprava--;
									}
									if (vstupnacteni.Key == ConsoleKey.S) {
										volbasprava++;
									}
									if (volbasprava >= cestaspr.GetLength (0)) {
										volbasprava = 0;
									}
									if (volbasprava <= -1) {
										volbasprava = cestaspr.GetLength (0) - 1;
									}
									if (vstupnacteni.Key == ConsoleKey.Escape) {
										sprava = false;
										break;
									}
								}

							}
							if (escmoz == 4) {//Zpět do menu
								kreslimapu = false;
								escm = false;
							}
							if (escmoz == 5) {
								Console.Clear();
								Console.SetCursorPosition(2,1);
								Console.WriteLine("Vyberte resource pack:");
								Console.SetCursorPosition(2,2);
								Console.WriteLine("1 - základní");
								Console.SetCursorPosition(2,3);
								Console.WriteLine("2 - sněžný");
								Console.SetCursorPosition(2,4);
								Console.WriteLine("3 - pouštní");
								ConsoleKeyInfo vstupRP = Console.ReadKey ();
								if (vstupRP.Key == ConsoleKey.D1) {
									hodnoty.RP = null;
									hodnoty.RP = hodnoty.RP;
								}
								if (vstupRP.Key == ConsoleKey.D2) {
									hodnoty.RP = null;
									hodnoty.RP = hodnoty.RP_snow;
								}
								if (vstupRP.Key == ConsoleKey.D3) {
									hodnoty.RP = null;
									hodnoty.RP = hodnoty.RP_desert;
								}
							}
						}
						if (vstupesc.Key == ConsoleKey.W) {
							escmoz--;
						}
						if (vstupesc.Key == ConsoleKey.S) {
							escmoz++;
						}
						if (escmoz <= -1) {
							escmoz = escmenu.GetLength (0)-1;
						}
						if (escmoz >= escmenu.GetLength (0)) {
							escmoz = 0;
						}
					}
					Console.CursorVisible = true;
				}
				if (posX <= -1) {
					posX = mapa.GetLength (1)-1;
				}
				if (posX >= mapa.GetLength(1)) {
					posX = 0;
				}
				if (posY >= mapa.GetLength(0)) {
					posY = 0;
				}
				if (posY <= -1) {
					posY = mapa.GetLength(0)-1;
				}
			}
			Console.CursorVisible = false;
   		}
		public int[,] NactiMapu(string soubor) {
			StreamReader mapa1 = new StreamReader(soubor);
			int sirkamapy1 = Int32.Parse(mapa1.ReadLine ());
			int vyskamapy1 = Int32.Parse(mapa1.ReadLine ());
			int[,] mapaNactena = new int[vyskamapy1,sirkamapy1];
			for (int x = 0; x <= vyskamapy1; x++) {
				string Lajna = mapa1.ReadLine ();
				if (Lajna == null) {
					break;
				}
				string[] rozdelenalajna = Lajna.Split (',');
				for (int i = 0; i <= rozdelenalajna.Length - 2; i++) {
					mapaNactena [x, i] = Int32.Parse (rozdelenalajna.ElementAt(i).ToString());
				}
			}
			mapa1.Close ();
			return mapaNactena;
		}
		public void UlozMapu(int[,] mapa, string soubor) {
			if (File.Exists (soubor)) {
				File.Delete (soubor);
			}
			StreamWriter mapa0 = new StreamWriter(soubor);
			mapa0.WriteLine (mapa.GetLength(1));
			mapa0.WriteLine (mapa.GetLength(0));
			for(int a = 0;a<= mapa.GetLength(0)-1;a++){
				for(int b = 0;b<= mapa.GetLength(1)-1;b++){
					mapa0.Write (mapa[a,b].ToString());
					mapa0.Write (',');
				}
				mapa0.WriteLine ();
			}
			mapa0.Close ();
		}
	}
}