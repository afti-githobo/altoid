using UnityEngine;
using UnityEditor.AssetImporters;
using System.IO;


[ScriptedImporter(1, "bscript")]
public class BScriptImporter : ScriptedImporter
{
    public override void OnImportAsset(AssetImportContext ctx)
    {
        TextAsset subAsset = new TextAsset(File.ReadAllText(ctx.assetPath));
        ctx.AddObjectToAsset("text", subAsset);
        ctx.SetMainObject(subAsset);
    }
}
