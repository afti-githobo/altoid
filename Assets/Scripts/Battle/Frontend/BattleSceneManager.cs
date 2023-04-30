using Altoid.Battle.Types.Battle;
using Altoid.Battle.Types.Environment;
using Altoid.Persistence;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Altoid.Battle.Frontend
{
    public class BattleSceneManager : MonoBehaviour
    {
        public static BattleSceneManager Instance { get; private set; }

        private BattleDef battle;
        private const int BATTLE_SCENE_INDEX = 2;

        private LinkedList<Scene> scenesLoadedBeforeBattleBegan;
        private Scene mainSceneBeforeBattleBegan;

        private void Awake()
        {
            Instance = this;
        }

        public static IEnumerable StartBattle (BattleDef battle)
        {
            var loadedScenes = new LinkedList<Scene>();
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                var scene = SceneManager.GetSceneAt(i);
                var objects = scene.GetRootGameObjects();
                for (int i2 = 0; i2 < objects.Length; i2++)
                {
                    var component = objects[i2].GetComponent<StoreStateBeforeBattleComponent>();
                    if (component != null) component.ExportState();
                }
                loadedScenes.AddLast(scene);
            }
            var mainScene = SceneManager.GetActiveScene();
            yield return SceneManager.LoadSceneAsync(BATTLE_SCENE_INDEX, LoadSceneMode.Single);
            yield return SceneManager.LoadSceneAsync(battle.InitialBattleScene.SceneIndex, LoadSceneMode.Additive);
            var individualBattleScene = SceneManager.GetSceneByBuildIndex(battle.InitialBattleScene.SceneIndex);
            SceneManager.SetActiveScene(individualBattleScene);
            var runner = new BattleRunner(battle);
            Instance.battle = battle;
            Instance.scenesLoadedBeforeBattleBegan = loadedScenes;
            Instance.mainSceneBeforeBattleBegan = mainScene;
            for (int i = 0; i < runner.Battlers.Count; i++) yield return BattleFrontendMain.Instance.AddPuppetForBattler(runner.Battlers[i]);
        }

        public static IEnumerable ReturnFromBattle()
        {
            var manager = FindObjectOfType<BattleSceneManager>();
            var mainScene = manager.mainSceneBeforeBattleBegan;
            var scenes = manager.scenesLoadedBeforeBattleBegan;
            yield return SceneManager.UnloadSceneAsync(manager.battle.InitialBattleScene.SceneIndex);
            yield return SceneManager.UnloadSceneAsync(BATTLE_SCENE_INDEX);
            foreach (var scene in scenes) yield return SceneManager.LoadSceneAsync(scene.buildIndex);
            SceneManager.SetActiveScene(mainScene);
        }
    }
}