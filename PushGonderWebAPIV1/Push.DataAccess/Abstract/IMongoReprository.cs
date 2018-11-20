using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using MongoDB.Driver;
using Push.Entity.Concreate;
using Push.Entity.Model;

namespace Push.DataAccess.Abstract
{
    public interface IMongoReprository<T> where T : class, IEntity, new()//genereic kısıtlar
    {
        /*  void AddMongoDb(T entity, string collectionName);
          int DeleteMongoDb(T entity, string collectionName);
          MongoCollection<T> Get(Expression<Func<T, bool>> filter = null);
           List<T> GetEntityList<T>();*/

        T Get(int i, string tblName);
        IQueryable<T> GetAll(string tblName);
        void Add(T entity, string tblName);
        void Delete(Expression<Func<T, int>> queryExpression, int id, string tblName);
        void Update(Expression<Func<T, int>> queryExpression, int id, T entity, string tblName);

    }

}
