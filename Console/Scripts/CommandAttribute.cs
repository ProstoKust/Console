using System;
using System.Collections.Generic;

public class CommandAttribute : Attribute
{
    public CommandAttribute() { }
    public CommandAttribute(params Type[] _ParamsTypes)
    {
        if (ParamsTypes != null)
            this.ParamsTypes.AddRange(_ParamsTypes);
    }
    public List<Type> ParamsTypes = new();
}