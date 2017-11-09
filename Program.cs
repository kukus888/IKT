using System;
using System.Threading;

namespace IKT
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			string verze = "0.1.0";

			int poziceKurzoruX = 0;
			int poziceKurzoruY = 0;
			int nejvetsisirka = 80;
			int nejvetsivyska = 24;

			string systemOS = Environment.OSVersion.ToString ();
			Console.WriteLine ("Pouzivate system: {0}",Environment.OSVersion.ToString());
			Console.WriteLine ("projekt IKT");
			Console.WriteLine ("verze: {0}",verze);
			if(systemOS.Contains("Unix")) {
				Console.WriteLine ("VAROVANI!!! Zmen si velikost konzole nyni, prizpusobi se pouze ted!");
			}
			Console.WriteLine ("Zmackni jakoukoliv klavesu pro pokracovani...");
			Console.ReadKey();
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
			int volba = 1;
			while (true) {
				Console.BackgroundColor = ConsoleColor.Blue;
				Console.ForegroundColor = ConsoleColor.White;
				if(volba == 1) {
					Console.Clear ();
					Console.SetCursorPosition (nejvetsisirka/2,nejvetsivyska/2-1);
					Console.WriteLine ("> HRA");
					Console.SetCursorPosition (nejvetsisirka/2,nejvetsivyska/2);
					Console.WriteLine ("KONEC");
				}
				if(volba == 2) {
					Console.Clear ();
					Console.SetCursorPosition (nejvetsisirka/2,nejvetsivyska/2-1);
					Console.WriteLine ("HRA");
					Console.SetCursorPosition (nejvetsisirka/2,nejvetsivyska/2);
					Console.WriteLine ("> KONEC");
				}
				Console.SetCursorPosition (poziceKurzoruX,poziceKurzoruY);
				ConsoleKeyInfo vstup = Console.ReadKey ();
				if (vstup.Key == ConsoleKey.W) {
					volba--;
					//Console.WriteLine ("W: {0}",volba);
				}
				if (vstup.Key == ConsoleKey.S) {
					volba++;
					//Console.WriteLine ("S: {0}",volba);
				}
				if (vstup.Key == ConsoleKey.Enter ) {
					if (volba == 1) {
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
					if (volba == 2) {
						//ZDE SE SKONCI
						Console.Clear();
						Console.SetCursorPosition (nejvetsisirka/2-23,nejvetsivyska/2);
						Console.WriteLine ("Zmackni libovolnou klavesu pro opusteni hry...");
						Console.SetCursorPosition (poziceKurzoruX,poziceKurzoruY);
						Console.ReadKey ();
						break;
					}	
				}
				if (volba >= 3 || volba <= 0) {
					volba = 1;
				}
			}
		}
		public static void hra() {
			//Zacina hra
			//8 biomů
			int sirkamapy = 32;//zde si muzes nastavit sirku mapy
			int vyskamapy = 16;//zde si muzes nastavit vysku mapy

			bool exitmapa = false;//NEMENIT! Laka te to? tak to zkus! :D
			Random rnd = new Random ();//NEMENIT! Rozjebes fungovani compileru

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

			Console.BackgroundColor = ConsoleColor.Black;
			Console.Clear ();

			int[,] mapa = new int[vyskamapy,sirkamapy];

			while (exitmapa == false) {
			//int[,] mapa = new int[vyskamapy,sirkamapy];//GENERATOR MAPY, faze 1
			Console.SetCursorPosition (poziceKurzoruX,poziceKurzoruY);
			Console.WriteLine ("Generuji mapu...");
			for(int dilky = 0;dilky <=vyskamapy-1;dilky++) {
				for(int dilky1 = 0;dilky1 <=sirkamapy-1;dilky1++) {//DODELAT VYGENEROVANI podle okoli !!!!!!!!!MOMENTALNE ROZJEBANE!!!!!!!!!
					mapa [dilky,dilky1] = 0;
			}	}
			Console.WriteLine ("Zvedam teren...");//GENERATOR MAPY, faze 2 zvedani terenu
			for(int dilky = 0;dilky <=vyskamapy-1;dilky++) {
				for(int dilky1 = 0;dilky1 <=sirkamapy-1;dilky1++) {
					int mnoznostpahorkatin = rnd.Next (0,10);
					if(mnoznostpahorkatin <= 2) {
					mapa [dilky,dilky1] = 1;
					}	}	}
			Console.WriteLine ("Zvedam teren podruhe...");//GENERATOR MAPY, faze 3 zvedani terenu podruhe
			for(int dilky = 0;dilky <=vyskamapy-1;dilky++) {
				for(int dilky1 = 0;dilky1 <=sirkamapy-1;dilky1++) {
					int mnoznosthor = rnd.Next (0,10);
					if(mapa[dilky,dilky1]==1 && mnoznosthor == 1) {
						mapa [dilky,dilky1] = 2;
					}	}	}
			bool pokus = false;
			while(pokus == false) {
				Console.WriteLine ("Vylivam vodu...");//GENERATOR MAPY, faze 4, generovani vodstva
				int mnoznostvodstva1 = rnd.Next(0,sirkamapy);
				int pozicevodstvy = mnoznostvodstva1;
				try{
					for(int vodstvo1 = 0;vodstvo1 <=sirkamapy-1;vodstvo1++) {mapa[pozicevodstvy,vodstvo1]=3;}
					pokus = true;
				}
				catch (System.IndexOutOfRangeException)  // CS0168
				{
					Console.WriteLine ("Ale toto je nepříjemné! Chyba: System.IndexOutOfRangeException ");
					Console.WriteLine ("Zmacknete libovolnou klavesu pro pokracovani...");
				}
			}
			Console.WriteLine ("Stavim domy...");//GENERATOR MAPY, faze 5 staveni domu
			for(int dilky = 0;dilky <=vyskamapy-1;dilky++) {
				for(int dilky1 = 0;dilky1 <=sirkamapy-1;dilky1++) {
					int mnoznostdomu = rnd.Next (0,100);
					if(mnoznostdomu == 1 && mapa[dilky,dilky1] != 3 && mapa[dilky,dilky1] != 5 && mapa[dilky,dilky1] != 6) {
						mapa [dilky,dilky1] = 6;
					}	}	}
			Console.WriteLine ("Pohazuji vetve...");//GENERATOR MAPY, faze 6 pohazovani vetvi
			for(int dilky = 0;dilky <=vyskamapy-1;dilky++) {
				for(int dilky1 = 0;dilky1 <=sirkamapy-1;dilky1++) {
					int mnoznostvetvi = rnd.Next (0,10);
					if(mnoznostvetvi == 1 && mapa[dilky,dilky1] != 1 && mapa[dilky,dilky1] != 3 && mapa[dilky,dilky1] != 5 && mapa[dilky,dilky1] != 6) {
						mapa [dilky,dilky1] = 7;
					}	}	}
				Console.BackgroundColor = ConsoleColor.Black;
				Console.ForegroundColor = ConsoleColor.White;
				Console.Clear ();
				int vykresliX = 1;//VYKRESLENI MAPY
				int zaklvykresliX = vykresliX;
				int radky = 1;
				for (int vykr = 0; vykr <= vyskamapy-1; vykr++) {
					for (int vykr1 = 0; vykr1 <= sirkamapy-1; vykr1++) {
						Console.BackgroundColor = ConsoleColor.Green;
						Console.ForegroundColor = ConsoleColor.White;
						Console.SetCursorPosition (vykresliX, radky);
						if(mapa [vykr, vykr1] == 0) {Console.Write ("░");}
						Console.BackgroundColor = ConsoleColor.DarkGreen;
						Console.ForegroundColor = ConsoleColor.DarkYellow;
						if(mapa [vykr, vykr1] == 1) {Console.Write ("▒");}
						Console.BackgroundColor = ConsoleColor.DarkGray;
						Console.ForegroundColor = ConsoleColor.Gray;
						if(mapa [vykr, vykr1] == 2) {Console.Write ("▓");}
						Console.BackgroundColor = ConsoleColor.Blue;
						Console.ForegroundColor = ConsoleColor.White;
						if(mapa [vykr, vykr1] == 3) {Console.Write ("⛆");}
						if(mapa [vykr, vykr1] == 4) {Console.Write (" ");}
						if(mapa [vykr, vykr1] == 5) {Console.Write ("ᚙ");}
						Console.ForegroundColor = ConsoleColor.Black;
						Console.BackgroundColor = ConsoleColor.Green;
						if(mapa [vykr, vykr1] == 6) {Console.Write ("⌂");}
						Console.BackgroundColor = ConsoleColor.Green;
						Console.ForegroundColor = ConsoleColor.DarkYellow;
						if(mapa [vykr, vykr1] == 7) {Console.Write ("ܓ");}
						vykresliX++;
					}
					Console.WriteLine ("");
					radky++;
					vykresliX = zaklvykresliX;
				}
				Console.BackgroundColor = ConsoleColor.Black;
				Console.ForegroundColor = ConsoleColor.White;
				Console.SetCursorPosition (1, vyskamapy+2);
				Console.WriteLine ("Chcete ponechat mapu? Y/n");
				Console.SetCursorPosition (1, vyskamapy+3);
				ConsoleKeyInfo vstup = Console.ReadKey ();
				if(vstup.Key == ConsoleKey.Y) {
					exitmapa = true;
				}
				if(vstup.Key == ConsoleKey.N) {
					exitmapa = false;
				}
			}
			//Zde by se mel vybirat hrdina
			string charakter = "NULL";
			int sila = 0;
			int odolnost = 0;
			int rychlost = 0;
			int inteligence = 0;
			int mana = 0;
			int exp = 0;
			Console.Clear();
			bool exithrdina= false;
			int volbahrdiny = 1;
			string[,] inventar = new string[11,10];//Generace inventare

			for(int i = 0;i<= 10;i++) {
				for(int j = 0;j <= 9;j++) {
					inventar[i,j] = "0";
				}
			}
			while(exithrdina==false) {
				Console.Clear ();
				Console.SetCursorPosition (1,1);
				Console.WriteLine ("Nyni si vyber hrdinu:");
				Console.SetCursorPosition (poziceKurzoruX,poziceKurzoruY);
				if(volbahrdiny == 1) {
					Console.BackgroundColor = ConsoleColor.Black;
					Console.ForegroundColor = ConsoleColor.White;
					Console.SetCursorPosition (1,2);
					Console.WriteLine ("> Bojovnik");
					Console.SetCursorPosition (1,3);
					Console.WriteLine ("Lucistnik");
					Console.SetCursorPosition (1,4);
					Console.WriteLine ("Carodej");
					Console.SetCursorPosition (1,5);
					Console.WriteLine ("Lahvickar");

					Console.SetCursorPosition (1,7);
					Console.WriteLine ("      Sila: 120 ");
					Console.WriteLine ("   Odolnost: 80");
					Console.WriteLine ("   Rychlost: 35");
					Console.WriteLine ("Inteligence: 40");
					Console.WriteLine ("       Mana: 5");
					Console.WriteLine ("Specialni atributy: Nejsilnejsi a nejodolnejsi bojovnik.");
				}
				if(volbahrdiny == 2) {
					Console.BackgroundColor = ConsoleColor.Black;
					Console.ForegroundColor = ConsoleColor.White;
					Console.SetCursorPosition (1,2);
					Console.WriteLine ("Bojovnik");
					Console.SetCursorPosition (1,3);
					Console.WriteLine ("> Lucistnik");
					Console.SetCursorPosition (1,4);
					Console.WriteLine ("Carodej");
					Console.SetCursorPosition (1,5);
					Console.WriteLine ("Lahvickar");

					Console.SetCursorPosition (1,7);
					Console.WriteLine ("      Sila: 40");
					Console.WriteLine ("   Odolnost: 40");
					Console.WriteLine ("   Rychlost: 80");
					Console.WriteLine ("Inteligence: 60");
					Console.WriteLine ("       Mana: 10");
					Console.WriteLine ("Specialni atributy: Vidi o kus dal nez vsichni ostatni a tak muze nepratele sestrelit.");
				}
				if(volbahrdiny == 3) {
					Console.BackgroundColor = ConsoleColor.Black;
					Console.ForegroundColor = ConsoleColor.White;
					Console.SetCursorPosition (1,2);
					Console.WriteLine ("Bojovnik");
					Console.SetCursorPosition (1,3);
					Console.WriteLine ("Lucistnik");
					Console.SetCursorPosition (1,4);
					Console.WriteLine ("> Carodej");
					Console.SetCursorPosition (1,5);
					Console.WriteLine ("Lahvickar");

					Console.SetCursorPosition (1,7);
					Console.WriteLine ("      Sila: 55");
					Console.WriteLine ("   Odolnost: 20");
					Console.WriteLine ("   Rychlost: 55");
					Console.WriteLine ("Inteligence: 80");
					Console.WriteLine ("       Mana: 100");
					Console.WriteLine ("Specialni atributy: Muze pouzivat vseruzne kouzla.");
				}
				if(volbahrdiny == 4) {
					Console.BackgroundColor = ConsoleColor.Black;
					Console.ForegroundColor = ConsoleColor.White;
					Console.SetCursorPosition (1,2);
					Console.WriteLine ("Bojovnik");
					Console.SetCursorPosition (1,3);
					Console.WriteLine ("Lucistnik");
					Console.SetCursorPosition (1,4);
					Console.WriteLine ("Carodej");
					Console.SetCursorPosition (1,5);
					Console.WriteLine ("> Lahvickar");

					Console.SetCursorPosition (1,7);
					Console.WriteLine ("      Sila: 50");
					Console.WriteLine ("   Odolnost: 20");
					Console.WriteLine ("   Rychlost: 60");
					Console.WriteLine ("Inteligence: 70");
					Console.WriteLine ("       Mana: 20");
					Console.WriteLine ("Specialni atributy: Lahvickar ma moznost pouzit lektvary, usnadnujici at uz boj, obranu, regeneraci, ci cokoliv jineho.");
				}
				Console.SetCursorPosition (poziceKurzoruX,poziceKurzoruY);
				ConsoleKeyInfo vstup = Console.ReadKey ();
				if (vstup.Key == ConsoleKey.W) {
					volbahrdiny--;
				}
				if (vstup.Key == ConsoleKey.S) {
					volbahrdiny++;
				}
				if (vstup.Key == ConsoleKey.Enter) {
					if(volbahrdiny == 1) {charakter = "Bojovnik"; sila = 120; odolnost = 80; rychlost = 35; inteligence = 40; mana = 5;inventar[0,0]="Zelezny mec";inventar[0,1] = "Zakladni mec. Nedava moc poskozeni.";inventar[0,2]="zbran";inventar [0, 3] = "10";inventar [0, 4] = "20";inventar[1,0]="Dratena kosile";inventar[1,1] = "Zakladni brneni.";inventar[1,2]="brneni";inventar [1, 3] = "10";inventar [1, 4] = "30";inventar[2,0]="Chleba";inventar[2,1] = "Klasicky psenicny chleb.";inventar[2,2]="jidlo";inventar [2, 3] = "10";inventar [2, 4] = "2";}
					if(volbahrdiny == 2) {charakter = "Lucistnik"; sila = 40; odolnost = 40; rychlost = 80; inteligence = 60; mana = 10;}
					if(volbahrdiny == 3) {charakter = "Carodej"; sila = 50; odolnost = 20; rychlost = 60; inteligence = 70; mana = 20;}
					if(volbahrdiny == 4) {charakter = "Lahvickar"; sila = 55; odolnost = 20; rychlost = 55; inteligence = 80; mana = 100;}
					exithrdina = true;
				}
				if(volbahrdiny <=0|| volbahrdiny >= 5) {
					volbahrdiny = 1;
				}
			}
			//Vysvetlivky
			Console.BackgroundColor = ConsoleColor.Black;
			Console.ForegroundColor = ConsoleColor.White;
			Console.Clear();
			Console.SetCursorPosition (1,1);//vysvetlivky pohybu a akci
			Console.WriteLine ("[WASD] - Pohyb");
			Console.SetCursorPosition (1,2);
			Console.WriteLine ("[Q] - Vyckavat");
			Console.SetCursorPosition (1,3);
			Console.WriteLine ("[R] - vstoupit do vesnice");
			Console.SetCursorPosition (1,4);
			Console.WriteLine ("[ESC] - Menu");
			Console.SetCursorPosition (1,5);
			Console.WriteLine ("[E] - Otevrit inventar");
			Console.SetCursorPosition (1,6);
			Console.WriteLine ("Zmackni jakoukoliv klavesu pro pokracovani...");
			Console.SetCursorPosition (poziceKurzoruX,poziceKurzoruY);
			Console.ReadKey ();

			//Hra by mela zapocat
			bool exitgame = false;
			int charakterX = 1;
			int charakterY = 1;
			bool cheat = false;
			string oznameni = " ";
			int zlato = 0;

			while(exitgame == false) {
				Console.BackgroundColor = ConsoleColor.Black;
				Console.ForegroundColor = ConsoleColor.White;
				Console.Clear ();
				int vykresliX = 1;//VYKRESLENI MAPY
				int zaklvykresliX = vykresliX;
				int radky = 1;
				for (int vykr = 0; vykr <= vyskamapy - 1; vykr++) {
					for (int vykr1 = 0; vykr1 <= sirkamapy - 1; vykr1++) {
						Console.BackgroundColor = ConsoleColor.Green;
						Console.ForegroundColor = ConsoleColor.White;
						Console.SetCursorPosition (vykresliX, radky);
						if(mapa [vykr, vykr1] == 0) {Console.Write ("░");}
						Console.BackgroundColor = ConsoleColor.DarkGreen;
						Console.ForegroundColor = ConsoleColor.DarkYellow;
						if(mapa [vykr, vykr1] == 1) {Console.Write ("▒");}
						Console.BackgroundColor = ConsoleColor.DarkGray;
						Console.ForegroundColor = ConsoleColor.Gray;
						if(mapa [vykr, vykr1] == 2) {Console.Write ("▓");}
						Console.BackgroundColor = ConsoleColor.Blue;
						Console.ForegroundColor = ConsoleColor.White;
						if(mapa [vykr, vykr1] == 3) {Console.Write ("⛆");}
						if(mapa [vykr, vykr1] == 4) {Console.Write (" ");}
						Console.ForegroundColor = ConsoleColor.Black;
						Console.BackgroundColor = ConsoleColor.Green;
						if(mapa [vykr, vykr1] == 5) {Console.Write ("ᚙ");}
						Console.ForegroundColor = ConsoleColor.Black;
						Console.BackgroundColor = ConsoleColor.Green;
						if(mapa [vykr, vykr1] == 6) {Console.Write ("⌂");}
						Console.BackgroundColor = ConsoleColor.Green;
						Console.ForegroundColor = ConsoleColor.DarkYellow;
						if(mapa [vykr, vykr1] == 7) {Console.Write ("ܓ");}
						vykresliX++;
					}
					Console.WriteLine ("");
					radky++;
					vykresliX = zaklvykresliX;
				}
				/*const int pozX = 33;   //VYKRESLENI MAPY CISLY JEN PRO KONTROLU
				int newpozX = 33;
				int radky3 = 1;
				for(int ex=0;ex<=vyskamapy-1;ex++){
					for(int ix=0;ix<=sirkamapy-1;ix++) {
						Console.SetCursorPosition (newpozX,radky3);
						Console.Write(mapa[ex,ix]);
						newpozX++;
					}
					newpozX = pozX;
					radky3++;
				}*/
				Console.BackgroundColor = ConsoleColor.Green;
				Console.ForegroundColor = ConsoleColor.Black;
				Console.SetCursorPosition (charakterX,charakterY);
				if (cheat == false) {Console.WriteLine ("\uC637");}//normalni panacek
				if (cheat == true) {Console.WriteLine ("\uC634");}//cheat panacek
				Console.BackgroundColor = ConsoleColor.Black;
				Console.ForegroundColor = ConsoleColor.White;
				Console.SetCursorPosition (1,0);//          NAPIS MAPA
				Console.WriteLine ("MAPA:");
				Console.SetCursorPosition (1,vyskamapy+1);//OZNAMOVACI RADEK
				Console.WriteLine ("Oznamovaci radek:");
				Console.SetCursorPosition (1,vyskamapy+2);
				Console.WriteLine (oznameni);
				oznameni = " ";//resetuje oznameni
				Console.SetCursorPosition (sirkamapy+2,1);//NA CEM STOJIS
				Console.Write ("Na cem stojis: ");
				string stojiciblok = "ERROR";
				string stojiciblok1 = "ERROR";
				int promennaprourcenipozicenamapeX = charakterY - 1;
				int promennaprourcenipozicenamapeY = charakterX - 1;
				if(mapa[promennaprourcenipozicenamapeX,promennaprourcenipozicenamapeY] == 0){stojiciblok = "Planiny";}
				if(mapa[promennaprourcenipozicenamapeX,promennaprourcenipozicenamapeY] == 1){stojiciblok = "Pahorkatiny";}
				if(mapa[promennaprourcenipozicenamapeX,promennaprourcenipozicenamapeY] == 2){stojiciblok = "Hory";}
				if(mapa[promennaprourcenipozicenamapeX,promennaprourcenipozicenamapeY] == 3){stojiciblok = "Voda";}
				if(mapa[promennaprourcenipozicenamapeX,promennaprourcenipozicenamapeY] == 4){stojiciblok = "Nic";}
				if(mapa[promennaprourcenipozicenamapeX,promennaprourcenipozicenamapeY] == 5){stojiciblok = "Dungeon";}
				if(mapa[promennaprourcenipozicenamapeX,promennaprourcenipozicenamapeY] == 6){stojiciblok = "Vesnice";}
				if(mapa[promennaprourcenipozicenamapeX,promennaprourcenipozicenamapeY] == 7){stojiciblok = "Vetve";}
				if(mapa[promennaprourcenipozicenamapeX,promennaprourcenipozicenamapeY+1] == 0){stojiciblok1 = "Planiny";}
				if(mapa[promennaprourcenipozicenamapeX,promennaprourcenipozicenamapeY+1] == 1){stojiciblok1 = "Pahorkatiny";}
				if(mapa[promennaprourcenipozicenamapeX,promennaprourcenipozicenamapeY+1] == 2){stojiciblok1 = "Hory";}
				if(mapa[promennaprourcenipozicenamapeX,promennaprourcenipozicenamapeY+1] == 3){stojiciblok1 = "Voda";}
				if(mapa[promennaprourcenipozicenamapeX,promennaprourcenipozicenamapeY+1] == 4){stojiciblok1 = "Nic";}
				if(mapa[promennaprourcenipozicenamapeX,promennaprourcenipozicenamapeY+1] == 5){stojiciblok1 = "Dungeon";}
				if(mapa[promennaprourcenipozicenamapeX,promennaprourcenipozicenamapeY+1] == 6){stojiciblok1 = "Vesnice";}
				if(mapa[promennaprourcenipozicenamapeX,promennaprourcenipozicenamapeY+1] == 7){stojiciblok1 = "Vetve";}
				Console.Write("{0} / {1}",stojiciblok,stojiciblok1);
				if(cheat == true) {
					//Console.SetCursorPosition (1,vyskamapy+3);//CHEAT MODE ACTIVATED/DEACTIVATED
					//Console.WriteLine ("CHEAT MODE ACTIVATED");
					sila = 1000; odolnost = 1000; rychlost = 1000; inteligence = 1000; mana = 1000; exp = 1000; charakter = "Admin"; zlato = 1000000;
				}
				if (cheat == true) {
					Console.SetCursorPosition (sirkamapy+2,2);//Info o charakteru v CHEAT MODU
					Console.WriteLine("{0}:EXP = {1}",charakter,exp);
					Console.SetCursorPosition (sirkamapy+3,3);
					Console.WriteLine ("GOD MODE ACTIVATED");
				}
				if(cheat == false) {
					Console.SetCursorPosition (sirkamapy+2,2);//Info o charakteru
					Console.WriteLine("{0}:EXP = {1}:{2}/{3}/{4}/{5}/{6}",charakter,exp,sila,odolnost,rychlost,inteligence,mana);
					Console.SetCursorPosition (sirkamapy+3,3);
					Console.WriteLine ("Sila/Odolnost/Rychlost/Inteligence/Mana");
				}
				int pravdepodobnostmonstra = rnd.Next (0,100);//NAHODNE GENEROVANI PRISER
				if(pravdepodobnostmonstra <= 10 && stojiciblok != "Vesnice" && stojiciblok != "Dungeon") {//Monstrum >>> Souboj
					int monstrumodolnost = 0;
					int monstrumsila = 0;
					string monstrum = "ERROR";
					int vybermonstra = rnd.Next (0,100);
					if(vybermonstra <= 30 ) {monstrum = "Krysa"; monstrumsila = 40; monstrumodolnost = 80;}// 30% na Krysu
					if(vybermonstra >= 31 && vybermonstra <= 100/*40*/ ) {monstrum = "Zbesila krysa"; monstrumsila = 45; monstrumodolnost = 120;}// 10% na Zbesilou krysu
					Console.SetCursorPosition (1,vyskamapy+2);
					Console.WriteLine ("Potkal jsi monstrum jmenem {0}! Budes muset bojovat...",monstrum);
					Console.SetCursorPosition (1,vyskamapy+3);
					Console.WriteLine ("Zmackni klavesu pro pokracovani..");
					Console.SetCursorPosition(poziceKurzoruX,poziceKurzoruY);
					Console.ReadKey();
					Console.Clear();
					bool utoceni = false;
					int utocenivolba = 1;
					int monstrumodolnostpred;
					int exppred;
					int odolnostpred;
					int regenerace;
					while(utoceni == false) {
						Console.Clear();
						Console.SetCursorPosition(1,1);
						Console.WriteLine("Bojujes proti: {0}",monstrum);
						Console.SetCursorPosition(1,2);
						Console.WriteLine("Ty: {0}: {1}/{2}",charakter,sila+Int32.Parse(inventar[0,3]),odolnost+Int32.Parse(inventar[0,3]));
						Console.SetCursorPosition(2,3);
						Console.WriteLine("Sila/Odolnost");
						Console.SetCursorPosition(1,4);
						Console.WriteLine("{0}: {1}/{2}",monstrum,monstrumsila,monstrumodolnost);
						Console.SetCursorPosition(1,6);
						Console.WriteLine ("Co udelas?");
						if(utocenivolba == 1) {
							Console.SetCursorPosition (1,7);
							Console.WriteLine ("> Zautocit");
							Console.SetCursorPosition (1,8);
							Console.WriteLine ("Branit se        ");
							Console.SetCursorPosition (1,9);
							Console.WriteLine ("Pouzit predmet    ");
						}
						if(utocenivolba == 2) {
							Console.SetCursorPosition (1,7);
							Console.WriteLine ("Zautocit         ");
							Console.SetCursorPosition (1,8);
							Console.WriteLine ("> Branit se");
							Console.SetCursorPosition (1,9);
							Console.WriteLine ("Pouzit predmet    ");
						}
						if(utocenivolba == 3) {
							Console.SetCursorPosition (1,7);
							Console.WriteLine ("Zautocit         ");
							Console.SetCursorPosition (1,8);
							Console.WriteLine ("Branit se     ");
							Console.SetCursorPosition (1,9);
							Console.WriteLine ("> Pouzit predmet");
						}
						Console.SetCursorPosition(poziceKurzoruX,poziceKurzoruY);
						ConsoleKeyInfo vstupUto = Console.ReadKey();
						if (vstupUto.Key == ConsoleKey.W) {utocenivolba--;}
						if (vstupUto.Key == ConsoleKey.S) {utocenivolba++;}
						if(utocenivolba <= 0 || utocenivolba >= 4) {
							utocenivolba = 1;
						}
						if (vstupUto.Key == ConsoleKey.Enter) {
							if(utocenivolba == 1) {//UTOK
								Console.SetCursorPosition (1,11);
								Console.WriteLine ("Utocis na {0}...",monstrum);
								Console.SetCursorPosition (1,12);
								monstrumodolnostpred = monstrumodolnost;
								monstrumodolnost = monstrumodolnost - (rnd.Next(sila/2,sila)+Int32.Parse(inventar[0,3]));
								Console.WriteLine ("Ubral jsi nepriteli jmenem {0} {1} zivotu!",monstrum,monstrumodolnostpred-monstrumodolnost);
								inventar [0, 4] = (Int32.Parse(inventar [0, 4]) - 1).ToString();
								if (Int32.Parse(inventar [0, 4]) <= 0 && inventar[0,0] != "Zadna zbran") {//Rozbiti zbrane
									Console.SetCursorPosition (1,13);
									Console.WriteLine ("Rozbila se ti zbran: {0}",inventar[0,0]);
									inventar[0,0] = "Zadna zbran";
									inventar[0,1] = " ";
									inventar[0,2] = "zbran";
									inventar[0,3] = "0";
									inventar[0,4] = "0";
								}
								Console.SetCursorPosition(poziceKurzoruX,poziceKurzoruY);
								if(monstrumodolnost <= 0) {//porazis monstrum
									Console.SetCursorPosition (1,13);
									Console.WriteLine ("Vyhral jsi!");
									exppred = exp;
									exp = exp + rnd.Next (0,10);
									Console.SetCursorPosition (1,14);
									Console.WriteLine ("Bylo ti pricteno {0} EXP! Nyni mas {1} EXP!",exp - exppred,exp);
									utoceni = true;
									break;
								}
								Console.SetCursorPosition (1,13);
								Console.WriteLine ("Nyni je na tahu {0}...",monstrum);
								Console.SetCursorPosition (1,14);
								odolnostpred = odolnost;
								odolnost = odolnost - rnd.Next(monstrumsila/3,monstrumsila) + Int32.Parse(inventar[1,3]);
								inventar [1, 4] = (Int32.Parse(inventar [1, 4]) - 1).ToString();
								Console.WriteLine ("Nepritel ti ubral {0} zivotu!",odolnostpred - odolnost);
								if (Int32.Parse(inventar [1, 4]) <= 0 && inventar[1,0] != "Zadne brneni") {//Rozbiti brneni
									Console.WriteLine ("Rozbilo se ti brneni: {0}",inventar[1,0]);
									inventar[1,0]= "Zadne brneni";
									inventar[1,1]= "";
									inventar[1,2]= "brneni";
									inventar[1,3]= "0";
									inventar[1,4]= "0";
								}
								if(odolnost <= 0) {
									Console.SetCursorPosition (1,15);
									Console.WriteLine ("{0} te usmrtil... :( ",monstrum);
									Console.SetCursorPosition (1,16);
									Console.WriteLine ("Konec hry.");
									exitgame = true;
									utoceni = true;
									break;
								}
							}
							if(utocenivolba == 2) {//OBRANA
								Console.SetCursorPosition (1,11);
								Console.WriteLine ("Snazis se ubranit proti {0}...",monstrum);
								int obranarandom = rnd.Next (0,10);
								if(obranarandom <= 7) {
									Console.SetCursorPosition (1,12);
									Console.WriteLine ("Uspesne ses ubranil...");
									regenerace = rnd.Next (5,10);
									Console.SetCursorPosition (1,13);
									Console.WriteLine ("Regenerovaly se ti {0} bodu zdravi!",regenerace);
									odolnost = odolnost + regenerace;
								}
								if(obranarandom >= 8) {
									Console.SetCursorPosition (1,12);
									Console.WriteLine ("Nedokazal ses ubranit!");
									Console.SetCursorPosition (1,13);
									Console.WriteLine ("Nyni je na tahu {0}...",monstrum);
									Console.SetCursorPosition (1,14);
									odolnostpred = odolnost;
									odolnost = odolnost - rnd.Next(monstrumsila/3,monstrumsila) + Int32.Parse(inventar[1,3]);
									inventar[1,4] = (Int32.Parse(inventar[1,4]) - 1).ToString();
									Console.WriteLine ("Nepritel te lehce zranil! Ubral ti {0} zivotu...",odolnostpred - odolnost);
									if (Int32.Parse(inventar [1, 4]) <= 0 && inventar[1,0] != "Zadne brneni") {//Rozbiti brneni
										Console.WriteLine ("Rozbilo se ti brneni: {0}",inventar[1,0]);
										inventar[1,0]= "Zadne brneni";
										inventar[1,1]= "";
										inventar[1,2]= "brneni";
										inventar[1,3]= "0";
										inventar[1,4]= "0";
									}
									if(odolnost <= 0) {
										Console.SetCursorPosition (1,15);
										Console.WriteLine ("{0} te usmrtil... :( ",monstrum);
										Console.SetCursorPosition (1,16);
										Console.WriteLine ("Konec hry.");
										exitgame = true;
										utoceni = true;
										break;
									}
								}
							}
							if(utocenivolba == 3) {//Predmet
								utoceni = true;
							}
							Console.SetCursorPosition (1,15);
							Console.WriteLine ("Zmackni libovolnou klavesu pro pokracovani...");
							Console.SetCursorPosition(poziceKurzoruX,poziceKurzoruY);
							Console.ReadKey ();
						}
					}
				}
				int pravdepodobnostdungeonu = rnd.Next (0,100);//Nahodne generovani dungeonu, 2% sance na dungeon
				if (pravdepodobnostdungeonu <= 2) {//vygeneruje dungeon
					int randomDungeonX = rnd.Next(1,sirkamapy-1);
					int randomDungeonY = rnd.Next(1,vyskamapy-1);
					if(mapa[randomDungeonY-1,randomDungeonX-1] != 3 || mapa[randomDungeonY-1,randomDungeonX-1] != 5) {
						mapa[randomDungeonY,randomDungeonX] = 5;
					}
				}
				Console.SetCursorPosition (poziceKurzoruX,poziceKurzoruY);//ZDE SE ZACINA CIST
				ConsoleKeyInfo vstup = Console.ReadKey ();
				if(vstup.Key == ConsoleKey.W) {charakterY--;}
				if(vstup.Key == ConsoleKey.A) {charakterX--;}
				if(vstup.Key == ConsoleKey.S) {charakterY++;}
				if(vstup.Key == ConsoleKey.D) {charakterX++;}
				if(vstup.Key == ConsoleKey.Q) {
					int regenerace1;
					regenerace1 = rnd.Next (5,10);
					Console.SetCursorPosition (1,vyskamapy+2);
					Console.WriteLine ("Regenerovaly se ti {0} bodu zdravi!",regenerace1);
					odolnost = odolnost + regenerace1;
				}
				if(vstup.Key == ConsoleKey.E) { //OTEVRIT INVENTAR
					bool otevrenyinventar = true;
					int volbainventar = 0;
					while (otevrenyinventar) {
						Console.Clear ();
						Console.SetCursorPosition (1, 1);
						for (int i = 0; i <= 10; i++) {
							if (volbainventar == i) {
								Console.SetCursorPosition (2, i + 2);
								Console.Write ("> ");
								if (inventar [i, 0] == "0") {
									Console.Write ("Prazdne misto");
								} else {
									Console.Write (inventar [i, 0]);
								}
								if(inventar[volbainventar,1] != "0") {
									Console.SetCursorPosition (2,14);
									Console.Write (inventar[volbainventar,1]);
								}
								Console.SetCursorPosition (2,15);
								if (inventar [volbainventar, 2] == "zbran") { Console.Write ("Poskozeni: "); Console.Write (inventar [volbainventar, 3]); }
								if (inventar [volbainventar, 2] == "brneni") { Console.Write ("Odolnost: "); Console.Write (inventar [volbainventar, 3]); }
								if (inventar [volbainventar, 2] == "jidlo") { Console.Write ("Doplni zivotu: "); Console.Write (inventar [volbainventar, 3]); }
								if (inventar [volbainventar, 2] == "lektvar-zivot") { Console.Write ("Doplni zivotu: "); Console.Write (inventar [volbainventar, 3]); }
								if (inventar [volbainventar, 2] == "lektvar-otrava") { Console.Write ("Ubere zivotu: "); Console.Write (inventar [volbainventar, 3]); }
								Console.SetCursorPosition (2,16);
								if (inventar [volbainventar, 2] == "zbran") { Console.Write ("Vydrz: "); Console.Write (inventar [volbainventar, 4]); }
								if (inventar [volbainventar, 2] == "brneni") { Console.Write ("Vydrz: "); Console.Write (inventar [volbainventar, 4]); }
								if (inventar [volbainventar, 2] == "jidlo") { Console.Write ("V zasobe: "); Console.Write (inventar [volbainventar, 4]); }
								if (inventar [volbainventar, 2] == "lektvar-zivot") { Console.Write ("V zasobe: "); Console.Write (inventar [volbainventar, 4]); }
								if (inventar [volbainventar, 2] == "lektvar-otrava") { Console.Write ("V zasobe: "); Console.Write (inventar [volbainventar, 4]); }
							}
							if(volbainventar != i) {
								if (inventar [i, 0] == "0") {
									Console.SetCursorPosition (2, i + 2);
									Console.Write ("Prazdne misto");
								} else {
									Console.SetCursorPosition (2, i + 2);
									Console.Write (inventar [i, 0]);
								}
							}
						}
						Console.SetCursorPosition (2, 1);
						Console.Write ("Mas {0} zlataku.", zlato);
						Console.SetCursorPosition (poziceKurzoruX,poziceKurzoruY);
						ConsoleKeyInfo vstupinventar = Console.ReadKey();
						if(vstupinventar.Key == ConsoleKey.W) { volbainventar--; }
						if(vstupinventar.Key == ConsoleKey.S) { volbainventar++; }
						if (vstupinventar.Key == ConsoleKey.Enter) { //menu pro item
							if(inventar[volbainventar,2] == "zbran") {//zjisti typ zbrane a nabidne moznosti
								//NWM
							}
							if(inventar[volbainventar,2] == "brneni") {
								//NWM
							}
							if(inventar[volbainventar,2] == "jidlo") {
								bool invpouzit = true;
								int volbainvpouzit = 0;
								while (invpouzit) {
									if(Int32.Parse(inventar [volbainventar, 4]) <= 0) {//Kdyz dojde item
										Console.Clear ();
										for(int i = 0;i <= 9;i++) {
											inventar [volbainventar, i] = "0";
										}
										invpouzit = false;
										break;
									}
									Console.Clear ();
									Console.SetCursorPosition (2,1);
									Console.Write (inventar[volbainventar,0]);
									Console.SetCursorPosition (2,2);
									Console.Write ("Doplni {0} zivotu.",inventar[volbainventar,3]);
									Console.SetCursorPosition (2,3);
									Console.Write ("Mas jich {0} v zasobe.",inventar[volbainventar,4]);
									Console.SetCursorPosition (2,4);
									Console.Write ("Mas {0} zivotu.",odolnost);
									if (volbainvpouzit == 0) {
										Console.SetCursorPosition (2,5);
										Console.Write ("> Snist");
										Console.SetCursorPosition (2,6);
										Console.Write ("Jit zpet");
									}
									if (volbainvpouzit == 1) {
										Console.SetCursorPosition (2,5);
										Console.Write ("Snist");
										Console.SetCursorPosition (2,6);
										Console.Write ("> Jit zpet");
									}
									Console.SetCursorPosition (poziceKurzoruX,poziceKurzoruY);
									ConsoleKeyInfo vstupinvpouzit = Console.ReadKey ();
									if (vstupinvpouzit.Key == ConsoleKey.W) { volbainvpouzit--; }
									if (vstupinvpouzit.Key == ConsoleKey.S) { volbainvpouzit++; }
									if (vstupinvpouzit.Key == ConsoleKey.Enter && volbainvpouzit == 0) {//Snist
										odolnost = odolnost + Int32.Parse(inventar[volbainventar,3]);
										inventar [volbainventar, 4] = (Int32.Parse(inventar [volbainventar, 4])-1).ToString();
									}
									if (vstupinvpouzit.Key == ConsoleKey.Enter && volbainvpouzit == 1) { invpouzit = false; }//zpet
									if (volbainvpouzit <= -1) { volbainvpouzit = 1; }
									if (volbainvpouzit >= 2) { volbainvpouzit = 0; }
								}
							}
							if(inventar[volbainventar,2] == "lektvar-zivot") {
								bool invpouzit = true;
								int volbainvpouzit = 0;
								while (invpouzit) {
									if(Int32.Parse(inventar [volbainventar, 4]) <= 0) {//Kdyz dojde item
										Console.Clear ();
										for(int i = 0;i <= 9;i++) {
											inventar [volbainventar, i] = "0";
										}
										invpouzit = false;
										break;
									}
									Console.Clear ();
									Console.SetCursorPosition (2,1);
									Console.Write (inventar[volbainventar,0]);
									Console.SetCursorPosition (2,2);
									Console.Write ("Doplni {0} zivotu.",inventar[volbainventar,3]);
									Console.SetCursorPosition (2,3);
									Console.Write ("Mas jich {0} v zasobe.",inventar[volbainventar,4]);
									Console.SetCursorPosition (2,4);
									Console.Write ("Mas {0} zivotu.",odolnost);
									if (volbainvpouzit == 0) {
										Console.SetCursorPosition (2,5);
										Console.Write ("> Vypit");
										Console.SetCursorPosition (2,6);
										Console.Write ("Jit zpet");
									}
									if (volbainvpouzit == 1) {
										Console.SetCursorPosition (2,5);
										Console.Write ("Vypit");
										Console.SetCursorPosition (2,6);
										Console.Write ("> Jit zpet");
									}
									Console.SetCursorPosition (poziceKurzoruX,poziceKurzoruY);
									ConsoleKeyInfo vstupinvpouzit = Console.ReadKey ();
									if (vstupinvpouzit.Key == ConsoleKey.W) { volbainvpouzit--; }
									if (vstupinvpouzit.Key == ConsoleKey.S) { volbainvpouzit++; }
									if (vstupinvpouzit.Key == ConsoleKey.Enter && volbainvpouzit == 0) {//vypit
										odolnost = odolnost + Int32.Parse(inventar[volbainventar,3]);
										inventar [volbainventar, 4] = (Int32.Parse(inventar [volbainventar, 4])-1).ToString();
									}
									if (vstupinvpouzit.Key == ConsoleKey.Enter && volbainvpouzit == 1) { invpouzit = false; }//zpet
									if (volbainvpouzit <= -1) { volbainvpouzit = 1; }
									if (volbainvpouzit >= 2) { volbainvpouzit = 0; }
								}
							}
							if(inventar[volbainventar,2] == "lektvar-otrava") {
								bool invpouzit = true;
								int volbainvpouzit = 0;
								while (invpouzit) {
									if(Int32.Parse(inventar [volbainventar, 4]) <= 0) {//Kdyz dojde item
										Console.Clear ();
										for(int i = 0;i <= 9;i++) {
											inventar [volbainventar, i] = "0";
										}
										invpouzit = false;
										break;
									}
									Console.Clear ();
									Console.SetCursorPosition (2,1);
									Console.Write (inventar[volbainventar,0]);
									Console.SetCursorPosition (2,2);
									Console.Write ("Doplni {0} zivotu.",inventar[volbainventar,3]);
									Console.SetCursorPosition (2,3);
									Console.Write ("Mas jich {0} v zasobe.",inventar[volbainventar,4]);
									Console.SetCursorPosition (2,4);
									Console.Write ("Mas {0} zivotu.",odolnost);
									if (volbainvpouzit == 0) {
										Console.SetCursorPosition (2,5);
										Console.Write ("> Vypit");
										Console.SetCursorPosition (2,6);
										Console.Write ("Aplikovat");
										Console.SetCursorPosition (2,7);
										Console.Write ("Jit zpet");
									}
									if (volbainvpouzit == 1) {
										Console.SetCursorPosition (2,5);
										Console.Write ("Vypit");
										Console.SetCursorPosition (2,6);
										Console.Write ("> Aplikovat");
										Console.SetCursorPosition (2,7);
										Console.Write ("Jit zpet");
									}
									if (volbainvpouzit == 2) {
										Console.SetCursorPosition (2,5);
										Console.Write ("Vypit");
										Console.SetCursorPosition (2,6);
										Console.Write ("Aplikovat");
										Console.SetCursorPosition (2,7);
										Console.Write ("> Jit zpet");
									}
									Console.SetCursorPosition (poziceKurzoruX,poziceKurzoruY);
									ConsoleKeyInfo vstupinvpouzit = Console.ReadKey ();
									if (vstupinvpouzit.Key == ConsoleKey.W) { volbainvpouzit--; }
									if (vstupinvpouzit.Key == ConsoleKey.S) { volbainvpouzit++; }
									if (vstupinvpouzit.Key == ConsoleKey.Enter && volbainvpouzit == 0) {//vypit
										odolnost = odolnost - Int32.Parse(inventar[volbainventar,3]);
										inventar [volbainventar, 4] = (Int32.Parse(inventar [volbainventar, 4])-1).ToString();
									}
									if (vstupinvpouzit.Key == ConsoleKey.Enter && volbainvpouzit == 0) {//aplikovat
										inventar [0,3] = (Int32.Parse(inventar[0,3])+Int32.Parse(inventar[volbainventar,3])).ToString();
										inventar [volbainventar, 4] = (Int32.Parse(inventar [volbainventar, 4])-1).ToString();
									}
									if (vstupinvpouzit.Key == ConsoleKey.Enter && volbainvpouzit == 1) { invpouzit = false; }//zpet
									if (volbainvpouzit <= -1) { volbainvpouzit = 1; }
									if (volbainvpouzit >= 3) { volbainvpouzit = 0; }
								}
							}
							if(inventar[volbainventar,2] == "0") { /*Prazdny slot*/ }
						}
						if (volbainventar <= -1) {volbainventar = 10; }
						if (volbainventar >= 11) {volbainventar = 0; }
						if(vstupinventar.Key == ConsoleKey.Escape || vstupinventar.Key == ConsoleKey.E) { otevrenyinventar = false; }
					}
				}
				if(vstup.Key == ConsoleKey.R && stojiciblok == "Vesnice") {//VESNICE - nazvy z skyrimu; vlastni rozhrani
					string[,,] obchod = new string[5,10,6];
					for(int a = 0;a<=4;a++) {
						for (int b = 0; b <= 9; b++) {
							for (int c = 0; c <= 5; c++) {
								obchod [a, b, c] = "0";
							}
						}
					}
					bool uvnitrvesnice = true;
					string[] nazvymest = {"Bily prusmyk","Jitrenka","Ledohrad","Morthal","Riften","Samota","Vetrny zleb","Vorarov","Helgen"};
					string nazevmesta = nazvymest[rnd.Next(0,8)];
					int volbamesto = 0;
					while(uvnitrvesnice == true) {
						Console.Clear ();
						Console.SetCursorPosition (2,1);
						Console.Write ("Vitej v meste jmenem {0}",nazevmesta);
						if (volbamesto == 0) {
							Console.SetCursorPosition (2,2);
							Console.Write ("> Hokynarstvi");//potraviny
							Console.SetCursorPosition (2,3);
							Console.Write ("Kovarna");
							Console.SetCursorPosition (2,4);
							Console.Write ("Kvetinarstvi");
							Console.SetCursorPosition (2,5);
							Console.Write ("Doly");
							Console.SetCursorPosition (2,6);
							Console.Write ("Hospoda");
						}
						if (volbamesto == 1) {
							Console.SetCursorPosition (2,2);
							Console.Write ("Hokynarstvi");//potraviny
							Console.SetCursorPosition (2,3);
							Console.Write ("> Kovarna");
							Console.SetCursorPosition (2,4);
							Console.Write ("Kvetinarstvi");
							Console.SetCursorPosition (2,5);
							Console.Write ("Doly");
							Console.SetCursorPosition (2,6);
							Console.Write ("Hospoda");
						}
						if (volbamesto == 2) {
							Console.SetCursorPosition (2,2);
							Console.Write ("Hokynarstvi");//potraviny
							Console.SetCursorPosition (2,3);
							Console.Write ("Kovarna");
							Console.SetCursorPosition (2,4);
							Console.Write ("> Kvetinarstvi");
							Console.SetCursorPosition (2,5);
							Console.Write ("Doly");
							Console.SetCursorPosition (2,6);
							Console.Write ("Hospoda");
						}
						if (volbamesto == 3) {
							Console.SetCursorPosition (2,2);
							Console.Write ("Hokynarstvi");//potraviny
							Console.SetCursorPosition (2,3);
							Console.Write ("Kovarna");
							Console.SetCursorPosition (2,4);
							Console.Write ("Kvetinarstvi");
							Console.SetCursorPosition (2,5);
							Console.Write ("> Doly");
							Console.SetCursorPosition (2,6);
							Console.Write ("Hospoda");
						}
						if (volbamesto == 4) {
							Console.SetCursorPosition (2,2);
							Console.Write ("Hokynarstvi");//potraviny
							Console.SetCursorPosition (2,3);
							Console.Write ("Kovarna");
							Console.SetCursorPosition (2,4);
							Console.Write ("Kvetinarstvi");
							Console.SetCursorPosition (2,5);
							Console.Write ("Doly");
							Console.SetCursorPosition (2,6);
							Console.Write ("> Hospoda");
						}
						Console.SetCursorPosition (poziceKurzoruX,poziceKurzoruY);
						ConsoleKeyInfo vesnicevolba = Console.ReadKey ();
						if(vesnicevolba.Key == ConsoleKey.Escape || vesnicevolba.Key == ConsoleKey.E) { uvnitrvesnice = false; }
						if (vesnicevolba.Key == ConsoleKey.W) { volbamesto--; }
						if (vesnicevolba.Key == ConsoleKey.S) { volbamesto++; }
						if (vesnicevolba.Key == ConsoleKey.Enter && volbamesto == 0) {//Hokynarstvi-potraviny
							int obchodvolba = 0;
							bool vobchode = true;
							while (vobchode) {
								Console.Clear ();
								Console.SetCursorPosition (2,1);
								Console.Write ("Hokynarstvi");
								if (obchodvolba == 0) {
									Console.SetCursorPosition (2,2);
									Console.Write ("> {0}",obchod[0,0,0]);
								}
								Console.SetCursorPosition (poziceKurzoruX,poziceKurzoruY);
								ConsoleKeyInfo vstupobchod = Console.ReadKey ();

							}
						}
						if (vesnicevolba.Key == ConsoleKey.Enter && volbamesto == 1) {//kovarna

						}
						if (vesnicevolba.Key == ConsoleKey.Enter && volbamesto == 2) {//kvetinarstvi

						}
						if (vesnicevolba.Key == ConsoleKey.Enter && volbamesto == 3) {//doly

						}
						if (vesnicevolba.Key == ConsoleKey.Enter && volbamesto == 4) {//hospoda

						}
						if (volbamesto <= -1) { volbamesto = 4; }
						if (volbamesto >= 5) { volbamesto = 0; }
					}
				}
				if(vstup.Key == ConsoleKey.Enter && stojiciblok == "Dungeon") {//DUNGEON
					oznameni = "Vstupujes do dungeonu...";
					Console.SetCursorPosition (1,vyskamapy+2);
					Console.WriteLine (oznameni);
					oznameni = " ";
					Console.ReadKey ();
					//...

					//...
					mapa[charakterY-1,charakterX-1] = 0;//pokud je vycisten, je znicen navzdy...
				}
				if(vstup.Key == ConsoleKey.Escape) {
					bool exitESCmenu = false;
					int menuvolba = 1;
					while(exitESCmenu == false) {
						Console.BackgroundColor = ConsoleColor.Black;
						Console.ForegroundColor = ConsoleColor.White;
						Console.Clear ();
						Console.SetCursorPosition (1,1);
						Console.WriteLine ("Menu");
						if (menuvolba == 1) {
							Console.SetCursorPosition (1, 2);
							Console.WriteLine ("> Zpet");
							Console.SetCursorPosition (1, 3);
							Console.WriteLine ("Konec");
						}
						if (menuvolba == 2) {
							Console.SetCursorPosition (1, 2);
							Console.WriteLine ("Zpet");
							Console.SetCursorPosition (1, 3);
							Console.WriteLine ("> Konec");
						}
						Console.SetCursorPosition (poziceKurzoruX,poziceKurzoruY);
						ConsoleKeyInfo vstupESC = Console.ReadKey ();
						//CHEATY ON pluskem
						if(vstupESC.Key == ConsoleKey.Add) {Console.Clear (); cheat = true; Console.SetCursorPosition (1,1); Console.WriteLine ("Tak cheatovat bys chtel..."); Console.SetCursorPosition (poziceKurzoruX,poziceKurzoruY); Thread.Sleep (2000);}
						if(vstupESC.Key == ConsoleKey.W) {menuvolba--;}
						if(vstupESC.Key == ConsoleKey.S) {menuvolba++;}
						if (vstupESC.Key == ConsoleKey.Enter && menuvolba == 1) {
							//Zpet
							exitESCmenu = true;
						}
						if (vstupESC.Key == ConsoleKey.Enter && menuvolba == 2) {
							//Konec
							exitESCmenu = true;
							exitgame = true;
						}
						if(menuvolba <= 0 || menuvolba >= 3) {menuvolba = 1;}
					}
				}
				if (charakterX <= 0 || charakterX >= sirkamapy) {
					charakterX = 1;
				}
				if (charakterY <= 0 || charakterY >= vyskamapy+1) {
					charakterY = 1;
				}
			}
				/*
				Console.Write("suma:");
				Console.WriteLine ("\u03A3");//UNICODE znak pro SUMU (divne E)
				Console.Write("loop:");
				Console.WriteLine ("\u047A");//UNICODE znak pro LOOP
				Console.Write("zraneni:");
				Console.WriteLine ("\u0482");//UNICODE znak pro ZRANENI
				Console.Write("Sanky:");
				Console.WriteLine ("\u077A");//UNICODE znak pro SANKY
				Console.Write("Aladin:");
				Console.WriteLine ("\u0AD0");//UNICODE znak pro ALADIN
				Console.Write("Slon:");
				Console.WriteLine ("\u0BF5");//UNICODE znak pro SLON
				Console.Write("slaby vitr:");
				Console.WriteLine ("\u3137");//UNICODE znak pro slaby vitr
				Console.Write("vetsi vitr:");
				Console.WriteLine ("\u3143");//UNICODE znak pro vetsi vitr
				Console.Write("vichrice:");
				Console.WriteLine ("\u3140");//UNICODE znak pro vichrice
				Console.Write("Labut s vrtuli:");
				Console.WriteLine ("\u0C60");//UNICODE znak pro Labut s vrtuli
				Console.Write("Labut:");
				Console.WriteLine ("\u0CAD");//UNICODE znak pro Labut
				Console.Write("Housenka:");
				Console.WriteLine ("\u0DF4");//UNICODE znak pro Housenka
				Console.Write("Dyka:");
				Console.WriteLine ("\u0F10");//UNICODE znak pro Dyka
				Console.Write("Svastika:");//HITLERBOSSLEVEL
				Console.WriteLine ("\u0FD5");//UNICODE znak pro Svastika
				Console.Write("swaztika:");
				Console.WriteLine ("\u0FD7");//UNICODE znak pro Svastika LVL ZWEI
				Console.Write("Africky meister:");
				Console.WriteLine ("\u1360");//UNICODE znak pro Africky meister
				Console.Write("3times:");
				Console.WriteLine ("\u1392");//UNICODE znak pro 3nadsebou
				Console.Write("2times:");
				Console.WriteLine ("\u1393");//UNICODE znak pro 2nadsebou
				Console.Write("mrizka:");
				Console.WriteLine ("\u1699");//UNICODE znak pro mrizka
				Console.Write("BTN:    ");
				Console.WriteLine ("\u20E3");//UNICODE znak pro BTN
				Console.Write(":");
				Console.WriteLine ("\u2328");//UNICODE znak pro 
				Console.Write(":");
				Console.WriteLine ("\u22D9");//UNICODE znak pro OKOLO JE JICH TU VICE!!!
				Console.Write(":");
				Console.WriteLine ("\u2302");//UNICODE znak pro 
				Console.Write(":");
				Console.WriteLine ("\u2317");//UNICODE znak pro 
				Console.Write(":");
				Console.WriteLine ("\u2318");//UNICODE znak pro 
				Console.Write(":");
				Console.WriteLine ("\u2313");//UNICODE znak pro pocasi
				Console.Write(":");
				Console.WriteLine ("\u2591");//UNICODE znak pro THREE SHADES OF PAINT
				Console.Write(":");
				Console.WriteLine ("\u2592");//UNICODE znak pro 
				Console.Write(":");
				Console.WriteLine ("\u2593");//UNICODE znak pro 
				Console.Write(":");
				Console.WriteLine ("\u26C6");//UNICODE znak pro 

				*/
		}
	}
}
