﻿using System;
using System.Linq;
using UnityEngine;
using Rnd = UnityEngine.Random;

public class FakeScrew : ImpostorMod
{
    public override string ModAbbreviation { get { return "Scw"; } }
    public MeshRenderer[] screwHoles;
    public Texture[] screwTextures;
    public Texture cyanTexture;
    public TextMesh[] buttonTexts;
    public TextMesh screenText;

    private int Case;

    void Start()
    {
        Case = Rnd.Range(0, 5);
        var shuff = Enumerable.Range(0, 6).ToArray().Shuffle();
        for (int i = 1; i < 6; i++)
            screwHoles[i].material.mainTexture = screwTextures[shuff[i]];
        var btnLetters = Enumerable.Range(0, 4).ToArray().Shuffle();
        for (int i = 0; i < buttonTexts.Length; i++)
            buttonTexts[i].text = "ABCD"[btnLetters[i]].ToString();
        screenText.text = "1";
        switch (Case)
        {
            case 0:
                int rnd1, rnd2;
                do
                {
                    rnd1 = Rnd.Range(1, 6);
                    rnd2 = Rnd.Range(1, 6);
                } while (rnd1 == rnd2);
                var dupeShuff = Enumerable.Range(0, 6).ToArray().Shuffle();
                dupeShuff[rnd1] = dupeShuff[rnd2];
                flickerObjs.Add(screwHoles[rnd1].gameObject);
                flickerObjs.Add(screwHoles[rnd2].gameObject);
                for (int i = 0; i < screwHoles.Length; i++)
                    screwHoles[i].material.mainTexture = screwTextures[dupeShuff[i]];
                LogQuirk("there is a duplicate colored hole, at holes {0} and {1}", rnd1 + 1, rnd2 + 1);
                break;
            case 1:
                var numShuff = Enumerable.Range(0, 4).ToArray().Shuffle();
                for (int i = 0; i < buttonTexts.Length; i++)
                {
                    buttonTexts[i].text = (numShuff[i] + 1).ToString();
                    flickerObjs.Add(buttonTexts[i].gameObject);
                }
                LogQuirk("the buttons have numbers instead of letters");
                break;
            case 2:
                var newLetters = Enumerable.Range(0, 4).ToArray();
                int btnRand, letterRand;
                do
                {
                    btnRand = Rnd.Range(0, 4);
                    letterRand = Rnd.Range(4, 26);
                } while (btnRand == letterRand || letterRand == 8); // 'I' may look too much like '1'
                newLetters[btnRand] = letterRand;
                newLetters.Shuffle();
                flickerObjs.Add(buttonTexts[Array.IndexOf(newLetters, letterRand)].gameObject);
                for (int i = 0; i < 4; i++)
                    buttonTexts[i].text = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"[newLetters[i]].ToString();
                LogQuirk("one of the buttons has the letter {0}", "ABCDEFGHIJKLMNOPQRSTUVWXYZ"[letterRand]);
                break;
            case 3:
                var cyanHole = Rnd.Range(1, 6);
                screwHoles[cyanHole].material.mainTexture = cyanTexture;
                flickerObjs.Add(screwHoles[cyanHole].gameObject);
                LogQuirk("there is a cyan colored hole");
                break;
            case 4:
                screenText.text = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".PickRandom().ToString();
                flickerObjs.Add(screenText.gameObject);
                LogQuirk("the screen has a letter instead of a number");
                break;
        }
    }
}
