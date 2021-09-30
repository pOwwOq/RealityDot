using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening; 

public class EnemyRayLight : MonoBehaviour
{
    [SerializeField] GameObject RayShooter;
    private EnemyMove _EnemyMove;

    private Vector3 direction;

    private float maxDistance;

    public float EYE_SIGHT = 0.4f;
    // Start is called before the first frame update
    void Start()
    {
        direction = Vector3.right;
        maxDistance = 0;
        _EnemyMove = this.gameObject.GetComponent<EnemyMove>();
    }

    // Update is called once per frame
    void Update()
    {
        setDirection();
        setEyeSight();
        
        Ray ray = new Ray(RayShooter.transform.position, direction);
        RaycastHit hit;
        
        if (PhysicsExtentsion.RaycastAndDraw(ray, out hit, maxDistance, 1 << 8))
        {
            //Rayが当たるオブジェクトがあった場合はそのオブジェクト名をログに表示
            Debug.Log("見つかった！！！！");
        }
    }

    private void setEyeSight()
    {
        if (_EnemyMove.getIsRotate())
        {
            maxDistance = 0;
        }
        else 
        {
            maxDistance = EYE_SIGHT;
        }
    }

    private void setDirection()
    {
        if (this.transform.eulerAngles.y <= 1 && this.transform.eulerAngles.y >= 359)
        {
            direction = Vector3.forward;
        } 
        else if (this.transform.eulerAngles.y <= 91 && this.transform.eulerAngles.y >= 89)
        {
            direction = Vector3.right;
        } 
        else if (this.transform.eulerAngles.y <= 181 && this.transform.eulerAngles.y >= 179)
        {
            direction = Vector3.back;
        } 
        else if (this.transform.eulerAngles.y <= 271 && this.transform.eulerAngles.y >= 269)
        {
            direction = Vector3.left;
        }
    }
}
