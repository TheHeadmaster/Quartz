gitmoji-changelog --preset generic

$Path = $PSScriptRoot + '\CHANGELOG.md'
Write-Host $Path
Set-Content -Path $Path -Value (get-content -Path $Path | Select-String -Pattern '^((-  .+)|### Remove)' -NotMatch)
Set-Content -Path $Path -Value (Get-Content -Path $Path -Raw).TrimEnd()