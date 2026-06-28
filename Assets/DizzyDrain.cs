using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DizzyDrain : MonoBehaviour
{
    public float drainAmount = 1.0F;
    public Image bar;
    bool gameEnd = false;

    void Start()
    {
        bar = GetComponent<Image>();
    }

    void Update()
    {
        drainAmount = drainAmount - 0.000005F;
        bar.fillAmount = drainAmount;

        if(bar.fillAmount == 0.0F && gameEnd == false)
        {
            gameEnd = true;
            SceneManager.LoadScene(1);
        }

    }
}
