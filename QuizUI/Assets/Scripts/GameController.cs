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
    public GameObject[] AnswerItems = new GameObject[4];
    [SerializeField]
    public GameObject EndGameCanvas;
    [SerializeField]
    public GameObject EndGameMessage;
    [SerializeField]
    public GameObject EndGameScoreMessage;

    List<Question> QuestionList = new List<Question>();

    private int CorrectAnswerId = 0;
    private int CurrentQuestionNumber = 0;
    private int Score = 0;

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
        NextQuestion(QuestionList[CurrentQuestionNumber++]);
    }

    public void GetAnswer( GameObject clickedItem )
    {
        AnswerItem clickedAnswer = clickedItem.GetComponent<AnswerItem>();
        if (clickedAnswer.answer.isCorrect())
        {
            clickedAnswer.showAsCorrect();
            Score++;
        }
        else
            clickedAnswer.showAsIncorrect();

        StartCoroutine( delayAfterAnswer() );

        
    }

    IEnumerator delayAfterAnswer()
    {
        yield return new WaitForSeconds(1);
        if (CurrentQuestionNumber < QuestionList.Count) NextQuestion(QuestionList[CurrentQuestionNumber++]);
        else DisplayEndGameCanvas();
    }

    void NextQuestion(Question q)
    {
        QuestionText.GetComponent<Text>().text = q.QuestionText;
        Answer[] shuffledAnswers = Question.shuffleAnswers(q);
        for( int i = 0; i < 4; i++)
        {
            AnswerItems[i].GetComponent<AnswerItem>().setAnswer(shuffledAnswers[i]);
        }
    }

    void DisplayEndGameCanvas()
    {
        EndGameCanvas.SetActive(true);
        if (((float) Score / QuestionList.Count) > 0.5)
        {
            EndGameMessage.GetComponent<Text>().text = "Gratulacje! Zdałeś!";
        }
        else
        {
            EndGameMessage.GetComponent<Text>().text = "Przykro mi! Nie zdałeś!";
        }
        EndGameScoreMessage.GetComponent<Text>().text = "Twój wynik to "+ Score +"/" + QuestionList.Count;
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
