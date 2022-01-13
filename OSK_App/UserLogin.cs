using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OSK_App
{
    public class UserLogin
    {
        public static string CreateLogin(string FirstName, string Surname, int userID) {
            string userName = "";
            userName += checkNationalCharacter(FirstName.Substring(0, 1)) + "" + checkNationalCharacter(Surname.Substring(0, 1));

            userName += userID >= 1000 ? userID.ToString() :
                userID >= 100 ? ("0" + userID.ToString()) :
                    userID >= 10 ? ("00" + userID.ToString()) :
                        ("000" + userID.ToString());

            return userName;
        }

        public static string CreatePassword() {
            Random r = new Random();
            return r.Next(10000, 99999).ToString();
        }

        private static string checkNationalCharacter(string val) {
            switch(val){
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
