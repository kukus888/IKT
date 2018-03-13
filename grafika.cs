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
		mapPresets DefinovaneKrajiny = new mapPresets();

		public grafika ()
		{
		}
		public void HealthBar(int odolnost, int odolnostMax, int X, int Y, int delka,bool displayHP){
				bool barovani = true;
				delka++;
				int jednopole = odolnostMax / delka;
				int posunuti = 1;
				Console.ForegroundColor = ConsoleColor.White;
				char[] HPall = (odolnost.ToString() + "/" + odolnostMax.ToString()).ToCharArray();
				int psatHP = 0;
				int lenghtHP = odolnost.ToString ().Length + odolnostMax.ToString ().Length + 1;
				while (barovani) {
					Console.SetCursorPosition (X + posunuti - 1, Y);
					if (odolnost >= (jednopole * posunuti)) {
					if (posunuti >= ((delka / 2) - (lenghtHP / 2)) && posunuti <= ((delka / 2) + (lenghtHP / 2))) {
							Console.BackgroundColor = ConsoleColor.Green;
							try{
								Console.Write ("{0}",HPall[psatHP]);
							} catch {

							}
							psatHP++;
						} else {
							Console.BackgroundColor = ConsoleColor.Green;
							Console.Write (" ");
						}
					} else {
						if (posunuti >= ((delka / 2) - (lenghtHP / 2)) && posunuti <= ((delka / 2) + (lenghtHP / 2))) {
							Console.BackgroundColor = ConsoleColor.Red;
							try{
							Console.Write ("{0}",HPall[psatHP]);
							} catch {
								
							}
							psatHP++;
						} else {
							Console.BackgroundColor = ConsoleColor.Red;
							Console.Write (" ");
						}
					}

					if (posunuti >= delka) {
						barovani = false;
					}
					posunuti++;
				}
				Console.ResetColor ();
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
		public map[,] VygenerujMapu(int SirkaMapy, int VyskaMapy) {
			map[,] mapa = new map[SirkaMapy, VyskaMapy];
			//Odsud se nastavují pravděpodobnosti na generaci
			int PvPahorkatin = 35;
			int ProstHor = 10;
			int MnozstviDomu = (SirkaMapy / VyskaMapy) * 3;
			int VyskaVody = -11;
			//Konec generací
			int PvHor = 0;
			bool exitmapa = false;
			bool regenMap = true;
			int menuVyberY = 0;
			int menuVyberX = 0;
			int PrdpVetvi = 10;
			string[] menuMoznosti0 = new string[]{ "Vygenerovat novou mapu","Ponechat mapu", "Zpět do menu"};
			string[] menuMoznosti1 = new string[]{ "Změnit výšku vodní hladiny", "Změnit maximální počet měst", "Změnit pravděpodobnost generování větví"};
			while (exitmapa == false) {
				if (regenMap == true) {
					for (int dilkyX = 0; dilkyX <= mapa.GetLength (0) - 1; dilkyX++) {
						for (int dilkyY = 0; dilkyY <= mapa.GetLength (1) - 1; dilkyY++) {//DODELAT VYGENEROVANI podle okoli !!!!!!!!!MOMENTALNE ROZJEBANE!!!!!!!!!
							mapa [dilkyX, dilkyY] = DefinovaneKrajiny.Planiny;
						}
					}
					//GENERATOR MAPY, faze 2 zvedani terenu
					for (int dilkyX = 0; dilkyX <= mapa.GetLength (0) - 1; dilkyX++) {
						for (int dilkyY = 0; dilkyY <= mapa.GetLength (1) - 1; dilkyY++) {
							if (rnd.Next (0, 99) >= PvPahorkatin) {
								mapa [dilkyX, dilkyY] = DefinovaneKrajiny.Pahory;
							}
						}
					}
					//GENERATOR MAPY, faze 3 zvedani terenu podruhe
					for (int dilkyX = 0; dilkyX <= mapa.GetLength (0) - 1; dilkyX++) {
						for (int dilkyY = 0; dilkyY <= mapa.GetLength (1) - 1; dilkyY++) {
							PvHor = 0;
							#region countMountains
							try {
								if (mapa [dilkyX + 1, dilkyY].Height >= 2) {
									PvHor = PvHor + mapa [dilkyX + 1, dilkyY].Height;
								}
							} catch {
							}
							try {
								if (mapa [dilkyX - 1, dilkyY].Height >= 2) {
									PvHor = PvHor + mapa [dilkyX - 1, dilkyY].Height;
								}
							} catch {
							}
							try {
								if (mapa [dilkyX, dilkyY + 1].Height >= 2) {
									PvHor = PvHor + mapa [dilkyX, dilkyY + 1].Height;
								}
							} catch {
							}
							try {
								if (mapa [dilkyX, dilkyY - 1].Height >= 2) {
									PvHor = PvHor + mapa [dilkyX, dilkyY - 1].Height;
								}
							} catch {
							}
							try {
								if (mapa [dilkyX, dilkyY].Height >= 2) {
									PvHor = PvHor + mapa [dilkyX, dilkyY].Height;
								}
							} catch {
							}
							#endregion
							if (rnd.Next (0, 99) >= PvPahorkatin && PvHor >= ProstHor) {
								mapa [dilkyX, dilkyY] = DefinovaneKrajiny.Hory;
								try {
									mapa [dilkyX + 1, dilkyY] = DefinovaneKrajiny.HoryDop;
								} catch {
								}
							}
						}
					}
					//GENERATOR MAPY, faze 4 generovani vodstva
					double[,] vodniPrdp = new double[mapa.GetLength (0), mapa.GetLength (1)];
					for (int x = 0; x <= mapa.GetLength (0) - 1; x++) {
						for (int y = 0; y <= mapa.GetLength (1) - 1; y++) {
							vodniPrdp [x, y] = 0;
						}
					}
					for (int x = 0; x <= mapa.GetLength (0) - 1; x++) {
						for (int y = 0; y <= mapa.GetLength (1) - 1; y++) {
							try {
								vodniPrdp [x, y] = vodniPrdp [x, y] - mapa [x, y].Height;
							} catch {
							}
							try {
								vodniPrdp [x, y] = vodniPrdp [x, y] - mapa [x + 1, y].Height;
							} catch {
							}
							try {
								vodniPrdp [x, y] = vodniPrdp [x, y] - mapa [x - 1, y].Height;
							} catch {
							}
							try {
								vodniPrdp [x, y] = vodniPrdp [x, y] - mapa [x, y + 1].Height;
							} catch {
							}
							try {
								vodniPrdp [x, y] = vodniPrdp [x, y] - mapa [x, y - 1].Height;
							} catch {
							}
							try {
								vodniPrdp [x, y] = vodniPrdp [x, y] - (mapa [x + 1, y + 1].Height * 0.5);
							} catch {
							}
							try {
								vodniPrdp [x, y] = vodniPrdp [x, y] - (mapa [x - 1, y + 1].Height * 0.5);
							} catch {
							}
							try {
								vodniPrdp [x, y] = vodniPrdp [x, y] - (mapa [x + 1, y - 1].Height * 0.5);
							} catch {
							}
							try {
								vodniPrdp [x, y] = vodniPrdp [x, y] - (mapa [x - 1, y - 1].Height * 0.5);
							} catch {
							}
						}
					}
					for (int x = 0; x <= mapa.GetLength (0) - 1; x++) {
						for (int y = 0; y <= mapa.GetLength (1) - 1; y++) {
							if (vodniPrdp [x, y] >= VyskaVody && mapa [x, y].Replaceable == true) {
								mapa [x, y] = DefinovaneKrajiny.Reka;
							}
						}
					}
					//GENERATOR MAPY, faze 5 staveni domu
					int pocetMest = 0;
					double[,] dumPrdp = new double[mapa.GetLength (0), mapa.GetLength (1)];
					for (int x = 0; x <= mapa.GetLength (0) - 1; x++) {//pocitani nejlepsich mist pro vesnice
						for (int y = 0; y <= mapa.GetLength (1) - 1; y++) {
							try{
								if(mapa[x-1,y].Type == "River"){
									dumPrdp[x,y] = dumPrdp[x,y] + 1;
								}
							} catch {
							}
							try{
								if(mapa[x+1,y].Type == "River"){
									dumPrdp[x,y] = dumPrdp[x,y] + 1;
								}
							} catch {
							}
							try{
								if(mapa[x,y-1].Type == "River"){
									dumPrdp[x,y] = dumPrdp[x,y] + 1;
								}
							} catch {
							}
							try{
								if(mapa[x,y+1].Type == "River"){
									dumPrdp[x,y] = dumPrdp[x,y] + 1;
								}
							} catch {
							}

							dumPrdp [x, y] = dumPrdp [x, y]+Math.Abs(vodniPrdp [x, y]);
						}
					}

					double prumerPrdpDomu;
					double SumHodnot = 0;
					for (int x = 0; x <= mapa.GetLength (0) - 1; x++) {//pocitani prumeru domu
						for (int y = 0; y <= mapa.GetLength (1) - 1; y++) {
							SumHodnot = SumHodnot + dumPrdp[x,y];
						}
					}
					prumerPrdpDomu = SumHodnot / (mapa.GetLength(0)*mapa.GetLength(1));
					double Prdp75Domu = prumerPrdpDomu + (prumerPrdpDomu / 5);
					bool placeCity = true;
					while (placeCity) {
						int x = rnd.Next (0,mapa.GetLength(0)-1);
						int y = rnd.Next (0,mapa.GetLength(1)-1);
						if (pocetMest >= MnozstviDomu) {
							placeCity = false;
						}
						if (dumPrdp [x, y] >= Prdp75Domu && mapa[x,y].Type != "River" && mapa[x,y].Height <= 2) {
							mapa [x, y] = DefinovaneKrajiny.Mesto;
							pocetMest++;
						}
					}
					//GENERATOR MAPY, faze 6 pohazovani vetvi
					for (int x = 0; x <= mapa.GetLength (0) - 1; x++) {//aktualni placeovani vesnic
						for (int y = 0; y <= mapa.GetLength (1) - 1; y++) {
							if (rnd.Next (0, 99) <= PrdpVetvi && mapa[x,y].Type != "River" && mapa[x,y].Height <= 2 && mapa[x,y].Replaceable == true) {
								mapa [x, y] = DefinovaneKrajiny.Vetve;
							}
						}
					}

					regenMap = false;
				}

				bool menuOpen = true;
				while (menuOpen) {
					Console.BackgroundColor = ConsoleColor.Black;
					Console.ForegroundColor = ConsoleColor.White;
					Console.Clear ();
					VykresliMapu (mapa, 1, 1);

					Console.BackgroundColor = ConsoleColor.Black;
					Console.ForegroundColor = ConsoleColor.White;
					for (int i = 0; i <= menuMoznosti1.GetLength (0) - 1; i++) {
						Console.SetCursorPosition (mapa.GetLength (0) + 2, mapa.GetLength (1) + 2 + i);
						if (menuVyberY == i && menuVyberX == 1) {
							Console.Write ("> ");
						}
						Console.Write (menuMoznosti1 [i]);
					}
					Console.SetCursorPosition (mapa.GetLength(0) + 2,1);
					Console.Write ("Výška vodní hladiny: ");
					Console.Write (Math.Abs(VyskaVody));
					Console.SetCursorPosition (mapa.GetLength(0) + 2,2);
					Console.Write ("Maximální počet domů: ");
					Console.Write (Math.Abs(MnozstviDomu));
					Console.SetCursorPosition (mapa.GetLength(0) + 2,3);
					Console.Write ("Pravděpodobnost generování větví: {0}%",PrdpVetvi);

					Console.BackgroundColor = ConsoleColor.Black;
					Console.ForegroundColor = ConsoleColor.White;
					for (int i = 0; i <= menuMoznosti0.GetLength (0) - 1; i++) {
						Console.SetCursorPosition (1, VyskaMapy + 2 + i);
						if (menuVyberY == i && menuVyberX == 0) {
							Console.Write ("> ");
						}
						Console.Write (menuMoznosti0[i]);
					}
					ConsoleKeyInfo vstup = Console.ReadKey ();
					if (vstup.Key == ConsoleKey.Enter) {
						if (menuVyberY == 0 && menuVyberX == 0) {//Nová mapa
							exitmapa = false;
							regenMap = true;
							menuOpen = false;
						}
						if (menuVyberY == 1 && menuVyberX == 0) {//Ponechat mapu
							exitmapa = true;
							menuOpen = false;
						}
						if (menuVyberY == 2 && menuVyberX == 0) {//Zpět do menu
						}
						if (menuVyberY == 0 && menuVyberX == 1) {//zmenit vysku hladiny
							Console.CursorVisible = true;
							Console.Clear();
							Console.SetCursorPosition (1,1);
							Console.Write ("Změna výšky hladiny");
							Console.SetCursorPosition (1,2);
							Console.Write ("Aktuálně: {0}",Math.Abs(VyskaVody));
							Console.SetCursorPosition (1,3);
							Console.Write ("Nová hodnota: ");
							string novaVyskaVody = Console.ReadLine ();
							Console.CursorVisible = false;
							try{
								VyskaVody = Int32.Parse(("-"+novaVyskaVody));
								regenMap = true;
								menuOpen = false;
							} catch (System.FormatException){
								Console.Clear ();
							}
						}
						if (menuVyberY == 1 && menuVyberX == 1) {//zmenit pocet domu
							Console.CursorVisible = true;
							Console.Clear();
							Console.SetCursorPosition (1,1);
							Console.Write ("Změna maximálního počtu měst");
							Console.SetCursorPosition (1,2);
							Console.Write ("Aktuálně: {0}%",Math.Abs(MnozstviDomu));
							Console.SetCursorPosition (1,3);
							Console.Write ("Nová hodnota: ");
							string novyPocetDomu = Console.ReadLine ();
							Console.CursorVisible = false;
							try{
								MnozstviDomu = Int32.Parse((novyPocetDomu));
								regenMap = true;
								menuOpen = false;
							} catch (System.FormatException){
								Console.Clear ();
							}
						}
						if (menuVyberY == 2 && menuVyberX == 1) {//zmenit prdp vetvi
							Console.CursorVisible = true;
							Console.Clear();
							Console.SetCursorPosition (1,1);
							Console.Write ("Změna procenta pravděpodobnosti generace větví");
							Console.SetCursorPosition (1,2);
							Console.Write ("Aktuálně: {0}",Math.Abs(PrdpVetvi));
							Console.SetCursorPosition (1,3);
							Console.Write ("Nová hodnota: ");
							string novyPrdpVetvi = Console.ReadLine ();
							Console.CursorVisible = false;
							try{
								PrdpVetvi = Int32.Parse((novyPrdpVetvi));
								regenMap = true;
								menuOpen = false;
							} catch (System.FormatException){
								Console.Clear ();
							}
						}
					}
					if (vstup.Key == ConsoleKey.W || vstup.Key == ConsoleKey.UpArrow) {
						menuVyberY--;
					}
					if (vstup.Key == ConsoleKey.S || vstup.Key == ConsoleKey.DownArrow) {
						menuVyberY++;
					}
					if (vstup.Key == ConsoleKey.A || vstup.Key == ConsoleKey.LeftArrow) {
						menuVyberX--;
					}
					if (vstup.Key == ConsoleKey.D || vstup.Key == ConsoleKey.RightArrow) {
						menuVyberX++;
					}
					if (menuVyberY <= -1 && menuVyberX == 0) {
						menuVyberY = menuMoznosti0.GetLength (0) - 1;
					}
					if (menuVyberY >= menuMoznosti0.GetLength (0) && menuVyberX == 0) {
						menuVyberY = 0;
					}
					if (menuVyberY <= -1 && menuVyberX == 1) {
						menuVyberY = menuMoznosti1.GetLength (0) - 1;
					}
					if (menuVyberY >= menuMoznosti1.GetLength (0) && menuVyberX == 1) {
						menuVyberY = 0;
					}
					if (menuVyberX <= -1) {
						menuVyberX = 1;
					}
					if (menuVyberX >= 2) {
						menuVyberX = 0;
					}
				}
			}
			return mapa;
		}
		public void Vysvetlivky() {
			string[] vysv = new string[] { "[ESC] - Menu","[WASD] - Pohyb","[E] - Vstoupit do vesnice / Otevřít inventář","[F] - Sebrat věc z země"};
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
		public void VykresliMapu(map[,] mapa, int offsetX, int offsetY) {
			for (int x = 0; x <= mapa.GetLength (0) - 1; x++) {
				for (int y = 0; y <= mapa.GetLength (1) - 1; y++) {
					try {
						if (mapa [x, y].Print == true) {
							Console.BackgroundColor = mapa [x, y].TileBackground;
							Console.ForegroundColor = mapa [x, y].TileForeground;
							Console.SetCursorPosition (x + offsetX, y + offsetY);
							Console.Write (mapa [x, y].Tile);
							if(mapa[x,y].Type == "LandscapeExtended"){
								Console.Write(" ");
							}
						}
					} catch (System.NullReferenceException) {
						
					}
				}
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
			map[,] mapa = new map[sloupce,radky];
			bool kreslimapu = true;
			for (int i = 0; i <= mapa.GetLength (0) - 1; i++) {
				for (int a = 0; a <= mapa.GetLength (1) - 1; a++) {
					mapa[i,a] = DefinovaneKrajiny.Planiny;//TOTO BYS MĚL OPRAVIT!!!
				}
			}
			int posX = 0;
			int posY = 0;
			map[] buffer = new map[10];
			string[] moznosti = new string[] { "[WASD] - Pohyb po mapě","[ESC] - Menu","[E] - Vybrat stavěcí pole", "[Q] - Nástroje", "[F] - Zvýraznit kurzor"};
			string[] escmenu = new string[] { "Zpět do editoru","Uložit","Načíst","Spravovat soubory","Ukončit editor","Změna resource packu"};
			bool zvyraznitKurzor = false;
			while (kreslimapu) {
				Console.BackgroundColor = ConsoleColor.Black;
				Console.ForegroundColor = ConsoleColor.White;
				Console.Clear ();
				VykresliMapu (mapa,2,1);
				Console.BackgroundColor = ConsoleColor.Black;
				Console.ForegroundColor = ConsoleColor.White;
				for (int x = 0; x <= moznosti.GetLength (0)-1; x++) {
					Console.SetCursorPosition (mapa.GetLength(1) + 2, 1+x);
					Console.WriteLine (moznosti[x]);
				}
				for (int i = 0; i <= buffer.GetLength (0)-1; i++) {
					Console.SetCursorPosition (mapa.GetLength(1) + 2,i+moznosti.GetLength(0)+2);
					//Console.Write ("{0} - {1}",i,hodnoty.RP[buffer[i],4]);
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
				if (vstup.Key == ConsoleKey.D0) { mapa [posY, posX] = DefinovaneKrajiny.Planiny;}
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
							map vylitblokem = null;
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
								/*if (v == buffer [i]) {
									nazevBuf = hodnoty.RP[v,4];
									break;
								}*/
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
									//buffer[vyberbufferu] = Int32.Parse(hodnoty.RP[indexbufferu,0]);
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
		public map[,] NactiMapu(string soubor) {
			StreamReader mapa1 = new StreamReader(soubor);
			map[,] mapa = null;
			int sirkamapy1 = -1;
			int vyskamapy1 = -1;
			bool mapaConstructed = false;
			bool loaded = false;
			while (loaded == false) {
				string data = mapa1.ReadLine ();
				string[] dataSplitted = data.Split (' ');
				if (dataSplitted [0] == "MapWidth") {
					sirkamapy1 = Int32.Parse (dataSplitted [1]);
				}
				if (dataSplitted [0] == "MapHeight") {
					vyskamapy1 = Int32.Parse (dataSplitted [1]);
				}
				if (sirkamapy1 != -1 && vyskamapy1 != -1 && mapaConstructed == false) {
					mapa = new map[sirkamapy1,vyskamapy1];
					mapaConstructed = true;
				}
				if (dataSplitted [0] == "MapBlock") {
					int X = Int32.Parse(dataSplitted [1]);
					int Y = Int32.Parse(dataSplitted [2]);
					bool MapBlockWriting = true;
					map docasne = new map ();
					docasne.IsWalkable = true;
					docasne.Print = true;
					while (MapBlockWriting) {
						string[] dataMap = mapa1.ReadLine ().Split (' ');
						if (dataMap [0] == "Name") {
							docasne.Name = dataMap [1];
						}
						if (dataMap [0] == "Tile") {
							docasne.Tile = dataMap [1];
						}
						if (dataMap [0] == "TileBackground") {
							docasne.TileBackground = (ConsoleColor) Enum.Parse(typeof(ConsoleColor), dataMap [1]);
						}
						if (dataMap [0] == "TileForeground") {
							docasne.TileForeground = (ConsoleColor) Enum.Parse(typeof(ConsoleColor), dataMap [1]);
						}
						if (dataMap [0] == "PlayerColor") {
							docasne.PlayerColor = (ConsoleColor) Enum.Parse(typeof(ConsoleColor), dataMap [1]);
						}
						if (dataMap [0] == "Height") {
							docasne.Height = Int32.Parse(dataMap [1]);
						}
						if (dataMap [0] == "IsWalkable") {
							docasne.IsWalkable = Boolean.Parse(dataMap [1]);
						}
						if (dataMap [0] == "Print") {
							docasne.Print = Boolean.Parse(dataMap [1]);
						}
						if (dataMap [0] == "Replaceable") {
							docasne.Replaceable = Boolean.Parse(dataMap [1]);
						}
						if (dataMap [0] == "Type") {
							docasne.Type = dataMap[1];
						}
						if (dataMap [0] == "EndOfMapBlock") {
							MapBlockWriting = false;
						}
					}
					mapa [X, Y] = docasne;
					docasne = null;
				}


				if (mapa1.EndOfStream == true) {
					loaded = true;
					break;
				}
			}
			return mapa;
		}
		public void UlozMapu(map[,] mapa, string soubor) {
			if (File.Exists (soubor)) {
				File.Delete (soubor);
			}
			StreamWriter mapa0 = new StreamWriter(soubor);
			mapa0.WriteLine ("MapWidth {0}",mapa.GetLength(0));
			mapa0.WriteLine ("MapHeight {0}",mapa.GetLength(1));
			for(int a = 0;a<= mapa.GetLength(0)-1;a++){
				for(int b = 0;b<= mapa.GetLength(1)-1;b++){
					mapa0.WriteLine ("MapBlock {0} {1}",a,b);
					mapa0.WriteLine ("Name {0}",mapa[a,b].Name);
					mapa0.WriteLine ("Tile {0}",mapa[a,b].Tile);
					mapa0.WriteLine ("TileBackground {0}",mapa[a,b].TileBackground);
					mapa0.WriteLine ("TileForeground {0}",mapa[a,b].TileForeground);
					mapa0.WriteLine ("PlayerColor {0}",mapa[a,b].PlayerColor);
					mapa0.WriteLine ("Height {0}",mapa[a,b].Height);
					mapa0.WriteLine ("IsWalkable {0}",mapa[a,b].IsWalkable);
					mapa0.WriteLine ("Print {0}",mapa[a,b].Print);
					mapa0.WriteLine ("Replaceable {0}",mapa[a,b].Replaceable);
					mapa0.WriteLine ("Type {0}",mapa[a,b].Type);

					mapa0.WriteLine ("EndOfMapBlock");
				}
			}
			mapa0.Close ();
		}
	}
}