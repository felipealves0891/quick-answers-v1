using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QAC.Application.Models.TypificationTree
{
    public class Leaf : TypificationComponent
    {
        public override bool IsComposite()
        {
            return false;
        }

    }
}
