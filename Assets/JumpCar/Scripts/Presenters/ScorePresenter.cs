using System;
using UnityEngine;

public class ScorePresenter: IDisposable
{
    private IScoreObservable _model;
    private CounterView _view;

    public ScorePresenter(IScoreObservable model, CounterView view)
    {
        _model = model;
        _view = view;

        _model.OnValueChanged += SetViewValue;
    }

    private void SetViewValue(int value)
    {
        _view.SetValue(value.ToString());
    }

    public void Dispose()
    {
        _model.OnValueChanged -= SetViewValue;
    }
}
