$dest = "./virtru-uitests"
$zipfile = $dest + "-" + ".zip"
Import-Module Pscx

if(Test-Path $dest) {
    Remove-Item -recurse $dest
}
New-Item $dest -ItemType directory

if(Test-Path $zipfile) {
    Remove-Item $zipfile
}

Copy-Item *.exe -destination $dest
Copy-Item *.dll -destination $dest
Copy-Item *.yaml -destination $dest
Copy-Item *.config -destination $dest
Copy-Item .\BrowserSupport -destination $dest -recurse

Get-ChildItem -Recurse $dest |
Write-Zip -OutputPath $zipfile
