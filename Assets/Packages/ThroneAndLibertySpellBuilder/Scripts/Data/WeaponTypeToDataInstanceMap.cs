using System;
using Packages.ThroneAndLibertySpellBuilder.Scripts.Data.Enums;
using Packages.ThroneAndLibertySpellBuilder.Scripts.Extensions;

namespace Packages.ThroneAndLibertySpellBuilder.Scripts.Data
{
    [Serializable]
    public class WeaponTypeToDataInstanceMap : SerializableDictionary<WeaponVariant, WeaponTypeData>
    {
        
    }
}