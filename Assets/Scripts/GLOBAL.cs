using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GLOBAL
{
    public static string module { get; private set; }
    public static string username { get; private set; } = "Anon";
    public static void SetUsername(string uname)
    {
        username = uname;
    }
}