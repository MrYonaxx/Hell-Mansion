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
using UnityEngine.Events;

namespace HellMansion
{

    [System.Serializable]
    public class CharacterStat
    {
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */

        [SerializeField]
        private float hp = 1;
        public float HP
        {
            get { return hp; }
            set { hp = Mathf.Clamp(value, 0, HPMax.Value); OnHPChanged?.Invoke(hp, HPMax.Value); }
        }


        private float HpMax = 1;
        private void SetHpMax()
        {
            HpMax = HPMax.BaseValue;
        }

        [SerializeField]
        public Stat HPMax;

        [SerializeField]
        public Stat Attack;

        [SerializeField]
        public Stat Defense;

        [SerializeField]
        public Stat Speed;

        [Header("Resistances")]
        [SerializeField]
        public List<ElementalResistance> ElementalResistances = new List<ElementalResistance>();

 


        public delegate void ActionDoubleFloat(float a, float b);
        public event ActionDoubleFloat OnHPChanged;

        #endregion

        #region GettersSetters 

        /* ======================================== *\
         *           GETTERS AND SETTERS            *
        \* ======================================== */

        #endregion

        #region Functions 

        /* ======================================== *\
         *                FUNCTIONS                 *
        \* ======================================== */
        public CharacterStat()
        {

        }


        public CharacterStat(CharacterStat characterStat)
        {
            HPMax = new Stat(characterStat.HPMax.Value);
            Attack = new Stat(characterStat.Attack.Value);
            Defense = new Stat(characterStat.Defense.Value);
            Speed = new Stat(characterStat.Speed.Value);

            for (int i = 0; i < characterStat.ElementalResistances.Count; i++)
            {
                ElementalResistances.Add(new ElementalResistance(characterStat.ElementalResistances[i].Element, characterStat.ElementalResistances[i].BaseValue));
            }

            HP = HPMax.Value;
        }


        public void AddStat(StatModifier stat)
        {
            GetStat(stat.CharStat).AddStatBonus(stat.Value, stat.ModifierType);
        }
        public void RemoveStat(StatModifier stat)
        {
            GetStat(stat.CharStat).AddStatBonus(-stat.Value, stat.ModifierType);
        }

        public Stat GetStat(StatEnum charStat)
        {
            switch (charStat)
            {
                case StatEnum.HPMax:
                    return HPMax;
                case StatEnum.Attack:
                    return Attack;
                case StatEnum.Defense:
                    return Defense;        
                case StatEnum.Speed:
                    return Speed;
            }

            return null;
        }

        public float GetElementalResistance(int element)
        {
            for (int i = 0; i < ElementalResistances.Count; i++)
            {
                if (element == ElementalResistances[i].Element)
                {
                    return ElementalResistances[i].Value;
                }
            }
            return 0;
        }

        #endregion

    } 

} // #PROJECTNAME# namespace