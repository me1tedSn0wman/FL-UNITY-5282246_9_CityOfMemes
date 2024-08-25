using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class GameManager : Soliton<GameManager>
{
    public LangManager langManager;

    public override void Awake()
    {
        base.Awake();
        langManager = new LangManager();

    }
}
