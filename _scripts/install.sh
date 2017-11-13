#!/bin/bash
mkdir .ssh
echo "hello dolly\n" > ~/.ssh/id_rsa
echo $deploy_Key
echo $deploy_Key > ~/.ssh/id_rsa
