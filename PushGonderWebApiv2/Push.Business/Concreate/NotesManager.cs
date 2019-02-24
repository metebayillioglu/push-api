using Push.Business.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Push.Business.Concreate
{
    public class NotesManager : INotesService
    {
        public string GetNote(string key)
        {
           var notes = System.IO.File.ReadAllText(@"C:\PushForever\Push.txt");
            var splitNotes = notes.Split("\r\n");
            foreach(var x in splitNotes)
            {
                if(x.Contains(key))
                {
                    return x.Replace(key + "=", "");

                }
            }

            return "";

        }
    }
}
