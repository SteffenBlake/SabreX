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

        static void Main(string[] args)
        {
            ObjectFactory FACTORY = new ObjectFactory();
            Templates TEMPLATES = new Templates();

            FACTORY.Generate(out ObjectBase Room, TEMPLATES.BaseTemplateList.First());

            Room.Generate(FACTORY);
        }
    }
}
