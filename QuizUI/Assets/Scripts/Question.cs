using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Question
{
    public string QuestionText { get;}
    public string[] Answers = new string[4];

    public Question(string question, string answer0, string answer1, string answer2, string answer3) {
        this.QuestionText = question;
        this.Answers[0] = answer0;
        this.Answers[1] = answer1;
        this.Answers[2] = answer2;
        this.Answers[3] = answer3;
    }
}
