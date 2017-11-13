#!/bin/bash
mkdir .ssh
echo copying ssh key...  $Deploy_Key
echo $Deploy_Key > ~/.ssh/id_rsa
