$VSIX = "NuGet.Tools.vsix"
$ProjectDirectory="VSIXWrapper\"
$Sln = "NuGetVSIXWrapper.sln"
$7zipPath

7z x $VSIX -o"$ProjectDirectory" -aoa	
rm $ProjectDirectory\manifest.json
rm $ProjectDirectory\catalog.json
rm $ProjectDirectory\[Content_Types].xml