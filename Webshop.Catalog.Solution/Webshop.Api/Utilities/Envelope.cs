using System;
using System.Text.Json.Serialization;

namespace Webshop.Api.Utilities;

/// <summary>
/// The responsibility for the envelope is to wrap any result to create a consistent resultset to the clients.
/// </summary>
public class Envelope : Envelope<string>
{
    protected internal Envelope(string errorMessage) : base(null, errorMessage)
    {
    }

    public static Envelope<T> Ok<T>(T result)
    {
        return new Envelope<T>(result, null);
    }

    public static Envelope Ok()
    {
        return new Envelope(null);
    }

    public static Envelope Error(string errormessage)
    {
        return new Envelope(errormessage);
    }
}

public class Envelope<T>
{
    [JsonPropertyName("result")]
    public T Result { get; set; }
    public string ErrorMessage { get; set; }
    public DateTime TimeGenerated { get; set; }
    protected internal Envelope(T result, string errorMessage)
    {
        this.Result = result;
        this.ErrorMessage = errorMessage;
        this.TimeGenerated = DateTime.UtcNow;
    }

    public Envelope()
    {
        
    }
}