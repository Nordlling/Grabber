using System;
using UnityEngine;


public class FailTrigger : MonoBehaviour
{
    [SerializeField] private GameObject failPanel;
    private ILevelResultDisplay _levelResultDisplay;

    private void Start()
    {
        _levelResultDisplay = FindObjectOfType<LevelResultDisplay>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag.Equals("Citizen"))
        {
            LevelStatus.ChangeLevelResult(false);
            _levelResultDisplay.Fail();
        }
    }
}
