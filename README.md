# Portfolio API

API ASP.NET Core responsável pelos dados de timeline, conhecimentos e navegação do portfólio.

## Configuração local com `.env`

1. Copie `.env.example` para `.env` na raiz deste projeto.
2. Preencha `ConnectionStrings__DefaultConnection` com a connection string do PostgreSQL **já rotacionada**.
3. Execute a API:

```bash
dotnet restore
dotnet run --project api.csproj
```

O `Program.cs` carrega o `.env` apenas se o arquivo existir. Em produção, a aplicação lê a mesma configuração por variável de ambiente, sem precisar distribuir um arquivo `.env`.

**Não versione `.env`.** O `.gitignore` ignora `.env` e outros arquivos de ambiente, mantendo somente `.env.example` versionado.

## GitHub Actions Secrets

No repositório `rafaelpmesquita/apiblogback`, crie um Repository Secret com este nome:

```text
CONNECTIONSTRINGS__DEFAULTCONNECTION
```

Caminho: `Settings` → `Secrets and variables` → `Actions` → `New repository secret`.

Use como valor a connection string com a senha nova, depois de rotacionar a credencial antiga. Não recupere nem reutilize a credencial que estava hardcoded no histórico do Git.

Em um workflow, o secret pode ser disponibilizado como variável de ambiente assim:

```yaml
env:
  ConnectionStrings__DefaultConnection: ${{ secrets.CONNECTIONSTRINGS__DEFAULTCONNECTION }}
```

Atenção: o secret fica disponível ao workflow, mas o GitHub não o injeta automaticamente no processo da API hospedada. O workflow de deploy precisa atualizar a configuração de ambiente do host (por exemplo, o App Service, caso esse continue sendo o destino) usando esse valor. Este repositório não tinha um workflow de deploy versionado na auditoria; por isso, não foi criado um deploy automático presumindo nomes de recursos ou credenciais de infraestrutura.

## Endpoints públicos

A API pública foi limitada a operações de leitura. As operações antigas de POST/PUT/DELETE foram removidas dos controllers porque não havia fluxo de autenticação/autorização ativo no `Startup.cs`. O controller `/v1/user` também foi desabilitado: ele expunha registros de usuários e um valor derivado do campo de senha. Não reative essas rotas sem implementar autenticação, autorização e armazenamento seguro de senhas.

| Método | Rota | Uso |
|---|---|---|
| GET | `/v1/` | Lista conhecimentos |
| GET | `/v1/tl` | Lista itens da timeline |
| GET | `/v1/nv` | Lista itens da navegação |

## Observações de manutenção

- O projeto principal ainda está em **.NET Core 2.1**, sem suporte desde 21 de agosto de 2021. A migração para uma versão suportada deve ser tratada separadamente, pois envolve runtime, ASP.NET Core, Entity Framework Core, Npgsql e possíveis alterações de código.
- O repositório também contém um projeto `WebApiHeroku.Api` separado e artefatos `bin/obj` versionados historicamente. Esta correção não remove esses elementos para evitar mudanças estruturais sem validação.
- A connection string antiga apareceu no histórico do Git. Depois de rotacionar, avalie a limpeza do histórico e verifique se a credencial não foi reutilizada em outros ambientes. Apagar o segredo do arquivo atual não invalida uma credencial exposta.

## Cadastrar a connection string como GitHub Secret (sem deploy)

Depois de criar `backend/.env` com a credencial nova e rotacionada, você pode cadastrar o valor no repositório `rafaelpmesquita/apiblogback` usando o GitHub CLI:

PowerShell (Windows):

```powershell
 gh auth login
 .\scripts\Set-GitHubSecret.ps1
```

Bash / Git Bash:

```bash
 gh auth login
 ./scripts/set-github-secret.sh
```

O script lê `ConnectionStrings__DefaultConnection` do `.env` e envia o conteúdo pela entrada padrão do `gh`; ele não imprime a connection string. O secret ficará armazenado em `Settings → Secrets and variables → Actions`.

**Isto apenas armazena o secret no GitHub.** Não cria workflow, não configura hospedagem e não executa deploy. O secret só será consumido quando algum workflow fizer referência explícita a ele.
