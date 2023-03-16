using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


[Serializable] 
public class SaveData
{
    private Dictionary<int, LevelInfo> _levelMap;
    private int _selectedLevel;

    public Dictionary<int, LevelInfo> LevelMap
    {
        get => _levelMap;
        set => _levelMap = value;
    }

    public int SelectedLevel
    {
        get => _selectedLevel;
        set => _selectedLevel = value;
    }
}
