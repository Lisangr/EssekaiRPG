using UnityEngine;

public class Destroer : MonoBehaviour
{
    public float liveTime = 3f;
    void Start()
    {
        Destroy(this.gameObject, liveTime);
    }

}
