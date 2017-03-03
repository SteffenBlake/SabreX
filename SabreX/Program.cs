using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SabreX;

namespace SabreX
{
    public class Program
    {
        public static ObjectFactory FACTORY = new ObjectFactory();

        static void Main(string[] args)
        {
            Templates TEMPLATES = new Templates();

            FACTORY.Generate(out ObjectBase Room, TEMPLATES.BaseTemplateList.First());

            Room.Generate();
        }

        public static ObjectFactory CurrentFactory()
        {
            return FACTORY;
        }
    }
}
