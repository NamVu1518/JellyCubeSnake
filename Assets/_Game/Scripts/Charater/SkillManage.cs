using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManage : Singleton<SkillManage>
{
    public void Push(Transform transform, Vector3 dir, float force, float time)
    {
        dir = dir.normalized;
        transform.DOMove(transform.position + dir * force, time).SetEase(Ease.Linear);
    }
}
