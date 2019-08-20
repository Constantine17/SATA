using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SATA.Global
{
    public class Static
    {
        public static string DataFormat = "dd'/'MM'/'yyyy HH:mm";

        public const string RegexEmail = @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                      @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";
    }
}
