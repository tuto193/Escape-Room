using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;
using TMPro;

[RequireComponent(typeof(AudioSource))]
public class GameManager : MonoBehaviour
{
    private static bool m_ShuttingDown = false;
    private static object m_Lock = new object();
    [Header("Parameters")]
    [Tooltip("Duration of the fade-to-black at the end of the game")]
    public float EndSceneLoadDelay = 3f;


    [Tooltip("The canvas group of the fade-to-black screen")]
    public CanvasGroup EndGameFadeCanvasGroup;

    [Header("Win")] [Tooltip("This string has to be the name of the scene you want to load when winning")]
    public string WinSceneName = "WinScene";

    [Tooltip("Duration of delay before the fade-to-black, if winning")]
    public float DelayBeforeFadeToBlack = 4f;

    [Tooltip("Win game message")]
    public string WinGameMessage;
    [Tooltip("Duration of delay before the win message")]
    public float DelayBeforeWinMessage = 2f;

    [Tooltip("Sound played on win")] public AudioClip VictorySound;

    [Header("Lose")] [Tooltip("This string has to be the name of the scene you want to load when losing")]
    public string LoseSceneName = "LoseScene";


    public bool GameIsEnding { get; private set; }

    float m_TimeLoadEndGameScene;
    string m_SceneToLoad;

    [Header("Sounds imported from base game")]
    public AudioSource AudioSource;
    public AudioClip CorrectSfx;
    public AudioClip CollectSfx;

    public AudioClip WrongSfx;
    public AudioClip DropSfx;
    public AudioClip GameEndSfx;
    public AudioClip KeyAppearsSfx;
    public AudioClip KeyTextSfx;
    public AudioClip StartingTextSfx;
    public AudioClip EndTextSfx;

    [Header("UI Stuff")]
    public ProgressBar ProgressBar;

    private bool _sawIntro;

    private int _itemsCollected;

    [Tooltip("Beginning instructions")]
    public TMP_InputField InputField;

    [Tooltip("Background for Start Text")]
    public Image startTextBackground;

    [Tooltip("Background for Ending Text")]
    public Image endTextBackground;

    [Tooltip("Video played at the beginning")]
    public RawImage IntroVideo;
    public VideoPlayer VideoPlayer;

    [Tooltip("Ending Text")]
    public TextMeshProUGUI endText;

    [Tooltip("Image for Key's background")]
    public Image keyTextBackground;
    [Tooltip("Key object to finish part 1")]
    private GameObject _key;
    [Tooltip("Picture-frame riddles to solve.")]
    private GameObject[] _riddles;

    private int _winningThreshold;

    private string _currentRiddle;
    private bool _seenRiddle;
    private bool _wonTheGame;

    private static GameManager _instance;

    public static GameManager Instance {
        get {
            if (m_ShuttingDown)
            {
                Debug.LogWarning("[Singleton] Instance '" + typeof(GameManager) +
                    "' already destroyed. Returning null.");
                return null;
            }

            lock (m_Lock)
            {
                if (_instance == null)
                {
                    // Search for existing instance.
                    _instance = (GameManager)FindObjectOfType(typeof(GameManager));

                    // Create new instance if one doesn't already exist.
                    if (_instance == null)
                    {
                        // Need to create a new GameObject to attach the singleton to.
                        var singletonObject = new GameObject();
                        _instance = singletonObject.AddComponent<GameManager>();
                        singletonObject.name = typeof(GameManager).ToString() + " (Singleton)";

                        // Make instance persistent.
                        DontDestroyOnLoad(singletonObject);
                    }
                }

                return _instance;
            }
        }
    }

    void Awake()
    {
    }

    void Start()
    {
        _sawIntro = false;
        GameIsEnding = false;
        _wonTheGame = false;
        _itemsCollected = 0;
        _key = GameObject.FindGameObjectWithTag("Key");
        print(InputField.text);
        // (De-)Activate stuff here
        ProgressBar.gameObject.SetActive(false);
        InputField.gameObject.SetActive(false);
        endTextBackground.gameObject.SetActive(false);
        endText.gameObject.SetActive(false);
        keyTextBackground.gameObject.SetActive(false);
        _key.SetActive(false);
        _riddles = GameObject.FindGameObjectsWithTag("Puzzle");
        _winningThreshold = GameObject.FindGameObjectsWithTag("Collectable").Length;
        _currentRiddle = _riddles[0].GetComponent<Collider>()
            .gameObject.GetComponent<KeyCards>()
            .whatIsMyNumber.ToString();
        for (int i = 1; i < _winningThreshold; i++) {
            _riddles[i].SetActive(false);
        }
    }

    void Update()
    {
        if (GameIsEnding)
        {
            float timeRatio = 1 - (m_TimeLoadEndGameScene - Time.time) / EndSceneLoadDelay;
            EndGameFadeCanvasGroup.alpha = timeRatio;


            // See if it's time to load the end scene (after the delay)
            if (Time.time >= m_TimeLoadEndGameScene)
            {
                SceneManager.LoadScene(m_SceneToLoad);
                GameIsEnding = false;
            }
        }
    }

    void LateUpdate() {
        if (_itemsCollected >= _winningThreshold && !_wonTheGame) {
            _key.SetActive(true);
            keyTextBackground.gameObject.SetActive(true);
            AudioSource.clip = KeyTextSfx;
            AudioSource.volume = 1f;
            AudioSource.Play();


            Debug.Log("You solved all the riddles and suddenly you hear a strange noise behind you. What is it?");
            //Debug.Log(string.Format("You took : {} seconds", Time.realtimeSinceStartup));
            _wonTheGame = true;
        }
    }

