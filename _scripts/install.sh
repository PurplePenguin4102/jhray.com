#!/bin/bash
mkdir -p ~/.ssh 
chmod 700 ~/.ssh
echo "$Deploy_Key" > ~/.ssh/id_rsa
sed 's/\\n/\
/g'
chmod 600 ~/.ssh/id_rsa
