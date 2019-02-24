using System;
using System.Collections.Generic;
using System.Text;

namespace Push.Business.Abstract
{
   public interface INotesService
    {
        string GetNote(string key);
    }
}
