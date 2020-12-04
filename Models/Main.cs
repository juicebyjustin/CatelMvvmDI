using Catel;
using Catel.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatelMvvmDI.Models
{

    [Serializable]
    public class Main : ModelBase
    {
        public Main()
        {
        }

        public string WindowTitle { get; set; }
        public string Guid { get; set; }
    }
}
