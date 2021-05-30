using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Record
{
    public string name;
    public int id;
    public int score;

    public Record(string name, int id, int score)
    {
        this.name = name;
        this.id = id;
        this.score = score;
    }
}
