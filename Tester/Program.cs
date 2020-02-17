using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Tester
{
    class Program
    {
        static void Main(string[] args)
        {
           string str = "gjhghjghjhj<div class=\"content-pic\">\r\n<a href=\"5346_5.html\"><img alt=\"活泼女孩允儿肉丝高跟可爱又性感(图4)\" src=\"https://img1.mmmw.net/pic/5346/4.jpg\"></a></div>kjhjkhkj";
           string pattern = "<div class=\"content-pic\"><a href=\"[\\S]*\"><img alt=\"[\\S\\s]*\" src=\"[\\S]*.jpg\"></a></div>";
           Regex regex = new Regex(pattern, RegexOptions.Singleline);
           Match mt = regex.Match(str.Replace("\r","").Replace("\n",""));
           Console.WriteLine(mt.Value);

           Console.Read();
        }
    }
}
