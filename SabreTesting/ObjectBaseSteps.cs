using System;
using TechTalk.SpecFlow;
using NUnit.Framework;
using SabreX;

namespace SabreTesting
{
    [Binding]
    public class ObjectBaseSteps
    {
        ObjectBase objParent = new ObjectBase();

        ObjectBase objChild = new ObjectBase();



        [Given(@"I have a Parent Object")]
        public void GivenIHaveAParentObject()
        {
            objParent.Brightness.Value = Data.BrightnessEnum.Blinding;
            objParent.Functions.Add("hello", s =>
            {
                Console.WriteLine("Hello World, I'm a Parent!");
                return 3;
            });
        }

        [Given(@"I have also a Child Object")]
        public void GivenIHaveAlsoAChildObject()
        {
            objChild.Brightness.Value = Data.BrightnessEnum.PitchBlack;
            objChild.Functions.Add("hello", s =>
            {
                Console.WriteLine("Hello World, I'm a child!");
                return 5;
            });
        }

        [When(@"I make the Child Inherit Normally")]
        public void WhenIMakeTheChildInheritNormally()
        {
            objChild.Inherit(objParent);
        }

        [When(@"I make the Child Inherit Not Maintained")]
        public void WhenIMakeTheChildInheritNotMaintained()
        {
            objChild.Inherit(objParent, false);
        }

        [When(@"I make the Child Inherit Maintained")]
        public void WhenIMakeTheChildInheritMaintained()
        {
            objChild.Inherit(objParent, true);
        }


        [Then(@"the Child will be the same as the Parent")]
        public void ThenTheChildWillBeTheSameAsTheParent()
        {
            Assert.AreEqual(objParent.Functions["hello"].Invoke(null), objChild.Functions["hello"].Invoke(null));
        }

        [Then(@"the Child will keep its non-distinct properties")]
        public void ThenTheChildWillKeepItsNon_DistinctProperties()
        {
            Assert.AreEqual(objParent.Functions["hello"].Invoke(null), objChild.Functions["hello"].Invoke(null));

        }
    }
}
