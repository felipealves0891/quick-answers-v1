using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QAC.Application.Models.TypificationTree
{
    public class TypificationTree
    {
        protected IEnumerable<Typification> typifications;

        protected List<TypificationComponent> components = new List<TypificationComponent>();

        public TypificationTree(IEnumerable<Typification> typifications)
        {
            this.typifications = typifications;
        }

        public List<TypificationComponent> AssembleComponent() 
        {
            var trunk = typifications.Where(typification => typification.ParentId == 0);

            foreach(Typification typification in trunk) 
            {
                bool isComposite = typifications.Where(t => t.ParentId == typification.Id)
                                                .ToList()
                                                .Count() > 0;

                TypificationComponent component = factoryComponent(typification, isComposite);

                if (component.IsComposite()) 
                {
                    var c = Mount(component);
                    components.Add(c);
                }
                else 
                {
                    components.Add(component);
                }
                    

            }

            return components;
        }

        public TypificationComponent Mount(TypificationComponent component) 
        {
            var branch = typifications.Where(t => t.ParentId == component.Id);

            foreach (Typification typification in branch)
            {
                bool isComposite = typifications.Where(t => t.ParentId == typification.Id)
                                                .ToList()
                                                .Count() > 0;

                TypificationComponent c = factoryComponent(typification, isComposite);

                if (component.IsComposite())
                    component.Add(Mount(c));
                else
                    component.Add(c);

            }

            return component;

        }

        public TypificationComponent factoryComponent(Typification typification, bool composite = true) 
        {
            TypificationComponent component;

            if (composite)
                component = new Composite();
            else
                component = new Leaf();

            component.Id = typification.Id;
            component.Name = typification.Name;
            component.Description = typification.Description;

            return component;
                
        }
    }
}
