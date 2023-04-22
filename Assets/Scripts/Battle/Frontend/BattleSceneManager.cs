using Altoid.Battle.Types;
using Altoid.Battle.Logic;
using Altoid.Persistence;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Altoid.Util;
using Altoid.Battle.Datastores;

namespace Altoid.Battle.Frontend
{
    public class BattleSceneManager : MonoBehaviour
    {
        private List<BattlerPuppet> puppets = new();
        private const int BATTLE_SCENE_INDEX = 2;

        private LinkedList<Scene> scenesLoadedBeforeBattleBegan;

        public IEnumerable AddPuppetForBattler(Battler b)
        {
            var prefabTable = Datastore.Query<BattlerPuppetPrefabTable>();
            var row = prefabTable.Data[b.BattlerDef.AssetType];
            if (row == null) throw new Exception($"No prefab table entry for asset type {b.BattlerDef.AssetType}");
            var request = Resources.LoadAsync(row.PrefabAssetPath);
            yield return request;
            var puppet = Instantiate(request.asset as GameObject);
            puppet.transform.parent = transform;
            puppet.name = $"[PUPPET] {b.BattlerDef.name}";
            puppets.Add(puppet.GetComponent<BattlerPuppet>());
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
            yield return SceneManager.LoadSceneAsync(BATTLE_SCENE_INDEX, LoadSceneMode.Single);
            yield return SceneManager.LoadSceneAsync(battle.InitialBattleScene.SceneIndex, LoadSceneMode.Additive);
            var individualBattleScene = SceneManager.GetSceneByBuildIndex(battle.InitialBattleScene.SceneIndex);
            // load battle+scene scripts...
            SceneManager.SetActiveScene(individualBattleScene);
            var runner = new BattleRunner(battle);
            var manager = FindObjectOfType<BattleSceneManager>();
            manager.scenesLoadedBeforeBattleBegan = loadedScenes;
            for (int i = 0; i < runner.Battlers.Count; i++)
            {
                yield return manager.AddPuppetForBattler(runner.Battlers[i]);
            }
        }

        public static IEnumerable ReturnFromBattle()
        {
            throw new NotImplementedException();
        }
    }
}