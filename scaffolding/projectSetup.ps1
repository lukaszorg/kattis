# remove origin
Start-Process git -ArgumentList "remote remove origin" -wait -NoNewWindow -PassThru

# Download the file to a specific location
$clnt = new-object System.Net.WebClient
$workingDir = Get-Location
Write-Host "Working dir " $workingDir

# https://open.kattis.com/problems/{problem}}/file/statement/samples.zip
$url = "https://open.kattis.com/problems/" + $args[0] + "/file/statement/samples.zip"
$file = $workingDir.ToString() + "\sampledata.zip"
$clnt.DownloadFile($url,$file)
Write-Host "File downloaded to " $file

# Unzip the file to specified location
$shell_app=new-object -com shell.application
$zip_file = $shell_app.namespace($file)
$destination = $shell_app.namespace($workingDir.ToString())
$destination.Copyhere($zip_file.items())
Write-Host "Unzipped:" $zip_file

# delete zip file
Remove-Item $file

$newSlnName = $args[0]+".sln"
Rename-Item KattisSolution.sln $newSlnName

$newSubmitScript = "python kattisSubmit.py -p" + $args[0] +" KattisSolution\Program.cs KattisSolution\InputOutput.cs -f"
New-Item "submitMe.bat" -type file -force -value $newSubmitScript

# $gitCommand = "checkout -b " + $args[0]
# Start-Process git -ArgumentList $gitCommand -wait -NoNewWindow -PassThru
Start-Process git -ArgumentList "add --all --verbose" -wait -NoNewWindow
Start-Process git -ArgumentList "commit -m ""setup commit"" --verbose" -wait -NoNewWindow -PassThru

Start-Process $newSlnName
