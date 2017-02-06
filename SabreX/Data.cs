// This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
// If a copy of the MPL was not distributed with this file, 
// You can obtain one at https://mozilla.org/MPL/2.0/.

namespace SabreX
{
    /// <summary>
    ///     Holds all the Enums and Dictionaries for generating properties.
    /// </summary>
    public class Data
    {
        /// <summary>
        ///     Modifies how bright an object is.
        /// </summary>
        public enum BrightnessEnum
        {
            PitchBlack = -3,
            Dark = -2,
            Moody = -1,
            Normal = 0,
            Dim = 1,
            Bright = 2,
            Blinding = 3
        }

        /// <summary>
        ///     Modifies how dense an object is. (Weight)=(Density)x(Size)
        /// </summary>
        public enum DensityEnum
        {
            Weightless = 0,
            Featherlike = 1,
            Light = 2,
            Normal = 3,
            Weighty = 4,
            Heavy = 5
        }

        public enum SizeEnum
        {
            NonCorporeal = 0,
            Miniscule = 1,
            Tiny = 2,
            Small = 3,
            Normal = 4,
            Big = 5,
            Massive = 6,
            Huge = 7
        }

        /// <summary>
        ///     Modifies what an object smells like.
        /// </summary>
        public enum SmellEnum
        {
            Disgusting = -3,
            Nasty = -2,
            Smelly = -1,
            Nothing = 0,
            Armoatic = 1,
            Fragrant = 2,
            Wonderful = 3
        }

        /// <summary>
        ///     Modifies what something tastes like
        /// </summary>
        public enum TasteEnum
        {
            Disgusting = -3,
            Nasty = -2,
            Gross = -1,
            Nothing = 0,
            Tasty = 1,
            Yummy = 2,
            Delicious = 3
        }

        /// <summary>
        ///     Modifes how hot/cold an object is.
        /// </summary>
        public enum TemperatureEnum
        {
            Freezing = -3,
            Cold = -2,
            Chilly = -1,
            Normal = 0,
            Warm = 1,
            Hot = 2,
            Searing = 3
        }

        /// <summary>
        ///     Modifies the feel of an object
        /// </summary>
        public enum TextureEnum
        {
            Scratchy = -3,
            Rough = -2,
            Bumpy = -1,
            Normal = 0,
            Smooth = 1,
            Sheer = 2,
            Reflective = 3
        }

        /// <summary>
        ///     Modifies how loud something is
        /// </summary>
        public enum VolumeEnum
        {
            Silent = 0,
            Quiet = 1,
            Audible = 2,
            Loud = 3,
            Deafening = 4
        }

        public enum StyleEnum
        {
            Horrifying = 0,
            Gross = 1,
            Creepy = 2,
            Dull = 3,
            Elegant = 4
        }
    }
}