using System;
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

        public void AddSpell(SpellData spellData)
        {
            switch (spellData.Type)
            {
                case SpellType.Active:
                    AddActiveSpell(spellData);
                    break;
                case SpellType.Passive:
                    AddPassiveSpell(spellData);
                    break;
                case SpellType.Undefined:
                    throw new Exception("There are no such spell type");
            }
        }

        private void AddActiveSpell(SpellData spellData)
        {
            if (ActiveSpells.Contains(spellData))
            {
                Debug.LogError("You already have that spell, so spells should be unique");
                return;
            }

            ActiveSpells.Add(spellData);
        }

        private void AddPassiveSpell(SpellData spellData)
        {
            if (PassiveSpells.Contains(spellData))
            {
                Debug.LogError("You already have that spell, so spells should be unique");
                return;
            }
            
            PassiveSpells.Add(spellData);
        }
    }
}