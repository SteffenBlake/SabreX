using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SabreX
{
    class Templates
    {
        List<ObjectTemplate> TemplateList = new List<ObjectTemplate>();

        public void InitializeTemplates()
        {
            TemplateList.AddRange( new List<ObjectTemplate>()
            {
                new ObjectTemplate()
                {
                    Name = "Chair",
                    MaxParams = new Dictionary<string, int>()
                    {
                        {"Size", (int) Data.SizeEnum.Big},
                        {"Texture", (int) Data.TextureEnum.Bumpy},
                        {"Style", (int) Data.StyleEnum.Elegant}
                    },
                    MinParams = new Dictionary<string, int>()
                    {
                        {"Size", (int) Data.SizeEnum.Small},
                        {"Texture", (int) Data.TextureEnum.Rough},
                        {"Style", (int) Data.StyleEnum.Creepy}
                    }
                },

                new ObjectTemplate()
                {
                    Name = "Table",
                    MaxParams = new Dictionary<string, int>()
                    {
                        {"Size", (int) Data.SizeEnum.Big},
                        {"Texture", (int) Data.TextureEnum.Bumpy},
                        {"Style", (int) Data.StyleEnum.Elegant}
                    },
                    MinParams = new Dictionary<string, int>()
                    {
                        {"Size", (int) Data.SizeEnum.Small},
                        {"Texture", (int) Data.TextureEnum.Rough},
                        {"Style", (int) Data.StyleEnum.Creepy}
                    }
                },

            });
        }
    }
}
