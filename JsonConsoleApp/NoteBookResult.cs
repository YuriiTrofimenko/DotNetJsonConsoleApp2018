using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonConsoleApp
{
    //Модель ответа от сервера, когда он содержит массив объектов "Состояние"
    class NoteBookResult
    {
        public IList<State> PagesData { get; set; }
    }
}
