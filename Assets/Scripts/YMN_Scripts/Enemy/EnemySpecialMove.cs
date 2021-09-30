using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemySpecialMove : EnemyMove
{
    public new int RotateDegree = 90;
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (!base.isMove)
        {
            base.isMove = true;
            base.blocks = CalcBlocks(nextTarget);
            // Debug.Log("目標地点までブロック"+blocks+"個");
            transform.DOMove(_targets[Index].transform.position, blocks*SPEED_PER_BLOCK).SetEase(Ease.Linear)
                .OnComplete(() => { StartCoroutine(ChangeIndexAndRotate()); });
        } 
    }

    private new IEnumerator ChangeIndexAndRotate()
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
        base.setNextTarget();

        for (int turn=0; turn<RotateDegree; turn++)
        {
            transform.Rotate(0,1*1,0);
            yield return new WaitForSeconds(1f/90f);
        }

        yield return new WaitForSeconds(2f);
        isRotate = false;
        transform.DOLookAt(_targets[Index].transform.position, ROTATE_SPEED,axisConstraint:AxisConstraint.Y).SetEase(Ease.Linear).OnComplete(() => {base.setBoolsFalse();});
    }

    private new float CalcBlocks(GameObject target)
    {
        return ComparePosition(transform.position,target.transform.position) / 0.4f;
    }

    private float ComparePosition(Vector3 currentPos, Vector3 targetPos)
    {
        float tmpX = Mathf.Abs(currentPos.x - targetPos.x);
        float tmpZ = Mathf.Abs(currentPos.z - targetPos.z);

        return (tmpX>tmpZ) ? tmpX : tmpZ;
    }
}
