using UnityEngine;
using UnityEngine.UI;
using Microsoft.CognitiveServices.Speech;
#if PLATFORM_ANDROID
using UnityEngine.Android;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
#endif
#if PLATFORM_IOS
using UnityEngine.iOS;
using System.Collections;
#endif

public class HelloWorld : MonoBehaviour
{
    // Hook up the two properties below with a Text and Button object in your UI.
    public Text outputText;
    public Button startRecoButton;
    public Sprite[] Sprites;
    public Image[] images;
    public string[] texts;
    public string[] keys;

    public SpeechRecognizer[] recognizers;
    public Dictionary<string, int> dict;
    public SortedSet<string> usedSet;
    public GameObject emodziCreateObject;

    private object threadLocker = new object();
    private bool waitingForReco;
    private string message;
    private string buf;
    
    private bool micPermissionGranted = false;

#if PLATFORM_ANDROID || PLATFORM_IOS
    // Required to manifest microphone permission, cf.
    // https://docs.unity3d.com/Manual/android-manifest.html
    private Microphone mic;
#endif

    public void Recognized(object s,SpeechRecognitionEventArgs e)
    {
        var rec = s as SpeechRecognizer;
        var result = e.Result;
        // Checks result.
        string newMessage = outputText.text;
        if (result.Reason == ResultReason.RecognizedSpeech)
        {
            buf += result.Text;
        }
        else if (result.Reason == ResultReason.NoMatch)
        {

        }
        else if (result.Reason == ResultReason.Canceled)
        {
            var cancellation = CancellationDetails.FromResult(result);
            newMessage = $"CANCELED: Reason={cancellation.Reason} ErrorDetails={cancellation.ErrorDetails}";
        }

        lock (threadLocker)
        {
            waitingForReco = false;
        }
    }

    void CreateE(int id)
    {
        emodziCreateObject.GetComponent<CreateEmodziScript>().CreateEmodzi(Sprites[id],texts[id],id < 16);
    }

    IEnumerator StopRecognizer(SpeechRecognizer rec)
    {
        yield return new WaitForSeconds(1.5f);
        rec.StopContinuousRecognitionAsync().ConfigureAwait(false);
    }

    IEnumerator myCoroutine(SpeechRecognizer[] recognizers)
    {
        foreach(var rec in recognizers)
        {
            rec.StartContinuousRecognitionAsync().ConfigureAwait(false);
            StartCoroutine(StopRecognizer(rec));
            yield return new WaitForSeconds(1);
        }
        StartCoroutine(myCoroutine(recognizers));
    }

    public async void ButtonClick()
    {
        // Creates an instance of a speech config with specified subscription key and service region.
        // Replace with your own subscription key and service region (e.g., "westus").
        var config = SpeechConfig.FromSubscription("462dd0b2f7e94ff5a8ffb57537bcb9de", "westeurope");
        config.SpeechRecognitionLanguage = "ru-RU";

        // Make sure to dispose the recognizer after use!
        recognizers = new SpeechRecognizer[4];
        for (int i = 0; i < 4; i++)
            recognizers[i] = new SpeechRecognizer(config);

        lock (threadLocker)
        {
            waitingForReco = true;
        }
        // Starts speech recognition, and returns after a single utterance is recognized. The end of a
        // single utterance is determined by listening for silence at the end or until a maximum of 15
        // seconds of audio is processed.  The task returns the recognition text as result.
        // Note: Since RecognizeOnceAsync() returns only a single utterance, it is suitable only for single
        // shot recognition like command or query.
        // For long-running multi-utterance recognition, use StartContinuousRecognitionAsync() instead.
        for (int i = 0; i < 4; i++)
        {
            recognizers[i].Recognized += new EventHandler<SpeechRecognitionEventArgs>(Recognized);
            await recognizers[i].StartContinuousRecognitionAsync();
        }
        StartCoroutine(myCoroutine(recognizers));
    }

    void Start()
    {
        buf = "";
        message = "";
        dict = new Dictionary<string, int>();
        usedSet = new SortedSet<string>();
        int i = 0;

        foreach (var line in keys)
        {
            foreach (var word in line.Split(','))
            {
                dict.Add(word, i);
            }
            i++;
        }

        if (outputText == null)
        {
            UnityEngine.Debug.LogError("outputText property is null! Assign a UI Text element to it.");
        }
        else if (startRecoButton == null)
        {
            message = "startRecoButton property is null! Assign a UI Button to it.";
            UnityEngine.Debug.LogError(message);
        }
        else
        {
            // Continue with normal initialization, Text and Button objects are present.
#if PLATFORM_ANDROID
            // Request to use the microphone, cf.
            // https://docs.unity3d.com/Manual/android-RequestingPermissions.html
            message = "Waiting for mic permission";
            if (!Permission.HasUserAuthorizedPermission(Permission.Microphone))
            {
                Permission.RequestUserPermission(Permission.Microphone);
            }
#elif PLATFORM_IOS
            if (!Application.HasUserAuthorization(UserAuthorization.Microphone))
            {
                Application.RequestUserAuthorization(UserAuthorization.Microphone);
            }
#else
            micPermissionGranted = true;
            message = "Click button to recognize speech";
#endif
            startRecoButton.onClick.AddListener(ButtonClick);
        }
    }

    void Update()
    {
#if PLATFORM_ANDROID
        if (!micPermissionGranted && Permission.HasUserAuthorizedPermission(Permission.Microphone))
        {
            micPermissionGranted = true;
            message = "";
        }
#elif PLATFORM_IOS
        if (!micPermissionGranted && Application.HasUserAuthorization(UserAuthorization.Microphone))
        {
            micPermissionGranted = true;
            message = "Click button to recognize speech";
        }
#endif

        lock (threadLocker)
        {
            List<int> list = new List<int>();
            if (startRecoButton != null)
            {
                startRecoButton.interactable = !waitingForReco && micPermissionGranted;
            }
            if (outputText != null && buf != "")
            {
                var words = buf.Replace(".", "").Replace(",","").ToLower().Split();
                buf = "";
                foreach(var word in words)
                {
                    if (dict.ContainsKey(word) && !usedSet.Contains(word))
                    {
                        Debug.Log(word);
                        message += word + " ";
                        usedSet.Add(word);
                        list.Add(dict[word]);
                    }
                }
                outputText.text = message;
                foreach (var elem in list)
                    CreateE(elem);
            }
        }
    }
}