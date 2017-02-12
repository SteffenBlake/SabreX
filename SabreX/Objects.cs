using System;
using System.Collections.Generic;
using System.Linq;

namespace SabreX
{
    /// <summary>
    /// Class to use to generate distinct objects in the game. All Objects have distinct Ids
    /// that can be referenced in the Dictionary of the Factory, or more simply by using
    /// the built in methods of this factory.
    /// One should avoid ever creating objects manually using the New() method.
    /// Use Factory.Generate() to create objects instead.
    /// </summary>
    public class ObjectFactory
    {
        public Dictionary<Guid, ObjectBase> ObjectDictionary = new Dictionary<Guid, ObjectBase>();

        public ObjectFactory()
        {
            ObjectDictionary = new Dictionary<Guid, ObjectBase>();
        }

        public ObjectFactory(Dictionary<Guid, ObjectBase> LoadList)
        {
            ObjectDictionary = LoadList;
        }

        public void Generate(out Guid Id)
        {
            ObjectBase genObj = new ObjectBase();
            ObjectDictionary.Add(genObj.Id, genObj);
            Id = genObj.Id;
        }

        public void Generate(out ObjectBase Obj)
        {
            ObjectBase genObj = new ObjectBase();
            ObjectDictionary.Add(genObj.Id, genObj);
            Obj = genObj;
        }

        public void Generate(out Guid Id, ObjectBase Template)
        {
            ObjectBase genObj = new ObjectBase();
            genObj.Inherit(Template);
            ObjectDictionary.Add(genObj.Id, genObj);
            Id = genObj.Id;
        }

        public void Generate(out ObjectBase Obj, ObjectBase Template)
        {
            ObjectBase genObj = new ObjectBase();
            genObj.Inherit(Template);
            ObjectDictionary.Add(genObj.Id, genObj);
            Obj = genObj;
        }

        public void Generate(out Guid Id, ObjectBase Template, Boolean Maintain)
        {
            ObjectBase genObj = new ObjectBase();
            genObj.Inherit(Template, Maintain);
            ObjectDictionary.Add(genObj.Id, genObj);
            Id = genObj.Id;
        }

        public void Generate(out ObjectBase Obj, ObjectBase Template, Boolean Maintain)
        {
            ObjectBase genObj = new ObjectBase();
            genObj.Inherit(Template, Maintain);
            ObjectDictionary.Add(genObj.Id, genObj);
            Obj = genObj;
        }

        public ObjectBase Get(Guid Id)
        {
            if (ObjectDictionary.ContainsKey(Id)) { return ObjectDictionary[Id]; }
            else
            {
                throw new ArgumentException("Attempted to index an object Id that does not exist in the factory!", "Id");
            }
        }

        public List<ObjectBase> GetList(List<Guid> Ids)
        {
            if (Ids.Except(ObjectDictionary.Keys).Any()) { throw new ArgumentException("Attempted to index object Ids that do not exist in the factory!", "Id List");}
            else
            {
                return ObjectDictionary.Where(p => Ids.Contains(p.Key)).Select(p => p.Value).ToList();
            }
        }
    }

    /// <summary>
    ///     Generic Object type
    /// </summary>
    public class ObjectBase
    {
        public Guid Id = Guid.Empty;

        public bool isContainer = false;
        public bool isSurface = false;

        public List<Guid> Container = new List<Guid>();
        public List<Guid> Surface = new List<Guid>();

        /// <summary>
        /// Creates an Ungenerated new Object with a 
        /// </summary>
        public ObjectBase()
        {
            Id = new Guid();
            DoSpecialLoad();
        }

        /// <summary>
        /// If Generating a Template Object, there's no point in assigning it a Guid
        /// </summary>
        /// <param name="Template">Is this a Template Object?</param>
        public ObjectBase(Boolean Template)
        {
            if (Template) { return; }
            Id = new Guid();
            DoSpecialLoad();
        }

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

        /// <summary>
        ///     Copies all the parent methods/properties to the child, writing over anything with the same name.
        /// </summary>
        /// <param name="parent">Parent object to derive from</param>
        /// <returns></returns>
        public void Inherit(ObjectBase parent)
        {
            Name = parent.Name;
            isContainer = parent.isContainer;
            isSurface = parent.isSurface;

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
        }

        /// <summary>
        ///     Copies the parent methods/properties to the child.
        /// </summary>
        /// <param name="parent">Parent object to derive from</param>
        /// <param name="maintain">If true, won't overwrite values that already exist.</param>
        /// <returns></returns>
        public void Inherit(ObjectBase parent, bool maintain)
        {
            if (!maintain)
            {
                Inherit(parent);
                return;
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
        }
        
        public void Generate(ObjectFactory FACTORY)
        {
            if (isContainer)
            {
                foreach (var child in FACTORY.GetList(Container))
                {
                    child.Generate(FACTORY);
                }
            }

            if (isSurface) {
                foreach (var child in FACTORY.GetList(Surface))
                {
                    child.Generate(FACTORY);
                }
            }

            Brightness.Generate();
            Density.Generate();
            Size.Generate();
            Smell.Generate();
            Taste.Generate();
            Temperature.Generate();
            Texture.Generate();
            Volume.Generate();
            Style.Generate();

            DoSpecialGenerate();
        }

        /// <summary>
        /// Gets a list of all children objects of this object, including subchildren recursively.
        /// </summary>
        /// <param name="FACTORY">The ObjectFactory of the project.</param>
        /// <returns></returns>
        public List<Guid> getChildren(ObjectFactory FACTORY)
        {
            List<Guid> outlist = new List<Guid>();
            if (isSurface)
            {
                outlist.AddRange(Surface.SelectMany(child => FACTORY.Get(child).getChildren(FACTORY)).ToList());
                outlist.AddRange(Surface);
            }
            if (isContainer)
            {
                outlist.AddRange(Container.SelectMany(child => FACTORY.Get(child).getChildren(FACTORY)).ToList());
                outlist.AddRange(Container);
            }
            return outlist;
        }

        public virtual void DoSpecialLoad() { }
        public virtual void DoSpecialGenerate() { }
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

        public void Generate()
        {
            Value = (Data.BrightnessEnum)(new Random().Next((int)Min, (int)Max + 1));
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

        public void Generate()
        {
            Value = (Data.DensityEnum)(new Random().Next((int)Min, (int)Max + 1));
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

        public void Generate()
        {
            Value = (Data.SizeEnum)(new Random().Next((int)Min, (int)Max + 1));
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

        public void Generate()
        {
            Value = (Data.SmellEnum)(new Random().Next((int)Min, (int)Max + 1));
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

        public void Generate()
        {
            Value = (Data.TasteEnum)(new Random().Next((int)Min, (int)Max + 1));
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

        public void Generate()
        {
            Value = (Data.TemperatureEnum)(new Random().Next((int)Min, (int)Max + 1));
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

        public void Generate()
        {
            Value = (Data.TextureEnum)(new Random().Next((int)Min, (int)Max + 1));
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

        public void Generate()
        {
            Value = (Data.VolumeEnum)(new Random().Next((int)Min, (int)Max + 1));
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

        public void Generate()
        {
            Value = (Data.StyleEnum)(new Random().Next((int)Min, (int)Max + 1));
        }
    }
}