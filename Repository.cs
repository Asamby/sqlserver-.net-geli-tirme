
using sqlDataBase.Geliştirme.abstrac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace sqlDataBase.Geliştirme
{
    public class Repository<T> : IRepository<T> where T : class, new()
    {
        SqlCekme<T> depo;
        public Repository()
        {
            depo = new SqlCekme<T>();
        }
        public void Düzenle(T entity)
        {
            depo.düzenle(entity);
        }

     

        public List<T> GetAll(Expression<Func<T, bool>> filter = null)
        {
            if (filter == null)
            {
                return depo.Tolist();
            }
            else
                return depo.Tolist().Where(x=>x.Equals(filter)).ToList();



        }

        public T GetById(int Id)
        {
         return  depo.GetById(Id);
        }

        public void Kaydet(T entity)
        {
           depo.kaydet(entity);
        }

        public void Sil(T entity)
        {
            depo.sil(entity);
        }
    }
}
