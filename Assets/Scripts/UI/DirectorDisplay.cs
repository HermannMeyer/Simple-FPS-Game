using UnityEngine;
using UnityEngine.UI;

public class DirectorDisplay : MonoBehaviour
{
    public Text directorText;
    public GameObject director;

    GameDirector gameDirector;

    // Start is called before the first frame update
    void Awake()
    {
        gameDirector = director.GetComponent<GameDirector>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
