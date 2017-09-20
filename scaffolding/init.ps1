$cloneParams = "clone -v https://karpik@bitbucket.org/karpik/kattissolution.git " + $args[0]
Start-Process git -ArgumentList $cloneParams -wait -NoNewWindow -PassThru

$newPath = ".\\" + $args[0]

Set-Location -Path $newPath -PassThru

$expression = ".\\projectSetup.ps1 " + $args[0]
Invoke-Expression $expression
