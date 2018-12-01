using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_PaintBug : MiniGameManagerBase {

    public MiniGame game = MiniGame.PaintBug;

    public GameObject groundBugPrefab;
    public Transform SpawnPosition1;
    public Transform SpawnPosition21;
    public Transform SpawnPosition22;
    public Transform SpawnPosition31;
    public Transform SpawnPosition32;
    public Transform SpawnPosition33;
    public Transform SpawnPosition41;
    public Transform SpawnPosition42;
    public Transform SpawnPosition43;
    public Transform SpawnPosition44;

    [Header("Tutorial")]
    public GameObject tutorialObject;

    private int level;
    private bool won = true;
    private bool gamePlaying;
    private float gameTime;
    private System.DateTime startTime;
    private int bugNum;
    private int bugPainted;
    private List<BugData> bugs;

    private void OnEnable()
    {
        MiniGameTimerUI.OnTimeUp += OnTimeUp;
        BugUtils.OnBugPainted += OnBugPainted;
        PlayerUtils.OnPlayerDead += OnPlayerDead;
    }

    private void OnDisable()
    {
        MiniGameTimerUI.OnTimeUp -= OnTimeUp;
        BugUtils.OnBugPainted -= OnBugPainted;
        PlayerUtils.OnPlayerDead -= OnPlayerDead;
    }

    private void Start()
    {
        gamePlaying = false;
        LoadGame();
        StartGame();
    }

	private void Update()
	{
        if (Input.GetKeyDown(KeyCode.A))
        {
            ColorWheel.Instance.TrySpinRight();
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            ColorWheel.Instance.TrySpinLeft();
        }
        else if (Input.GetKeyDown(KeyCode.J))
        {
            BugColor colorToPaint = ColorWheel.Instance.GetCurrentColor();
            PaintQueueManager.Instance.TryPaint(colorToPaint);
        }
	}

	public override void StartGame()
    {
        gamePlaying = true;
        startTime = System.DateTime.Now;
        MiniGameTimerUI.Instance.StartTimer();

        bugPainted = 0;
        PaintQueueManager.Instance.Init(bugs);
        PaintQueueManager.Instance.SpawnNext();
    }

    public override void EndGame()
    {
        MainGameManager.Instance.FinishMiniGame(game, won);
    }

    public void OnTimeUp()
    {
        //play effect
        if (won && bugPainted < bugNum)
            won = false;

        EndGame();
    }

    private void SpawnBugs()
    {
        for (int i = 0; i < bugs.Count; i++)
        {
            BugData data = bugs[i];
            StartCoroutine(SpawnBug(i, bugs[i]));
        }
    }

    IEnumerator SpawnBug(int subIndex, BugData data)
    {
        yield return new WaitForSeconds(data.spawnTime);

        Transform spawnLocation = null;
        if (bugNum == 1)
        {
            spawnLocation = SpawnPosition1;
        }
        else if(bugNum == 2)
        {
            if (subIndex == 1)
                spawnLocation = SpawnPosition21;
            else if (subIndex == 2)
                spawnLocation = SpawnPosition22;
        }
        else if (bugNum == 3)
        {
            if (subIndex == 1)
                spawnLocation = SpawnPosition31;
            else if (subIndex == 2)
                spawnLocation = SpawnPosition32;
            else if (subIndex == 3)
                spawnLocation = SpawnPosition33;
        }
        else if (bugNum == 4)
        {
            if (subIndex == 1)
                spawnLocation = SpawnPosition41;
            else if (subIndex == 2)
                spawnLocation = SpawnPosition42;
            else if (subIndex == 3)
                spawnLocation = SpawnPosition43;
            else if (subIndex == 4)
                spawnLocation = SpawnPosition44;
        }

        GameObject bug = Instantiate(groundBugPrefab, spawnLocation.position, Quaternion.identity);
        Bug bugCtrl = bug.GetComponent<Bug>();
        bugCtrl.Init(data);

    }

    public void OnBugPainted()
    {
        bugPainted += 1;
        PaintQueueManager.Instance.SpawnNext();
    }

    private void OnPlayerDead()
    {
        won = false;
    }

    private void SetupTimer()
    {
        float seconds = MainGameManager.Instance.GetMiniGameTime(level);
        MiniGameTimerUI.Instance.ResetTimer(seconds);
    }

    public override void LoadGame()
    {
        level = MainGameManager.Instance.GetMiniGameLevel(game);
        SetupTimer();

        if (level < 3)
        {
            bugNum = 1;
            bugs = new List<BugData>();
            bugs.Add(new BugData(BugColoring.Instance.GetRandomPrimaryColor()));
        }
        else if (level == 3)
        {
            bugNum = 2;
            bugs = new List<BugData>();
            bugs.Add(new BugData(BugColoring.Instance.GetRandomPrimaryColor()));
            bugs.Add(new BugData(BugColoring.Instance.GetRandomPrimaryColor()));
        }
        else if (level == 4)    //tutorial
        {
            bugNum = 1;
            bugs = new List<BugData>();
            bugs.Add(new BugData(BugColoring.Instance.GetRandomSecondaryColor()));
            //tutorialObject.SetActive(true);
        }
        else if (level < 8)
        {
            bugNum = 2;
            bugs = new List<BugData>();
            bugs.Add(new BugData(BugColoring.Instance.GetRandomColor()));
            bugs.Add(new BugData(BugColoring.Instance.GetRandomColor()));
        }
        else if (level < 12)
        {
            bugNum = 3;
            bugs = new List<BugData>();
            bugs.Add(new BugData(BugColoring.Instance.GetRandomPrimaryColor()));
            bugs.Add(new BugData(BugColoring.Instance.GetRandomPrimaryColor()));
            bugs.Add(new BugData(BugColoring.Instance.GetRandomPrimaryColor()));
        }
        else
        {
            bugNum = 3;
            bugs = new List<BugData>();
            bugs.Add(new BugData(BugColoring.Instance.GetRandomColor()));
            bugs.Add(new BugData(BugColoring.Instance.GetRandomColor()));
            bugs.Add(new BugData(BugColoring.Instance.GetRandomColor()));
        }
    }
}
