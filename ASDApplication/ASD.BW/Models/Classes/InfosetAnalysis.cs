using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASD.BW.Models.Classes
{
    public class InfosetAnalysis
    {
        public string Infoset { get; set; }
        public string InfosetDesc { get; set; }
        public string InfosetInfoCube { get; set; }
        public string InfosetInfoObject { get; set; }
        public string InfosetDSO { get; set; }
        public int RowCount { get; set; }
        public string Mode { get; set; }
    }
}
