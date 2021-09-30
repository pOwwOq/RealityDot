using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;
using UnityEngine.UI;

public class CameraControllTest : MonoBehaviour
{
    [SerializeField] List<Material> FadeMaterials;
    // 右回転
    [SerializeField] private List<GameObject> _targets03;
    [SerializeField] private List<GameObject> _targets32;
    [SerializeField] private List<GameObject> _targets21;
    [SerializeField] private List<GameObject> _targets10;
    // 左回転
    [SerializeField] private List<GameObject> _targets01;
    [SerializeField] private List<GameObject> _targets12;
    [SerializeField] private List<GameObject> _targets23;
    [SerializeField] private List<GameObject> _targets30;
    
    private List<GameObject>[] PathLists;

    public float TargetPositionY;
    public float FrontTargetPositionY;
    public float UpYPos;
    public int CameraPosInt = 0;
    public static bool coroutineBool;
    public static bool FrontRear;

    private int FrontCameraPosInt;

    private void Awake()
    {
        // 右回転左回転の順でインデックスを整合性とって格納
        PathLists = new List<GameObject>[8] 
        {
            _targets03, _targets10, _targets21, _targets32,
            _targets01, _targets12, _targets23, _targets30
        };
        coroutineBool = false;
        FrontRear = true;
        FrontCameraPosInt = CameraPosInt;
        
        foreach (var material in FadeMaterials)
        {
            BlendModeUtils.SetBlendMode(material, BlendModeUtils.Mode.Opaque);
            material.color = new Color32(255, 255, 255, 255);
        }
    }


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
            if (!coroutineBool)
            {
                coroutineBool = true;
                List<GameObject> _path = setTargetCameraPosList(CameraPosInt, "right");
                CameraPosInt = setCameraPosInt(CameraPosInt, "right");
                // Debug.Log(CameraPosInt);
                // Debug.Log(FrontCameraPosInt);
                DoMove(_path, "right");
                
            }
        }

        if(Input.GetKeyDown("left"))
        {
            //回転中ではない場合は実行 
            if (!coroutineBool)
            {
                coroutineBool = true;
                List<GameObject> _path = setTargetCameraPosList(CameraPosInt, "left");
                CameraPosInt = setCameraPosInt(CameraPosInt, "left");
                DoMove(_path, "left");
            }
        }
    }

    private bool setFrontRearBool(int cameraPosInt)
    {
        if (cameraPosInt % 2 == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    
    private void SetMaterialFade(List<Material> materials, int cameraPos)
    {
        // カメラが背後に有る時は背後のブロックを透かす
        if (cameraPos == 2)
        {
            foreach (var material in materials)
            {
                // Fadeモードに変更
                BlendModeUtils.SetBlendMode(material, BlendModeUtils.Mode.Fade);
                // 透明度をいじる
                material.color = new Color32(255, 255, 255, 150);
            }
        }
        else
        {
            foreach (var material in materials)
            {
                BlendModeUtils.SetBlendMode(material, BlendModeUtils.Mode.Opaque);
                material.color = new Color32(255, 255, 255, 255);
            }
        }
    }

    // カメラの位置でインデックスを決定する
    private int setCameraPosInt(int currentPos, string direction)
    {
        int tmp = currentPos;
        if (direction == "right")
        {
            tmp -= 1;
        }
        else
        {
            tmp += 1;
        }

        if (tmp < 0)
        {
            return 3;
        }else if (tmp > 3)
        {
            return 0;
        }
        else
        {
            return tmp;
        }
    }

    // 移動先を決定する
    private List<GameObject> setTargetCameraPosList(int currentPos, string direction)
    {
        int tmp = currentPos;
        if (direction == "left")
        {
            tmp += 4;
        }
        return PathLists[tmp];
    }

    private void DoMove(List<GameObject> _path, string direction)
    {
        // 現在の回転を取得
        Vector3 tmp = transform.eulerAngles;

        tmp.x += 25;
        // 上にあげて斜め下を向く
        transform.DOLocalMoveY(UpYPos, 0.25f).SetEase(Ease.InQuart);
        transform.DOLocalRotate(tmp, 0.25f).SetEase(Ease.InQuart);

        // パスでカメラを動かす
        if (direction == "right")
        {
            tmp.y -= 90;
        }
        else
        {
            tmp.y += 90;
        }

        transform.DOLocalPath(
            _path.Select(target => target.transform.position).ToArray(),
            1f, PathType.CatmullRom, gizmoColor: Color.red).SetEase(Ease.InQuart).SetDelay(0.25f);
        transform.DOLocalRotate(tmp, 1f).SetEase(Ease.InQuart).SetDelay(0.25f);

        tmp.x -= 25;
        // 下に下げて水平を向く
        if (CameraPosInt%2 == 0){
            transform.DOLocalMoveY(FrontTargetPositionY, 0.5f).SetEase(Ease.InQuart).SetDelay(1.25f);
        }else{
            transform.DOLocalMoveY(TargetPositionY, 0.5f).SetEase(Ease.InQuart).SetDelay(1.25f);
        }

        transform.DOLocalRotate(tmp,0.5f).SetEase(Ease.InQuart).SetDelay(1.25f).OnKill(() =>
        {
            FrontRear = setFrontRearBool(CameraPosInt);
            SetMaterialFade(FadeMaterials, CameraPosInt);
            setCoroutineBoolFALSE();
        });
    }

    private void setCoroutineBoolFALSE()
    {
        coroutineBool = false;
    }

    public static bool getCoroutineBool()
    {
        return coroutineBool;
    }

    public static bool getFrontRear()
    {
        return FrontRear;
    }

    public int getCameraPosInt()
    {
        return CameraPosInt;
    }
    
    public enum Mode {
        Opaque,
        Cutout,
        Fade,
        Transparent,
    }
}
