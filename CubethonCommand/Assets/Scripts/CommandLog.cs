using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CommandLog
{
    public static bool doQueue = false;
    public static Queue<Command> commands = new Queue<Command>();
}

