$dir = "\\app-psso-china\WAAD Password SSO Wicresoft Team\metaData\2014_06_09_100Apps";
$output = "\\app-psso-china\WAAD Password SSO Wicresoft Team\metaData\2014_06_09_100Apps\next_block_of_apps_to_ship1.txt";

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
