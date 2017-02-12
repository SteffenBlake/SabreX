using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SabreX
{
    class Templates
    {
        public List<ObjectBase> BaseTemplateList = new List<ObjectBase>();
        public List<ObjectBase> SurfaceTemplateList = new List<ObjectBase>();
        public List<ObjectBase> ContainerTemplateList = new List<ObjectBase>();
        public List<ObjectBase> CompoundTemplateList = new List<ObjectBase>();

        public Templates()
        {
            BaseTemplateList.Add(new ObjectBase(true)
            {

            });
        }
    }
}
