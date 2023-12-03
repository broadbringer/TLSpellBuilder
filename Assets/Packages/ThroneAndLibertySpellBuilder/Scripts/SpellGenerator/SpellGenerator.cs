using System;
using Packages.ThroneAndLibertySpellBuilder.Scripts.Data;
using Packages.ThroneAndLibertySpellBuilder.Scripts.Data.Enums;
using Sirenix.OdinInspector;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Packages.ThroneAndLibertySpellBuilder.Scripts.SpellGenerator
{
    public class SpellGenerator : MonoBehaviour
    {
        public static int IDForNextSpell;
        
        [HideIf(nameof(ShouldHideLinksToOtherComponents))]
        public GeneralDataBase GeneralDataBase;
        
        public WeaponVariant WeaponVariant;
        public string Name;
        public string Description;
        public string Effect;
        public string Cooldown;
        public Sprite Texture;
        public SpellType Type;
        
        private bool CanGenerate =>    IsNotEmpty(Name)
                                    && IsNotEmpty(Description)
                                    && IsNotEmpty(Effect)
                                    && IsNotEmpty(Cooldown)
                                    && Texture != null 
                                    && Type != SpellType.Undefined
                                    && WeaponVariant != WeaponVariant.Undefined;


        private bool ShouldHideLinksToOtherComponents => GeneralDataBase != null;
        
        [Button("Generate"), ShowIf("CanGenerate")]
        public void Generate()
        {
            
        }

        private bool IsNotEmpty(string value) => value != String.Empty;
        private bool IsNotNull(Object value) => value != null;
    }
}