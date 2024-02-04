using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace felveteli
{
	public class Kuldo : IFelvetelizo
	{

		public Kuldo()
		{

		}

		public Kuldo(string omAzonosito, string nev, string ertesitesiCim, DateTime szuletesiDatum, string elerhetosegEmail, int matekPontszam, int magyarPontszam)
		{
			this.OM_Azonosito = omAzonosito;
			this.Neve = nev;
			this.ErtesitesiCime = ertesitesiCim;
			this.SzuletesiDatum = szuletesiDatum;
			this.Email = elerhetosegEmail;
			this.Matematika = matekPontszam;
			this.Magyar = magyarPontszam;
		}

		public string OM_Azonosito { get; set; }
		public string Neve { get; set; }
		public string ErtesitesiCime { get; set; }
		public DateTime SzuletesiDatum { get; set; }
		public string Email { get; set; }
		public int Matematika { get; set; }
		public int Magyar { get; set; }



		public String CSVSortAdVissza()
		{
			return $"{OM_Azonosito};{Neve};{ErtesitesiCime};{Email};{SzuletesiDatum};{Matematika};{Magyar}";
		}

		public void ModositCSVSorral(String csvString)
		{

		}
	}
}