    void EndGame(bool win)
    {
        // unlocks the cursor before leaving the scene, to be able to click buttons
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Remember that we need to load the appropriate end scene after a delay
        GameIsEnding = true;
        EndGameFadeCanvasGroup.gameObject.SetActive(true);
        if (win)
        {
            m_SceneToLoad = WinSceneName;
            m_TimeLoadEndGameScene = Time.time + EndSceneLoadDelay + DelayBeforeFadeToBlack;

            // play a sound on win
            var audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = VictorySound;
            audioSource.playOnAwake = false;
            audioSource.PlayScheduled(AudioSettings.dspTime + DelayBeforeWinMessage);

            // create a game message
            //var message = Instantiate(WinGameMessagePrefab).GetComponent<DisplayMessage>();
            //if (message)
            //{
            //    message.delayBeforeShowing = delayBeforeWinMessage;
            //    message.GetComponent<Transform>().SetAsLastSibling();
            //}

        }
        else
        {
            m_SceneToLoad = LoseSceneName;
            m_TimeLoadEndGameScene = Time.time + EndSceneLoadDelay;
        }
    }

    public void HandleIntro() {
        if (!_sawIntro) {
            // Play intro stuff
            Destroy(IntroVideo.gameObject);
            Destroy(VideoPlayer.gameObject);
            _sawIntro = true;
            AudioSource.clip = StartingTextSfx;
            AudioSource.volume = 1f;
            AudioSource.Play();
        }
        else {
            startTextBackground.gameObject.SetActive(false);
            ProgressBar.gameObject.SetActive(true);
            if (AudioSource.clip == StartingTextSfx && AudioSource.isPlaying) {
                // If audio is playing, please make it stop
                AudioSource.Stop();
            }
        }
        if (_wonTheGame) {
            AudioSource.clip = KeyAppearsSfx;
            AudioSource.volume = 0.5f;
            AudioSource.Play();
            keyTextBackground.gameObject.SetActive(false);
        }
        else if (AudioSource.clip == KeyAppearsSfx && AudioSource.isPlaying) {
            AudioSource.Stop();
        }
    }

    public void HandleClickedPuzzle(ref GameObject puzzle) {
        if (puzzle.GetComponent<KeyCards>().whatIsMyNumber.ToString() == _currentRiddle) {

            // puzzle will be displayed
            // and also the input field
            Debug.Log("Please solve the " + puzzle.name);
            _currentRiddle = puzzle.GetComponent<KeyCards>().whatIsMyNumber
                .ToString();
            _seenRiddle = true;
            InputField.gameObject.SetActive(true);
            InputField.ActivateInputField();
            AudioSource.clip = DropSfx;
            AudioSource.volume = 0.4f;
            AudioSource.Play();
        }
        else
        {
            Debug.Log("Not possible yet. Please solve the riddle with " + _currentRiddle + " first. This is the riddle with " + puzzle.GetComponent<KeyCards>().whatIsMyNumber);
        }
    }

    public void HandleClickedCollectable(ref GameObject collectable) {
        if (collectable.GetComponent<KeyCards>().whatIsMyNumber.ToString() == _currentRiddle) {
            if (_seenRiddle == true) {
                Debug.Log("Collected " + collectable.name);
                Destroy(collectable);
                AudioSource.clip = CollectSfx;
                AudioSource.volume = 0.5f;
                AudioSource.Play();
                int currentState = (int) Char.GetNumericValue(_currentRiddle[2]);
                if (currentState < _winningThreshold) {
                    _riddles[currentState].SetActive(true);
                }
                currentState++;
                _currentRiddle = "nr" + currentState.ToString();
                _itemsCollected++;
                _seenRiddle = false;
                ProgressBar.current = _itemsCollected;
            }
            else
            {
                Debug.Log("Cannot be collected yet. Please look at the next riddle first.");
            }
        }
        else
        {
            Debug.Log("Cannot be collected yet. Please solve the riddle with " + _currentRiddle + " first.");
        }
    }

    public void HandleClickedKey() {
        AudioSource.clip = GameEndSfx;
        AudioSource.volume = 0.5f;
        AudioSource.Play();
        Debug.Log("hit key");
        _key.SetActive(false);
        endText.gameObject.SetActive(true);
        endTextBackground.gameObject.SetActive(true);

        AudioSource.clip = EndTextSfx;
        AudioSource.volume = 1f;
        AudioSource.Play();
    }


    public void HandleTextInput(String objectText) {
        print(InputField.text);
        if (InputField.text == null)
        {
            Debug.Log("No input text.");
        }
        else if (objectText == null)
        {
            Debug.Log("No object or no text of object.");
        }
        else if (objectText.ToUpper() == "SKIP" || objectText == InputField.text)
        {
            AudioSource.clip = CorrectSfx;
            AudioSource.volume = 0.4f;
            AudioSource.Play();
            InputField.DeactivateInputField(true);
            InputField.gameObject.SetActive(false);
        }
        else
        {
            AudioSource.clip = WrongSfx;
            AudioSource.volume = 0.8f;
            AudioSource.Play();
            Debug.Log("The answer is not correct. Try again!");
        }
    }
    private void OnApplicationQuit()
    {
        m_ShuttingDown = true;
    }


    private void OnDestroy()
    {
        m_ShuttingDown = true;
    }
}
