function WriteHeader($text){
	Write-Output "------ $text ------"
}

function CreateFolder($folder){
	if (!(Test-Path $folder))
	{
		New-Item $folder -ItemType Directory
	}
}

function CopyFile(){
param(
[Parameter (Mandatory = $true)] $source_folder,
[Parameter (Mandatory = $true)] $target_folder,
[Parameter (Mandatory = $true)] $file
)
	if (Test-Path $source_folder){
		$source_file = $source_folder + $file
		if (Test-Path $source_file){
			Copy-Item $source_file $target_folder
		} else {
			Write-Output "The file was not copied because it does not exist: $source_file"
		}
	}
}

# Define folders and subfolders to work with
$release_folder=".\..\Release\"
$nuget_folder=".\..\Release\NuGet\"
$binaries_folder=".\..\Release\Binaries\"
$all_folders=($release_folder,$nuget_folder,$binaries_folder)
$net_subfolders=('net5.0-windows','net6.0-windows','net7.0-windows','net48','netcoreapp3.1')

# Create base folders
foreach($one_folder in $all_folders)
{
	CreateFolder $one_folder
}

# Create net subfolders
foreach($one_subfolder in $net_subfolders)
{
	$one_folder = $binaries_folder + $one_subfolder + '\'
	CreateFolder $one_folder
}

# Remove every file from release folder and all subfolders
Get-ChildItem -Path $release_folder -Include * -File -Recurse | foreach { $_.Delete()}

# Clean all build outputs
WriteHeader "Clean PDFiumDotNET"
dotnet clean --configuration Release --verbosity quiet

# Build all projects
WriteHeader "Build PDFiumDotNET"
dotnet build --configuration Release --verbosity quiet

# Copy NuGet packages
WriteHeader "Copy NuGet packages"
Copy-Item ".\Wrapper\Wrapper\bin\Release\*.nupkg" $nuget_folder
Copy-Item ".\Components\Components.Contracts\bin\Release\*.nupkg" $nuget_folder
Copy-Item ".\Components\Components\bin\Release\*.nupkg" $nuget_folder
Copy-Item ".\Components\Components.Factory\bin\Release\*.nupkg" $nuget_folder
Copy-Item ".\Controls\WpfControls\bin\Release\*.nupkg" $nuget_folder

# Copy binaries
WriteHeader "Copy binaries"
foreach($one_net_subfolder in $net_subfolders)
{
	$target_folder = $binaries_folder + $one_net_subfolder + '\'
	# wrapper
	$source_folder = '.\Wrapper\Wrapper\bin\Release\' + $one_net_subfolder + '\'
	CopyFile $source_folder $target_folder 'PDFiumDotNET.Wrapper.dll'
	CopyFile $source_folder $target_folder 'PDFiumDotNET.Wrapper.xml'
	# components contracts
	$source_folder = '.\Components\Components.Contracts\bin\Release\' + $one_net_subfolder + '\'
	CopyFile $source_folder $target_folder 'PDFiumDotNET.Components.Contracts.dll'
	CopyFile $source_folder $target_folder 'PDFiumDotNET.Components.Contracts.xml'
	# components 
	$source_folder = '.\Components\Components\bin\Release\' + $one_net_subfolder + '\'
	CopyFile $source_folder $target_folder 'PDFiumDotNET.Components.dll'
	CopyFile $source_folder $target_folder 'PDFiumDotNET.Components.xml'
	# components factory
	$source_folder = '.\Components\Components.Factory\bin\Release\' + $one_net_subfolder + '\'
	CopyFile $source_folder $target_folder 'PDFiumDotNET.Components.Factory.dll'
	CopyFile $source_folder $target_folder 'PDFiumDotNET.Components.Factory.xml'
	# controls WPFControls
	$source_folder = '.\Controls\WpfControls\bin\Release\' + $one_net_subfolder + '\'
	CopyFile $source_folder $target_folder 'PDFiumDotNET.WpfControls.dll'
	CopyFile $source_folder $target_folder 'PDFiumDotNET.WpfControls.xml'
	# controls WinFormsControls
	$source_folder = '.\Controls\WinFormsControls\bin\Release\' + $one_net_subfolder + '\'
	CopyFile $source_folder $target_folder 'PDFiumDotNET.WinFormsControls.dll'
	CopyFile $source_folder $target_folder 'PDFiumDotNET.WinFormsControls.xml'
	# apps Common
	$source_folder = '.\Apps\Common\bin\Release\' + $one_net_subfolder + '\'
	CopyFile $source_folder $target_folder 'PDFiumDotNET.Apps.Common.dll'
	CopyFile $source_folder $target_folder 'PDFiumDotNET.Apps.Common.xml'
	# apps PDFMerge
	$source_folder = '.\Apps\PDFMerge\bin\Release\' + $one_net_subfolder + '\'
	CopyFile $source_folder $target_folder 'PDFiumDotNET.Apps.PDFMerge.exe'
	CopyFile $source_folder $target_folder 'PDFiumDotNET.Apps.PDFMerge.xml'
	CopyFile $source_folder $target_folder 'PDFiumDotNET.Apps.PDFMerge.dll'
	CopyFile $source_folder $target_folder 'PDFiumDotNET.Apps.PDFMerge.runtimeconfig.json'
	# apps PDFViewForms
	$source_folder = '.\Apps\PDFViewForms\bin\Release\' + $one_net_subfolder + '\'
	CopyFile $source_folder $target_folder 'PDFiumDotNET.Apps.PDFViewForms.exe'
	CopyFile $source_folder $target_folder 'PDFiumDotNET.Apps.PDFViewForms.xml'
	CopyFile $source_folder $target_folder 'PDFiumDotNET.Apps.PDFViewForms.dll'
	CopyFile $source_folder $target_folder 'PDFiumDotNET.Apps.PDFViewForms.runtimeconfig.json'
	# apps PDFViewWPF
	$source_folder = '.\Apps\PDFViewWPF\bin\Release\' + $one_net_subfolder + '\'
	CopyFile $source_folder $target_folder 'PDFiumDotNET.Apps.PDFViewWPF.exe'
	CopyFile $source_folder $target_folder 'PDFiumDotNET.Apps.PDFViewWPF.xml'
	CopyFile $source_folder $target_folder 'PDFiumDotNET.Apps.PDFViewWPF.dll'
	CopyFile $source_folder $target_folder 'PDFiumDotNET.Apps.PDFViewWPF.runtimeconfig.json'
}

# Copy PDFium
WriteHeader "Copy PDFium"
foreach($one_net_subfolder in $net_subfolders)
{
	$target_folder = $binaries_folder + $one_net_subfolder + '\PDFium\'
	$target_folder_x86 = $target_folder + 'x86\'
	$target_folder_x64 = $target_folder + 'x64\'
	CreateFolder $target_folder
	CreateFolder $target_folder_x86
	CreateFolder $target_folder_x64

	$source_folder = '.\..\Libs\PDFium\x86\'
	CopyFile $source_folder $target_folder_x86 'pdfium.dll'
	CopyFile $source_folder $target_folder_x86 'LICENSE'
	$source_folder = '.\..\Libs\PDFium\x64\'
	CopyFile $source_folder $target_folder_x64 'pdfium.dll'
	CopyFile $source_folder $target_folder_x64 'LICENSE'
}

# Create zip file
WriteHeader "Create zip"
$compress_path=$binaries_folder + '*'
$zip_file=$binaries_folder + 'binaries.zip'
Compress-Archive -Path $compress_path -DestinationPath $zip_file -CompressionLevel Optimal
