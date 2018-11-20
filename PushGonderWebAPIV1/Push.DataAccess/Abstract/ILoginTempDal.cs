using Push.Entity.Concreate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Push.DataAccess.Abstract
{
    public interface ILoginTempDal : IMongoReprository<LoginTemp>
    {
        void AddTemp(LoginTemp deneeme);
    }
}
