
using UnityEngine;

public class DisactivateEffects : MonoBehaviour
{
    private void OnEnable()
    {
        if (GetComponent<AudioSource>()) GetComponent<AudioSource>().Play(0);
        Invoke("setFalseGameObj", 1);
    }

    private void setFalseGameObj()
    {
        gameObject.SetActive(false);
    }
}
