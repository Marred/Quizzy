﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField]
    public GameObject QuestionText;
    [SerializeField]
    public AnswerItem[] AnswerItems = new AnswerItem[4];
    [SerializeField]
    public GameObject EndGameCanvas;
    [SerializeField]
    public GameObject EndGameMessage;
    [SerializeField]
    public GameObject EndGameScoreMessage;

    private SoundController soundSource;

    List<Question> QuestionList = new List<Question>();

    private int CurrentQuestionNumber = 0;
    private int Score = 0;

    private int ModeId = 0;

    void Awake()
    {
        ModeId = GameObject.Find("GameDataObject").GetComponent<GameData>().ModeId;

        if (ModeId == 1)
        {
            StartCoroutine(delayUntilEndGame());
        }
    }

    void Start()
    {

        soundSource = (GameObject.Find("SoundController")).GetComponent<SoundController>();

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

    public void GetAnswer( AnswerItem clickedAnswer )
    {
        if (clickedAnswer.answer.isCorrect())
        {
            clickedAnswer.showAsCorrect();
            soundSource.playCorrectSound();
            Score++;
        }
        else
        {
            soundSource.playIncorrectSound();
            clickedAnswer.showAsIncorrect();
            if(ModeId == 2) DisplayEndGameCanvas("Błędna odpowiedź!", "Twój wynik to " + Score + "/" + CurrentQuestionNumber);
        }

        StartCoroutine( delayAfterAnswer() );
    }

    IEnumerator delayAfterAnswer()
    {
        foreach (AnswerItem item in AnswerItems)
            item.disableButton();

        yield return new WaitForSeconds(1);
        if (CurrentQuestionNumber < QuestionList.Count)
        {
            NextQuestion(QuestionList[CurrentQuestionNumber++]);
            foreach (AnswerItem item in AnswerItems) {
                item.enableButton();
            }
                
        }
        else DisplayEndGameCanvas("Koniec pytań!", "Twój wynik to " + Score + "/" + CurrentQuestionNumber);
    }

    void NextQuestion(Question q)
    {
        QuestionText.GetComponent<Text>().text = q.QuestionText;
        Answer[] shuffledAnswers = Question.shuffleAnswers(q);
        for( int i = 0; i < 4; i++)
        {
            AnswerItems[i].setAnswer(shuffledAnswers[i]);
        }
    }

    IEnumerator delayUntilEndGame()
    {
        yield return new WaitForSeconds(20);
        DisplayEndGameCanvas("Koniec czasu!", "Twój wynik to " + Score + "/" + CurrentQuestionNumber);
    }

    void DisplayEndGameCanvas(string MainMessage, string ScoreMessage)
    {
        EndGameCanvas.SetActive(true);
        EndGameMessage.GetComponent<Text>().text = MainMessage;
        EndGameScoreMessage.GetComponent<Text>().text = ScoreMessage;
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
