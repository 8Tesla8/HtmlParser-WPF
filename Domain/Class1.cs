using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Model;

namespace Domain
{
    public class Class1
    {
        public List<HtmlElements> Get()
        {
            using (dbEntities db = new dbEntities())
            {
                 return db.HtmlElements.ToList();

            }


            //using (ModelDb db = new ModelDb())
            //{
            //    return db.HtmlElements.ToList();

            //    //HtmlElements h = new HtmlElements();
            //}
        }
    }
}
