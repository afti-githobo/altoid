using UnityEngine;

namespace Altoid.Util
{
    public class AliasAttribute : PropertyAttribute
    {
        public string Name { get; private set; }
        public AliasAttribute(string name) => Name = name;
    }
}