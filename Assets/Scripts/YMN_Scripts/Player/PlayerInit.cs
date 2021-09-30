using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInit : MonoBehaviour
{
    [SerializeField] private GameObject StartPos;
    // Start is called before the first frame update
    void Start()
    {
        this.transform.position = StartPos.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
