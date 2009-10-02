using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDMX
{

    public class Dimension : Component
    {
        public bool IsMeasureDimension { get; set; }
        
        public Dimension(Concept concept)
            : base(concept)
        {

        }

        public Dimension(Concept concept, CodeList codeList)
            : base(concept, codeList)
        {
        }
    }
}