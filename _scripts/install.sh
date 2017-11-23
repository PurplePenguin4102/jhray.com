#!/bin/bash
mkdir -p ~/.ssh 
chmod 700 ~/.ssh
echo "$Deploy_Key" | perl -pe 's/\\n/\n/g' > ~/.ssh/id_rsa
echo "Key successfully copied to id_rsa"
chmod 600 ~/.ssh/id_rsa
 