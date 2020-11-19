using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonInterfaces;

namespace GUIandStatistics
{
    class Statistics : IStatistics
    {
        public double CalculatecertainErrorDimensions(DataTable MLResult, DataTable realResut)
        {
            throw new NotImplementedException();
        }

        public DataTable confusionMatrix(DataTable MLResult, DataTable realResut)
        {
            throw new NotImplementedException();
        }
    }
}
