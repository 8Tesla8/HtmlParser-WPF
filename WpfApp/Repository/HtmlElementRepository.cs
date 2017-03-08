using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Model;

namespace WpfApp.Repository
{
    public class HtmlElementRepository : IRepository<HtmlElements>
    {
        public void Create(ICollection<HtmlElements> items)
        {
            using (SqliteModel db = new SqliteModel())
            {
                //db.HtmlElements.AddRange(items);
                //db.SaveChanges();

                //foreach (var item in items)
                //{
                //    db.HtmlElements.Add(item);
                //    db.SaveChanges();
                //}
                try
                {
                    long maxId = 0;

                    var elementWitMaxId = db.HtmlElements.OrderByDescending(u => u.Id).FirstOrDefault();

                    if (elementWitMaxId != null)
                    {
                        maxId = elementWitMaxId.Id;
                    }

                    maxId++;

                    foreach (var item in items)
                    {
                        db.HtmlElements.Add( new HtmlElements()
                        {
                            Id  = maxId++,
                            Url = item.Url,
                            Html = item.Html,
                            TypeId = item.TypeId,
                            Content = item.Content,
                        });
                        db.SaveChanges();
                    }
                }
                catch (DbEntityValidationException e)
                {
                    List<string> list = new List<string>();

                    foreach (var eve in e.EntityValidationErrors)
                    {
                        list.Add(string.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State));
                        foreach (var ve in eve.ValidationErrors)
                        {
                            list.Add(string.Format("- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage));
                        }
                    }
                    throw;
                }

            }
        }

        public void Delete(int id)
        {
            using (SqliteModel db = new SqliteModel())
            {
                var found = db.HtmlElements.Find(id);
                if (found != null)
                {
                    db.HtmlElements.Remove(found);
                    db.SaveChanges();
                }
            }
        }

        public HtmlElements GetElement(int id)
        {
            using (SqliteModel db = new SqliteModel())
            {
               return db.HtmlElements.Find(id);
            }
        }

        public ICollection<HtmlElements> GetList()
        {
            using (SqliteModel db = new SqliteModel())
            {
                return db.HtmlElements.ToList();
            }
        }
    }
}
