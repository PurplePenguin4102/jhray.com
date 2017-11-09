pipeline {
    agent any

    stages {
        stage('Build') {
            steps {
                dotnet -h
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