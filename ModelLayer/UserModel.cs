using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
    public class UserModel
    {
        private string _username;
        private string _password;

        // datos SalesForce
        private string userNameSf;
        //private string passWordSf;

        public string Username
        {
            get
            {
                return _username;
            }
            set
            {
                _username = value;
            }
        }

        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
            }
        }

        public string UserNameSf
        {
            get
            {
                return userNameSf;
            }
            set
            {
                userNameSf = value;
            }
        }

        //public string PasswordSf
        //{
        //    get
        //    {
        //        return passWordSf;
        //    }
        //    set
        //    {
        //        passWordSf = value;
        //    }
        //}
    }
}
