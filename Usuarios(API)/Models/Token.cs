﻿namespace Usuarios_API_.Models;

public class Token
{
    public Token(string value)
    {
        Value = value;
    }
    public string Value { get; }
}
