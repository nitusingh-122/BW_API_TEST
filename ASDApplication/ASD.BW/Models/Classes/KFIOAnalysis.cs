using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASD.BW.Models.Classes
{
    public class KFIOAnalysis
    {
        public List<KFIOMode> KfioModes { get; set; }
        public List<KFIOException> KfioExceptions { get; set; }
        //public void AddNewKfioMode(int rowCount, string mode)
        //{
        //    KfioModes.Add(new KFIOMode(rowCount, mode));
        //}
        //public void AddNewKfioException(string kfExceptionAggregation, int maxCount)
        //{
        //    KfioExceptions.Add(new KFIOException(kfExceptionAggregation, maxCount));
        //}
    }

    public class KFIOMode
    {
        public int RowCount { get; set; }
        public string Mode { get; set; }

        //public KFIOMode(int rowCount, string mode)
        //{
        //    this.RowCount = rowCount;
        //    this.Mode = mode;
        //}
    }

    public class KFIOException
    {
        public string KFExceptionAggregation { get; set; }
        public int MaxCount { get; set; }

        //public KFIOException(string kfExceptionAggregation, int maxCount)
        //{
        //    this.KFExceptionAggregation = kfExceptionAggregation;
        //    this.MaxCount = maxCount;
        //}
    }
}
