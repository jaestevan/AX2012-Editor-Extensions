set EditorComponents="c:\Program Files (x86)\Microsoft Dynamics AX\60\Client\Bin\EditorComponents\"

copy ..\JAEEEditorExtensionSettings\bin\Debug\JAEE.AX.EditorExtensions.EditorSettings.dll .
copy ..\JAEEEditorExtensionSettingsForm\bin\Debug\JAEE.AX.EditorExtensions.EditorSettingsForm.exe .
copy ..\JAEEBraceMatchingExtension\bin\Debug\JAEE.AX.EditorExtensions.BraceMatching.dll .
copy ..\JAEEHighlightWordExtension\bin\Debug\JAEE.AX.EditorExtensions.HighlightWord.dll .
copy ..\JAEEOutliningExtension\bin\Debug\JAEE.AX.EditorExtensions.Outlining.dll .
copy ..\JAEECurrentLineHighlight\bin\Debug\JAEE.AX.EditorExtensions.CurrentLineHighlight.dll .

copy .\JAEE.AX.EditorExtensions.EditorSettings.dll %EditorComponents%
copy .\JAEE.AX.EditorExtensions.EditorSettingsForm.exe %EditorComponents%
copy .\JAEE.AX.EditorExtensions.BraceMatching.dll %EditorComponents%
copy .\JAEE.AX.EditorExtensions.HighlightWord.dll %EditorComponents%
copy .\JAEE.AX.EditorExtensions.Outlining.dll %EditorComponents%
copy .\JAEE.AX.EditorExtensions.CurrentLineHighlight.dll %EditorComponents%

pause
