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
            var objParent = new ObjectBase();
            objParent.Functions.Add("hello", s =>
            {
                Console.WriteLine("Hello World, I'm a Parent!");
                return 3;
            });

            var objChild = new ObjectBase();
            objChild.Functions.Add("hello", s =>
            {
                Console.WriteLine("Hello World, I'm a child!");
                return 5;
            });

            objChild.Inherit(objParent);

            var maintChild = new ObjectBase();
            maintChild.Functions.Add("hello", s =>
            {
                Console.WriteLine("Hello World, I'm a child!");
                return 5;
            });

            maintChild.Inherit(objParent, true);

            Assert.AreEqual(3, objParent.Functions["hello"].Invoke(null));
            Assert.AreEqual(3, objChild.Functions["hello"].Invoke(null));
            Assert.AreEqual(5, maintChild.Functions["hello"].Invoke(null));
        }
    }
}