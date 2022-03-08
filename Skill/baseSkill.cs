using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
public class baseSkill : MonoBehaviour
{

    #region 

    public SkillData data;

    public bool Flag = true;
    public int level
    {
        get { if (data != null) return data.level; else return 0; }
        set { data.level = value; }
    }
    public int OperateValue
    {
        get { if (data != null) return data.OperateValue[level]; else return 0; }
        set { data.OperateValue[level] = value; }

    }
    public float CD
    {
        get { if (data != null) return data.CD[level]; else return 0; }
        set { data.CD[level] = value; }
    }
    public int UseTime
    {
        get { if (data != null) return data.UseTime[level]; else return 0; }
        set { data.UseTime[level] = value; }
    }
    public float SkillTime
    {
        get { if (data != null) return data.SkillTime[level]; else return 0; }
        set { data.SkillTime[level] = value; }
    }
    public float Atk
    {
        get { if (data != null) return data.Atk[level]; else return 0; }
        set { data.Atk[level] = value; }
    }


    public float SkillTimeBuffer
    {
        get { if (data != null) return data.SkillTimeBuffer[level]; else return 0; }
        set { data.SkillTimeBuffer[level] = value; }
    }


    public float Speed
    {
        get { if (data != null) return data.Speed[level]; else return 0; }
        set { data.Speed[level] = value; }
    }

    public KeyCode key
    {
        get { if (data != null) return data.key[level]; else return 0; }
        set { data.key[level] = value; }
    }
    #endregion

    public float nextSkillTime = 0f;

    public virtual void SkillStart(Player player)
    {
        player.operateValue = OperateValue;
        StartCoroutine(SkillEnd(player));
    }

    public virtual void Skill(Player player)
    {

    }


    public virtual bool CDUse()
    {

        if (nextSkillTime > Time.time)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    IEnumerator SkillEnd(Player player)
    {
        yield return new WaitForSeconds(SkillTime);
        if (player.operateValue == OperateValue)
            player.operateValue = 0;
        nextSkillTime = Time.time + CD;

        yield return null;
    }

}