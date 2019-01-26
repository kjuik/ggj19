﻿using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityUtilities;

namespace Platformer
{
    public class TheftManager : SingletonMonoBehaviour<TheftManager>
    {
        [SerializeField] GameObject overlay;
        [SerializeField] GameObject playerKilledNotification;
        [SerializeField] GameObject playerWonNotification;
        [SerializeField] float waitUntilFadeout = 1f;

        public bool Running { get; private set; }

        void Awake()
        {
            Running = true;
            overlay.SetActive(false);
            playerKilledNotification.SetActive(false);
            playerWonNotification.SetActive(false);
        }
        
        public void KillPlayer()
        {
            if (!Running)
                return;

            DataManager.Instance.ChosenPerson.Status = PersonStatus.Failed;
            
            overlay.SetActive(true);
            playerKilledNotification.SetActive(true);
            SwitchToHomeAfterDelay();
        }

        public void ReachedExit()
        {
            if (!Running)
                return;
            
            DataManager.Instance.ChosenPerson.Status = PersonStatus.Succeeded;
            
            overlay.SetActive(true);
            playerWonNotification.SetActive(true);
            SwitchToHomeAfterDelay();
        }

        public void SwitchToHomeAfterDelay()
        {
            StartCoroutine(SwitchToHomeAfterDelayCoroutine());
        }

        IEnumerator SwitchToHomeAfterDelayCoroutine()
        {
            Running = false;
            
            yield return new WaitForSeconds(waitUntilFadeout);
            
            FadeInOut.Instance.FadeOut(() => SceneManager.LoadScene("Home"));
        }
    }
}