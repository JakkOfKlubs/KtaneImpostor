﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using KModkit;
using Rnd = UnityEngine.Random;

public class FakeChess : ImpostorMod 
{
    public override string ModAbbreviation { get { return "Ch"; } }
    public TextMesh dispLetter, dispNumber;
    public TextMesh[] letters, numbers;
    public MeshRenderer[] leds;

    private int Case;

    void Start()
    {
        leds[0].material.color = Color.green;
        Case = Rnd.Range(0, 4);
        dispLetter.text = ((char)(Rnd.Range(0, 6) + 'a')).ToString();
        dispNumber.text = Rnd.Range(1, 7).ToString();
        switch (Case)
        {
            case 0:
                flickerObjs.Add(dispLetter.gameObject);
                dispLetter.text = ((char)(Rnd.Range(7, 26) + 'a')).ToString();
                LogQuirk("the displayed coordinate is {0}", dispLetter.text + dispNumber.text);
                break;
            case 1:
                flickerObjs.Add(dispNumber.gameObject);
                dispNumber.text = (Rnd.Range(7, 11) % 10).ToString();
                LogQuirk("the displayed coordinate is {0}", dispLetter.text + dispNumber.text);
                break;
            case 2:
                for (int i = 0; i < 6; i++)
                {
                    flickerObjs.Add(letters[i].gameObject);
                    flickerObjs.Add(numbers[i].gameObject);
                    letters[i].text = (i + 1).ToString();
                    numbers[i].text = ((char)(i + 'a')).ToString();
                }
                LogQuirk("the number and letter buttons are swapped");
                break;
            case 3:
                for (int i = 0; i < 6; i++)
                {
                    flickerObjs.Add(leds[i].gameObject);
                    leds[i].material.color = Color.black;
                }
                LogQuirk("the LEDs are all unlit");
                break;
        }
    }
}
