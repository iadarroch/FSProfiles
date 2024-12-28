namespace FSProfiles.Common.Classes;

public readonly struct Result<TV, TE>
{
    public readonly TV Value;
    public readonly TE Error;

    private Result(TV v, TE e, bool success)
    {
        Value = v;
        Error = e;
        Success = success;
    }

    public bool Success { get; }

    public static Result<TV, TE> Ok(TV v)
    {
#pragma warning disable CS8604 // Possible null reference argument.
        return new(v, default, true);
#pragma warning restore CS8604 // Possible null reference argument.
    }

    public static Result<TV, TE> Err(TE e)
    {
#pragma warning disable CS8604 // Possible null reference argument.
        return new Result<TV, TE>(default, e, false);
#pragma warning restore CS8604 // Possible null reference argument.
    }

#pragma warning disable CS8604 // Possible null reference argument.
    public static implicit operator Result<TV, TE>(TV v) => new(v, default, true);
#pragma warning restore CS8604 // Possible null reference argument.
#pragma warning disable CS8604 // Possible null reference argument.
    public static implicit operator Result<TV, TE>(TE e) => new(default, e, false);
#pragma warning restore CS8604 // Possible null reference argument.

    public TR Match<TR>(
        Func<TV, TR> success,
        Func<TE, TR> failure) =>
        Success ? success(Value) : failure(Error);
}

public class AggregatedResult<TV, TE>
{
    public readonly List<TV> Values;
    public readonly List<TE> Errors;

    public AggregatedResult(List<TV> v, List<TE> e)
    {
        Values = v;
        Errors = e;
    }

    public bool HasErrors => Errors.Count > 0;
}