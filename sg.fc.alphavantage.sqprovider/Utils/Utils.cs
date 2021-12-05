using Nancy.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sg.Fc.Alphavantage.Sqprovider
{
    public class Utils
    {
        public static Dictionary<string, dynamic> JsonDeserialize(string Json)
        {
            var jss = new JavaScriptSerializer();
            var dict = jss.Deserialize<Dictionary<string, dynamic>>(Json);
            return dict;
        }
    }
}
