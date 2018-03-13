using System;

namespace IKT
{
	public class promenne
	{
		public int zlato{ get; set;}
		public int odolnost{ get; set;}
		public int odolnostMax{ get; set;}
		public string[,] inventar{ get; set;}
		public int exp{ get; set;}
		public bool cheat{ get; set;}
		public bool neviditelnost_pred_monstry{ get; set;}
		public int delka_neviditelnosti{ get; set;}
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
		public map[,] mapa{ get; set;}
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
	public class map{
		public string Name{ get; set;}
		public string Tile{ get; set;}//co se ukáže na obrazovce
		public ConsoleColor TileBackground{ get; set;}
		public ConsoleColor TileForeground{ get; set;}
		public ConsoleColor PlayerColor{ get; set;}
		public string Type{ get; set;}//Mozne jsou: City River Dungeon Bridge Landscape LandscapeExtended
		public bool IsWalkable{get; set;}
		public int Height{ get; set;}// 0 je more, 1 planiny , 2++ hory
		public bool Print{ get; set;}//pokud true, vypise znak. pokud ne nevypise
		public bool Replaceable{get; set;}//muze byt prepsan?
	}
	public class mapPresets{
		public map Planiny{ get; set; }
		public map Pahory{ get;}
		public map Hory{ get;}
		public map HoryDop{ get;}
		public map Dungeon{ get;}
		public map Reka{ get;}
		public map Null{ get;}
		public map Mesto{ get;}
		public map Vetve{ get;}
		public mapPresets(){
			Planiny = new map();//-----------------PLANINY
			Planiny.Name = "Planiny";
			Planiny.IsWalkable = true;
			Planiny.PlayerColor = ConsoleColor.Black;
			Planiny.Tile = "░";
			Planiny.TileBackground = ConsoleColor.Green;
			Planiny.TileForeground = ConsoleColor.White;
			Planiny.Height = 1;
			Planiny.Print = true;
			Planiny.Type = "Landscape";
			Planiny.Replaceable = true;
			Pahory = new map ();//----------------PAHORKATINY
			Pahory.Name = "Pahorkatiny";
			Pahory.IsWalkable = true;
			Pahory.PlayerColor = ConsoleColor.Black;
			Pahory.Tile = "▒";
			Pahory.TileBackground = ConsoleColor.DarkGreen;
			Pahory.TileForeground = ConsoleColor.DarkYellow;
			Pahory.Height = 2;
			Pahory.Print = true;
			Pahory.Type = "Landscape";
			Pahory.Replaceable = true;
			Hory = new map ();//-------------------HORA ⛰
			Hory.Name= "Hory";
			Hory.IsWalkable = false;
			Hory.PlayerColor = ConsoleColor.Black;
			Hory.Tile="⛰";
			Hory.TileBackground = ConsoleColor.White;
			Hory.TileForeground = ConsoleColor.DarkGray;
			Hory.Print = true;
			Hory.Height = 3;
			Hory.Type = "LandscapeExtended";
			Hory.Replaceable = false;
			HoryDop = new map ();//-menší přídavek :)
			HoryDop.Name= "Hory";
			HoryDop.IsWalkable = false;
			HoryDop.PlayerColor = ConsoleColor.Black;
			HoryDop.Tile=" ";
			HoryDop.TileBackground = ConsoleColor.White;
			HoryDop.TileForeground = ConsoleColor.White;
			HoryDop.Print = false;
			HoryDop.Height = 3;
			HoryDop.Type = "Landscape";
			HoryDop.Replaceable = false;
			Dungeon = new map ();//--------------DUNGEON
			Dungeon.Name = "Dungeon";
			Dungeon.IsWalkable = true;
			Dungeon.PlayerColor = ConsoleColor.Black;
			Dungeon.Tile = "ᚙ";
			Dungeon.TileBackground = ConsoleColor.Green;
			Dungeon.TileForeground = ConsoleColor.Black;
			Dungeon.Print = true;
			Dungeon.Height = 1;
			Dungeon.Type = "LandscapeExtended";
			Dungeon.Replaceable = false;
			Reka = new map ();//---------------ŘEKA
			Reka.Name = "Řeka";
			Reka.IsWalkable = false;
			Reka.PlayerColor = ConsoleColor.Black;
			Reka.Tile = "≈";
			Reka.TileBackground = ConsoleColor.Blue;
			Reka.TileForeground = ConsoleColor.White;
			Reka.Print = true;
			Reka.Height = 0;
			Reka.Type = "River";
			Reka.Replaceable = false;
			Null = new map ();//----------------SPECIALNI NULL TYP
			Null.Name = "NULL";
			Null.IsWalkable = true;
			Null.PlayerColor = ConsoleColor.White;
			Null.Tile = " ";
			Null.TileBackground = ConsoleColor.Black;
			Null.TileForeground = ConsoleColor.White;
			Null.Print = true;
			Null.Height = 0;
			Null.Type = "NULL";
			Null.Replaceable = true;
			Mesto = new map ();//-------------Město
			Mesto.Name = "Město";
			Mesto.IsWalkable = true;
			Mesto.Height = 1;
			Mesto.PlayerColor = ConsoleColor.Black;
			Mesto.Print = true;
			Mesto.Replaceable = false;
			Mesto.Tile = "⌂";
			Mesto.TileBackground = ConsoleColor.Green;
			Mesto.TileForeground = ConsoleColor.Black;
			Mesto.Type = "City";
			Vetve = new map ();//-------------Větve
			Vetve.Name = "Větve";
			Vetve.IsWalkable = true;
			Vetve.Height = 1;
			Vetve.PlayerColor = ConsoleColor.Green;
			Vetve.Print = true;
			Vetve.Replaceable = true;
			Vetve.Tile = "ܓ";
			Vetve.TileBackground = ConsoleColor.Green;
			Vetve.TileForeground = ConsoleColor.DarkYellow;
			Vetve.Type = "Landscape";
		}
	}
}

