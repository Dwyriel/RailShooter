using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
    [SerializeField] float TimeBeforeDestruction = 3f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, TimeBeforeDestruction);
    }
}
