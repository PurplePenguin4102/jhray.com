#!bin/bash

echo "Running deploy script on master branch"

dotnet publish jhray.com.csproj
cd /home/travis/build/PurplePenguin4102/jhray.com/bin/Debug/netcoreapp2.0/publish/

git init
git remote add deploy "deploy:7qt59947@jhray.com:/var/aspnetcore"
git config user.name "Travis CI"
git confi user.email "joseph.h.ray@gmail.com"
git add .
git commit -m "Deploy"
git push --force deploy master
