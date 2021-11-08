/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Sirenix.OdinInspector;
using Stats;

namespace Stats
{
    [System.Serializable]
    public class ElementalResistance : Stat
    {
        [SerializeField]
        /*[HorizontalGroup("ElementalResistance")]
        [HideLabel]
        [ValueDropdown("SelectCardElement")]*/
        private int element;
        public int Element
        {
            get { return element; }
        }

        public ElementalResistance(int elementID, float baseV)
        {
            element = elementID;
            baseValue = baseV;
            flatBonus = 0;
            multiplierBonus = 1;
            CalculateFinalValue();
        }

/*#if UNITY_EDITOR
        private static IEnumerable SelectCardElement()
        {
            return UnityEditor.AssetDatabase.LoadAssetAtPath<CardType>(UnityEditor.AssetDatabase.GUIDToAssetPath(UnityEditor.AssetDatabase.FindAssets("ElementTypeData")[0]))
                .GetAllTypeID();
        }
#endif */
    }

} // #PROJECTNAME# namespace