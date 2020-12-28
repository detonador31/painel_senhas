using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace PainelDeSenhas.Serialization
{
    public class Config
    {
        public string ArquivoSom { get; set; }
        public string Voz { get; set; }
        public int Volume { get; set; }
        public string WebSocketURL { get; set; }
        public string DateSenhas { get; set; }
    }

}
