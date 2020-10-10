using System;
using System.Collections.Generic;

namespace QAC.Application.Models.TypificationTree
{
    public abstract class TypificationComponent
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public List<TypificationComponent> Children { get; }

        public TypificationComponent() 
        {
            Children = new List<TypificationComponent>();
        }

        public virtual bool IsComposite()
        {
            return true;
        }

        public virtual void Add(TypificationComponent component) 
        {
            throw new NotImplementedException();
        }

        public virtual void Remove(TypificationComponent component)
        {
            throw new NotImplementedException();
        }
    }
}
