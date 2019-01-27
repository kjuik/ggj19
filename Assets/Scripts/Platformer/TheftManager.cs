using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityUtilities;

namespace Platformer
{
    public class TheftManager : SingletonMonoBehaviour<TheftManager>
    {
        [SerializeField] GameObject overlay;
        [SerializeField] GameObject playerKilledNotification;
        [SerializeField] Text playerKilledNotificationText;
        [SerializeField] GameObject playerWonNotification;
        [SerializeField] Text playerWonNotificationText;
        [SerializeField] float waitUntilFadeout = 1f;
        [SerializeField] Transform levelContainer;

        AudioSource audioSource;

        public bool Running { get; private set; }

        void Awake()
        {
            Running = true;
            overlay.SetActive(false);
            playerKilledNotification.SetActive(false);
            playerWonNotification.SetActive(false);
            audioSource = GetComponent<AudioSource>();
        }

        void Start()
        {
            for (var i = 0; i < levelContainer.childCount; i++)
            {
                levelContainer.GetChild(i).gameObject.SetActive(false);
            }
            
            var level = levelContainer.Find(DataManager.Instance.ChosenPerson.Id);
            level.gameObject.SetActive(true);
        }
        
        public void KillPlayer(AudioClip sfx, string gameOverMessage)
        {
            if (!Running)
                return;

            if (sfx != null)
            {
                audioSource.PlayOneShot(sfx);
            }

            DataManager.Instance.ChosenPerson.Status = PersonStatus.DateSucceeded;
            
            overlay.SetActive(true);
            playerKilledNotificationText.text = gameOverMessage;
            playerKilledNotification.SetActive(true);
            SwitchToHomeAfterDelay();
        }

        public void ReachedExit()
        {
            if (!Running)
                return;
            
            DataManager.Instance.ChosenPerson.Status = PersonStatus.TheftSucceeded;
            
            overlay.SetActive(true);
            playerWonNotificationText.text = DataManager.Instance.ChosenPerson.MetaData.TheftSuccessMessage;
            playerWonNotification.SetActive(true);
            SwitchToHomeAfterDelay();
        }

        public void SwitchToHomeAfterDelay()
        {
            StartCoroutine(SwitchToHomeAfterDelayCoroutine());
        }

        IEnumerator SwitchToHomeAfterDelayCoroutine()
        {
            Time.timeScale = 0;
            Running = false;
            
            yield return new WaitForSecondsRealtime(waitUntilFadeout);
            
            FadeInOut.Instance.FadeOut(() =>
            {
                Time.timeScale = 1;
                SceneManager.LoadScene("Home");
            });
        }

        public void PlaySfx(AudioClip sfx)
        {
            audioSource.PlayOneShot(sfx);
        }
    }
}