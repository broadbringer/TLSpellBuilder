using Packages.ThroneAndLibertySpellBuilder.Scripts.Data.Enums;
using Packages.ThroneAndLibertySpellBuilder.Scripts.Extensions;
using UnityEngine;

namespace Packages.ThroneAndLibertySpellBuilder.Scripts.Data
{
    [CreateAssetMenu(fileName = "GeneralDataBase", menuName = "StaticData/DataBase/General", order = 0)]
    public class GeneralDataBase : ScriptableObject
    {
        public WeaponTypeToDataInstanceMap WeaponTypeToDataInstanceMap;

        public WeaponTypeData GetWeaponTypeData(WeaponVariant byWeaponVariant) =>
            WeaponTypeToDataInstanceMap[byWeaponVariant];
    }
}