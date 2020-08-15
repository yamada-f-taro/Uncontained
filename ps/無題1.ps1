
Param( [string]$file1="File1.csv", [string]$File2="file2.csv",[string]$KeySta="0=0",[string]$ValSta="1=1" )

function main{
    $dic = @{}

    $wkkey = $keySta.Split('=');
    $wkval = $valSta.Split('=');

    #file1をdicに格納
    $file = New-Object System.IO.StreamReader((Convert-Path $file1), [System.Text.Encoding]::GetEncoding("UTF-8"))
    while (($line = $file.ReadLine()) -ne $null)
    {
        $arr = $line.Split(',');
        $key = $arr[[int]$wkkey[0]];
        $val = $arr[[int]$wkval[0]];
        if ($dic.ContainsKey($key)) {
            write-host ${key}が重複ていします。file1を見直してください。
            return
        }
        $dic.Add($key,$val);
    }
    $file.Close()

    #dicとfile2を比較
    $file = New-Object System.IO.StreamReader((Convert-Path $file2), [System.Text.Encoding]::GetEncoding("UTF-8"))
    while (($line = $file.ReadLine()) -ne $null)
    {
        $arr = $line.Split(',');
        $key = $arr[[int]$wkkey[1]];
        $val = $arr[[int]$wkval[1]];

        #file1にないのは知らない
        if ($dic.ContainsKey($key))
        {
            $f1val = $dic[$key];
            if ($f1val -ne $val)
            {
                #$out = "key:" + $key + "の値が異なっています。 file1:" + $f1val + ", file2:" + $line
                #write-host $out
                write-host key:${key}の値が異なっています。 file1:${f1val}, file2:${line}
            }
        }
    }
    $file.Close()
}

main
