using System;
using System.Collections.Generic;
using System.Text;

namespace SystemModel
{
    public class LoginModel
    {
        public LoginModel()
        {

        }
        private int _ID;

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        private string _LoginName;

        public string LoginName
        {
            get { return _LoginName; }
            set { _LoginName = value; }
        }
        private string _LoginPassword;

        public string LoginPassword
        {
            get { return _LoginPassword; }
            set { _LoginPassword = value; }
        }
    }
}