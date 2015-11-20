$dir = "C:\Cloud\sources\dev\Common\ApplicationDefinitions";
$output = "C:\data\AppKey.txt";

$stream = [System.IO.StreamWriter] $output

Function writeToFile{
    foreach($fileName in $input)
    {
        $stream.WriteLine($fileName.Name)
        Write-Output $fileName.Name
    }
}

Get-ChildItem $dir | where {$_.mode -match "d"} | writeToFile


$stream.Close();
