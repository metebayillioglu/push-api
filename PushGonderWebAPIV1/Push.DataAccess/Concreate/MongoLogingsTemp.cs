using Push.Entity.Concreate;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Push.DataAccess.Abstract;
namespace Push.DataAccess.Concreate
{
    public class MongoLogingsTemp : MongoEntityRepositoryBase<LoginTemp>, ILoginTempDal
    {
        public void AddTemp(LoginTemp deneeme)
        {
            Add(deneeme,"LoginTest");
        }
    }
}
