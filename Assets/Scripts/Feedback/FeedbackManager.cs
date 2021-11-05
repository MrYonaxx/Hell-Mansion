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

namespace Feedbacks
{
    // Feedbacks global
    public class FeedbackManager : MonoBehaviour
    {
        #region Attributes 

        /* ======================================== *\
         *               ATTRIBUTES                 *
        \* ======================================== */


        private static FeedbackManager instance = null;
        public static FeedbackManager Instance
        {
            get { return instance; }
        }




        [SerializeField]
        private CameraController camera = null;
        public CameraController Camera
        { 
            get { return camera; } 
        }



        List<IMotionSpeed> listMotionSpeed = new List<IMotionSpeed>();
        private IEnumerator motionSpeedCoroutine;

        #endregion


        #region Functions 

        /* ======================================== *\
         *                FUNCTIONS                 *
        \* ======================================== */



        public void Awake()
        {
            if (instance == null)
                instance = this;
            else
                Destroy(this.gameObject);
        }


        // Ajoute un IMotionSpeed à la liste de ceux qui subissent le hitstop
        public void AddIMotionSpeed(IMotionSpeed character)
        {
            listMotionSpeed.Add(character);
        }

        public void RemoveIMotionSpeed(IMotionSpeed character)
        {
            listMotionSpeed.Remove(character);
        }

       
        public void SetMotionSpeed(float motionSpeed, float time)
        {
            for(int i = 0; i < listMotionSpeed.Count; i++)
            {
                listMotionSpeed[i].SetCharacterMotionSpeed(motionSpeed);
            }

            if (motionSpeedCoroutine != null)
                StopCoroutine(motionSpeedCoroutine);
            motionSpeedCoroutine = MotionSpeedCoroutine(time);
            StartCoroutine(motionSpeedCoroutine);
        }

        private IEnumerator MotionSpeedCoroutine(float time)
        {
            while (time > 0)
            {
                time -= Time.deltaTime;
                yield return null;
            }
            for (int i = 0; i < listMotionSpeed.Count; i++)
            {
                listMotionSpeed[i].SetCharacterMotionSpeed(1);
            }
        }



        #endregion

    } 

} // #PROJECTNAME# namespace