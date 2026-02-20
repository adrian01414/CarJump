using System;
using UnityEngine;

public class Score : IScoreObservable
{
    public event Action<int> OnValueChanged;

    public int Value { get; private set; } = 0;
    public int MaxValue { get; private set; } = 0;

    public Score(int maxValue)
    {
        MaxValue = maxValue;
    }

    public void Increase()
    {
        Value++;
        if(Value > MaxValue)
        {
            MaxValue = Value;
        }
        OnValueChanged?.Invoke(Value);
    }

    public void Reset()
    {
        Value = 0;
        OnValueChanged?.Invoke(Value);
    }
}
