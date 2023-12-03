using System.Collections.Generic;
using Packages.ThroneAndLibertySpellBuilder.Scripts.Data.Enums;
using UnityEngine;

namespace Packages.ThroneAndLibertySpellBuilder.Scripts.Data
{
    [CreateAssetMenu(fileName = "WeaponTypeData", menuName = "StaticData/Weapons/WeaponTypeData", order = 0)]
    public class WeaponTypeData : ScriptableObject
    {
        public WeaponVariant WeaponVariant;
        
        public List<SpellData> ActiveSpells;
        public List<SpellData> PassiveSpells;
    }
}