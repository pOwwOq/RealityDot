using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerRotate : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("right"))
        {
            //回転中ではない場合は実行 
            if (!CameraControllTest.coroutineBool)
            {
                StartCoroutine("RightMove");
            }
        }
 
        if(Input.GetKeyDown("left"))
        {
            //回転中ではない場合は実行 
            if (!CameraControllTest.coroutineBool)
            {
                StartCoroutine("LeftMove");
            }
        }
    }
    
    public IEnumerator RightMove()
    {
        for (int turn=0; turn<90; turn++)
        {
            transform.Rotate(0,-1,0);
            yield return new WaitForSeconds(1.5f/90);
        }
    }
 
    //左にゆっくり回転して90°でストップ
    public IEnumerator LeftMove()
    {
        for (int turn=0; turn<90; turn++)
        {
            transform.Rotate(0,1,0);
            yield return new WaitForSeconds(1.5f/90);
        }
    }
}
