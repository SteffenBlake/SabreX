using System;
using System.Collections.Generic;

namespace SabreX
{
    /// <summary>
    ///     Generic Object type
    /// </summary>
    public class ObjectBase
    {
        public Dictionary<string, Action> Commands = new Dictionary<string, Action>();
        public Dictionary<string, Func<int?, int>> Functions = new Dictionary<string, Func<int?, int>>();
        //private string Name = "Blank_Object";

        /// <summary>
        ///     Copies all the parent methods to the child, writing over anything with the same name.
        /// </summary>
        /// <param name="_parent">Parent object to derive from</param>
        /// <returns></returns>
        public bool Inherit(ObjectBase _parent)
        {
            foreach (var pair in _parent.Commands)
            {
                Commands[pair.Key] = pair.Value;
            }

            foreach (var pair in _parent.Functions)
            {
                Functions[pair.Key] = pair.Value;
            }

            return true;
        }

        /// <summary>
        ///     Copies the parent methods to the child.
        /// </summary>
        /// <param name="_parent">Parent object to derive from</param>
        /// <param name="_maintain">If true, won't overwrite values that already exist.</param>
        /// <returns></returns>
        public bool Inherit(ObjectBase _parent, bool _maintain)
        {
            if (!_maintain)
            {
                Inherit(_parent);
                return true;
            }
            foreach (var pair in _parent.Commands)
            {
                if (!Commands.ContainsKey(pair.Key))
                {
                    Commands[pair.Key] = pair.Value;
                }
            }

            foreach (var pair in _parent.Functions)
            {
                if (!Functions.ContainsKey(pair.Key))
                {
                    Functions[pair.Key] = pair.Value;
                }
            }

            return true;
        }
    }
}