using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;

public class GameController : MonoBehaviour
{
    enum GameModes { Simple, TimeFight, Perfect };

    [SerializeField]
    public GameObject QuestionText;
    [SerializeField]
    private Text QuestionNumText;
    [SerializeField]
    private Text PointsText;
    [SerializeField]
    public AnswerItem[] AnswerItems = new AnswerItem[4];

    [SerializeField] public GameObject QuestionsCanvas;
    [SerializeField] public GameObject TeacherCanvasImage;
    [SerializeField] public GameObject TeacherCanvasText;
    [SerializeField] public GameObject TeacherEndGameObject;
    [SerializeField] public Text TeacherEndGameText;

    [SerializeField]
    public GameObject EndGameScoreMessage;
    [SerializeField]
    private GameObject RemoveIncorrectButton;
    [SerializeField]
    private Slider TimeBar;

    private byte CorrectAnswerId;

    private SoundController soundSource;

    List<Question> QuestionList = new List<Question>();

    private int CurrentQuestionNumber = 0;
    private int Score = 0;
    private int TimeFightSeconds = 20;
    private float secondsCount;

    private GameModes ModeId = GameModes.Simple;

    private bool RemovedIncorrect;

    /*void Awake()
    {
        try {
            ModeId = (GameModes)GameObject.Find("GameDataObject").GetComponent<GameData>().ModeId;
        } catch {
            throw new Exception("You should start the game from the Menu scene!");
        }

        if (ModeId == GameModes.TimeFight)
        {
            TimeBar.gameObject.SetActive(true);
            secondsCount = TimeFightSeconds;
        }
        else
            TimeBar.gameObject.SetActive(false);
    }*/

    void Start()
    {

        soundSource = (GameObject.Find("SoundController")).GetComponent<SoundController>();

        QuestionList.Add(new Question("Premierem którego kraju był Winston Churchill?", "Wielkiej Brytanii",
            "Stanów Zjednoczonych", "Australii", "Rosji"));
        QuestionList.Add(new Question("W którym roku odbyła się bitwa pod Grunwaldem?", "1410", "996", "1944", "2137"));
        QuestionList.Add(new Question("Które miasto było pierwszą stolicą Polski?", "Gniezno", "Warszawa", "Kalety",
            "Bogdaniec"));
        QuestionList.Add(new Question("Na terenie którego kraju powstała pierwsza cywilizacja?", "Indie",
            "Stany Zjednoczone", "Egipt", "Rosja"));
        QuestionList.Add(new Question("Który z władców nie władał Rzymem?", "Aleksander Wielki", "Neron",
            "Juliusz Cezar", "Kaligula"));
        StartCoroutine(DisplayStartGameCanvas());
    }
    void Update()
    {
        if (ModeId == GameModes.TimeFight)
        {
            delayUntilEndGame();
        }
    }
    public void GetAnswer(AnswerItem clickedAnswer)
    {
        if (clickedAnswer.answer.isCorrect())
        {
            clickedAnswer.showAsCorrect();
            soundSource.playCorrectSound();
            Score++;

            PointsText.text = "Punktów: " + Score;
        }
        else
        {
            soundSource.playIncorrectSound();
            clickedAnswer.showAsIncorrect();
            AnswerItems[CorrectAnswerId].showAsCorrect();
            if (ModeId == GameModes.Perfect) StartCoroutine(DisplayEndGameCanvas());
        }

        StartCoroutine(delayAfterAnswer());
    }

    public void removeTwoIncorrectAnswers()
    {
        if (RemovedIncorrect) return;

        RemovedIncorrect = true;
        RemoveIncorrectButton.SetActive(false);

        System.Random rnd = new System.Random();
        AnswerItem[] items = AnswerItems.OrderBy(x => rnd.Next()).
            Where(item => item.answer.isCorrect() == false).
            Take(2).ToArray();

        foreach (AnswerItem item in items)
        {
            item.hideButton();
        }
    }
    IEnumerator delayAfterAnswer()
    {
        foreach (AnswerItem item in AnswerItems)
            item.disableButton();

        yield return new WaitForSeconds(1);
        if (CurrentQuestionNumber < QuestionList.Count)
        {
            NextQuestion(QuestionList[CurrentQuestionNumber++]);
            foreach (AnswerItem item in AnswerItems)
            {
                item.enableButton();
            }

        }
        else StartCoroutine(DisplayEndGameCanvas());
    }

    void NextQuestion(Question q)
    {
        RemovedIncorrect = false;
        RemoveIncorrectButton.SetActive(true);
        QuestionNumText.text = "Pytanie: " + CurrentQuestionNumber + "/" + QuestionList.Count;
        QuestionText.GetComponent<Text>().text = q.QuestionText;
        Answer[] shuffledAnswers = Question.shuffleAnswers(q);
        CorrectAnswerId = (byte)Array.IndexOf(shuffledAnswers, q.Answers[0]);

        for (int i = 0; i < 4; i++)
        {
            AnswerItems[i].showButton();
            AnswerItems[i].setAnswer(shuffledAnswers[i]);
        }
    }

    void delayUntilEndGame()
    {
        TimeBar.value = secondsCount / TimeFightSeconds;
        secondsCount -= Time.deltaTime;
        if (secondsCount <= 0)
        {
            StartCoroutine(DisplayEndGameCanvas());
        }
    }

    IEnumerator DisplayStartGameCanvas()
    {
        Transform teacherPosition = TeacherCanvasImage.GetComponent<Transform>();
        while (teacherPosition.localPosition.x > 650)
        {
            teacherPosition.localPosition += Vector3.left * Time.deltaTime / 0.001f;
            yield return null;
        }
        TeacherCanvasText.SetActive(true);
        yield return new WaitForSeconds(4);
        TeacherCanvasText.SetActive(false);
        while (teacherPosition.localPosition.x < 1200)
        {
            teacherPosition.localPosition += Vector3.right * Time.deltaTime / 0.001f;
            yield return null;
        }
        QuestionsCanvas.SetActive(true);
        NextQuestion(QuestionList[CurrentQuestionNumber++]);
    }

    IEnumerator DisplayEndGameCanvas()
    {
        QuestionsCanvas.SetActive(false);
        Transform teacherPosition = TeacherCanvasImage.GetComponent<Transform>();
        while (teacherPosition.localPosition.x > 650)
        {
            teacherPosition.localPosition += Vector3.left * Time.deltaTime / 0.001f;
            yield return null;
        }
        TeacherEndGameObject.SetActive(true);
        String EndGameMessage = "Gratulacje! Zdałeś! \n Twój wynik to " + Score + "/" + CurrentQuestionNumber;
        TeacherEndGameText.text = EndGameMessage;
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
