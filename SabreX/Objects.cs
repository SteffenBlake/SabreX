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

        //Property Objects for Generation
        public string Name { get; set; }

        //Properties
        public BrightnessProperty Brightness = new BrightnessProperty();
        public DensityProperty Density = new DensityProperty();
        public SizeProperty Size = new SizeProperty();
        public SmellProperty Smell = new SmellProperty();
        public TasteProperty Taste = new TasteProperty();
        public TemperatureProperty Temperature = new TemperatureProperty();
        public TextureProperty Texture = new TextureProperty();
        public VolumeProperty Volume = new VolumeProperty();
        public StyleProperty Style = new StyleProperty();

        //Library
        public Dictionary<string, Action> Commands = new Dictionary<string, Action>();
        public Dictionary<string, Func<int?, int>> Functions = new Dictionary<string, Func<int?, int>>();
        public ObjectBase Parent = null;

        //Heirachy
        public List<ObjectBase> Surface = new List<ObjectBase>();


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

            (Brightness.Min, Brightness.Max) = parent.Brightness.MinMax();
            (Density.Min, Density.Max) = parent.Density.MinMax();
            (Size.Min, Size.Max) = parent.Size.MinMax();
            (Smell.Min, Smell.Max) = parent.Smell.MinMax();
            (Taste.Min, Taste.Max) = parent.Taste.MinMax();
            (Temperature.Min, Temperature.Max) = parent.Temperature.MinMax();
            (Texture.Min, Texture.Max) = parent.Texture.MinMax();
            (Volume.Min, Volume.Max) = parent.Volume.MinMax();
            (Style.Min, Style.Max) = parent.Style.MinMax();

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
            foreach (var child in Surface)
            {
                child.Generate();
            }
        }
    }

    public class ObjectContainer : ObjectBase
    {
        //Heirachy
        public List<ObjectBase> Container = new List<ObjectBase>();

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

    public class BrightnessProperty
    {
        public Data.BrightnessEnum Value { get; set; }
        public Data.BrightnessEnum Min { get; set; }
        public Data.BrightnessEnum Max { get; set; }

        public BrightnessProperty()
        {
            List<Data.BrightnessEnum> enumList = Enum.GetValues(typeof(Data.BrightnessEnum)).Cast<Data.BrightnessEnum>().ToList();
            Value = Data.BrightnessEnum.Normal;
            Min = enumList.Min();
            Max = enumList.Max();
        }

        public (Data.BrightnessEnum, Data.BrightnessEnum) MinMax()
        {
            return (Min, Max);
        }
    }
    public class DensityProperty
    {
        public Data.DensityEnum Value { get; set; }
        public Data.DensityEnum Min { get; set; }
        public Data.DensityEnum Max { get; set; }

        public DensityProperty()
        {
            List<Data.DensityEnum> enumList = Enum.GetValues(typeof(Data.DensityEnum)).Cast<Data.DensityEnum>().ToList();
            Value = Data.DensityEnum.Normal;
            Min = enumList.Min();
            Max = enumList.Max();
        }
        public (Data.DensityEnum, Data.DensityEnum) MinMax()
        {
            return (Min, Max);
        }
    }
    public class SizeProperty
    {
        public Data.SizeEnum Value { get; set; }
        public Data.SizeEnum Min { get; set; }
        public Data.SizeEnum Max { get; set; }

        public SizeProperty()
        {
            List<Data.SizeEnum> enumList = Enum.GetValues(typeof(Data.SizeEnum)).Cast<Data.SizeEnum>().ToList().ToList();
            Value = Data.SizeEnum.Normal;
            Min = enumList.Min();
            Max = enumList.Max();
        }
        public (Data.SizeEnum, Data.SizeEnum) MinMax()
        {
            return (Min, Max);
        }
    }
    public class SmellProperty
    {
        public Data.SmellEnum Value { get; set; }
        public Data.SmellEnum Min { get; set; }
        public Data.SmellEnum Max { get; set; }

        public SmellProperty()
        {
            List<Data.SmellEnum> enumList = Enum.GetValues(typeof(Data.SmellEnum)).Cast<Data.SmellEnum>().ToList();
            Value = Data.SmellEnum.Nothing;
            Min = enumList.Min();
            Max = enumList.Max();
        }
        public (Data.SmellEnum, Data.SmellEnum) MinMax()
        {
            return (Min, Max);
        }
    }
    public class TasteProperty
    {
        public Data.TasteEnum Value { get; set; }
        public Data.TasteEnum Min { get; set; }
        public Data.TasteEnum Max { get; set; }

        public TasteProperty()
        {
            List<Data.TasteEnum> enumList = Enum.GetValues(typeof(Data.TasteEnum)).Cast<Data.TasteEnum>().ToList();
            Value = Data.TasteEnum.Nothing;
            Min = enumList.Min();
            Max = enumList.Max();
        }
        public (Data.TasteEnum, Data.TasteEnum) MinMax()
        {
            return (Min, Max);
        }
    }
    public class TemperatureProperty
    {
        public Data.TemperatureEnum Value { get; set; }
        public Data.TemperatureEnum Min { get; set; }
        public Data.TemperatureEnum Max { get; set; }

        public TemperatureProperty()
        {
            List<Data.TemperatureEnum> enumList = Enum.GetValues(typeof(Data.TemperatureEnum)).Cast<Data.TemperatureEnum>().ToList();
            Value = Data.TemperatureEnum.Normal;
            Min = enumList.Min();
            Max = enumList.Max();
        }
        public (Data.TemperatureEnum, Data.TemperatureEnum) MinMax()
        {
            return (Min, Max);
        }
    }
    public class TextureProperty
    {
        public Data.TextureEnum Value { get; set; }
        public Data.TextureEnum Min { get; set; }
        public Data.TextureEnum Max { get; set; }

        public TextureProperty()
        {
            List<Data.TextureEnum> enumList = Enum.GetValues(typeof(Data.TextureEnum)).Cast<Data.TextureEnum>().ToList();
            Value = Data.TextureEnum.Normal;
            Min = enumList.Min();
            Max = enumList.Max();
        }
        public (Data.TextureEnum, Data.TextureEnum) MinMax()
        {
            return (Min, Max);
        }
    }
    public class VolumeProperty
    {
        public Data.VolumeEnum Value { get; set; }
        public Data.VolumeEnum Min { get; set; }
        public Data.VolumeEnum Max { get; set; }

        public VolumeProperty()
        {
            List<Data.VolumeEnum> enumList = Enum.GetValues(typeof(Data.VolumeEnum)).Cast<Data.VolumeEnum>().ToList();
            Value = Data.VolumeEnum.Silent;
            Min = enumList.Min();
            Max = enumList.Max();
        }

        public (Data.VolumeEnum, Data.VolumeEnum) MinMax()
        {
            return (Min, Max);
        }
    }
    public class StyleProperty
    {
        public Data.StyleEnum Value { get; set; }
        public Data.StyleEnum Min { get; set; }
        public Data.StyleEnum Max { get; set; }

        public StyleProperty()
        {
            List<Data.StyleEnum> enumList = Enum.GetValues(typeof(Data.StyleEnum)).Cast<Data.StyleEnum>().ToList();
            Value = Data.StyleEnum.Dull;
            Min = enumList.Min();
            Max = enumList.Max();
        }
        public (Data.StyleEnum, Data.StyleEnum) MinMax()
        {
            return (Min, Max);
        }
    }
}