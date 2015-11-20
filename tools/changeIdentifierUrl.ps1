$dir = "C:\share\bak"

$toReplace1 = @"
"Visible": false,
"@

$toReplace2 = @"
<Url>http
"@

$toReplace3 = @"
Name="http
"@

$replaceTo1 = @"
"Visible": true,
"@

$replaceTo2 = @"
<Url>https
"@

$replaceTo3 = @"
Name="https
"@

$toReplace100 = @"
"IdentifierUrl": 
"@

$replaceTo100 = @"
"IdentifierUris": [
"@

$toReplace101 = @"
,
"@

$replaceTo101 = @"
],
"@

$applist = "C:\share\bak\bak.txt"

$appkeys = Get-Content $applist


for($i=0; $i -lt $appkeys.Length; $i++){
    $appkey = $appkeys[$i]
    $dir1 = $dir + "\" + $appkey
    $dir1
    $files = Get-ChildItem $dir1 -recurse | Where-Object{$_.Name -eq "metadata.json"} | %{$_.FullName}
    foreach($filename in $files){
        $filename
        $content = Get-Content $filename
        $str = $content[3]
        $temp = $str -replace $toReplace100, $replaceTo100
        $temp1 = $temp -replace $toReplace101, $replaceTo101
        $s = $content -creplace [regex]::Escape($str), $temp1
        $s | Set-Content $filename -Force
    }
}


