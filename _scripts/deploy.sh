#!bin/bash

echo "Running deploy script on master branch"

dotnet publish jhray.com.csproj
cd /home/travis/build/PurplePenguin4102/jhray.com/bin/Debug/netcoreapp2.0/publish/
cat ~/.ssh/id_rsa
git init
git remote add deploy "deploy@jhray.com:/var/aspnetcore"
git config user.name "Travis CI"
git config user.email "joseph.h.ray@gmail.com"
git add .
git commit -m "Deploy"
git push --force deploy master
