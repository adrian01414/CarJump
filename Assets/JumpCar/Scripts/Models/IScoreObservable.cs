using System;

public interface IScoreObservable
{
    public event Action<int> OnValueChanged;
}
