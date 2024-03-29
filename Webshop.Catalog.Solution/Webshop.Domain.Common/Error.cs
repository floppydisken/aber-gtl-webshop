﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webshop.Domain.Common;
using Vogen;

namespace Webshop.Domain.Common;

[ValueObject<string>]
public partial struct ErrorCode {}

[ValueObject<string>]
public partial struct ErrorMessage {}

[ValueObject<int>]
public partial struct ErrorStatusCode {}

public class Error : ValueObject
{
    public ErrorCode Code { get; }
    public ErrorMessage Message { get; }
    public ErrorStatusCode StatusCode { get; }

    public Error(string code, string message, int statusCode = 400)
    {
        Code = ErrorCode.From(code);
        Message = ErrorMessage.From(message);
        StatusCode = ErrorStatusCode.From(statusCode);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Code;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Code);
    }

    public override string ToString()
    {
        return $"ErrorCode: {Code}; Message: {Message}; StatusCode: {StatusCode}";
    }
}