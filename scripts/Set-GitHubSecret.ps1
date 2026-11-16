param(
    [string]$Repository = "rafaelpmesquita/apiblogback",
    [string]$EnvFile = (Join-Path (Split-Path -Parent $PSScriptRoot) ".env")
)

$ErrorActionPreference = "Stop"
$secretName = "CONNECTIONSTRINGS__DEFAULTCONNECTION"

if (-not (Get-Command gh -ErrorAction SilentlyContinue)) {
    throw "GitHub CLI (gh) não foi encontrado. Instale-o e autentique com 'gh auth login'."
}

if (-not (Test-Path -LiteralPath $EnvFile)) {
    throw "Arquivo .env não encontrado em: $EnvFile. Crie-o a partir de .env.example e use uma senha já rotacionada."
}

if ($Repository -notmatch '^[A-Za-z0-9_.-]+/[A-Za-z0-9_.-]+$') {
    throw "Nome de repositório inválido: $Repository"
}

$matchingLine = Get-Content -LiteralPath $EnvFile | Where-Object {
    $_ -match '^\s*ConnectionStrings__DefaultConnection\s*='
} | Select-Object -First 1

if (-not $matchingLine) {
    throw "A chave ConnectionStrings__DefaultConnection não foi encontrada no .env."
}

$value = ($matchingLine -replace '^\s*ConnectionStrings__DefaultConnection\s*=\s*', '').Trim()
if ($value.Length -ge 2) {
    if (($value.StartsWith('"') -and $value.EndsWith('"')) -or
        ($value.StartsWith("'") -and $value.EndsWith("'"))) {
        $value = $value.Substring(1, $value.Length - 2)
    }
}

if ([string]::IsNullOrWhiteSpace($value) -or $value -match 'CHANGE_ME|YOUR_PASSWORD') {
    throw "O valor da connection string parece ser apenas um placeholder. Preencha .env com a credencial nova antes de cadastrar."
}

# Send the secret through stdin, not as a command-line argument, and never print it.
$startInfo = New-Object System.Diagnostics.ProcessStartInfo
$startInfo.FileName = "gh"
$startInfo.Arguments = "secret set $secretName --repo $Repository"
$startInfo.UseShellExecute = $false
$startInfo.RedirectStandardInput = $true
$startInfo.RedirectStandardOutput = $true
$startInfo.RedirectStandardError = $true

$process = New-Object System.Diagnostics.Process
$process.StartInfo = $startInfo
[void]$process.Start()
$process.StandardInput.Write($value)
$process.StandardInput.Close()
$stdout = $process.StandardOutput.ReadToEnd()
$stderr = $process.StandardError.ReadToEnd()
$process.WaitForExit()
$value = $null

if ($process.ExitCode -ne 0) {
    throw "Não foi possível cadastrar o secret no GitHub. $stderr"
}

if ($stdout) { Write-Output $stdout.Trim() }
Write-Output "Secret $secretName cadastrado/atualizado em $Repository. Nenhum deploy foi executado."
