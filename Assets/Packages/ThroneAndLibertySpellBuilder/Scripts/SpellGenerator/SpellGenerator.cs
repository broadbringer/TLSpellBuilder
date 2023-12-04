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

        public WeaponTypeToItIDMap WeaponTypeToItIDMap;

        [HideIf(nameof(ShouldHideLinksToOtherComponents))]
        public GeneralDataBase GeneralDataBase;

        public WeaponVariant WeaponVariant;
        public string Name;
        public string Description;
        public string Effect;
        public string Cooldown;
        public Sprite Texture;
        public SpellType Type;

        private bool CanGenerate => IsNotEmpty(Name)
                                    && IsNotEmpty(Description)
                                    && IsNotEmpty(Effect)
                                    && IsNotEmpty(Cooldown)
                                    && Texture != null
                                    && Type != SpellType.Undefined
                                    && WeaponVariant != WeaponVariant.Undefined;


        private bool ShouldHideLinksToOtherComponents => GeneralDataBase != null;

        [Button("Generate"), ShowIf("CanGenerate")]
        public void Generate() =>
            CreateSpellDataAndMoveToFolder();


        private SpellData CreateSpellDataAndMoveToFolder()
        {
            if (!CanCreateSpell())
            {
                Debug.LogError("You already have spell with that name");
                return null;
            }

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
                AssetDatabase.CreateFolder("Assets/Packages/ThroneAndLibertySpellBuilder/Data/Skills",
                    $"{WeaponVariant}");
            }

            string assetPath = folderPath + "/" + $"{Name}.asset";

            var weaponData = GeneralDataBase.WeaponTypeToDataInstanceMap[WeaponVariant];

            var nextChar = weaponData.GetNextCharForID();
            weaponData.IncrementNextLetter();

            string Id = nextChar + WeaponTypeToItIDMap[WeaponVariant].ToString();

            newInstance.SetID(Id);

            // Create the asset at the specified path
            AssetDatabase.CreateAsset(newInstance, assetPath);

            weaponData.AddSpell(newInstance);

            EditorUtility.SetDirty(weaponData);
            
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            return newInstance;
        }

        private bool CanCreateSpell() =>
            !GeneralDataBase.WeaponTypeToDataInstanceMap[WeaponVariant].HasSpellWithName(Name);

        private bool IsNotEmpty(string value) => value != String.Empty;
        private bool IsNotNull(Object value) => value != null;
    }
}