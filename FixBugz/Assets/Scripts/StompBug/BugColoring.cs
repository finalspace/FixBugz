using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BugColor
{
    White = -1, Red, Blue, Green, Cyan, Purple
}

[System.Serializable]
public class BugSprite
{
    public BugColor bugColor;
    public Sprite bugSprite;
    public Color color;
}

public class BugColoring : SingletonBehaviour<BugColoring> {
    public List<BugSprite> bugSprites;

    public Sprite GetSpriteByType(BugColor bugColor, out Color color)
    {
        for (int i = 0; i < bugSprites.Count; i++)
        {
            if (bugSprites[i].bugColor == bugColor)
            {
                color = bugSprites[i].color;
                return bugSprites[i].bugSprite;
            }
        }

        Debug.LogWarning("Sprite not found");
        color = bugSprites[0].color;
        return null;
    }

    public BugColor GetRandomColor()
    {
        System.Array bugColors = System.Enum.GetValues(typeof(BugColor));
        BugColor bugColor = (BugColor)bugColors.GetValue(UnityEngine.Random.Range(0, bugColors.Length - 1));
        return bugColor;
    }

    public BugColor GetRandomPrimaryColor()
    {
        System.Array bugColors = System.Enum.GetValues(typeof(BugColor));
        BugColor bugColor = (BugColor)bugColors.GetValue(UnityEngine.Random.Range(0, 3));
        return bugColor;
    }

    public BugColor GetRandomSecondaryColor()
    {
        System.Array bugColors = System.Enum.GetValues(typeof(BugColor));
        BugColor bugColor = (BugColor)bugColors.GetValue(UnityEngine.Random.Range(3, 5));
        return bugColor;
    }
}
