using Push.Business.Abstract;
using Push.DataAccess.Abstract;
using Push.Entity.Concreate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Push.Business.Concreate
{
    public class LoginTempManager : ILoginTempService
    {
        private ILoginTempDal _iDal;
        public LoginTempManager(ILoginTempDal iDal)
        {
            _iDal = iDal;
        }
        public void AddTemp(LoginTemp deneeme)
        {
            _iDal.AddTemp(deneeme);
        }
    }
}
