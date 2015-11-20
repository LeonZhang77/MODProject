<#
 .Synopsis
  This function find the target folders based a txt file under the source folder and move them to destination folder
 .Description
  This function find the target folders based a txt file under the source folder and move them to destination folder. If there is folder with same name existing at destination, that folder will be overwritten.
 .Example
  FindFoldersAndCopyTo "D:\SourceFolder" "E:\DestinationFolder"
 .Parameter source
  The source folder that has target folders and a txt file containing name of the target folders resides in
 .Parameter destination
  The destination folder to copy to
#>

Param(
 [string]$source,
 [string]$destination
) #end param

# just in case, trim '\' from the path
$source = $source.TrimEnd("\")
$destination = $destination.TrimEnd("\")

$txtFileName = "list.txt"
# check file exists
$fullPath = $source + "\" + $txtFileName
$sourceExists = Test-Path $fullPath
$destinationExists = Test-Path $destination
if ($sourceExists)
{
# get names of files to copy
    $targetFolderNames = Get-Content $fullPath
}
else
{
    Write-Host "source file" $fullPath " doesn't exist!" -ForegroundColor Red
}
if ($destinationExists)
{
    $count = 0
    Write-Host "copy from:" $source -BackgroundColor Black
    Write-Host "copy to:" $destination -BackgroundColor Black
    Write-Host "total # folders to copy:" $targetFolderNames.Length -BackgroundColor Black
    # copy target folders to destination
    foreach ($targetFolderName in $targetFolderNames)
    {
        $targetFolder = $source + "\" + $targetFolderName
        if (Test-Path $targetFolder)
        {
            Copy-Item $targetFolder $destination -Recurse -Force
            Write-Host "folder copied succeed:" $targetFolderName -ForegroundColor Green
            $count++
        }
        else
        {
            Write-Host "folder copied failed:" $targetFolderName "as it doesn't exist!" -ForegroundColor Red
        }
    }
    Write-Host "actual # folders are copied:" $count -BackgroundColor Black
}
else
{
    Write-Host "destination" $destination "doesn't exist!" -ForegroundColor Red
}


