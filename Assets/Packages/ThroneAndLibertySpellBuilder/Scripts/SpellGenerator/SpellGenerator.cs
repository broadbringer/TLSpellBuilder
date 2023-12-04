using System;
using Packages.ThroneAndLibertySpellBuilder.Scripts.Data;
using Packages.ThroneAndLibertySpellBuilder.Scripts.Data.Enums;
using Sirenix.OdinInspector;
using UnityEditor;
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
            var createdSpell = CreateSpellDataAndMoveToFolder();
            GeneralDataBase.WeaponTypeToDataInstanceMap[WeaponVariant].AddSpell(createdSpell);
            
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
        
        
        private SpellData CreateSpellDataAndMoveToFolder()
        {
            // Create an instance of the ScriptableObject
            SpellData newInstance = ScriptableObject.CreateInstance<SpellData>();

            newInstance.Name = Name;
            newInstance.Description = Description;
            newInstance.Effect = Effect;
            newInstance.Cooldown = Cooldown;
            newInstance.Texture = Texture;
            newInstance.Type = Type;
            
            string folderPath = $"Assets/Packages/ThroneAndLibertySpellBuilder/Data/Skills/{WeaponVariant}";

            // Ensure the folder exists, create it if it doesn't
            if (!AssetDatabase.IsValidFolder(folderPath))
            {
                AssetDatabase.CreateFolder("Assets/Packages/ThroneAndLibertySpellBuilder/Data/Skills", $"{WeaponVariant}");
            }

            string assetPath = folderPath + "/" + $"{Name}.asset";

            // Create the asset at the specified path
            AssetDatabase.CreateAsset(newInstance, assetPath);

            return newInstance;
        }

        private bool IsNotEmpty(string value) => value != String.Empty;
        private bool IsNotNull(Object value) => value != null;
    }
}