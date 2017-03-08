using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp.Repository
{
    public interface IRepository<T>
           where T : class
    {

        ICollection<T> GetList();
        T GetElement(int id);
        void Create(ICollection<T> item);
        void Delete(int id);
    }

}