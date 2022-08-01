using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEditor.Scripting;
using UnityEditor.Scripting.Compilers;

[InitializeOnLoad]
public static class CSharp60SupportActivator {
    static CSharp60SupportActivator() {
#if UNITY2017
        _SetupLanguageUnity2017();
#else
        _SetupLanguageUnity45();
#endif
    }

    private static void _SetupLanguageUnity45() {
        var customLanguage = new CustomCSharpLanguage();

        var list = GetSupportedLanguages();
        list.RemoveAll(language => language is CSharpLanguage);
        list.Add(customLanguage);

        // Replace CSharpSupportedLanguage
        var fieldInfo = typeof(ScriptCompilers).GetField("CSharpSupportedLanguage", BindingFlags.NonPublic | BindingFlags.Static);
        fieldInfo.SetValue(null, customLanguage);
    }

    private static void _SetupLanguageUnity2017() {
        var customLanguage = new CustomCSharpLanguage();

        var list = GetSupportedLanguages();
        list.RemoveAll(language => language is CSharpLanguage);
        list.Add(customLanguage);

        // Replace CSharpSupportedLanguage
        var fieldInfo = typeof(ScriptCompilers).GetField("CSharpSupportedLanguage", BindingFlags.NonPublic | BindingFlags.Static);
        fieldInfo.SetValue(null, customLanguage);

        // Refresh all predefined assemblies
        var field1 = typeof(UnityEditor.Scripting.ScriptCompilation.EditorBuildRules).GetField("predefinedTargetAssemblies", BindingFlags.NonPublic | BindingFlags.Static);
        var array = field1.GetValue(null) as UnityEditor.Scripting.ScriptCompilation.EditorBuildRules.TargetAssembly[];
        if (array != null) {
            var assemblies = UnityEditor.Scripting.ScriptCompilation.EditorBuildRules.CreatePredefinedTargetAssemblies();
            for (int i = 0, count = System.Math.Min(assemblies.Length, array.Length); i < count; i++) {
                array[i] = assemblies[i];
            }
        }

        // Listen on "build finish"
        UnityEditor.Scripting.ScriptCompilation.EditorCompilationInterface.Instance.assemblyCompilationFinished += (_1, _2) => {
            UnityEngine.Debug.Log(_1 + " build finish.");
        };
    }

    private static List<SupportedLanguage> GetSupportedLanguages() {
        var fieldInfo = typeof(ScriptCompilers).GetField("SupportedLanguages", BindingFlags.NonPublic | BindingFlags.Static);
        var languages = (List<SupportedLanguage>)fieldInfo.GetValue(null);
        return languages;
    }
}
