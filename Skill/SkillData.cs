using System.Collections;
using System.Collections.Generic;
using UnityEngine;




[CreateAssetMenu(fileName = "New Data",menuName ="Skills Stats/Data")]
public class SkillData : ScriptableObject
{
    [Header("Stats Info")] 
    public int level;
    public List<int> OperateValue;
    public List<float> CD;
    public List<int> UseTime;
    public List<float> SkillTime;
    public List<float> Atk;
    public List<float> SkillTimeBuffer;
    public List<float> Speed;
    public List<KeyCode> key;
}
