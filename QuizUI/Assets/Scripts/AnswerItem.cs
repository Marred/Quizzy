﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnswerItem : MonoBehaviour
{
    public Answer answer;
    private Text answerTextItem;
    private Color defaultColor;

    void Start()
    {
        answerTextItem = this.transform.GetChild(0).GetComponent<Text>();
        defaultColor = GetComponent<Image>().color;
    }

    public void setAnswer( Answer answer )
    {
        this.answer = answer;
        this.answerTextItem.text = this.answer.getText();

        setItemColor(defaultColor);
    }

    public void showAsCorrect()
    {
        setItemColor(Color.green);
    }
    public void showAsIncorrect()
    {
        setItemColor(Color.red);
    }

    private void setItemColor( Color newColor )
    {
        GetComponent<Image>().color = newColor;
        this.answerTextItem.color = newColor;
    }
}
public class Answer {
    private bool isAnswerCorrect = false;
    private string text;

    public Answer( string answer )
    {
        this.text = answer;
    }
    public Answer( string answer, bool isCorrect) : this( answer )
    {
        this.isAnswerCorrect = isCorrect;
    }
    public bool isCorrect()
    {
        return this.isAnswerCorrect;
    }
    public void setText( string text )
    {
        this.text = text;
    }

    public string getText()
    {
        return this.text;
    }
   
}