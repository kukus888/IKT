using System;

namespace IKT
{
	public class generaceobchodu
	{
		public generaceobchodu (string[,,] obchod, promenne hodnoty)
		{
			Random rnd = new Random ();
			string[,] nazvyjidel = new string[,] { //NAZVY JIDEL , popis, kolik HP pridaji
				{ "Jablko", "Zdrava vyziva!", "5" },
				{ "Chleba", "Klasický pšeničný chléb.", "10" },
				{ "Houska", "Zakulaceny kus peciva.", "7" },
				{ "Rizek", "Poradny kus masa v trojobalu.", "15" },
				{ "Cinske nudle", "S prichuti z praveho psa!", "20" },
				{ "Losos", "Prava severska ryba.", "11" },
				{ "Kapr", "Tradicni ceske vanocni jidlo, jen nespolknout kost!", "10" },
				{ "Uhor", "Doslova elektrizujici...", "15" },
				{ "Vakuovane testoviny", ("Vydrzi do roku: ")+(Int32.Parse (DateTime.Now.Year.ToString()) + 3).ToString (), "20" },
				{ "Grilovane jehneci", "", "22" },
				{ "Brambor", "Kobzol", "6" },
				{ "Grilovane veprove", "Dodá spoustu síly a navíc i ohřeje", "22" },
				{ "Hovezi vyvar", "Dodá spoustu síly a navíc i ohřeje", "24" }
			};
			string[] zbrane_carodej = new string[] { "hulka", "prut"};
			string[] zbrane_M = new string[] { "mec", "palcat", "nozik" };
			string[] zbrane_Z = new string[] { "dyka", "sekera", "valecna sekera"};
			string[] zbrane_S = new string[] { "kladivo", "valecne kladivo"};
			string[] materialy_M = new string[] { "Ebenitovy", "Zelezny", "Skleneny", "Ocelovy", "Bronzovy", "Chitinovy" };
			string[] materialy_Z = new string[] { "Ebenitova", "Zelezna", "Sklenena", "Ocelova", "Bronzova", "Chitinova" };
			string[] materialy_S = new string[] { "Ebenitove", "Zelezne", "Sklenene", "Ocelove", "Bronzove", "Chitinove" };
			string[] materialy_zbroj = new string[] { "Usnova", "Okovana", "Elfska", "Supinova", "Sklenena", "Ocelova platova", "Zelezna", "Ebenitova", "Chitinova" };
			string[] materialy_zbroj_modifier = new string[] { "uslechtilosti", "zdravi", "obrany", "niceni", "regenerace" };
			string[,] lektvary_nazvy = new string[,] //nazev, typ, A1, cena za 1 min, cena za 1 max 
			{	{ "Lektvar neviditelnosti", "lektvar-neviditelnost", "5", "25", "35" },
				{ "Lektvar obnoveni zdravi", "lektvar-zdravi", "30", "15", "30"},
				{ "Lektvar regenerace zdravi", "lektvar-zdravi", "45", "25", "40"},
				{ "Jed snizeni zdravi", "lektvar-otrava", "10", "5", "12"},
				{ "Jed zniceni zdravi", "lektvar-otrava", "40", "30", "40"}
			};
			for (int a = 0; a <= 9; a++) {				//JIDLA & ZAPIS JIDEL
				int cislo = rnd.Next (0,12); //PREPSAT PRI KAZDE ZMENE nazvyjidla[] !!!
				obchod [0, a, 0] = (nazvyjidel [cislo,0]).ToString ();
				obchod [0, a, 1] = (nazvyjidel [cislo,1]).ToString ();
				obchod [0, a, 2] = "jidlo";
				obchod [0, a, 3] = (nazvyjidel[cislo,2]).ToString();
				obchod [0, a, 4] = rnd.Next (1,5).ToString();
				obchod [0, a, 5] = ((Int32.Parse (obchod [0, a, 3]) / 5) * (Int32.Parse (obchod [0, a, 4]))).ToString();
			}
			for (int b = 0; b <= 9; b++) {//Lektvary
				int cislo = rnd.Next (0, 4); //PREPSAT PRI KAZDE ZMENE lektvary_nazvy[] !!!
				obchod [2, b, 0] = (lektvary_nazvy [cislo, 0]).ToString ();
				obchod [2, b, 1] = "prazdny popisek"; /*().ToString();*/
				obchod [2, b, 2] = (lektvary_nazvy [cislo, 1]).ToString ();
				obchod [2, b, 3] = (lektvary_nazvy [cislo, 2]).ToString ();
				obchod [2, b, 4] = (rnd.Next (1, 3)).ToString ();
				obchod [2, b, 5] = (Int32.Parse (obchod [2, b, 4]) * (rnd.Next (Int32.Parse (lektvary_nazvy [cislo, 3]), Int32.Parse (lektvary_nazvy [cislo, 4])))).ToString ();
			}
			if (hodnoty.charakter == "Čaroděj") {
				for (int c = 0; c <= 9; c++) {//zbrane
					rnd.Next (0, 3);
				}
			}
		}
	}
}

