using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// I'm breaking my rules by locating this class inside CoreApp namespace
// The reason for that is that the extensions are always exposed. 
// I'm just being lazy with adding this namespace everytime to every file.
namespace DotNetCoreVueTemplate.CoreApp //.Components
{
    public static class Extensions
    {
        public static void AddOrUpdate<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue value)
        {
            if(!dictionary.ContainsKey(key))
            {
                dictionary.Add(key, value);
            }
            else
            {
                dictionary[key] = value;
            }
        }
    }
}
