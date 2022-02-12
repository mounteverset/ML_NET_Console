using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ML.Data;

namespace MLAdapter
{
    public class ObjectPrediction
    {
        [ColumnName("Score")]
        public float[] Predictions { get; set; }
        [ColumnName("PredictedLabel")]
        public uint Label { get; set; }
    }
}
