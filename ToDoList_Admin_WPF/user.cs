using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList_Admin_WPF
{
    public class User
    {
        private int id;
        public User(int id)
        {
            this.id = id;
        }

        public void updateUser()
        {
            //Stub
        }

        public int getUserId()
        {
            return id;
        }

        public override string ToString()
        {
            return "Stub user";
        }
    }
}
