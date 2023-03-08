using UnityEngine;
using UnityEngine.UI;
using System;
using Random = UnityEngine.Random;

public class Manager : MonoBehaviour
{
    private static Manager instance;
    public static Manager Instance
    {
        get
        {
            if(!instance)
            {
                instance = FindObjectOfType<Manager>();
            }

            return instance;
        }
    }

    bool trySpin;
    int totalBet;

    [SerializeField] Text winText;

    [Space(10)]
    [SerializeField] GameObject autoSpinEnable;
    [SerializeField] GameObject autoSpinDisable;

    //[Space(10)]
    //[SerializeField] Transform winlineParent;
    //[SerializeField] GameObject[] winLinePrefabs;

    GameInfo gameInfo;
    RollInfo rollInfo;

    public static Action<int> OnEndRolling { get; set; }
    public static Action<int> OnSetLine { get; set; }

    private void Start()
    {
        totalBet = 10;
        gameInfo = new GameInfo
        {
            balance = 1000, bids = new int[] { 10, 25, 50, 100}
        };
    }

    bool CanSpin()
    {
        return gameInfo.balance >= totalBet && totalBet > 0;
    }

    public void TrySpin()
    {
        if (!CanSpin() || trySpin)
        {
            if (gameInfo.balance <= 0)
            {
                SlotMachine.autoSpin = false;


                autoSpinEnable.SetActive(false);
                autoSpinDisable.SetActive(true);
            }
            return;
        }

        trySpin = false;
        rollInfo = new RollInfo
        {
            win = true,
            win_amount = 100,
            balance = gameInfo.balance + 0,
            winlines = new int[] { 1 },
            result = GetReelData()
        };

        winText.text = $"{0:N}";
        SlotMachine.Instance.Pull(rollInfo.result);
    }

    ReelData[] GetReelData()
    {
        ReelData[] _reelDatas = new ReelData[5];
        
        for(int i = 0; i < _reelDatas.Length; i++)
        {
            _reelDatas[i] = new ReelData
            {
                iconNames = GetIconNames()
            };
        }

        return _reelDatas;
    }

    string GetRandomIconName()
    {
        string[] names = new string[] 
        { 
            "Strawberry",
            "Orange",
            "Banana",
            "Blueberry",
            "Lemon",
            "Plum",
            "Pear",
            "Cherry"
        };

        return names[Random.Range(0, names.Length)];
    }

    string[] GetIconNames()
    {
        string[] _iconNames = new string[3];

        for(int i = 0; i < _iconNames.Length; i++)
        {
            _iconNames[i] = GetRandomIconName();
        }

        return _iconNames;
    }

    //GameObject GetWinLineById(int id) => id switch
    //{
    //    1 => winLinePrefabs[0],
    //    2 => winLinePrefabs[1],
    //    3 => winLinePrefabs[2]
    //};

    //public void CalculatePrize()
    //{
    //    foreach (int i in rollInfo.winlines)
    //    {
    //        Instantiate(GetWinLineById(i), winlineParent);
    //    }
    //}

    public void SetAutoSpin()
    {
        SlotMachine.autoSpin = !SlotMachine.autoSpin;


        autoSpinEnable.SetActive(SlotMachine.autoSpin);
        autoSpinDisable.SetActive(!SlotMachine.autoSpin);
    }

    [Serializable]
    public class GameInfo
    {
        public int balance;
        public int[] bids;
    }

    [Serializable]
    public class RollInfo
    {
        public bool win;
        public int win_amount;
        public int balance;
        public int[] winlines;
        public ReelData[] result;
    }

    [Serializable]
    public class ReelData
    {
        public string[] iconNames;
    }
}
