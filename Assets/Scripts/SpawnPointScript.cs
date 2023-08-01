using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointScript : MonoBehaviour
{
    [SerializeField] GameObject GraphicObject;

    // Start is called before the first frame update
    void Start()
    {
        GraphicObject.SetActive(false);
    }
}
