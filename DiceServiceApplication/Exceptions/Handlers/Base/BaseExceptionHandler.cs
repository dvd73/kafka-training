﻿using System.Text;
using DiceServiceApplication.Exceptions.Handlers.Base.Interfaces;
using DiceServiceApplication.Extensions;

namespace DiceServiceApplication.Exceptions.Handlers.Base;

internal abstract class BaseExceptionHandler<TException> : IExceptionHandler where TException : Exception
{
    public abstract string ErrorCode { get; }
    public abstract string Message { get; }
    public virtual string? Details { get; private set; }

    protected virtual bool HandlingRequired(Exception exception)
    {
        return exception.GetType() == typeof(TException);
    }

    public bool Handle(Exception exception)
    {
        if (HandlingRequired(exception))
        {
            Details = exception.ComposeFullExceptionMessage();
            return true;
        }

        return false;
    }
}