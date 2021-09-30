using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;

public class EnemyMove : MonoBehaviour
{
    // private bool croutinBool;

    // Unity上で空のオブジェクトでパスを描かせ、その上を動かさせる
    // List<GameObject>でパスのオブジェクトを指定 -> インデックスを別途用意してそこめがけてトゥイーン -> 到着後次のオブジェクトの方向を向くトゥイーン & インデックス+1
    // -> リストの長さのインデックスまで来たらインデックスの変数を今度は-1ずつしていく
    // -> インデックスの変数の他に、リストの長さに到達したらプラマイを変換するint型変数が必要（Index = Index*PlusMinusInt）
    [SerializeField] protected List<GameObject> _targets;

    protected int Index;
    protected int IndexChanger;

    protected float blocks;

    protected bool isMove;
    protected bool isRotate;
    protected bool isCroutine;
    protected bool dontMove;

    protected GameObject nextTarget;
    
    public float ROTATE_SPEED = 1.5f;
    public float SPEED_PER_BLOCK = 0.5f;
    // Start is called before the first frame update
    protected void Start()
    {
        Index = 1; // 0はエネミーが最初からいる地点なので1から始める
        IndexChanger = 1;
        blocks = 0;
        isMove = false;
        isCroutine = false;
        dontMove = false;
        if (_targets.Count > 1)
        {
            nextTarget = _targets[Index];
            // 最初の回転
            transform.DOLookAt(_targets[Index].transform.position, 0.0001f,axisConstraint:AxisConstraint.Y).SetEase(Ease.Linear);
        }
        else
        {
            dontMove = true;
            nextTarget = null;
        }
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (_targets.Count > 1)
        {
            if (!isMove)
            {
                isMove = true;
                blocks = CalcBlocks(nextTarget);
                // Debug.Log("目標地点までブロック"+blocks+"個");
                transform.DOMove(_targets[Index].transform.position, blocks*SPEED_PER_BLOCK).SetEase(Ease.Linear)
                    .OnComplete(() => { ChangeIndexAndRotate(); });
            }
        }
        else
        {
            if (!isCroutine)
            {
                isCroutine = true;
                //  その場で回転のみする関数
                StartCoroutine(RotateDegree());
            }
        }
    }

    protected virtual void ChangeIndexAndRotate()
    {
        isRotate = true;
        if (Index + 1 == _targets.Count && IndexChanger==1)
        {
            IndexChanger = -1;
        }else if (Index==0 && IndexChanger==-1)
        {
            IndexChanger = 1;
        }
        Index += IndexChanger*1;
        
        // Debug.Log(_targets[Index]);
        setNextTarget();
        transform.DOLookAt(_targets[Index].transform.position,ROTATE_SPEED,axisConstraint:AxisConstraint.Y).SetEase(Ease.Linear).OnComplete(() => {setBoolsFalse();});
    }

    protected float CalcBlocks(GameObject target)
    {
        if (transform.eulerAngles.y == 90 || transform.eulerAngles.y == 270)
        {
            return Mathf.Abs(transform.position.x - target.transform.position.x)/0.4f;
        } 
        else if (transform.eulerAngles.y == 0 || transform.eulerAngles.y == 180)
        {
            return Mathf.Abs(transform.position.y - target.transform.position.y)/0.4f;
        }
        else
        {
            return 0.4f;
        }
    }

    // DOTwennのコルーチンで処理を行っているので別途関数化
    protected void setBoolsFalse()
    {
        isRotate = false;
        isMove = false;
    }
    
    public bool getIsMove()
    {
        return isMove;
    }

    public bool getIsRotate()
    {
        return isRotate;
    }

    protected void setNextTarget()
    {
        nextTarget = _targets[Index];
    }
    public GameObject getNextTarget()
    {
        return nextTarget;
    }

    public bool getDontMove()
    {
        return dontMove;
    }

    protected virtual IEnumerator RotateDegree()
    {
        isRotate = true;
        for (int turn=0; turn<180; turn++)
        {
            transform.Rotate(0,1,0);
            yield return new WaitForSeconds(1f/180f);
        }
        isRotate = false;

        yield return new WaitForSeconds(2f);
        isCroutine = false;
    }

}
