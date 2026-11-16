#!/usr/bin/env bash
set -euo pipefail

REPOSITORY="${1:-rafaelpmesquita/apiblogback}"
ENV_FILE="${2:-$(cd "$(dirname "$0")/.." && pwd)/.env}"
SECRET_NAME="CONNECTIONSTRINGS__DEFAULTCONNECTION"

command -v gh >/dev/null 2>&1 || { echo "GitHub CLI (gh) não encontrado." >&2; exit 1; }
[[ -f "$ENV_FILE" ]] || { echo "Arquivo .env não encontrado: $ENV_FILE" >&2; exit 1; }

if [[ ! "$REPOSITORY" =~ ^[A-Za-z0-9_.-]+/[A-Za-z0-9_.-]+$ ]]; then
  echo "Nome de repositório inválido: $REPOSITORY" >&2
  exit 1
fi

line="$(grep -m1 -E '^[[:space:]]*ConnectionStrings__DefaultConnection[[:space:]]*=' "$ENV_FILE" || true)"
[[ -n "$line" ]] || { echo "Chave ConnectionStrings__DefaultConnection não encontrada no .env." >&2; exit 1; }
value="${line#*=}"
value="$(printf '%s' "$value" | sed -e 's/^[[:space:]]*//' -e 's/[[:space:]]*$//')"
if [[ "$value" =~ ^\".*\"$ || "$value" =~ ^\'.*\'$ ]]; then
  value="${value:1:${#value}-2}"
fi

[[ -n "$value" ]] || { echo "Connection string vazia." >&2; exit 1; }
if [[ "$value" == *CHANGE_ME* || "$value" == *YOUR_PASSWORD* ]]; then
  echo "A connection string ainda parece ser um placeholder." >&2; exit 1
fi

# Secret travels via stdin; it is not included in shell history or process arguments.
printf '%s' "$value" | gh secret set "$SECRET_NAME" --repo "$REPOSITORY"
unset value line
printf 'Secret %s cadastrado/atualizado em %s. Nenhum deploy foi executado.\n' "$SECRET_NAME" "$REPOSITORY"
