using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PaintQueueManager : SingletonBehaviour<PaintQueueManager> {
    public List<GameObject> targetBugSprites;
    public List<PaintBug> queueBugs;
    public Animator brushAnimator;

    public int total;
    public int currentIdx = -1;

    public bool readyToPaint = false;

    private int max;
    private List<BugData> targetBugs;
    private GameObject activeBugSprite;
    private BugColor targetColor;
    private PaintBug currentBug;
    private BugColor currentColor;

	public void Start()
	{
        max = queueBugs.Count;
	}


	public void Init(List<BugData> bugs)
    {
        this.targetBugs = bugs;
        total = bugs.Count;
        currentIdx = -1;

        for (int i = total; i < max; i++)
        {
            queueBugs[i].gameObject.SetActive(false);
        }
    }

    public void SpawnNext()
    {
        if (currentIdx >= total - 1)
            return;

        if (activeBugSprite != null)
            activeBugSprite.SetActive(false);

        StartCoroutine(SpawnBug());

        for (int i = currentIdx + 1; i < total; i++)
        {
            queueBugs[i].transform.DOLocalMoveX(1.5f, 0.2f).SetRelative().SetEase(Ease.OutExpo);
        }
    }


    IEnumerator SpawnBug()
    {
        yield return new WaitForSeconds(0.2f);

        readyToPaint = true;
        currentIdx++;
        int childIdx = (int)targetBugs[currentIdx].bugColor;
        activeBugSprite = targetBugSprites[childIdx];
        activeBugSprite.SetActive(true);

        targetColor = targetBugs[currentIdx].bugColor;
        currentBug = queueBugs[currentIdx];
        currentColor = currentBug.bugColor;
    }


    public void TryPaint(BugColor colorToPaint)
    {
        if (!readyToPaint)
            return;
        
        if (currentColor == BugColor.White)
        {
            currentBug.Paint(colorToPaint);
        }
        else if (currentColor == BugColor.Red)
        {
            if (colorToPaint == BugColor.Blue && targetColor == BugColor.Purple)
                currentBug.Paint(BugColor.Purple);
            else currentBug.Paint(colorToPaint);
        }
        else if (currentColor == BugColor.Green)
        {
            if (colorToPaint == BugColor.Blue && targetColor == BugColor.Cyan)
                currentBug.Paint(BugColor.Cyan);
            else currentBug.Paint(colorToPaint);
        }
        else if (currentColor == BugColor.Blue)
        {
            if (colorToPaint == BugColor.Red && targetColor == BugColor.Purple)
                currentBug.Paint(BugColor.Purple);
            else if (colorToPaint == BugColor.Green && targetColor == BugColor.Cyan)
                currentBug.Paint(BugColor.Cyan);
            else currentBug.Paint(colorToPaint);
        }

        brushAnimator.SetTrigger("IsBrushing");
        currentColor = currentBug.bugColor;
        CheckMatch();
    }

    private void CheckMatch()
    {
        if (currentBug.bugColor == targetColor)
        {
            BugUtils.PaintBugOccur();

            readyToPaint = false;
            LinearMove movementCtrl = currentBug.gameObject.AddComponent<LinearMove>();
            movementCtrl.Init(true, 5);
        }
    }

}
