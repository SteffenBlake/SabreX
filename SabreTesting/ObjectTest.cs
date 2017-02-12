using System;
using NUnit.Framework;
using SabreX;

namespace SabreTesting
{
    [TestFixture]
    public class ObjectTest
    {

        [Test]
        public void TestInheritance()
        {
            //Objects 
            //Parent
            var objParent = new ObjectBase();

            objParent.Brightness.Value = Data.BrightnessEnum.Blinding;
            objParent.Functions.Add("hello", s =>
            {
                Console.WriteLine("Hello World, I'm a Parent!");
                return 3;
            });

            //Child
            var objChild = new ObjectBase();

            objChild.Brightness.Value = Data.BrightnessEnum.PitchBlack;
            objChild.Functions.Add("hello", s =>
            {
                Console.WriteLine("Hello World, I'm a child!");
                return 5;
            });

            ParentCheck(objParent);
            ChildCheck(objChild);
            InheritNormal(objParent,objChild);
            InheritNoMaint(objParent,objChild);
            InheritMaintained(objParent,objChild);
        }

        public void ParentCheck(ObjectBase Parent)
        {
            Assert.AreEqual(3, Parent.Functions["hello"].Invoke(null));
            Assert.AreEqual(Data.BrightnessEnum.Blinding, Parent.Brightness.Value);
        }

        public void ChildCheck(ObjectBase Child)
        {
            Assert.AreEqual(5, Child.Functions["hello"].Invoke(null));
            Assert.AreEqual(Data.BrightnessEnum.PitchBlack, Child.Brightness.Value);
        }

        public void InheritNormal(ObjectBase Parent, ObjectBase Child)
        {
            Child.Inherit(Parent);
            Assert.AreEqual(3, Child.Functions["hello"].Invoke(null));
            Assert.AreEqual(Data.BrightnessEnum.Blinding, Child.Brightness.Value);
        }

        public void InheritNoMaint(ObjectBase Parent, ObjectBase Child)
        {
            Child.Inherit(Parent, false);
            Assert.AreEqual(3, Child.Functions["hello"].Invoke(null));
            Assert.AreEqual(Data.BrightnessEnum.Blinding, Child.Brightness.Value);
        }

        public void InheritMaintained(ObjectBase Parent, ObjectBase Child)
        {
            Child.Inherit(Parent, true);
            Assert.AreEqual(5, Child.Functions["hello"].Invoke(null));
            Assert.AreEqual(Data.BrightnessEnum.PitchBlack, Child.Brightness.Value);
        }
    }
}