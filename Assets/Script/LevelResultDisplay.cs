using System;
using UnityEngine;

public class LevelResultDisplay : MonoBehaviour, ILevelResultDisplay
{
    [SerializeField] private GameObject successPanel;
    [SerializeField] private GameObject failPanel;
    private bool _used = false;

    public void Success()
    {
        if (!_used)
        {
            successPanel.SetActive(true);
            _used = true;
        }
    }
    
    public void Fail()
    {
        if (!_used)
        {
            failPanel.SetActive(true);
            _used = true;
        }
    }
}
