using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerRayLight : MonoBehaviour
{
    // [SerializeField] private List<GameObject> _obstacle;
    [SerializeField] private CameraControllTest _camera;
    [SerializeField] GameObject RayShooterForward;
    [SerializeField] GameObject RayShooterBack;
    
    public Vector3 direction;
    private float maxDistance;
    private bool canMoveForward;
    private bool canMoveBack;

    public float EYE_SIGHT = 0.4f;
    // Start is called before the first frame update
    void Start()
    {
        maxDistance = 0;
        canMoveForward = true;
        canMoveBack = true;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateForwardRayLight();
        UpdateBackRayLight();
    }

    private void UpdateForwardRayLight()
    {
        maxDistance = EYE_SIGHT;
        if (_camera.getCameraPosInt() == 0)
        {
            direction = Vector3.right;
        }
        else if (_camera.getCameraPosInt() == 1)
        {
            direction = Vector3.back;
        }
        else if (_camera.getCameraPosInt() == 2)
        {
            direction = Vector3.left;
        }
        else if (_camera.getCameraPosInt() == 3)
        {
            direction = Vector3.forward;
        }
        
        Ray rayForward = new Ray(RayShooterForward.transform.position, direction);
        RaycastHit hitForward;

        if(PhysicsExtentsion.RaycastAndDraw(rayForward, out hitForward, maxDistance, 1<<7)){
            canMoveForward = false;
        }
        else
        {
            canMoveForward = true;
        }
    }
    
    private void UpdateBackRayLight()
    {
        maxDistance = EYE_SIGHT;
        if (_camera.getCameraPosInt() == 0)
        {
            direction = Vector3.left;
        }
        else if (_camera.getCameraPosInt() == 1)
        {
            direction = Vector3.forward;
        }
        else if (_camera.getCameraPosInt() == 2)
        {
            direction = Vector3.right;
        }
        else if (_camera.getCameraPosInt() == 3)
        {
            direction = Vector3.back;
        }
        
        Ray rayBack = new Ray(RayShooterBack.transform.position, direction);
        RaycastHit hitBack;

        if(PhysicsExtentsion.RaycastAndDraw(rayBack, out hitBack, maxDistance, 1<<7)){
            //Rayが当たるオブジェクトがあった場合はそのオブジェクト名をログに表示
            canMoveBack = false;
        }
        else
        {
            canMoveBack = true;
        }
    }

    public bool getCanMoveForward()
    {
        return canMoveForward;
    }

    public bool getCanMoveBack()
    {
        return canMoveBack;
    }
}
