using UnityEngine;

public class TraderPanel : MonoBehaviour
{
    public static bool tradePossible = false;
    void Start()
    {
        if (gameObject.active)
        {
            tradePossible = true;
            Debug.Log("торговать МОЖНО");
        }
        else
        {
            tradePossible = false;
            Debug.Log("торговать нельзя");
        }
    }
}
