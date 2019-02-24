using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Bson;
using Push.Entity.Concreate;
using Push.DataAccess.Abstract;
using Push.Entity.Model;

namespace Push.DataAccess.Concreate
{
    public class MongoEntityRepositoryBase<TEntity> : IMongoReprository<TEntity>
        where TEntity : class, IEntity, new()
    



        //<TEntity> where TEntity : class, IEntity, new()
    {
        public MongoDatabase _database;
        public string _tableName;
        public MongoCollection<TEntity> _collection;

        public MongoEntityRepositoryBase()
        {
            var connectionString = "mongodb://localhost:27017";
        
            var client = new MongoClient(connectionString);
            var server = client.GetServer();
            var databaseName = "PushSender";
            _database = server.GetDatabase(databaseName);

        }
        /// <summary>
        /// Generic Get method to get record on the basis of id
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public TEntity Get(int i, string tblName)
        {

            _collection = _database.GetCollection<TEntity>(tblName);
            return _collection.FindOneById(i);

        }

        public Announcements GetAnnouncementsFromId(string announcementsId)
        {
            var res = Query<Announcements>.EQ(p => p.AnnouncementId, announcementsId);
            return _database.GetCollection<Announcements>("Announcements").FindOne(res);
        }

        public void DeleteAnnouncements(Announcements duyuru)
        {
            var res = Query<Announcements>.EQ(pd => pd.AnnouncementId, duyuru.AnnouncementId);
            var sorgu = _database.GetCollection<Announcements>("Announcements").Find(Query<Announcements>.EQ(pd => pd.AnnouncementId, duyuru.AnnouncementId)).ToList()[0];
            duyuru.isDelete = 1;
            duyuru.Id = sorgu.Id;
         
            var operation = Update<Announcements>.Replace(duyuru);

            _database.GetCollection<Announcements>("Announcements").Update(res, operation);

        }

    

      
        /// <summary>
        /// Get all records 
        /// </summary>
        /// <returns></returns>
        public IQueryable<TEntity> GetAll(string tblName)
        {
            _collection = _database.GetCollection<TEntity>(tblName);
            MongoCursor<TEntity> cursor = _collection.FindAll();
            return cursor.AsQueryable<TEntity>();

        }
        public void Add(TEntity entity, string tblName)
        {
            _collection = _database.GetCollection<TEntity>(tblName);
            _collection.Insert(entity);
        }
        /// <summary>
        /// Generic add method to insert enities to collection 
        /// </summary>
        /// <param name="entity"></param>
     

        /// <summary>
        /// Generic delete method to delete record on the basis of id
        /// </summary>
        /// <param name="queryExpression"></param>
        /// <param name="id"></param>
        public void Delete(Expression<Func<TEntity, int>> queryExpression, int id, string tblName)
        {
            _collection = _database.GetCollection<TEntity>(tblName);
            var query = Query<TEntity>.EQ(queryExpression, id);
            _collection.Remove(query);
        }

        /// <summary>
        ///  Generic update method to delete record on the basis of id
        /// </summary>
        /// <param name="queryExpression"></param>
        /// <param name="id"></param>
        /// <param name="entity"></param>
        public void Update(Expression<Func<TEntity, int>> queryExpression, int id, TEntity entity, string tblName)
        {
            _collection = _database.GetCollection<TEntity>(tblName);
            var query = Query<TEntity>.EQ(queryExpression, id);
            _collection.Update(query, Update<TEntity>.Replace(entity));
        }

       
    }
}

