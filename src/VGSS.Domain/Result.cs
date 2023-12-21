using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Diagnostics.CodeAnalysis;

namespace VGSS.Domain;
public class Result
{
    [MemberNotNullWhen(false, nameof(Reason))]
    public virtual bool IsSuccess { get; }
    public string? Reason { get; }

    protected Result()
    {
        IsSuccess = true;
    }

    protected Result(string reason)
    {
        IsSuccess = false;
        Reason = reason;
    }

    public static Result Success()
    {
        return new Result();
    }

    public static Result<T?> Success<T>(T value)
    {
        return new Result<T?>(value);
    }

    public static Result Fail(string reason)
    {
        return new Result(reason);
    }

    public static Result<T?> Fail<T>(string reason)
    {
        return new Result<T?>(reason);
    }

    public static Result<T?> Fail<T>(T value, string reason)
    {
        return new Result<T?>(value, reason);
    }
}

public class Result<T> : Result
{
    [MemberNotNullWhen(true, nameof(Value))]
    public override bool IsSuccess => base.IsSuccess;
    public T? Value { get; }

    internal Result() : base() { }

    internal Result(T value) : base()
    {
        Value = value;
    }

    internal Result(string reason) : base(reason) { }

    internal Result(T value, string reason) : base(reason)
    {
        Value = value;
    }

    public static implicit operator T(Result<T> result) => result.Value ?? throw new InvalidOperationException("Value is null.");
}