using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Common;
using OXM;

namespace SDMX.Parsers
{
    public class TimeDimensionMap : CompoenentMap<TimeDimension>
    {
        TimeDimension _timeDimension;

        public TimeDimensionMap(DSD dsd)
            : base(dsd)
        {
            AttributesOrder("conceptRef",
                            "codelist",
                            "crossSectionalAttachmentLevel");

            ElementsOrder("TextFormat", "Annotations");
        }

        protected override TimeDimension Create(Concept conecpt)
        {
            _timeDimension = new TimeDimension(conecpt);
            return _timeDimension;
        }

        protected override void SetAnnotations(IEnumerable<Annotation> annotations)
        {
            annotations.ForEach(i => _timeDimension.Annotations.Add(i));
        }

        protected override TimeDimension Return()
        {
            return _timeDimension;
        }
    }
}