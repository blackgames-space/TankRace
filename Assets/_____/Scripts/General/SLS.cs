using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class SLS
{
    public static int GetInt(string key, int defaultVal = 0) => PlayerPrefs.GetInt(key, defaultVal);

    public static List<int> GetIntCollection(string key, List<int> defaultValues)
    {
        string str = PlayerPrefs.GetString(key);
        if (str == "")
            return defaultValues;
        else
            return str.Split('&').Select(t => int.Parse(t)).ToList();

    }

    public static List<bool> GetBoolCollection(string key, List<bool> defaultValues)
    {
        string str = PlayerPrefs.GetString(key);
        if (str == "")
            return defaultValues;
        else
            return str.Split('&').Select(t => int.Parse(t) == 1).ToList();
    }

    public static void SetInt(string key, int value) => PlayerPrefs.SetInt(key, value);

    public static void SetIntCollection(string key, List<int> values)
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < values.Count; i++)
        {
            sb.Append(values[i].ToString());
            if (i != values.Count - 1)
                sb.Append('&');
        }

        PlayerPrefs.SetString(key, sb.ToString());
    }

    public static void SetBoolCollection(string key, List<bool> values)
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < values.Count; i++)
        {
            sb.Append(values[i] ? '1' : '0');
            if (i != values.Count - 1)
                sb.Append('&');
        }

        PlayerPrefs.SetString(key, sb.ToString());
    }

    public class Keys
    {
        public class Progress
        {
            public const string LEVEL_CURRENT_INT = "LEVEL_CURRENT";
            public const string MONEY_INT = "MONEY";
            public const string LOADING_LEVEL_INT = "LOADING_LEVEL";
            public const string TANK_CHOSEN_INT = "TANK_CHOSEN";
        }
    }

    public class Snapshot
    {
        private int _currentLevel;
        public int CurrentLevel
        {
            get => _currentLevel;
            set
            {
                SLS.SetInt(SLS.Keys.Progress.LEVEL_CURRENT_INT, value);
                _currentLevel = value;
            }
        }

        private int _money;
        public int Money
        {
            get => _money;
            set
            {
                SLS.SetInt(SLS.Keys.Progress.MONEY_INT, value);
                _money = value;
            }
        }


        private int _loadingLevel;
        public int LoadingLevel
        {
            get => _loadingLevel;
            set
            {
                SLS.SetInt(SLS.Keys.Progress.LOADING_LEVEL_INT, value);
                _loadingLevel = value;
            }
        }

        private int _playerTankType;
        public int PlayerTankType
        {
            get => _playerTankType;
            set
            {
                SLS.SetInt(SLS.Keys.Progress.TANK_CHOSEN_INT, value);
                _playerTankType = value;
            }
        }

        public Snapshot()
        {
            _currentLevel = SLS.GetInt(SLS.Keys.Progress.LEVEL_CURRENT_INT);
            _money = SLS.GetInt(SLS.Keys.Progress.MONEY_INT);
            _loadingLevel = SLS.GetInt(SLS.Keys.Progress.LOADING_LEVEL_INT);
            _playerTankType = SLS.GetInt(SLS.Keys.Progress.TANK_CHOSEN_INT);
        }
    }
}
