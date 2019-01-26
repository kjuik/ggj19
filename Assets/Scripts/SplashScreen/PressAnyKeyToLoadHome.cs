using UnityEngine;
using UnityEngine.SceneManagement;

public class PressAnyKeyToLoadHome : MonoBehaviour
{
    void Update()
    {
        if (Input.anyKeyDown)
            FadeInOut.Instance.FadeOut(() => SceneManager.LoadScene("Home"));
    }
}
