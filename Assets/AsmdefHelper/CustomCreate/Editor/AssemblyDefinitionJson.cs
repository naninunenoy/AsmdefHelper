namespace AsmdefHelper.CustomCreate.Editor {
    public class AssemblyDefinitionJson {
        public string name = string.Empty;
#if UNITY_2020_2_OR_NEWER
        public string rootNamespace = string.Empty;
#endif
        public string[] references = new string[0];
        public string[] includePlatforms = new string[0];
        public string[] excludePlatforms = new string[0];
        public bool allowUnsafeCode = false;
        public bool overrideReferences = false;
        public string[] precompiledReferences = new string[0];
        public bool autoReferenced = false;
        public string[] defineConstraints = new string[0];
        public string[] versionDefines = new string[0];
        public bool noEngineReferences = false;
    }
}
