using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp.Parser
{
    public enum HtmlType
    {
        Title = 1,
        Description,
        Ahref,
        Link,
        Img,
        H1,
    }

    static class TypeHtml
    {
        public static string GetType(long type)
        {
            switch (type)
            {
                case 1:
                    return "Title";
                case 2:
                    return "Description";
                case 3:
                    return "Ahref";
                case 4:
                    return "Link";
                case 5:
                    return "Img";
                case 6:
                    return "H1";

                default:
                    return null;
            }
        }
    } 
}
