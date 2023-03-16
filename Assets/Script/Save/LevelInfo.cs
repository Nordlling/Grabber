using System;

[Serializable]
public class LevelInfo
{
    private bool completed;

    public bool Completed
    {
        get => completed;
        set => completed = value;
    }
}
