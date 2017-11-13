#!/bin/bash
mkdir .ssh
chmod 600 $Deploy_Key
echo $Deploy_Key > ~/.ssh/id_rsa
chmod 600 ~/.ssh/id_rsa
