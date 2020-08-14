using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Button start;
    public InputField X;
    public InputField Y;
    public InputField WL;
    public InputField EC;
    public InputField Iterations;

    void Start()
    {
        start.onClick.AddListener(GenerateTile);
    }

    void GenerateTile()
    {
        GetComponent<Generator>().CrateBoard(int.Parse(X.text), int.Parse(Y.text), int.Parse(Iterations.text), int.Parse(WL.text), int.Parse(EC.text));
    }
}
