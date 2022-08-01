using UnityEditor;
using UnityEditor.Modules;
using UnityEditor.Scripting;
using UnityEditor.Scripting.Compilers;

internal class CustomCSharpLanguage : CSharpLanguage
{
#if UNITY4
	public override ScriptCompilerBase CreateCompiler(MonoIsland island, bool buildingForEditor, BuildTarget targetPlatform)
	{
		// This method almost exactly copies CSharpLanguage.CreateCompiler(...)

		if (!buildingForEditor && targetPlatform.ToString().Contains("MetroPlayer") &&
			(PlayerSettings.Metro.compilationOverrides == PlayerSettings.MetroCompilationOverrides.UseNetCore ||
			 (PlayerSettings.Metro.compilationOverrides == PlayerSettings.MetroCompilationOverrides.UseNetCorePartially
			  && !island._output.Contains("Assembly-CSharp-firstpass.dll"))))
		{
			return new MicrosoftCSharpCompiler(island);
		}
		return new CustomCSharpCompiler(island, false); // MonoCSharpCompiler is replaced with CustomCSharpCompiler
	}
#else
    public override ScriptCompilerBase CreateCompiler(MonoIsland island, bool buildingForEditor, BuildTarget targetPlatform, bool runUpdater) {
        switch (GetCSharpCompiler(targetPlatform, buildingForEditor, island._output)) {
            case CSharpCompiler.Microsoft:
                return new MicrosoftCSharpCompiler(island, runUpdater);
            case CSharpCompiler.Mono:
            default:
                return new CustomCSharpCompiler(island, runUpdater);
        }
    }
#endif
}
