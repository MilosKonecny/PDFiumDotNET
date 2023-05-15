dotnet clean --configuration Release

dotnet build --configuration Release

# Create and empty release folder
$release_folder=".\..\Release\"
$nuget_folder=".\..\Release\NuGet\"
if (!(Test-Path $release_folder))
{
	New-Item $release_folder -ItemType Directory
}
if (!(Test-Path $nuget_folder))
{
	New-Item $nuget_folder -ItemType Directory
}

Get-ChildItem -Path $release_folder -Include *.* -File -Recurse | foreach { $_.Delete()}

Copy-Item ".\Wrapper\Wrapper\bin\Release\*.nupkg" $nuget_folder
Copy-Item ".\Components\Components.Contracts\bin\Release\*.nupkg" $nuget_folder
Copy-Item ".\Components\Components\bin\Release\*.nupkg" $nuget_folder
Copy-Item ".\Components\Components.Factory\bin\Release\*.nupkg" $nuget_folder
Copy-Item ".\Controls\WpfControls\bin\Release\*.nupkg" $nuget_folder

pause
