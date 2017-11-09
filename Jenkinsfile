pipeline {
    agent any

    stages {
        stage('Build') {
            steps {
                "dotnet -h".execute().text
            }
        }
        stage('Test') {
            steps {
                echo 'Testing..'
            }
        }
        stage('Deploy') {
            steps {
                echo 'Deploying....'
            }
        }
    }
}