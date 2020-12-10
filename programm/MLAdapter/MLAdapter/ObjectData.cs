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
    public class ObjectData
    {
        // ein Objekt dieser Klasse entspricht einer Zeile, alle Input-Werte der Zeile werden im Array gespeichert
        public float[] FloatFeatures { get; set; }

        //public int[] CategoricalFeatures { get; set; }

        // der Wert der Zielspalte, evtl ändern in int
        public int Target { get; set; }

        public ObjectData(float[] floatFeatures, int target)
        {
            this.FloatFeatures = floatFeatures;
            //this.CategoricalFeatures = categoricalFeatures;
            this.Target = target;
        }
    }
}
