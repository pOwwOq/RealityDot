using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private CameraControllTest MainCamera;
    private PlayerRayLight RayShooterForward;
    private PlayerRayLight RayShooterBack;
    private bool isMove;
    
    public float ONE_BLOCK = 0.4f;
    // Start is called before the first frame update
    void Start()
    {
        isMove = false;
        
        RayShooterForward = this.gameObject.GetComponent<PlayerRayLight>();
        RayShooterBack = this.gameObject.GetComponent<PlayerRayLight>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        var tmp = transform.position;
        switch (MainCamera.getCameraPosInt())
        {
            // カメラ位置0
            case 0:
                if (Input.GetKeyDown(KeyCode.D) && !isMove && !(CameraControllTest.getCoroutineBool()) && RayShooterForward.getCanMoveForward())
                {
                    tmp.x += ONE_BLOCK;
                    transform.DOMoveX(tmp.x, 0.5f).SetEase(Ease.Linear).OnStart(() => setIsMoveTrue()).OnComplete(() => setIsMoveFalse());
                }
                else if (Input.GetKeyDown(KeyCode.A) && !isMove && !(CameraControllTest.getCoroutineBool()) && RayShooterBack.getCanMoveBack())
                {
                    tmp.x -= ONE_BLOCK;
                    transform.DOMoveX(tmp.x, 0.5f).SetEase(Ease.Linear).OnStart(() => setIsMoveTrue()).OnComplete(() => setIsMoveFalse());
                }
                break;
            // カメラ位置1
            case 1:
                if (Input.GetKeyDown(KeyCode.D) && !isMove && !(CameraControllTest.getCoroutineBool()) && RayShooterForward.getCanMoveForward())
                {
                    tmp.z -= ONE_BLOCK;
                    transform.DOMoveZ(tmp.z, 0.5f).SetEase(Ease.Linear).OnStart(() => setIsMoveTrue()).OnComplete(() => setIsMoveFalse());
                }
                else if (Input.GetKeyDown(KeyCode.A) && !isMove && !(CameraControllTest.getCoroutineBool()) && RayShooterBack.getCanMoveBack())
                {
                    tmp.z += ONE_BLOCK;
                    transform.DOMoveZ(tmp.z, 0.5f).SetEase(Ease.Linear).OnStart(() => setIsMoveTrue()).OnComplete(() => setIsMoveFalse());
                }
                break;
            // カメラ位置2
            case 2:
                if (Input.GetKeyDown(KeyCode.D) && !isMove && !(CameraControllTest.getCoroutineBool()) && RayShooterForward.getCanMoveForward())
                {
                    tmp.x -= ONE_BLOCK;
                    transform.DOMoveX(tmp.x, 0.5f).SetEase(Ease.Linear).OnStart(() => setIsMoveTrue()).OnComplete(() => setIsMoveFalse());
                }
                else if (Input.GetKeyDown(KeyCode.A) && !isMove && !(CameraControllTest.getCoroutineBool()) && RayShooterForward.getCanMoveBack())
                {
                    tmp.x += ONE_BLOCK;
                    transform.DOMoveX(tmp.x, 0.5f).SetEase(Ease.Linear).OnStart(() => setIsMoveTrue()).OnComplete(() => setIsMoveFalse());
                }
                break;
            // カメラ位置3
            case 3:
                if (Input.GetKeyDown(KeyCode.D) && !isMove && !(CameraControllTest.getCoroutineBool()) && RayShooterForward.getCanMoveForward())
                {
                    tmp.z += ONE_BLOCK;
                    transform.DOMoveZ(tmp.z, 0.5f).SetEase(Ease.Linear).OnStart(() => setIsMoveTrue()).OnComplete(() => setIsMoveFalse());
                }
                else if (Input.GetKeyDown(KeyCode.A) && !isMove && !(CameraControllTest.getCoroutineBool()) && RayShooterBack.getCanMoveBack())
                {
                    tmp.z -= ONE_BLOCK;
                    transform.DOMoveZ(tmp.z, 0.5f).SetEase(Ease.Linear).OnStart(() => setIsMoveTrue()).OnComplete(() => setIsMoveFalse());
                }
                break;
            default:
                break;
        }
    }

    private void setIsMoveFalse()
    {
        isMove = false;
    }

    private void setIsMoveTrue()
    {
        isMove = true;
    }
}
