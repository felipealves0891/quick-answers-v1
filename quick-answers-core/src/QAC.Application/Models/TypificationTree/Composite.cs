using System;
using System.Collections.Generic;

namespace QAC.Application.Models.TypificationTree
{
    public class Composite : TypificationComponent
    {
        public override void Add(TypificationComponent component)
        {
            Children.Add(component);
        }

        public override void Remove(TypificationComponent component)
        {
            Children.Remove(component);
        }
    }
}
