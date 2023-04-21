using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GTranslatorAPI;

namespace Board.Application.AppData.Contexts.Translator
{
    public interface ITranslatorService
    {
        /// <summary>
        /// Перевод сообщения
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        string Translate(string from,string to,string message);


    }
}
