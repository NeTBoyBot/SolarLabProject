using GTranslatorAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board.Application.AppData.Contexts.Translator
{
    public class TranslatorService : ITranslatorService
    {
        public string Translate(string from, string to, string message)
        {
            var translator = new GTranslatorAPIClient();

            var data = translator.Translate(from, to, message);

            return data.TranslatedText;
        }
    }
}
