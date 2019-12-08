using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace AsmdefGraph.Editor {
    public class AsmdefFile {
        public string FilePath { set; get; }
        public string Guid { set; get; }
        public Asmdef Content { set; get; }
        public AsmdefFile() {
            FilePath = "";
            Guid = "";
            Content = new Asmdef();
        }
        public bool LoadFromPath(string fullPath, string assetPath) {
            FilePath = fullPath;
            Guid = AssetDatabase.AssetPathToGUID(assetPath);
            var json = File.ReadAllText(fullPath);
            if (string.IsNullOrEmpty(json)) {
                return false;
            }
            Content = JsonUtility.FromJson<Asmdef>(json);
            return true;
        }
        public IEnumerable<string> Guids =>
            Content.references.Where(x => !string.IsNullOrEmpty(x)).Select(x => x.Replace("GUID:", ""));
    }
}
