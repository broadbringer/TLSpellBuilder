using Packages.ThroneAndLibertySpellBuilder.Scripts.Data.Enums;
using UnityEngine;

namespace Packages.ThroneAndLibertySpellBuilder.Scripts.Data
{
    
    [CreateAssetMenu(fileName = "SpellData", menuName = "StaticData/Weapons/SpellData", order = 0)]
    public class SpellData : ScriptableObject
    {
        public Sprite Texture;
        public string Name;
        public string Description;
        public string Effect;
        public string Cooldown;
        public SpellType Type;

        public string ID;

        public void SetID(string id) => 
            ID = id;
    }
}