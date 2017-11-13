#!/bin/bash
mkdir .ssh
chmod 600 $Deploy_Key
echo $Deploy_Key > ~/.ssh/id_rsa
