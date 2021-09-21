using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace posV2
{
    class DBconnection
    {
        public string Myconnection() {

            string con = @"Data Source=DESKTOP-L325EO9;Initial Catalog=posV2;User ID=sa;Password=bunay123";
            return con;
        }
    }
}
