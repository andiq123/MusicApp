using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ttsBackEnd.Services.Youtube_DL.Models
{
    public class ProgressArgs : EventArgs
    {
        public string status { get; set; }
    }
}
