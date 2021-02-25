# Apply refactor name with VSCode, Visual Studio or Jetbrains Rider and use this script for rename de folders and files
$NewName = "New.Solution.Name"

Get-ChildItem -Filter “*NetCoreApiScaffolding*” -Recurse | Rename-Item -NewName {$_.name -replace ‘NetCoreApiScaffolding’, $NewName } -Force