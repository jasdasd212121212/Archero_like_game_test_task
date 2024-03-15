using System;
using UnityEngine;

public static class Wallet
{
    private static int _coins;
    private static int _maximalStepsCountForDelta = 1;
    private static int _currentDeltaStep = 0;

    public static int LastDelta { get; private set; }

    public static int LastStepsDelta { get; private set; }
    public static int MaximalStepsCountForDelta 
    { 
        get => _maximalStepsCountForDelta; 

        set 
        {
            if (value < 1)
            {
                _maximalStepsCountForDelta = 1;
                Debug.LogError("Can`t set a maximalStepsCountForDelta -> 'value < 1'");
            }

            _maximalStepsCountForDelta = value;
        } 
    }

    private static bool _inited = false;

    private static ISavingService _moneySaver;
    public static event Action MoneyChanged;

    private static readonly int START_MONEY = 0;

    private static void Init()
    {
        if (_inited == true)
        {
            return;
        }

        _moneySaver = new WalletSaver(START_MONEY);
        _inited = true;
    }

    public static int money 
    {
        get
        {
            Init();

            _coins = _moneySaver.Load<int>(SavingSystemConfig.MONEY_SAVE_KEY);
            return _coins;
        }

        private set => throw new NotImplementedException();
    }

    public static void ChargeMoney(int money)
    {
        if (money < 0)
        {
            Debug.LogError($"Can`t charge <{money}> because '< 0'");
            return;
        }

        Init();
        _coins = _moneySaver.Load<int>(SavingSystemConfig.MONEY_SAVE_KEY);

        _coins += money;
        CalculateDeltas(money);

        _moneySaver.Save<int>(SavingSystemConfig.MONEY_SAVE_KEY, _coins);

        MoneyChanged?.Invoke();
    }

    public static void DebitMoney(int money)
    { 
        if (money < 0)
        {
            Debug.LogError($"Can`t debit <{money}> because '< 0'");
            return;
        }

        Init();
        _coins = _moneySaver.Load<int>(SavingSystemConfig.MONEY_SAVE_KEY);

        _coins -= money;
        CalculateDeltas(-money);

        _moneySaver.Save<int>(SavingSystemConfig.MONEY_SAVE_KEY, _coins);

        MoneyChanged?.Invoke();
    }

    private static void CalculateDeltas(int money)
    {
        if (_currentDeltaStep >= _maximalStepsCountForDelta)
        {
            LastStepsDelta = 0;
            _currentDeltaStep = 0;
        }

        LastDelta = money;

        LastStepsDelta += money;
        _currentDeltaStep++;
    }

    public static void ClearDeltasHistory()
    {
        LastStepsDelta = 0;
        _currentDeltaStep = 0;
    }
}