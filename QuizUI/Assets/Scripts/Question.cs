using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

using System.Linq;
public class Question
{
    public string QuestionText { get;}
    public Answer[] Answers;

    public Question(string question, string answer0, string answer1, string answer2, string answer3) {
        this.QuestionText = question;
        this.Answers = new Answer[4] {
            new Answer( answer0, true ),
            new Answer( answer1 ),
            new Answer( answer2 ),
            new Answer( answer3 ) };
    }

    public static Answer[] shuffleAnswers( Question q )
    {
        System.Random rnd = new System.Random();
        return q.Answers.OrderBy(x => rnd.Next()).ToArray();
    }
}
