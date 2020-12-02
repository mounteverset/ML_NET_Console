using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLAdapter
{
    /// <summary>
    /// Class required to handle inputs from various DataTables where the exact types and names are unknown
    /// </summary>
    class ObjectData
    {
        public float[] FloatFeatures;

        public int[] CategoricalFeatures;

        public float Target;
    }
}
