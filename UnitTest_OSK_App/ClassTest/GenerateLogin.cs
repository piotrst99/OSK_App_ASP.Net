using OSK_App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest_OSK_App.ClassTest
{
    public class GenerateLogin{
        public UserLogin userLogin;

        public string CreateLogin(string firstName, string surname, int userID) {
            string userName = "";
            userName += checkNationalCharacter(firstName.Substring(0, 1)) + "" + checkNationalCharacter(surname.Substring(0, 1));

            userName += userID >= 1000 ? userID.ToString() :
                userID >= 100 ? ("0" + userID.ToString()) :
                    userID >= 10 ? ("00" + userID.ToString()) :
                        ("000" + userID.ToString());

            return userName;
        }

        private static string checkNationalCharacter(string val) {
            switch (val) {
                case "Ą": return "A"; break;
                case "Ć": return "C"; break;
                case "Ę": return "E"; break;
                case "Ł": return "L"; break;
                case "Ń": return "N"; break;
                case "Ó": return "O"; break;
                case "Ś": return "S"; break;
                case "Ź": return "Z"; break;
                case "Ż": return "Z"; break;
                default: return val; break;
            }
        }

    }
}
