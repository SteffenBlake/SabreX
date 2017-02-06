using System;
using System.Collections.Generic;
using System.Linq;

namespace SabreX
{
    /// <summary>
    ///     Generic Object type
    /// </summary>
    public class ObjectBase
    {
        public ObjectBase(){}
        
        public ObjectBase(ObjectTemplate template)
        {
            Inherit(template, true);

            Size.MinMax(template.MinParams["Size"], template.MaxParams["Size"]);
            Smell.MinMax(template.MinParams["Smell"], template.MaxParams["Smell"]);
            Taste.MinMax(template.MinParams["Taste"], template.MaxParams["Taste"]);
            Temperature.MinMax(template.MinParams["Temperature"], template.MaxParams["Temperature"]);
            Texture.MinMax(template.MinParams["Texture"], template.MaxParams["Texture"]);
            Volume.MinMax(template.MinParams["Volume"], template.MaxParams["Volume"]);
            Density.MinMax(template.MinParams["Density"], template.MaxParams["Density"]);
            Brightness.MinMax(template.MinParams["Brightness"], template.MaxParams["Brightness"]);
            Style.MinMax(template.MinParams["Style"], template.MaxParams["Style"]);

            DoSpecialLoad();
        }

        //Property Objects for Generation
        public string Name { get; set; }
        public PropertyBase Size = new PropertyBase(typeof(Data.SizeEnum));
        public PropertyBase Smell = new PropertyBase(typeof(Data.SmellEnum));
        public PropertyBase Taste = new PropertyBase(typeof(Data.TasteEnum));
        public PropertyBase Temperature = new PropertyBase(typeof(Data.TemperatureEnum));
        public PropertyBase Texture = new PropertyBase(typeof(Data.TextureEnum));
        public PropertyBase Volume = new PropertyBase(typeof(Data.VolumeEnum));
        public PropertyBase Density = new PropertyBase(typeof (Data.DensityEnum));
        public PropertyBase Brightness = new PropertyBase(typeof (Data.BrightnessEnum));
        public PropertyBase Style = new PropertyBase(typeof (Data.StyleEnum));

        //Library
        public Dictionary<string, Action> Commands = new Dictionary<string, Action>();
        public Dictionary<string, Func<int?, int>> Functions = new Dictionary<string, Func<int?, int>>();
        public ObjectBase Parent = null;

        //Heirachy
        public List<ObjectBase> Surface = new List<ObjectBase>();

        //Values (Readonly)
        public virtual int _size() => Size.Value;
        public virtual int _smell() => Smell.Value + Surface.Sum(p => p._smell());
        public virtual int _taste() => Taste.Value;
        public virtual int _temperature() => Parent?._temperature() ?? Temperature.Value;
        public virtual int _texture() => Texture.Value;
        public virtual int _volume() => Volume.Value + Surface.Sum(p => p._volume());
        public virtual int _density() => Density.Value;
        public virtual int _brightness() => Brightness.Value + Surface.Sum(p => p._brightness());
        public virtual int _weight() => _size()*_density() + Surface.Sum(p => p._weight());
        public virtual int _style() => Style.Value;

        /// <summary>
        ///     Copies all the parent methods to the child, writing over anything with the same name.
        /// </summary>
        /// <param name="parent">Parent object to derive from</param>
        /// <returns></returns>
        public bool Inherit(ObjectBase parent)
        {
            Name = parent.Name;

            foreach (var pair in parent.Commands)
            {
                Commands[pair.Key] = pair.Value;
            }

            foreach (var pair in parent.Functions)
            {
                Functions[pair.Key] = pair.Value;
            }

            return true;
        }

        /// <summary>
        ///     Copies the parent methods to the child.
        /// </summary>
        /// <param name="parent">Parent object to derive from</param>
        /// <param name="maintain">If true, won't overwrite values that already exist.</param>
        /// <returns></returns>
        public bool Inherit(ObjectBase parent, bool maintain)
        {
            if (!maintain)
            {
                Inherit(parent);
                return true;
            }
            Name = parent.Name;
            foreach (var pair in parent.Commands)
            {
                if (!Commands.ContainsKey(pair.Key))
                {
                    Commands[pair.Key] = pair.Value;
                }
            }

            foreach (var pair in parent.Functions)
            {
                if (!Functions.ContainsKey(pair.Key))
                {
                    Functions[pair.Key] = pair.Value;
                }
            }

            return true;
        }
        public void DoSpecialLoad() { }

        public virtual void Generate()
        {
            foreach (var Child in Surface)
            {
                Child.Generate();
            }
        }
    }

    public class PropertyBase
    {
        public PropertyBase(Type enumType)
        {
            var vals = enumType.GetEnumValues().Cast<int>();
            MinVal = vals.Min();
            MaxVal = vals.Max();
        }

        public int Value { get; set; }
        private int MaxVal { get; set; }
        private int MinVal { get; set; }

        public void Generate()
        {
            var rnd = new Random();
            Value = rnd.Next(MinVal, MaxVal + 1);
        }

        public void Max(int val) => MaxVal = val;
        public void Min(int val) => MinVal = val;

        public void MinMax(int min, int max)
        {
            Min(min);
            Max(max);
        }
    }

    public class ObjectContainer : ObjectBase
    {
        //Heirachy
        public List<ObjectBase> Container = new List<ObjectBase>();

        public override int _smell() => Smell.Value + Surface.Sum(p => p._smell()) + Container.Sum(p => p._smell());
        public override int _temperature() => Parent?._temperature() ?? Temperature.Value + Container.Sum(p => p._temperature());
        public override int _volume() => Volume.Value + Surface.Sum(p => p._volume()) + Container.Sum(p => p._volume());
        public override int _weight() => _size()*_density() + Surface.Sum(p => p._weight()) + Container.Sum(p => p._weight());
        public override int _brightness() => Container.Sum(p => p._brightness());

        public override void Generate()
        {
            foreach (var child in Surface)
            {
                child.Generate();
            }

            foreach (var child in Container)
            {
                child.Generate();
            }
        }
    }

    public class ObjectTemplate : ObjectBase
    {
        public Dictionary<string, int> MaxParams = new Dictionary<string, int>()
        {
            { "Size", (int)Data.SizeEnum.Normal },
            { "Smell", (int)Data.SmellEnum.Nothing },
            { "Taste", (int)Data.TasteEnum.Nothing },
            { "Temperature", (int)Data.TemperatureEnum.Normal },
            { "Texture", (int)Data.TextureEnum.Normal },
            { "Volume", (int)Data.VolumeEnum.Silent },
            { "Density", (int)Data.DensityEnum.Normal },
            { "Brightness", (int)Data.BrightnessEnum.Normal },
            { "Style", (int)Data.StyleEnum.Dull },
        };

        public Dictionary<string, int> MinParams = new Dictionary<string, int>()
        {
            { "Size", (int)Data.SizeEnum.Normal },
            { "Smell", (int)Data.SmellEnum.Nothing },
            { "Taste", (int)Data.TasteEnum.Nothing },
            { "Temperature", (int)Data.TemperatureEnum.Normal },
            { "Texture", (int)Data.TextureEnum.Normal },
            { "Volume", (int)Data.VolumeEnum.Silent },
            { "Density", (int)Data.DensityEnum.Normal },
            { "Brightness", (int)Data.BrightnessEnum.Normal },
            { "Style", (int)Data.StyleEnum.Dull },
        };
    }
}