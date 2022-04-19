using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace sqlDataBase.Geliştirme.abstrac
{
    public interface IRepository<T> where T :class, new()
    {
        List<T> GetAll(Expression<Func<T, bool>> filter = null);

        T GetById(int Id);
        void Kaydet(T entity);
        void Düzenle(T entity);
        void Sil(T entity);

    }
}
