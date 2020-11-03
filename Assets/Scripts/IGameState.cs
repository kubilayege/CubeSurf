using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameState 
{
    void FinishLine();
    void CubeDone();

    void Play();

}
