using System;
using System.Collections.Generic;
using ReactiveUI;

namespace Sample;

public static class ReactiveObjectExtensions
{
    public static TRet RaiseAndSetIfChangedMultiple<TObj, TRet>(
        this TObj reactiveObject,
        ref TRet backingField,
        TRet newValue,
        params string[] propertyNames)
        where TObj : IReactiveObject
    {
        ArgumentNullException.ThrowIfNull(propertyNames);

        if (EqualityComparer<TRet>.Default.Equals(backingField, newValue))
        {
            return newValue;
        }

        foreach (var propertyName in propertyNames)
        {
            reactiveObject.RaisePropertyChanging(propertyName);
        }
        backingField = newValue;
        foreach (var propertyName in propertyNames)
        {
            reactiveObject.RaisePropertyChanged(propertyName);
        }
        return newValue;
    }
}