using DungeonCrawl.Actors.Characters;
using DungeonCrawl.Actors.Items;
using DungeonCrawl.Actors.Static;
using DungeonCrawl.Save;
using System;
using System.Text.RegularExpressions;
using UnityEngine;

namespace DungeonCrawl.Core
{
    /// <summary>
    ///     MapLoader is used for constructing maps from txt files
    /// </summary>
    public static class MapLoader
    {
        /// <summary>
        ///     Constructs map from txt file and spawns actors at appropriate positions
        /// </summary>
        /// <param name="id"></param>
        public static void LoadMap(int id, Player player = null)
        {
            var lines = Regex.Split(Resources.Load<TextAsset>($"map_{id}").text, "\r\n|\r|\n");

            // Read map size from the first line
            var split = lines[0].Split(' ');
            var width = int.Parse(split[0]);
            var height = int.Parse(split[1]);
            
            // Create actors
            for (var y = 0; y < height; y++)
            {
                var line = lines[y+1];
                for (var x = 0; x < width; x++)
                {
                    var character = line[x];

                    if (player != null)
                    {
                        if (character == 'p')
                        {
                            SpawnPlayer(character, (x, -y), player);
                        }
                        else
                        {
                            SpawnActor(character, (x, -y));
                        }
                    }
                    else
                    {
                        SpawnActor(character, (x, -y));

                    }
                }
            }

            // Set default camera size and position
            CameraController.Singleton.Size = 7;
            CameraController.Singleton.Position = (width / 2, -height / 2);
        }

        private static void SpawnPlayer(char c, (int x, int y) position, Player player = null)
        {
            ActorManager.Singleton.SpawnPlayer(position, player);
            ActorManager.Singleton.Spawn<Floor>(position);
        }

        private static void SpawnActor(char c, (int x, int y) position)
        {
            switch (c)
            {
                case '#':
                    ActorManager.Singleton.Spawn<Wall>(position);
                    break;
                case '.':
                    ActorManager.Singleton.Spawn<Floor>(position);
                    break;
                case 'p':
                    ActorManager.Singleton.Spawn<Player>(position);
                    ActorManager.Singleton.Spawn<Floor>(position);
                    break;
                case 's':
                    ActorManager.Singleton.Spawn<Skeleton>(position);
                    ActorManager.Singleton.Spawn<Floor>(position);
                    break;
                case 'o':
                    ActorManager.Singleton.Spawn<Orc>(position);
                    ActorManager.Singleton.Spawn<Floor>(position);
                    break;
                case 'g':
                    ActorManager.Singleton.Spawn<Ghost>(position);
                    ActorManager.Singleton.Spawn<Floor>(position);
                    break;
                case 'i':
                      ActorManager.Singleton.Spawn<Sword>(position);
                    ActorManager.Singleton.Spawn<Floor>(position);
                    break;
                case 'a':
                    ActorManager.Singleton.Spawn<Axe>(position);
                    ActorManager.Singleton.Spawn<Floor>(position);
                    break;
                case 'k':
                     ActorManager.Singleton.Spawn<Key>(position);
                    ActorManager.Singleton.Spawn<Floor>(position);
                    break;
                case 'd':
                    ActorManager.Singleton.Spawn<Portal>(position);
                    break;
                case '*':
                    ActorManager.Singleton.Spawn<Skull>(position);
                    ActorManager.Singleton.Spawn<Floor>(position);
                    break;
                case 'h':
                    ActorManager.Singleton.Spawn<HealthPotion>(position);
                    ActorManager.Singleton.Spawn<Floor>(position);
                    break;
                case ' ':
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public static void LoadGameState()
        {
            var gameState = Deserialize.DeserializeGameState();
            int mapLevel = gameState.player.CurrentLevel;
            ActorManager.Singleton.DestroyAllActors();
            LoadMap(mapLevel);
            ActorManager.Singleton.DestroyItemAndCharacters();
            ActorManager.Singleton.SpawnPlayerFromLoadedGame(gameState.player);
        }
    }
}
