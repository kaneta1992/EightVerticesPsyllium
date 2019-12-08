using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scatter : MonoBehaviour
{
    public GameObject brightbar;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 100; i++) {
            for(int j = 0; j < 100; j++) {
                Instantiate(brightbar, new Vector3(i, 0.0f, j), Quaternion.identity);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
