using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DataMenu", fileName = "DataFile")]
public class Data : ScriptableObject
{
    public int levelCount;
    public bool[] islevel;
}
