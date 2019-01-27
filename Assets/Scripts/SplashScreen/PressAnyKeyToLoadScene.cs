using UnityEngine;
using UnityEngine.SceneManagement;

public class PressAnyKeyToLoadScene : MonoBehaviour
{
    [SerializeField] private string SceneToLoad;

    void Update()
    {
        if (Input.anyKeyDown)
            FadeInOut.Instance.FadeOut(() => SceneManager.LoadScene(SceneToLoad));
    }
}
