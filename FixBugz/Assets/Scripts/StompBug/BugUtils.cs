using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugUtils {

    public delegate void BugKilled();
    public static event BugKilled OnBugKilled;

    public delegate void BugPainted();
    public static event BugPainted OnBugPainted;


    public static void KillBugOccur()
    {
        OnBugKilled();
    }

    public static void PaintBugOccur()
    {
        OnBugPainted();
    }
}
