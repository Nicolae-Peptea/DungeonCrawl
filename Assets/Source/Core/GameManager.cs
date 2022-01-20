using UnityEngine;
using UnityEngine.SceneManagement;

namespace DungeonCrawl.Core
{
    /// <summary>
    ///     Loads the initial map and can be used for keeping some important game variables
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        public bool isNewGame { get; set; } = true;
        public void Start()
        {
            if (isNewGame)
            {
                MapLoader.LoadMap(1);
            }
            else
            {
                MapLoader.LoadMap(1);
                MapLoader.LoadGameState();
            }
        }

        public void NewGame()
        {
            SceneManager.LoadScene("Game");
            isNewGame = true;
        }

        public void LoadGame()
        {
            SceneManager.LoadScene("Game");
            isNewGame = false;
        }
    }
}
