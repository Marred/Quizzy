using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    [SerializeField]
    public GameObject QuestionText;
    [SerializeField]
    public GameObject AnswerText0;
    [SerializeField]
    public GameObject AnswerText1;
    [SerializeField]
    public GameObject AnswerText2;
    [SerializeField]
    public GameObject AnswerText3;
    [SerializeField]
    public GameObject EndCanvas;
    [SerializeField]
    public GameObject EndMessage;
    [SerializeField]
    public GameObject ScoreMessage;

    List<Question> QuestionList = new List<Question>();

    private int CorrectAnswer = 0;
    private int QuestionNumber = 0;
    private int Score = 0;


    // Use this for initialization
    void Start()
    {
        QuestionList.Add(new Question("Premierem którego kraju był Winston Churchill?", "Wielkiej Brytanii",
            "Stanów Zjednoczonych", "Australii", "Rosji"));
        QuestionList.Add(new Question("W którym roku odbyła się bitwa pod Grunwaldem?", "1410", "996", "1944", "2160"));
        QuestionList.Add(new Question("Które miasto było pierwszą stolicą Polski?", "Gniezno", "Warszawa", "Kalety",
            "Bogdaniec"));
        QuestionList.Add(new Question("Na terenie którego kraju powstała pierwsza cywilizacja?", "Indie",
            "Stany Zjednoczone", "Egipt", "Rosja"));
        QuestionList.Add(new Question("Który z władców nie władał Rzymem?", "Aleksander Wielki", "Neron",
            "Juliusz Cezar", "Kaligula"));
        NextQuestion(QuestionList[QuestionNumber++]);
    }

    void Update()
    {
        
    }

    public void GetAnswer(int AnswerId)
    {
        if (AnswerId == CorrectAnswer)
        {
            Score++;
        }
        if(QuestionNumber<QuestionList.Count) NextQuestion(QuestionList[QuestionNumber++]);
        else EndGame();
    }

    void NextQuestion(Question q)
    {
        QuestionText.GetComponent<Text>().text = q.QuestionText;
        System.Random rnd = new System.Random();
        CorrectAnswer = rnd.Next(0, 4);
        switch (CorrectAnswer)
        {
            case 0:
                AnswerText0.GetComponent<Text>().text = q.Answers[0];
                AnswerText1.GetComponent<Text>().text = q.Answers[1];
                AnswerText2.GetComponent<Text>().text = q.Answers[2];
                AnswerText3.GetComponent<Text>().text = q.Answers[3];
                break;
            case 1:
                AnswerText1.GetComponent<Text>().text = q.Answers[0];
                AnswerText0.GetComponent<Text>().text = q.Answers[1];
                AnswerText2.GetComponent<Text>().text = q.Answers[2];
                AnswerText3.GetComponent<Text>().text = q.Answers[3];
                break;
            case 2:
                AnswerText2.GetComponent<Text>().text = q.Answers[0];
                AnswerText1.GetComponent<Text>().text = q.Answers[1];
                AnswerText0.GetComponent<Text>().text = q.Answers[2];
                AnswerText3.GetComponent<Text>().text = q.Answers[3];
                break;
            case 3:
                AnswerText3.GetComponent<Text>().text = q.Answers[0];
                AnswerText1.GetComponent<Text>().text = q.Answers[1];
                AnswerText2.GetComponent<Text>().text = q.Answers[2];
                AnswerText0.GetComponent<Text>().text = q.Answers[3];
                break;
        }

    }

    void EndGame()
    {
        EndCanvas.SetActive(true);
        if (((float) Score / QuestionList.Count) > 0.5)
        {
            EndMessage.GetComponent<Text>().text = "Gratulacje! Zdałeś!";
        }
        else
        {
            EndMessage.GetComponent<Text>().text = "Przykro mi! Nie zdałeś!";
        }
        ScoreMessage.GetComponent<Text>().text = "Twój wynik to "+ Score +"/" + QuestionList.Count;
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
