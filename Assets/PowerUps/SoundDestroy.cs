using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("SoundDestroyTimer", 1f);
    }

    private void SoundDestroyTimer()
    {
        Destroy(this.gameObject);
    }
}
