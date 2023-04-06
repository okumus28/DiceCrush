using Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "level", menuName = "ScriptableObjects/level", order = 1)]
public class Level : ScriptableObject
{
    public int level;
    public int targetScore;
    public int jellyCount;
    public int moveCount;

    [System.Serializable]
    public class Column
    {
        public CellCharacteristic[] rows = new CellCharacteristic[9];
    }

    public Column[] columns = new Column[9];

    [System.Serializable]
    public class Value
    {
        public String[] values = new String[9];
    }

    public Value[] values = new Value[9];
}