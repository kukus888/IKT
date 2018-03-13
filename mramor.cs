using System;

namespace IKT
{
	public class mramor
	{
		Random rnd = new Random();
		grafika map = new grafika();
		tridainventar inv = new tridainventar();
		public mramor ()
		{
		}
		public void Dungeon(promenne hodnoty) {
			bool zijes = true;
			bool cleandungeon = false;
			//Part #1: Generating Dungeon
			DungeonTile[,] dun = GenerujDungeon (32, 16);

			int playerX = 0;
			int playerY = 0;
			for (int x = 0; x <= dun.GetLength (0) - 1; x++) {//searches for entrace and places player there
				for (int y = 0; y <= dun.GetLength (1) - 1; y++) {
					if (dun [x, y].TileName == "entrance") {
						playerX = x;
						playerY = y;					
						break;
					}
				}
			}
			int DunOffsetX = 1;
			int DunOffsetY = 1;
			while (zijes == true || cleandungeon == false) {
				Console.BackgroundColor = ConsoleColor.Black;
				Console.ForegroundColor = ConsoleColor.White;
				Console.Clear ();
				for (int x = 0; x <= dun.GetLength (0) - 1; x++) {
					for (int y = 0; y <= dun.GetLength (1) - 1; y++) {
						Console.BackgroundColor = dun [x, y].TileBackGroundColor;
						Console.ForegroundColor = dun [x, y].TileForeGroundColor;
						Console.SetCursorPosition (x + DunOffsetX, y + DunOffsetY);
						Console.Write (dun [x, y].Tile);
					}
				}
				Console.SetCursorPosition (playerX + DunOffsetX, playerY + DunOffsetY);
				Console.BackgroundColor = dun [playerX, playerY].TileBackGroundColor;
				Console.ForegroundColor = dun [playerX, playerY].PlayerColor;
				Console.Write ("\uC637");
				Console.ForegroundColor = ConsoleColor.White;
				Console.BackgroundColor = ConsoleColor.Black;
				map.HealthBar (hodnoty.odolnost, hodnoty.odolnostMax,dun.GetLength(0) + 2,1,20,true);//health bar
				Console.SetCursorPosition (dun.GetLength(0) + 2 + 7, 1);
				Console.BackgroundColor = ConsoleColor.Black;
				Console.SetCursorPosition (dun.GetLength(0) + 2, 2);//NA CEM STOJIS
				Console.Write ("Stojíš na: ");
				string stojiciblok = "nic";
				string stojiciblok1 = "nic";
				try{
					stojiciblok = dun[playerX,playerY].TileName;
					stojiciblok1 = dun[playerX,playerY+1].TileName;
				} catch {
				}
				Console.Write ("{0} / {1}", stojiciblok, stojiciblok1);
				Console.SetCursorPosition (dun.GetLength(0) + 2, 3);//Info o charakteru
				Console.WriteLine ("{0}: {1}/{2}/{3}", hodnoty.charakter, hodnoty.exp, hodnoty.sila, hodnoty.mana);
				Console.SetCursorPosition (dun.GetLength(0) + 3, 4);
				Console.WriteLine ("Zkušenosti/Síla/Mana");
				if (hodnoty.neviditelnost_pred_monstry == true) {
					Console.SetCursorPosition (dun.GetLength(1) + 2, 5);
					Console.Write ("Monstra si te nevsimaji ({0} tahu zbyva)",hodnoty.delka_neviditelnosti);
				}
				Console.SetCursorPosition (dun.GetLength(0) + 2, 6);
				Console.WriteLine ("Zlato: {0}",hodnoty.zlato);
				ConsoleKeyInfo vstup = Console.ReadKey ();
				try {
					if (vstup.Key == ConsoleKey.W && dun [playerX, playerY - 1].IsWalkable == true) {
						playerY--;
					}
					if (vstup.Key == ConsoleKey.A && dun [playerX - 1, playerY].IsWalkable == true) {
						playerX--;
					}
					if (vstup.Key == ConsoleKey.S && dun [playerX, playerY + 1].IsWalkable == true) {
						playerY++;
					}
					if (vstup.Key == ConsoleKey.D && dun [playerX + 1, playerY].IsWalkable == true) {
						playerX++;
					}
				} catch (System.IndexOutOfRangeException) {
				}
				if (vstup.Key == ConsoleKey.E) {
					if (dun [playerX, playerY].TileName == "entrance") {
						Console.Clear ();
						break;
					} else {//Inventář
						inv.action(hodnoty);
					}
				}
				if (vstup.Key == ConsoleKey.Escape) {//menu
					Console.Clear ();

				}
				if (playerX >= dun.GetLength (0)) {
					playerX = dun.GetLength (0) - 1;
				}
				if (playerY >= dun.GetLength (1)) {
					playerY = dun.GetLength (1) - 1;
				}
				if (playerX <= -1) {
					playerX = 0;
				}
				if (playerY <= -1) {
					playerY = 0;
				}
			}
			//hodnoty.mapa [hodnoty.charakterX, hodnoty.charakterY] = 0;//pokud je vycisten, je znicen navzdy...
			hodnoty.jenazivu = zijes;
		}
		public DungeonTile[,] GenerujDungeon(int sirka, int vyska){
			DungeonTile[,] dun = new DungeonTile[sirka,vyska];
			#region TILES
			DungeonTile entrance = new DungeonTile ();
			entrance.IsWalkable = true;
			entrance.Tile = "E";
			entrance.TileBackGroundColor = ConsoleColor.White;
			entrance.TileForeGroundColor = ConsoleColor.DarkGray;
			entrance.PlayerColor = ConsoleColor.Black;
			entrance.TileName = "entrance";
			DungeonTile wall = new DungeonTile ();
			wall.IsWalkable = false;
			wall.Tile = "▒";
			wall.TileBackGroundColor = ConsoleColor.DarkGray;
			wall.TileForeGroundColor = ConsoleColor.White;
			wall.PlayerColor = ConsoleColor.White;
			wall.TileName = "wall";
			DungeonTile path = new DungeonTile ();
			path.IsWalkable = true;
			path.Tile = "░";
			path.TileBackGroundColor = ConsoleColor.White;
			path.TileForeGroundColor = ConsoleColor.Gray;
			path.PlayerColor = ConsoleColor.Black;
			path.TileName = "path";
			#endregion
			//#1: Filling with walls
			for (int x = 0; x <= sirka - 1; x++) {
				for (int y = 0; y <= vyska - 1; y++) {
					dun [x, y] = wall;
				}
			}
			//#2: Entrance
			dun [rnd.Next (1, sirka - 1),rnd.Next (1, vyska - 1)] = entrance;

			for (int x = 0; x <= sirka - 1; x++) {
				for (int y = 0; y <= vyska - 1; y++) {
					try {
						if (dun [x, y].TileName == "entrance") {//searches for entrance
							int ways = rnd.Next(1,3);
							int doneWays = 0;
							while(true){
								int NWSE = rnd.Next(0,3);
								if(NWSE== 1){
									dun[x,y+1] = path;
									doneWays++;
								}
								if(NWSE== 2){
									dun[x,y-1] = path;
									doneWays++;
								}
								if(NWSE== 3){
									dun[x+1,y] = path;
									doneWays++;
								}
								if(NWSE== 0){
									dun[x-1,y] = path;
									doneWays++;
								}
								if(doneWays >= ways){
									break;
								}
							}
							break;
						}
					} catch (System.NullReferenceException) {
					}
				}
			}

			return dun;
		}
	}
	public class DungeonTile {
		public string Tile {get; set;}
		public ConsoleColor TileBackGroundColor { get; set;}
		public ConsoleColor TileForeGroundColor { get; set;}
		public ConsoleColor PlayerColor { get; set;}
		public bool IsWalkable{ get; set;}
		public string TileName{ get; set;}
	}
}

