﻿/*****************************************************************
 * Product:    #PROJECTNAME#
 * Developer:  #DEVELOPERNAME#
 * Company:    #COMPANY#
 * Date:       #CREATIONDATE#
******************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Sirenix.OdinInspector;

namespace Stats
{
    [System.Serializable]
    public class StatModifier
    {
        //[HorizontalGroup]
        //[HideLabel]
        [SerializeField]
        StatEnum charStat;
        public StatEnum CharStat
        {
            get { return charStat; }
        }


        //[HorizontalGroup]
        //[HideLabel]
        [SerializeField]
        StatBonusType modifierType;
        public StatBonusType ModifierType
        {
            get { return modifierType; }
        }

        //[HorizontalGroup]
        //[HideLabel]
        [SerializeField]
        float value = 0;
        public float Value
        {
            get { return value; }
        }
    } 

} // #PROJECTNAME# namespace