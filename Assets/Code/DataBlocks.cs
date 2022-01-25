using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "DataBlock", menuName = "Assets/DataBlocks", order = 10)]
public class DataBlocks : ScriptableObject
{
    public Sprite[] SpriteData;

    public Sprite GetSprite(int id)
    {
        if (SpriteData.Length <= id)
            return SpriteData[id];
        else
            return SpriteData[0];
    }


}
