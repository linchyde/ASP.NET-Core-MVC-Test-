using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBookDataAccess.Repository.IRepository
{
    public interface Irepository<T> where T : class
    {
        // assume T is a Category
        //we are looking at the core project controller file to see the methods needed here
        T GetFirstOrDefault(Expression<Func<T,bool>> filter);
        IEnumerable<T> GetAll();
        //Adding an object
        void Add(T entity);
        //edit based on id using the GetFirstOrDefault above

        //below for the remove or delete category
        void Remove(T entity);

        void RemoveRange(IEnumerable<T> entity);


    }
}
