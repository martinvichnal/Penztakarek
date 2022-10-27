// 10. osztály szakmai évzáró dolgozat. Vichnál Martin 10.D
// Láttam: Nagy Attila
using System;
using System.IO;
using System.Text;

namespace penztakarek
{
    class Program
    {

        public static class adat
        {
            public static int osszVagyon = 0;
            public static int haviKeret = 0;
            public static int defaultKeret = 0;
            public static int adottHonap = DateTime.Now.Month;
            public static int regiHonap = 0;
            public static bool honapValtozott = false;
            public static bool firstRun = true;
            public static bool mentes = false;
            public static string readingString = "";
        }

        static void Main(string[] args)
        {
            int menu;
            bool ifmenu = true;
            beolvasas();

            while (ifmenu == true)
            {
                Console.Clear();
                Console.WriteLine("Osszes vagyona: " + adat.osszVagyon + " Ft");
                if (adat.haviKeret <= 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Havi Keret: " + adat.haviKeret + " Ft");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                else
                {
                    Console.WriteLine("Havi Keret: " + adat.haviKeret + " Ft");
                }
                Console.WriteLine("Pontos ido: " + DateTime.Now);
                Console.Write("Uj honap lett: "); if (adat.honapValtozott == false) { Console.WriteLine("NEM"); } else { Console.WriteLine("IGEN"); }
                Console.Write("Folyamatos mentes: "); if (adat.mentes == false) { Console.WriteLine("KIKAKPCSOLVA"); } else { Console.WriteLine("BEKAPCSOLVA"); }
                Console.WriteLine("");
                Console.WriteLine("MENU\n");
                Console.WriteLine("Irja be a menupont szamat\n");
                Console.WriteLine("0. Kilépés");
                Console.WriteLine("1. Összes vagyon");
                Console.WriteLine("2. Keret");
                Console.WriteLine("3. Termekek");
                Console.WriteLine("4. Felvetel");
                Console.WriteLine("5. Segítség");
                Console.WriteLine("6. Beállítások\n");
                menu = Convert.ToInt32(ReadingLine(adat.readingString));

                switch (menu)
                {
                    case 0:
                        Console.WriteLine("KILÉPÉS");
                        Save();
                        ifmenu = false;
                        break;
                    case 1:
                        Console.WriteLine("ÁTIRÁNYíTÁSA AZ ÖSSZES VAGYONRA");
                        osszVa();
                        break;
                    case 2:
                        Console.WriteLine("ÁTIRÁNYíTÁSA A KERETRE");
                        keret();
                        adat.firstRun = false;
                        break;
                    case 3:
                        Console.WriteLine("ÁTIRÁNYíTÁSA A TERMEKEKRE");
                        termekek();
                        break;
                    case 4:
                        Console.WriteLine("ÁTIRÁNYíTÁSA A FELVETELRE");
                        felvetel();
                        break;
                    case 5:
                        Console.WriteLine("ÁTIRÁNYíTÁSA A SEGíTSÉGRE");
                        help();
                        break;
                    case 6:
                        Console.WriteLine("ÁTIRÁNYíTÁSA A BEALLITASRA");
                        settings();
                        break;
                }
            }

            Console.WriteLine("\nKILÉPÉSHEZ NYOMJON MEG EGY GOMBOT");
            Console.ReadKey();
        }



        public static void beolvasas()
        {
            string[] adatok = new string[5];
            FileStream fs = new FileStream("DATA.txt", FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs);

            int i = 0;
            while (!sr.EndOfStream)
            {

                string st = sr.ReadLine();
                if (!string.IsNullOrWhiteSpace(st))
                {
                    adatok[i] = st;
                    i++;
                }
            }
            fs.Close();
            sr.Close();

            adat.osszVagyon = Convert.ToInt32(adatok[0]);
            adat.haviKeret = Convert.ToInt32(adatok[1]);
            adat.defaultKeret = Convert.ToInt32(adatok[2]);
            adat.regiHonap = Convert.ToInt32(adatok[3]);
            adat.mentes = Convert.ToBoolean(adatok[4]);

            if (adat.adottHonap != adat.regiHonap)
            {
                adat.honapValtozott = true;
                adat.haviKeret = adat.defaultKeret;
                if (adat.mentes == true)
                {
                    Save();
                }
            }
            else
            {
                adat.honapValtozott = false;
            }
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///
        ////////////////////////////////////////////////////ÖSSZVAGYON MODUL/////////////////////////////////////////////////////
        public static void osszVa()
        {
            int beolvasott;
            int osszVagyon = adat.osszVagyon;

            Console.Clear();
            Console.WriteLine("ÜDVÖZÖLLEK AZ ÖSSZES VAGYON MENÜPNTNÁL!");
            Console.WriteLine("ITT TUDJA BEÁLLíTANI/MÓDOSíTANI A VAGYONA ÖSSZEGÉT\n");

            if (osszVagyon == 0)
            {
                //HA NINCS BEÍRVA SEMMI 
                Console.Write("KÉRLEK ÜSSE BE HOGY MOST MENNIY AZ ÖSSZES VAGYONA! : ");
                //beolvasott = Convert.ToInt32(Console.ReadLine());
                beolvasott = Convert.ToInt32(ReadingLine(adat.readingString));
                Console.WriteLine("");
                Console.WriteLine("EZ AZ ÖSSZEG AMIT BE AKART íRNI? : " + beolvasott + " Ft");
                Console.WriteLine("I/N");
                //string ifitis = Console.ReadLine();
                string ifitis = ReadingLine(adat.readingString);
                if (ifitis == "n" || ifitis == "N")
                {
                    //HA ROSSZA AZ ÖSSZEG AMIT BE AKART ÍRNI
                    Console.WriteLine("KÉRLEK ÜSSE BE HOGY MOST MENNIY AZ ÖSSZES VAGYNA!");
                    //adat.osszVagyon = Convert.ToInt32(Console.ReadLine());
                    adat.osszVagyon = Convert.ToInt32(ReadingLine(adat.readingString));
                }
                else
                {
                    //HA JÓ AZ ÖSSZEG AMIT BE AKART ÍRNI
                    Console.WriteLine("A VAGYON EL LETT MENTVE!");
                    adat.osszVagyon = beolvasott;
                }
            }
            else
            {
                //HA VAN BEÍRVA VALAMI 
                Console.WriteLine("ÖNNEK AZ ÖSSZES VAGOYNA : " + osszVagyon + " Ft.");
                Console.Write("MÓDOSíTANI AKARJA-E ? (I/N) : ");
                //Console.WriteLine("Y/N");
                //string ifitis = Console.ReadLine();
                string ifitis = ReadingLine(adat.readingString);
                if (ifitis == "Y" || ifitis == "y" || ifitis == "I" || ifitis == "i")
                {
                    //HA MÓDOSÍTANI AKARJA 
                    Console.WriteLine("\nKÉRLEK ÜSSE BE AZ ÖSSZEGET (SPACE NÉLKÜL)");
                    //adat.osszVagyon = Convert.ToInt32(Console.ReadLine());
                    adat.osszVagyon = Convert.ToInt32(ReadingLine(adat.readingString));
                    Console.WriteLine("A VAGYON EL LETT MENTVE!");
                }
            }
            if (adat.mentes == true)
            {
                Save();
            }

            Console.WriteLine("\nTOVÁBB LÉPÉSHEZ NYOMJON MEG EGY GOMBOT");
            Console.ReadKey();
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///
        ///////////////////////////////////////////////////////KERET MODUL///////////////////////////////////////////////////////
        public static void keret()
        {
            int keret = adat.haviKeret;

            Console.Clear();
            Console.WriteLine("ÜDVÖZÖLLEK A KERET MENÜPONTNÁL!");
            Console.WriteLine("ITT LEHET BEÁLLÍTANI A HAVI KERETET.\n");
            Console.WriteLine("AZ ÖN HAVI KERETE : " + keret + " Ft.");

            if (keret == 0)
            {
                Console.Write("ÍRJON BE EGY ÖSSZEGET : ");
                adat.haviKeret = Convert.ToInt32(ReadingLine(adat.readingString));
                adat.defaultKeret = adat.haviKeret;
            }
            else
            {
                Console.Write("MEGAKARJA VÁLTOZTATNI A HAVI KERETET ? (I/N) : ");
                string ifitis = ReadingLine(adat.readingString);
                if (ifitis == "Y" || ifitis == "y" || ifitis == "I" || ifitis == "i")
                {
                    //HA MÓDOSÍTANI AKARJA 
                    Console.WriteLine("\nKÉRLEK ÜSSE BE AZ ÖSSZEGET");
                    adat.haviKeret = Convert.ToInt32(ReadingLine(adat.readingString));
                    adat.defaultKeret = adat.haviKeret;
                    Console.WriteLine("A KERET EL LETT MENTVE!");
                }
            }

            if(adat.mentes == true)
            {
                Save();
            }
           
            Console.WriteLine("\nTOVÁBB LÉPÉSHEZ NYOMJON MEG EGY GOMBOT");
            Console.ReadKey();
        }
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///
        ///////////////////////////////////////////////////////ELŐZMÉNY MODUL////////////////////////////////////////////////////
        public static void termekek()
        {
            Console.Clear();
            Console.WriteLine("-----------------------------------------------------------------");
            Console.WriteLine("     Termék neve       |       Ára       |      DB      | Hónap |");
            Console.WriteLine("-----------------------------------------------------------------");

            StreamReader sr = new StreamReader("TERMEKEK.txt");
            string[] adatok = new string[4];
            string sor = "";

            while (!sr.EndOfStream)
            {
                sor = sr.ReadLine();
                if (!string.IsNullOrWhiteSpace(sor))
                {
                    adatok = sor.Split(",");
                    //Console.WriteLine(adatok[0] + " " + adatok[1] + "Ft, " + adatok[2] + "DB, " + "Melyik honapban lett veve: " + adatok[3] + ".");
                    Console.WriteLine(String.Format("{0,-22} | {1,-15} | {2,-12} | {3,-5} |", adatok[0], adatok[1] + " Ft", adatok[2] + " DB", adatok[3] + "."));
                }
            }

            sr.Close();
            Console.WriteLine("-----------------------------------------------------------------");

            Console.WriteLine("\nTOVÁBB LÉPÉSHEZ NYOMJON MEG EGY GOMBOT");
            Console.ReadKey();
        }
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///
        ///////////////////////////////////////////////////////KÖLTÉS MODUL//////////////////////////////////////////////////////
        public static void felvetel()
        {
            Console.Clear();
            Console.WriteLine("ÜDVÖZÖLLEK A FELVETELNEL!");
            Console.WriteLine("ITT íRHATJA BE A TERMÉK NEVÉT ÉS MÁS TULAJDONSÁGAIT");
            Console.WriteLine("\n");
            Console.Write("A TERMÉK NEVE: "); string nev = ReadingLine(adat.readingString);
            Console.Write("A TERMÉK ÁRA: "); int ar = Convert.ToInt32(ReadingLine(adat.readingString));
            Console.Write("A TERMÉK DB: "); int db = Convert.ToInt32(ReadingLine(adat.readingString));
            Console.WriteLine("\n");

            adat.haviKeret = adat.haviKeret - ar;
            adat.osszVagyon = adat.osszVagyon - ar;
            if (adat.haviKeret <= 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("A MEGADOTT HAVI KERETNEK A MINIMUMAT ELERTE\n");
                Console.ForegroundColor = ConsoleColor.Gray;
                adat.haviKeret = 0;
            }

            FileStream fs = new FileStream("TERMEKEK.txt", FileMode.Append, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);

            sw.WriteLine(nev + "," + ar + "," + db + "," + adat.adottHonap);

            sw.Flush();
            sw.Close();
            fs.Close();

            Console.WriteLine("A TERMEK EL LETT MENTVE\n");
            Console.WriteLine("\nTOVÁBB LÉPÉSHEZ NYOMJON MEG EGY GOMBOT");
            Console.ReadKey();
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///
        //////////////////////////////////////////////////////////SAVE MODUL/////////////////////////////////////////////////////
        public static void Save()
        {
            FileStream fs = new FileStream("DATA.txt", FileMode.Create, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);

            sw.WriteLine(adat.osszVagyon);
            sw.WriteLine(adat.haviKeret);
            sw.WriteLine(adat.defaultKeret);
            sw.WriteLine(adat.adottHonap);
            sw.WriteLine(adat.mentes);

            sw.Flush();
            sw.Close();
            fs.Close();

            //Console.Clear();
        }
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///
        ///////////////////////////////////////////////////////SEGÍTSÉG MODUL////////////////////////////////////////////////////
        public static void help()
        {
            Console.Clear();
            Console.WriteLine("ÜDVÖZÖLLEK AZ SEGÍTSÉGNÉL!\n");

            Console.Write("AZ 1-ES MENÜPONTON (Osszes Vagyon) TUDJA MÓDOSíTANI AZ ÖN VAGYONÁT AMI"); Console.ForegroundColor = ConsoleColor.Red; Console.Write(" MAXIMUM 2147483647"); Console.ForegroundColor = ConsoleColor.Gray; Console.Write(" Ft LEHET");
            Console.WriteLine("\n");

            Console.Write("A 2-ES MENÜPONTON (Havi Keret) TUDJA MÓDOSíTANI AZT A KERETET AMIT EGY HÓNAPBAN MAXIMUM ELKÖLTHET AMIT"); Console.ForegroundColor = ConsoleColor.Red; Console.Write(" MAXIMUM 2147483647"); Console.ForegroundColor = ConsoleColor.Gray; Console.Write(" Ft-RA ÁLLíTHAT");
            Console.WriteLine("HA AZ ADOTT HÓNAPBAN TÖBBET KÖLÖTT MINT AMEKKORA KERETET KISZABOTT AKKOR A HAVI KERET SORA PIROSAN LESZ LÁTHATÓ. A HAVI KERET VISSZAÁLL EREDETI KISZABOTT ÁLLAPOTÁBA MINDEN ÚJJABB HÓNAPPBAN");
            Console.WriteLine("");

            Console.WriteLine("A 3-AS MENÜPONTON (Termekek) TUDJA MEGTEKINTENI HOGY EDDIG MILYEN TERMÉKEKET VITT BE A RENDSZERBE\n");

            Console.Write("A 4-ES MENÜPONTON (Felvetel) TUDJA FELVENNI AZOKAT A TÁRGYAK -NEVÉT-ÉRTÉKÉT"); Console.ForegroundColor = ConsoleColor.Red; Console.Write("(MAXIMUM 2147483647"); Console.ForegroundColor = ConsoleColor.Gray; Console.Write(" Ft ÉRTÉKBEN)"); Console.Write("-DARABÁT- AMIKET ÖN VETT ÉS EZEKET RÖGTÖN LEVONJUK A HAVI ÉS ÖSSZES VAGYONBÓL, VALAMINT FELVÉTELRE KERÜL A 3-AS MENÜPONTBA (Termekek)");
            Console.WriteLine("\n");

            Console.WriteLine("A 6-OS MENÜPONTPBAN (Beallitasok) LEHET ÁLLíTANI HOGY A PORGRAM CSAK KILÉPÉSKOR VAGY VALAMILYEN VÁLTOZTATÁS UTÁN VALAMINT KILÉPÉSKOR IS MENTSEN \n");

           Console.Write("HA KÉSZ A PROGRAM HASZNÁLATÁVAL AKKOR A 0. MENÜPONTOT HASZNÁLJA A HELYES KILÉPÉS ÉS ADATOK ELMENTÉSE ÉRDEKÉBEN."); Console.ForegroundColor = ConsoleColor.Red; Console.Write(" A PROGRAM A MÓDOSíTOTT ADATAIT CSAK A HELYES KILÉPÉSI MÓD UTÁN MENIT LE! "); Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("\n");



            Console.WriteLine("\nKILÉPÉSHEZ NYOMJON MEG EGY GOMBOT");
            Console.ReadKey();
        }

        public static void settings()
        {
            Console.Clear();
            Console.WriteLine("ÜDVÖZÖLLEK A BEALLITASOKNAL");
            Console.WriteLine("ITT ALLITHATJA BE HOGY A PROGRAM MINDEN VALTOZTATASNAL MENTSEN VAGY CSAK A KILEPESKOR");
            Console.WriteLine("\n");
            Console.Write("FOLYAMATOS MENTES: "); if (adat.mentes == false) { Console.WriteLine("KIKAKPCSOLVA"); } else { Console.WriteLine("BEKAPCSOLVA"); }
            Console.Write("BEÁLLíTJA HOGY FOLYAMATOSAN MENTSEN? (I/N) :");

            //string ifitis = Console.ReadLine();
            string ifitis = ReadingLine(adat.readingString);
            if (ifitis == "Y" || ifitis == "y" || ifitis == "I" || ifitis == "i")
            {
                adat.mentes = true;
            }
            else
            {
                adat.mentes = false;
            }

            Save();
        }


        //Reading biztonsag hogy ne omoljon ossze a program ha egy entert utunk be
        public static string ReadingLine(string s)
        {
            s = "";
            bool isDone = false;
            while (isDone != true)
            {
                s = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(s))
                {
                    isDone = true;
                }
            }
            return s;
        }
    }
}
