using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MiddleAgeTower : ShootingTurret {

    [Header("MiddleAgeTower")]
    [SerializeField] public Transform partToRotate;
    [SerializeField] public float turnSpeed = 10f;

    protected void PartToRotateLockTarget()
    {
        Vector3 dir = Target.transform.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);//Quaternion -> Unity-ban kezeli a rotationt, nekünk csak tudni kell hogyan nézzünk arra
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }
}
