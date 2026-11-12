using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILDMS.Model
{
    // OBS_ClientReqHeader.cs  (or put inside your Models folder)
    public class OBS_ClientReqHeader
    {
        public string ClientReqID { get; set; }
        public string ClientReqNo { get; set; }
        public string Remarks { get; set; }          // Project Name
        public string ClientName { get; set; }
        public string SetBy { get; set; }
        public string SetByName { get; set; }
        public string SetOn { get; set; }
    }
}
