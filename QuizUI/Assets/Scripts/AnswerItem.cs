using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnswerItem : MonoBehaviour
{
    public Answer answer;
    private Text answerTextItem;
    private Color defaultColor;

    void Awake()
    {
        answerTextItem = this.gameObject.transform.GetComponentInChildren<Text>();
        defaultColor = GetComponent<Image>().color;
    }

    public void setAnswer( Answer answer )
    {
        this.answer = answer;
<<<<<<< HEAD
        this.answerTextItem.text = this.answer.getText();
=======
        answerTextItem.text = this.answer.getText();

>>>>>>> cb2ad886c38dbf16aee76c03bb6470589a59f2d7
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

    public void disableButton()
    {
        this.GetComponent<Button>().interactable = false;
    }

    public void enableButton()
    {
        this.GetComponent<Button>().interactable = true;
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
