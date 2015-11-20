$dir = "C:\Cloud\sources\dev\Common\ApplicationDefinitions"

$applist = "C:\Depot\getURL.txt"
$output = "C:\Depot\testttt.txt";

$stream = [System.IO.StreamWriter] $output
$appkeys = Get-Content $applist


for($i=0; $i -lt $appkeys.Length; $i++){
    $appkey = $appkeys[$i]
    $dir1 = $dir + "\" + $appkey
    $dir1
    $files = Get-ChildItem $dir1 -recurse | Where-Object{$_.Name -eq "passwordssodata.xml"} | %{$_.FullName}
    foreach($filename in $files){
        $filename
        $content = Get-Content $filename
        $xmlfile = [xml]$content
        $urls = $xmlfile.ApplicationDefinition.ApplicationLogonPages.ApplicationLogonPage.Urls.Url
        foreach ($url in $urls) {
            #$stream.WriteLine($appkey + "," +$url)
            $stream.WriteLine($url)
            break
        }
        
    }
}

$stream.Close();

