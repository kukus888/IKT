using System;

namespace IKT
{
	public class promenne
	{
		public int zlato{ get; set;}
		public int odolnost{ get; set;}
		public string[,] inventar{ get; set;}
		public int exp{ get; set;}
		public bool cheat{ get; set;}
		public bool neviditelnost_pred_monstry{ get; set;}
		public int delka_neviditelnosti{ get; set;}
		public int[,] mapa{ get; set;}
		public int nejvetsisirka{ get; set;}
		public int poziceKurzoruX{ get; set;}
		public int poziceKurzoruY{ get; set;}
		public int nejvetsivyska{ get; set;}
		public int vyskamapy{ get; set;}
		public int sirkamapy{ get; set;}
		public string charakter{ get; set;}
		public int sila{ get; set;}
		public int mana{ get; set;}
		public int charakterX{ get; set;}
		public int charakterY{ get; set;}
		public bool jenazivu{ get; set;}
		//Šablona pro craft: {"Nazev","Popis","typ","A1","A2","A3","suroviny1","STACKsurovin1"}
		public string[,] craft{ get; set;} = new string[,]{//co se vyrobi, popis, typ, A1, A2, A3		suroviny: typ, stack
			{"Most","Umožňuje přecházet vodu","Most","0","1","100","větve","25"},// Vetve >>> Most
			{"TEST","TEST ITEM","jidlo","0","1","1","větve","10"}//TEST ITEM
		};
		public string[,] monstra{ get; set;} = new string[,]{//Nazev, pravdepodobnost, Odolnost, Sila
			{"Krysa", "50", "60", "30"},
			{"Zběsilá krysa", "37", "70", "40"},
			{"Vlk","15","130","50"},
			{"Medvěd","10","160","55"}
		};
		public string[,] RP{ get; set;}
		public string[,] RP_basic{ get;} = new string[,]{//ID, BG, FG, ico, název
			{"0","Green","White","░","Planiny"},
			{"1","DarkGreen","DarkYellow","▒","Pahorkatiny"},
			{"2","DarkGray","Gray","▓","Hory"},
			{"3","Blue","White","≈","Voda"},
			{"4","Blue","White","⎔ ","Hexagon"},
			{"5","Green","Black","ᚙ ","Dungeon"},
			{"6","Green","Black","⌂","Vesnice"},
			{"7","Green","DarkYellow","ܓ","Větve"},
			{"8","DarkYellow","DarkRed","Ⅲ","Most"},
			{"9","Gray","DarkGray","☷ ","Cesta"},
			{"10","Black","White","⎚","Okno"},
			{"11","Black","White","⚙","Ozubené kolečko"},
			{"12","Black","White","♯","Sharp"},
			{"13","White","Gray","░","Sníh"},
			{"14","Black","White","",""},
			{"15","Black","White","",""},
			{"16","White","DarkGray","⛰ ","HORA"},
			{"17","White","White"," ","Bílá"},
			{"18","Green","Green"," ","Zelená"},
			{"19","Gray","Gray"," ","Šedá"},
			{"20","Black","White","⛓","Řetěz"}
		};
		public string[,] RP_snow{ get;} = new string[,]{//ID, BG, FG, ico, název
			{"0","White","Gray","░","Zasněžené planiny"},
			{"1","White","Gray","▒","Zasněžené pahorkatiny"},
			{"2","White","DarkGray","⛰ ","Hory"},
			{"3","White","Blue","≈","Voda"},
			{"4","Black","White","⛰ ","HORA"},
			{"5","White","Black","ᚙ ","Dungeon"},
			{"6","White","Black","⌂","Vesnice"},
			{"7","White","DarkYellow","ܓ","Větve"},
			{"8","DarkYellow","DarkRed","Ⅲ","Most"},
			{"9","Gray","DarkGray","☷ ","Cesta"},
			{"10","Black","White","⎚","Okno"},
			{"11","Black","White","⚙","Ozubené kolečko"},
			{"12","Black","White","♯","Sharp"},
			{"13","White","Gray","░","Sníh"},
			{"14","Black","White","",""},
			{"15","Black","White","",""},
			{"16","Blue","White","⎔ ","Hexagon"},
			{"17","White","White"," ","Bílá"},
			{"18","Green","Green"," ","Zelená"},
			{"19","Gray","Gray"," ","Šedá"},
			{"20","Black","White","⛓","Řetěz"}
		};
		public string[,] RP_desert{ get;} = new string[,]{//ID, BG, FG, ico, název
			{"0","Yellow","Gray","░","Poušť"},
			{"1","Yellow","Gray","▒","Poušť"},
			{"2","Yellow","DarkGray","⛰ ","Duny"},
			{"3","Blue","White","≈","Voda"},
			{"4","Black","White","⛰ ","HORA"},
			{"5","Yellow","Black","ᚙ ","Dungeon"},
			{"6","Yellow","Black","⌂","Vesnice"},
			{"7","Yellow","DarkYellow","ܓ","Větve"},
			{"8","DarkYellow","DarkRed","Ⅲ","Most"},
			{"9","Gray","DarkGray","☷ ","Cesta"},
			{"10","Black","White","⎚","Okno"},
			{"11","Black","White","⚙","Ozubené kolečko"},
			{"12","Black","White","♯","Sharp"},
			{"13","White","Gray","░","Sníh"},
			{"14","Black","White","",""},
			{"15","Yellow","Yellow"," ","Žlutá"},
			{"16","Blue","White","⎔ ","Hexagon"},
			{"17","White","White"," ","Bílá"},
			{"18","Green","Green"," ","Zelená"},
			{"19","Gray","Gray"," ","Šedá"},
			{"20","Black","White","⛓","Řetěz"}
		};
		public promenne ()
		{
		}
		public void init() {
			poziceKurzoruX = 0;
			poziceKurzoruY = 0;
			nejvetsisirka = 80;
			nejvetsivyska = 24;
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
		}
		public void vymazat() {
			zlato = 0; odolnost = 0; exp = 0; cheat = false; neviditelnost_pred_monstry = false; delka_neviditelnosti = 0;
			for(int a = 0; a <= inventar.GetLength(0)-1;a++) {
				for(int b = 0; b <= inventar.GetLength(1)-1;b++) {
					inventar [a, b] = "0";
				}
			}
		}
	}
}

