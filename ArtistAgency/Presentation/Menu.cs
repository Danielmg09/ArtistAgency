using ArtistAgency.Services;
using ArtistAgency.Services.DbService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtistAgency.Presentation
{
    public class Menu
    {
        #region ClassAtributes

        public SubMenuJob SJ { get; set; }
        public SubMenuClient SC { get; set; }
        public SubMenuUsers SU { get; set; }

        string Option { get; set; }
        bool Finished { get; set; }

        public Menu()
        {
            
            SU = new SubMenuUsers();
            SC = new SubMenuClient();
            SJ = new SubMenuJob();
            Finished = false;
            Option = "";
        }
        #endregion

        #region Run Application

        public void run()
        {
            JobServiceDB.loadJobListfromDB(JobServiceDB.DBaccess);
            UserServiceDB.loadUserListfromDB(JobServiceDB.DBaccess);

            Option = "0";
            Finished = false;

            do
            {
                Console.Clear();
                MainMenu();
                Console.WriteLine("Choose an option: ");
                Option = Console.ReadLine();
                SelectOptionMainMenu();

                if (Finished == false)
                {
                    Console.WriteLine();
                    Console.WriteLine("Pres any key to continue: ");
                    Console.ReadKey();
                }
            } while (Option != "3");
        }

        #endregion

        #region MainMenu
        public void MainMenu()
        {
            Console.WriteLine("1 - Clients");
            Console.WriteLine("2 - Jobs");
            Console.WriteLine("3 - Exit");
        }



        public void SelectOptionMainMenu()
        {
            switch (Option)
            {
                case "1":
                    SC.showClientsOptions();
                    break;
                case "2":
                    SJ.showJobOptions();
                    break;
                case "3":
                    Finished = true;
                    break;
            }
        }
        #endregion


    }
}
