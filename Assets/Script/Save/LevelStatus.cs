public static class LevelStatus
{
    private static bool? _levelResult;
    private static SaveData _saveData;
    private static int _activeSceneIndex;
    private static int _sceneCount;

    public static void ChangeLevelResult(bool? levelResult)
    {
        _levelResult = levelResult;
    }

    public static void SetSaveData()
    {
        if (_saveData == null)
        {
            _saveData = new SaveData();
            _saveData = LevelSaver.LoadData();
        }
    }

    public static void SetActiveSceneIndex(int activeSceneIndex)
    {
        _activeSceneIndex = activeSceneIndex;
    }
    
    public static void SetSceneCount(int sceneCount)
    {
        _sceneCount = sceneCount;
    }
    
    
    public static void LevelCompleted()
    {
        if (_levelResult == null)
        {
            _saveData.LevelMap[_activeSceneIndex].Completed = true;
            if (_sceneCount != _activeSceneIndex + 1)
            {
                _saveData.SelectedLevel = _activeSceneIndex + 1;
            }

            _levelResult = false;
            LevelSaver.SaveData(_saveData);
        }
    }


}
