using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtistAgency.Presentation
{
    public class SubMenuUsers
    {
        string Option { get; set; }
        bool Finished { get; set; }
        SubMenuClient SC { get; set; }

        public SubMenuUsers()
        {
            Option = "0";
            Finished = false;
            SC = new SubMenuClient();

        }

        #region UsersSubmenu
        public void showSubmenuUsers()
        {
            do
            {
                Console.Clear();
                Console.WriteLine("1 - Employees");
                Console.WriteLine("2 - Clients");
                Console.WriteLine("3 - Back");

                Console.WriteLine("Choose an option: ");
                Option = Console.ReadLine();
                SelectOptionUsersSubmenu();

                if (Finished == false)
                {
                    Console.WriteLine();
                    Console.WriteLine("Pres any key to continue: ");
                    Console.ReadKey();
                }
            } while (Option != "3");

            Option = "0";
            Finished = false;

        }

        public void SelectOptionUsersSubmenu()
        {
            switch (Option)
            {
                case "1":
                    //SubmenuEmployees();
                    break;
                case "2":
                    SC.showClientsOptions();
                    break;
                case "3":
                    Finished = true;
                    break;
            }

        }

        #endregion
    }
}
